using System.Collections.Generic;
using Acme.Billing.DomainModel;

namespace Acme.Billing.Repository.Interface
{
    /// <summary>
    /// Perform user management tasks.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Import customer information from a CSV file in local system.
        /// Each CSV line is in the format: 
        /// uuid, name, email, address, city, state, zip
        /// A2B4G60, Max Power, mpower@mail.fake, 123 Test Dr, San Francisco, CA, 94518
        /// </summary>
        /// <param name="csvPath">The path to the CSV file containing customer information.</param>
        /// <param name="csvContainHeader">If true, the CSV first line will be the header.</param>
        IList<Customer> GenerateCustomerFromCsv(string csvPath, bool csvContainHeader);

        /// <summary>
        /// Perform customer maintenance tasks (add, update, delete).
        /// </summary>
        /// <param name="customers"></param>
        void UpdateUsers(IList<Customer> customers);

        /// <summary>
        /// Get all active customers in the system.
        /// </summary>
        /// <returns></returns>
        IList<Customer> GetAllActiveCustomers();
    }
}
