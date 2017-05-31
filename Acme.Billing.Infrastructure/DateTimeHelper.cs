using System;

namespace Acme.Billing.Infrastructure
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Verify if the month and year is in the future.
        /// The day to be used will be the first day of the month.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsInFuture(int month, int year)
        {
            DateTime dt = new DateTime(year, month, 1);
            return dt.Date > DateTime.Now.Date;
        }
    }
}
