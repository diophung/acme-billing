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
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
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
        
        public string CustomerId { get; private set; }
        /// <summary>
        /// Full name in format of FirstName LastName
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Customer email
        /// </summary>
        public string Email { get; private set; }
        
        /// <summary>
        /// Address line
        /// </summary>
        public string Address { get; private set; }
        
        /// <summary>
        /// City name
        /// </summary>
        public string City { get; private set; }
        
        /// <summary>
        /// 2-character state code
        /// </summary>
        public string State { get; private set; }
        
        /// <summary>
        /// US Zip code
        /// </summary>
        public string Zip { get; private set; }

        /// <summary>
        /// Indicate whether a customer is a current customer.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// The bill statements of this customer.
        /// </summary>
        public IList<BillStatement> MonthlyStatements { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Customer other)
        {
            return string.Equals(CustomerId, other.CustomerId);
        }

        public override int GetHashCode()
        {
            return (CustomerId != null ? CustomerId.GetHashCode() : 0);
        }
    }
}
