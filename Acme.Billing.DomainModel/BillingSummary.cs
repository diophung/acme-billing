namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent the summary of the billing in a particular month of a year.
    /// </summary>
    public class BillingSummary
    {
        /// <summary>
        /// Store the total number of invoice sent to customers
        /// </summary>
        public int NumberOfInvoiceSent { get; private set; }

        /// <summary>
        /// Store the total amount due billed to customers.
        /// </summary>
        public decimal TotalAmountDueBilled { get; private set; }

        /// <summary>
        /// The datetime stamp of this summary, in the format of month_year
        /// </summary>
        public string DateTimeStamp { get; }

        public BillingSummary(string timeStamp, int invoiceCount, decimal amountBilled)
        {
            this.DateTimeStamp = timeStamp;
            this.NumberOfInvoiceSent = invoiceCount;
            this.TotalAmountDueBilled = amountBilled;
        }
    }
}
