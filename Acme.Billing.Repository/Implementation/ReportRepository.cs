using System;
using System.Collections.Generic;
using System.Linq;
using Acme.Billing.DomainModel;
using Acme.Billing.Repository.Interface;
using Acme.Billing.Repository.Resource;

namespace Acme.Billing.Repository.Implementation
{
    public class ReportRepository : RepositoryBase, IReportRepository
    {
        protected internal ICustomerRepository customerRepository;
        protected internal IBillStatementRepository billingStatementRepository;
        public ReportRepository(ICustomerRepository customerRepo, IBillStatementRepository billRepo)
        {
            this.customerRepository = customerRepo;
            this.billingStatementRepository = billRepo;
        }
        public Report GetCustomerReport(int month, int year)
        {
            IEnumerable<Email> sentInvoices = billingStatementRepository.GetInvoices(month, year).ToList();
            int invoiceSent = sentInvoices.Count();
            decimal totalAmountBilled = sentInvoices.Sum(x => x.BillStatement.AmountDue);

            string content = DomainResources.INVOICE_REPORT_TEMPLATE_BY_TIME
                .Replace("{month}", month.ToString())
                .Replace("{year}", year.ToString())
                .Replace("{total_invoice}", invoiceSent.ToString())
                .Replace("total_amount_billed", totalAmountBilled.ToString("C"));

            return new Report(content, month, year, DateTime.Now);
        }
    }
}
