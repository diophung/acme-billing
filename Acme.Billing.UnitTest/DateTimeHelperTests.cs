using System;
using Acme.Billing.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.Billing.UnitTest
{
    [TestClass]
    public class DateTimeHelperTests
    {
        [TestMethod, TestCategory("Helper")]
        public void IsInFutureTest()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            Assert.IsTrue(DateTimeHelper.IsInFuture(currentMonth, currentYear + 1));
        }
    }
}
