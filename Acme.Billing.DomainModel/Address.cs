namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent an US address in ACME billing system.
    /// </summary>
    public class Address
    {
        public Address(string addressLine, string city, string state, string zip)
        {
            this.AddressLine = addressLine;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.IsCurrent = true;
        }

        public string AddressLine { get; }
        public string City { get; }

        public string State { get; }

        public string Zip { get; }

        public bool IsCurrent { get; private set; }

        /// <summary>
        /// Update an address to mark it as non-current
        /// </summary>
        public void MarkAsNonCurrent()
        {
            this.IsCurrent = false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Address other)
        {
            return string.Equals(City, other.City) 
                && string.Equals(State, other.State) 
                && string.Equals(Zip, other.Zip) 
                && IsCurrent == other.IsCurrent;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (City != null ? City.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (State != null ? State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Zip != null ? Zip.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsCurrent.GetHashCode();
                return hashCode;
            }
        }
    }
}
