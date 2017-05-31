using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.Billing.UnitTest
{
    public abstract class UnitTestBase
    {
        [TestInitialize]
        public abstract void Initialize();

        [TestCleanup]
        public abstract void Cleanup();
    }
}
