using System;
using System.Collections.Generic;
using Acme.Billing.DomainModel;
using Acme.Billing.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acme.Billing.UnitTest
{
    [TestClass]
    public class BillingRepositoryTests : UnitTestBase
    {
        private Mock<ICustomerRepository> customerRepositoryMock;
        public BillingRepositoryTests()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(x => x.GetAllActiveCustomers())
                .Returns(new List<Customer>
                {
                    new Customer("A2B4G60", "Max Power", "mpower@mail.fake", "123 Test Dr", "San Francisco", "CA", "94518"),
                    new Customer("A2B4G61", "Max Power 1", "mpower1@mail.fake", "123 Test Dr", "San Francisco", "CA", "94518")
                });
        }

        [TestInitialize]
        public override void Initialize()
        {

        }

        [TestCleanup]
        public override void Cleanup()
        {

        }
    }
}
