using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acme.Billing.DomainModel;
using Acme.Billing.Infrastructure;
using Acme.Billing.Repository.Interface;

namespace Acme.Billing.Repository.Implementation
{
    /// <summary>
    /// Implementing the user management tasks using serialization.
    /// </summary>
    public class FlatFileUserRepository : ICustomerRepository
    {
        const int COLUMN_UUID = 0;
        const int COLUMN_NAME = 1;
        const int COLUMN_EMAIL = 2;
        const int COLUMN_ADDRESS = 3;
        const int COLUMN_CITY = 4;
        const int COLUMN_STATE = 5;
        const int COLUMN_ZIP = 6;
        static FlatFileUserRepository()
        {
            CustomerPool = new Dictionary<string, Customer>();
        }

        protected static readonly IDictionary<string, Customer> CustomerPool;

        /// <summary>
        /// Import customer information from a CSV file in local system.
        /// </summary>
        /// <param name="csvPath">the physical path to the CSV file</param>
        /// <param name="csvContainHeader">indicate if the first line of the CSV file contain header labels</param>
        /// <returns></returns>
        public IList<Customer> GetUsers(string csvPath, bool csvContainHeader)
        {
            IList<Customer> customers = new List<Customer>();
            
            using (FileStream filestream = File.OpenRead(csvPath))
            {
                using (StreamReader reader = new StreamReader(filestream))
                {
                    if (csvContainHeader)
                    {
                        string headerLabels = reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] data = line?.Split(',');
                        if (data == null || data.Length != 7)
                        {
                            throw new InvalidDataException("Wrong CSV format. Expecting {uuid, name, email, address, city, state, zip}");
                        }

                        Customer cust = new Customer(data[COLUMN_UUID], 
                            data[COLUMN_NAME], 
                            data[COLUMN_EMAIL], 
                            data[COLUMN_ADDRESS], 
                            data[COLUMN_CITY], 
                            data[COLUMN_STATE], 
                            data[COLUMN_ZIP]);

                        customers.Add(cust);
                    }
                }
            }
            return customers;
        }

        /// <summary>
        /// Perform customer maintenance tasks (add, update, delete).
        /// </summary>
        /// <param name="customers"></param>
        public void UpdateUserData(IList<Customer> customers)
        {
            if (customers != null && customers.Any())
            {
                foreach (Customer c in customers)
                {
                    if (CustomerPool.ContainsKey(c.CustomerId))
                    {
                        CustomerPool[c.CustomerId] = c;
                    }
                    else
                    {
                        Customer deactivatedCustomer = CustomerPool[c.CustomerId];
                        deactivatedCustomer.Deactivate();
                        CustomerPool[c.CustomerId] = deactivatedCustomer;
                    }
                }
            }
        }


        /// <summary>
        /// Get all active customers in the system.
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAllActiveCustomers()
        {
            if (!CustomerPool.Any())
            {
                return new List<Customer>();
            }

            return CustomerPool
                .Select(x => x.Value)
                .Where(x => x.IsActive)
                .ToList();
        }
    }
}
