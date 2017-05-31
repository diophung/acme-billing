using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Acme.Billing.DomainModel;
using Acme.Billing.Infrastructure;
using Acme.Billing.Infrastructure.RestHelper;
using Acme.Billing.Repository.Interface;
using Acme.Billing.Repository.Resource;
using Newtonsoft.Json.Linq;

namespace Acme.Billing.Repository.Implementation
{
    /// <summary>
    /// Handling bill statement tasks.
    /// </summary>
    public class BillStatementRepository : RepositoryBase, IBillStatementRepository
    {
        protected IEmailRepository emailRepository;
        protected ICustomerRepository customerRepository;
        protected static readonly string AmountDueUrl = ConfigurationManager.AppSettings["BillingService.AmountDueEndpoint"];
        protected static readonly string BaseUrl = ConfigurationManager.AppSettings["BillingService.BaseUrl"];
        protected const string AMOUNT_DUE_FIELD = "amount_due";

        /// <summary>
        /// Keep track of the customers who have been billed.
        /// 
        /// The key is in format of customerID_month_year,
        /// The value is the list of customer who are billed.
        /// </summary>
        protected internal IDictionary<string, BillStatement> billedCustomerByTime;

        /// <summary>
        /// Keep track of the customers who have been emailed.
        /// 
        /// The key is in format of customerID_month_year,
        /// The value is the email to send to that customer.
        /// </summary>
        protected internal IDictionary<string, Email> emailedCustomerByTime;

        /// <summary>
        /// Store the billing summary.
        /// 
        /// The key is in format month_year,
        /// The value is the billing summary.
        /// </summary>
        protected internal IDictionary<string, BillingSummary> billingSummaryByTime;

        protected internal IDictionary<string, ISet<Email>> invoiceGeneratedByTime;

        public BillStatementRepository(IEmailRepository emailRepository, ICustomerRepository customerRepository)
        {
            //Dependency injection
            this.emailRepository = emailRepository;
            this.customerRepository = customerRepository;

            this.billedCustomerByTime = new Dictionary<string, BillStatement>();
            this.emailedCustomerByTime = new Dictionary<string, Email>();
            this.billingSummaryByTime = new Dictionary<string, BillingSummary>();
            this.invoiceGeneratedByTime = new Dictionary<string, ISet<Email>>();
        }

        /// <summary>
        /// Get customer amount due for the <c>month</c> of the <c>year</c>
        /// </summary>
        /// <param name="cust">The customer to get amount due</param>
        /// <param name="month">The month</param>
        /// <param name="year">The year</param>
        /// <returns></returns>
        public decimal? GetAmountDue(Customer cust, int month, int year)
        {
            decimal amountDue = 0;

            // billing service does not support future date
            if (DateTimeHelper.IsInFuture(month, year))
            {
                throw new InvalidOperationException("Future date is not supported. Please adjust month and year");
            }

            string amountDueRequestPath = AmountDueUrl
                                    .Replace("{UUID}", cust.CustomerId)
                                    .Replace("{MONTH}", month.ToString())
                                    .Replace("{YEAR}", year.ToString());

            try
            {
                JObject jsonObj = JObject.Parse(RestClientHelper.GetJson(BaseUrl, amountDueRequestPath));
                var amountDueResult = jsonObj[AMOUNT_DUE_FIELD].FirstOrDefault();
                if (amountDueResult != null)
                {
                    amountDue = decimal.Parse(amountDueResult.ToString());
                }
                return amountDue;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Error($"Unable to get amount due for {cust.CustomerId}:{cust.Name} for {month}-{year}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generate the bill for a customer.
        /// Note: 
        ///     this action is idempotent. If the bill is generated, it will be retrieved.
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public BillStatement GenerateBill(Customer cust, int month, int year)
        {
            string billedCustomerKey = $"{month}_{year}_{cust.CustomerId}";
            // if the bill is not generated, create it
            if (billedCustomerByTime[billedCustomerKey] == null)
            {
                decimal? amountDue = GetAmountDue(cust, month, year);
                BillStatement bill = new BillStatement(cust, month, year, amountDue.GetValueOrDefault());

                //remember the bill statement for the month
                billedCustomerByTime[billedCustomerKey] = bill;
                return bill;
            }

            // if the bill has been generated, just retrieve it.
            return billedCustomerByTime[billedCustomerKey];
        }

        /// <summary>
        /// Generate and send the bill to all active customers for the month and year.
        /// Note:
        ///     This action is idempotent. If customer has already been emailed, the system will not send duplicate email.
        /// </summary>
        /// <param name="month">the month to bill customer.</param>
        /// <param name="year">the year to bill customer.</param>
        public void SendBillToActiveCustomers(int month, int year)
        {
            // Billing service does not support future date
            if (!DateTimeHelper.IsInFuture(month, year))
            {
                ISet<Email> emails = new HashSet<Email>();
                string datetimeStamp = $"{month}_{year}";
                IList<Customer> allActiveCustomers = this.customerRepository.GetAllActiveCustomers();
                
                foreach (Customer customer in allActiveCustomers)
                {
                    try
                    {
                        BillStatement bill = GenerateBill(customer, month, year);

                        // only send email if we have not done that.
                        string emailedCustomerKey = $"{customer.CustomerId}_{month}_{year}";
                        if (emailedCustomerByTime.ContainsKey(emailedCustomerKey))
                        {
                            Email email = GenerateEmail(bill, month, year);
                            
                            // remember the customer who was emailed
                            emailedCustomerByTime[emailedCustomerKey] = email;
                            emails.Add(email);
                        }
                    }
                    //Should not block the whole billing cycle if a customer failed 
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        Logger.Error($"Unable to generate bill for {customer.CustomerId}:{customer.Name} for the {month}-{year}");
                    }
                }

                //storing the generated email for reporting purpose
                invoiceGeneratedByTime[datetimeStamp] = emails;
                emailRepository.SendMultiple(emails);
            }
        }
        
        /// <summary>
        /// Generate an email content based on the time and the bill statement.
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public Email GenerateEmail(BillStatement bill, int month, int year)
        {
            string emailTemplate = DomainResources.BILLING_STATEMENT_EMAIL_TEMPLATE;
            string subject = $"Billing statement for {month}-{year}";

            string content = emailTemplate
                .Replace("{name}", bill.Customer.Name)
                .Replace("{address}", bill.Customer.Address)
                .Replace("{city}", bill.Customer.City)
                .Replace("{state}", bill.Customer.Address)
                .Replace("{zip}", bill.Customer.Address)
                .Replace("{bill_month}", month + "-" + year)
                .Replace("{amount_due}", bill.AmountDue.ToString("C"));

            return new Email(subject, content, bill.Customer, bill);
        }

        /// <summary>
        /// Retrieve the generated and sent invoices by month and year
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IEnumerable<Email> GetInvoices(int month, int year)
        {
            string key = $"{month}_{year}";
            if (invoiceGeneratedByTime.ContainsKey(key))
            {
                return invoiceGeneratedByTime[key];
            }
            return new List<Email>();
        }
    }
}
