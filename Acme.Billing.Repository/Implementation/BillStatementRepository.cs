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
using RestSharp;
using RestSharp.Deserializers;

namespace Acme.Billing.Repository.Implementation
{
    public class BillStatementRepository : IBillStatementRepository
    {
        protected IEmailRepository emailRepository;
        protected ICustomerRepository customerRepository;
        protected static readonly string AmountDueUrl = ConfigurationManager.AppSettings["BillingService.AmountDueEndpoint"];
        protected static readonly string BaseUrl = ConfigurationManager.AppSettings["BillingService.BaseUrl"];
        public BillStatementRepository(IEmailRepository emailRepository, ICustomerRepository customerRepository)
        {
            this.emailRepository = emailRepository;
            this.customerRepository = customerRepository;
        }

        /// <summary>
        /// Get customer amount due for the <c>month</c> of the <c>year</c>
        /// </summary>
        /// <param name="cust">The customer to get amount due</param>
        /// <param name="month">The month</param>
        /// <param name="year">The year</param>
        /// <returns></returns>
        public decimal GetAmountDue(Customer cust, int month, int year)
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


            JObject jsonObj = JObject.Parse(RestClientHelper.GetJson(BaseUrl, amountDueRequestPath));
            var amountDueResult = jsonObj["amount_due"].FirstOrDefault();
            if (amountDueResult != null)
            {
                amountDue = decimal.Parse(amountDueResult.ToString());
            }
            return amountDue;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public BillStatement GenerateBillStatement(Customer cust, int month, int year)
        {
            decimal amountDue = GetAmountDue(cust, month, year);
            string content = String.Format(DomainResources.BILLING_STATEMENT_EMAIL_TEMPLATE, cust.Name, String.Format("0:C", amountDue));

            BillStatement bill = new BillStatement(cust, month, year, amountDue);
            return bill;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        public void SendBillStatements(IList<Customer> customers, int month, int year)
        {
            IList<BillStatement> bills = new List<BillStatement>();
            foreach (Customer cust in customers)
            {
                BillStatement bill = GenerateBillStatement(cust, month, year);
                bills.Add(bill);
            }

            string emailTemplate = DomainResources.BILLING_STATEMENT_EMAIL_TEMPLATE;

            IList<Email> emails = new List<Email>();
            foreach (BillStatement bill in bills)
            {
                emails.Add(new Email
                {
                    Content = emailTemplate,
                    Customer = bill.Customer,
                });
            }

            this.emailRepository.SendMultiple(emails);

        }
    }
}
