using System;
using Acme.Billing.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Acme.Billing.UnitTest
{
    [TestClass]
    public class CustomerRepositoryTests : UnitTestBase
    {
        private Mock<ICustomerRepository> customerRepositoryMock;

        [TestInitialize]
        public override void Initialize()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            
        }

        [TestMethod]
        public void UpdateExistingCustomerTest()
        {

        }
    }
}
