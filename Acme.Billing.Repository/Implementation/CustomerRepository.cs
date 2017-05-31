using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acme.Billing.DomainModel;
using Acme.Billing.Repository.Interface;

namespace Acme.Billing.Repository.Implementation
{
    /// <summary>
    /// Handling customer management tasks.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        //The index of each column in CSV file
        const int COLUMN_UUID = 0;
        const int COLUMN_NAME = 1;
        const int COLUMN_EMAIL = 2;
        const int COLUMN_ADDRESS = 3;
        const int COLUMN_CITY = 4;
        const int COLUMN_STATE = 5;
        const int COLUMN_ZIP = 6;
        static CustomerRepository()
        {
            CustomerPool = new Dictionary<string, Customer>();
        }

        /// <summary>
        /// Storing the customers in ACME system.
        /// The key is customer ID, the value is customer data.
        /// </summary>
        protected static readonly IDictionary<string, Customer> CustomerPool;

        /// <summary>
        /// Import customer information from a CSV file.
        /// </summary>
        /// <param name="csvPath">the physical path to the CSV file</param>
        /// <param name="csvContainHeader">indicate if the first line of the CSV file contain header labels</param>
        /// <returns></returns>
        public IList<Customer> GenerateCustomerFromCsv(string csvPath, bool csvContainHeader)
        {
            IList<Customer> customers = new List<Customer>();
            
            using (FileStream filestream = File.OpenRead(csvPath))
            {
                using (StreamReader reader = new StreamReader(filestream))
                {
                    if (csvContainHeader)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] data = reader.ReadLine()?.Split(',');
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
        /// Save, update or soft-delete the customer
        /// </summary>
        /// <param name="c"></param>
        public void SaveOrUpdate(Customer c)
        {
            CustomerPool[c.CustomerId] = c;
        }

        /// <summary>
        /// Perform customer management tasks (add, update, delete).
        /// </summary>
        /// <param name="customers"></param>
        public void UpdateUsers(IList<Customer> customers)
        {
            if (customers != null && customers.Any())
            {
                foreach (Customer c in customers)
                {
                    SaveOrUpdate(c);
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
