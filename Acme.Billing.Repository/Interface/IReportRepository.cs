using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    /// <summary>
    /// Perform 
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Get the total number of successfully generated & emailed invoices, by month and year.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Report GetSystemReport(int month, int year);
    }
}