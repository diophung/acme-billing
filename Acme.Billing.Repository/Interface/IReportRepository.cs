using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    /// <summary>
    /// Perform the report tasks
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Get the total number of successfully generated & emailed invoices, by month and year.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Report GetCustomerReport(int month, int year);
    }
}