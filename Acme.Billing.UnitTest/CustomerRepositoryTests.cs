using System;
using Acme.Billing.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acme.Billing.UnitTest
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private Mock<ICustomerRepository> customerRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
