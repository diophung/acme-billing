using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Core;

namespace Acme.Billing.Repository
{
    /// <summary>
    /// The base class for all repository.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Get an instance of the logger based on current assembly
        /// </summary>
        /// <returns></returns>
        public static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}