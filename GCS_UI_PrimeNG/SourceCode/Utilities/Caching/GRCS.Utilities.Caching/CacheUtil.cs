using Microsoft.ApplicationServer.Caching;
using System.Diagnostics;

namespace GRCS.Utilities.Caching
{
    /// <summary>
    /// Class to initialize the data cache object
    /// </summary>
    internal class CacheUtil
    {
        private static DataCache _mCache;

        /// <summary>
        /// Method to  get the DataCache Object
        /// </summary>
        /// <returns>returns a DataCache Object</returns>
        internal static DataCache GetCache(string namedCache)
        {
            if (_mCache == null)
            {
                //Disable tracing to avoid informational/verbose messages on the web page
                DataCacheClientLogManager.ChangeLogLevel(TraceLevel.Error);
                _mCache = new DataCacheFactory().GetCache(namedCache);
            }
            return _mCache;
        }
    }
}