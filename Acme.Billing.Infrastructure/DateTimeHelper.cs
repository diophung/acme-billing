using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Billing.Infrastructure
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Verify if the month and year is in the future.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsInFuture(int month, int year)
        {
            DateTime dt = new DateTime(year, month, DateTime.Now.Day);
            return dt.Date > DateTime.Now.Date;
        }
    }
}
