using System;
using Acme.Billing.Repository.Implementation;
using Acme.Billing.Repository.Interface;
using StructureMap;
using StructureMap.Pipeline;
namespace Acme.Billing.ConsoleApp
{
    public class Program
    {
        static Program()
        {
            InitializeIoCcontainer();
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to the customer CSV file:");
            string csvPath = Console.ReadLine();
            //todo: implement UI & workflow

        }

        /// <summary>
        /// Initialize the IoC container for Dependency Injection.
        /// </summary>
        public static void InitializeIoCcontainer()
        {
            ObjectFactory.Initialize(c =>
            {
                ILifecycle lifecycle = new TransientLifecycle();
                c.For<ILifecycle>().Use(lifecycle);

                c.For<IBillStatementRepository>().LifecycleIs(lifecycle).Use<BillStatementRepository>();
                c.For<ICustomerRepository>().LifecycleIs(lifecycle).Use<CustomerRepository>();
                c.For<IEmailRepository>().LifecycleIs(lifecycle).Use<EmailRepository>();
                c.For<IReportRepository>().LifecycleIs(lifecycle).Use<ReportRepository>();
            });
        }
    }
}
