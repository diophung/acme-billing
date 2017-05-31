using System.Collections;
using System.Collections.Generic;

namespace Acme.Billing.Infrastructure
{
    /// <summary>
    /// Extension class for IList
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Check if a list is null or having no element.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this IList<object> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
