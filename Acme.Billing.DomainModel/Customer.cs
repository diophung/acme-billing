using System;
using System.Collections.Generic;

namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent a customer in ACME billing system
    /// </summary>
    public class Customer
    {
        public Customer(string uuid, string name, string email, string address, string city, string state, string zip)
        {
            this.CustomerId = uuid;
            this.Name = name;
            this.Email = email;
            this.Address = new Address(address, city, state, zip);
            this.MonthlyStatements = new List<BillStatement>();
            this.IsActive = true;
        }

        /// <summary>
        /// Mark a customer as non-active
        /// </summary>
        public void Deactivate()
        {
            this.IsActive = false;
        }
        public void UpdateDetails(string name, string email, Address addr)
        {
            this.Name = name;
            this.Email = email;
            this.Address = addr;
        }
        
        public string CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }

        /// <summary>
        /// Indicate whether a customer is a current customer.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// The bill statements of this customer.
        /// </summary>
        public IList<BillStatement> MonthlyStatements { get; }
    }
}
