using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    /// <summary>
    /// Perform tasks related to billing statement.
    /// </summary>
    public interface IBillStatementRepository   
    {
        /// <summary>
        /// Utilize metering service to get customer due amount
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="month">The month of the year, for e.g 1 is January, 12 is December</param>
        /// <param name="year">the year in 4-digit format</param>
        /// <returns></returns>
        decimal? GetAmountDue(Customer customer, int month, int year);

        /// <summary>
        /// Generate the bill for a customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        BillStatement GenerateBill(Customer customer, int month, int year);

        /// <summary>
        /// Generate the email with the bill statement to send to customer.
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Email GenerateEmail(BillStatement bill, int month, int year);

        /// <summary>
        /// Generate and send the bill to all active customers for the month and year.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        void SendBillToActiveCustomers(int month, int year);
    }
}