using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent an email to be sent to ACME customer.
    /// </summary>
    public class Email
    {
        public Email(string subject, string content, Customer cust, BillStatement bill)
        {
            this.Subject = subject;
            this.Content = content;
            this.Customer = cust;
            this.BillStatement = bill;
        }

        public string Subject { get; set; }
        public string Content { get; set; }
        public Customer Customer { get; }
        public BillStatement BillStatement { get; }
    }
}
