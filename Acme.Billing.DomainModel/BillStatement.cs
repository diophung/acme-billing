using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent a bill statement in ACME billing system.
    /// </summary>
    public class BillStatement
    {
        public BillStatement(Customer cust, int month, int year, decimal amountDue)
        {
            this.Customer = cust;
            this.StatementMonth = month;
            this.StatementYear = year;
            this.AmountDue = amountDue;
            this.IsSent = false;
            this.GeneratedDateTime = DateTime.Now;
        }
        /// <summary>
        /// The customer to be billed.
        /// </summary>
        public Customer Customer { get; private set; }

        /// <summary>
        /// The month of the bill statement.
        /// </summary>
        public int StatementMonth { get; private set; }

        /// <summary>
        /// The year of the bill statement.
        /// </summary>
        public int StatementYear { get; private set; }

        /// <summary>
        /// How much this customer have to pay.
        /// </summary>
        public decimal AmountDue { get; private set; }

        /// <summary>
        /// Indicate whether this bill has been sent successfully.
        /// </summary>
        public bool IsSent { get; private set; }

        /// <summary>
        /// The date when this statement is generated.
        /// </summary>
        public DateTime GeneratedDateTime { get; private set; }
    }
}
