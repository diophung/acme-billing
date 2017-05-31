using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Billing.DomainModel
{
    /// <summary>
    /// Represent a system report in ACME billing system.
    /// </summary>
    public class Report
    {
        public Report(string content, int month, int year, DateTime genDate)
        {
            this.ReportContent = content;
            this.GeneratedDateTime = genDate;
            this.Month = month;
            this.Year = year;
        }

        public string ReportContent { get; }
        public int Month { get; }
        public int Year { get; }
        public DateTime GeneratedDateTime { get; }
    }
}
