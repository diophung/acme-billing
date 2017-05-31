using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Billing.DomainModel;
using Acme.Billing.Repository.Interface;

namespace Acme.Billing.Repository.Implementation
{
    public class ReportRepository : IReportRepository
    {
        public Report GetCustomerReport(int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}
