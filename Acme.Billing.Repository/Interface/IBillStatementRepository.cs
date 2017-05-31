using System.Collections.Generic;
using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    public interface IBillStatementRepository
    {
        /// <summary>
        /// Utilize metering service to get customer due amount
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="month">The month of the year, for e.g 1 is January, 12 is December</param>
        /// <param name="year">the year in 4-digit format</param>
        /// <returns></returns>
        decimal GetAmountDue(Customer customer, int month, int year);


        /// <summary>
        /// Generate the bill for a customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        BillStatement GenerateBillStatement(Customer customer, int month, int year);

        /// <summary>
        /// Bill the customers for the month and year.
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        void SendBillStatements(IList<Customer> customers, int month, int year);
    }
}