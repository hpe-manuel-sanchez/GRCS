using System;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.Core.Utilities.logger;

namespace GRCS.Utilities.Caching
{
    /// <summary>
    /// Utility to handle the Cache requests
    /// </summary>
    public class CachingUtil
    {
        private static IGRCSCacheFactory _cacheFactory = GrcsCacheFactory.Instance;
        private static ILog _loggerInstance = LogUtil<CachingUtil>.CreateInstance();


        public static bool IsObjectInCache<T>(T cacheObjectKey, ref object objectFromCache, CacheDataType cacheDataType = CacheDataType.Reference)
        {
            _loggerInstance.MethodStart(string.Format("Looking for Item {0} in Cache {1}", cacheObjectKey, cacheDataType));
            if (_cacheFactory.IsCacheEnabled)
                try
                {
                    bool doesExistInCache = false;
                    CachingUtil.GetCachedObject(ref objectFromCache, ref doesExistInCache, false, cacheObjectKey.ToString(), cacheDataType);
                    return doesExistInCache;
                }
                catch (Exception exception)
                {
                    _loggerInstance.Error(Category.Business, "Error in Cache", exception);
                }
            return false;
        }

        /// <summary>
        /// Pushes to cache.
        /// </summary>
        /// <param name="cacheObjectKey">The p.</param>
        /// <param name="objectToCache">The object to cache.</param>
        public static void PushToCache<T>(T cacheObjectKey, object objectToCache, CacheDataType cacheDataType = CacheDataType.Reference)
        {
            _loggerInstance.MethodStart(string.Format("Pushing the item {0} to Cache {1} ", cacheObjectKey, cacheDataType));
            if (_cacheFactory.IsCacheEnabled)
                try
                {
                    bool doesExistInCache = false;
                    CachingUtil.GetCachedObject(ref objectToCache, ref doesExistInCache, true, cacheObjectKey.ToString(), cacheDataType);
                }
                catch (Exception exception)
                {
                    _loggerInstance.Error(Category.Business, "Error in Cache", exception);
                }
        }

        /// <summary>
        /// Gets the cached object.
        /// </summary>
        /// <param name="cachedResult">The cached result.</param>
        /// <param name="doesExist">if set to <c>true</c> [does exist].</param>
        /// <param name="doUpdate">if set to <c>true</c> [do update].</param>
        /// <param name="cacheObjectName">Name of the cache object.</param>
        /// <param name="cacheDataType">Type of the cache data.</param>
        public static void GetCachedObject(ref object cachedResult, ref bool doesExist, bool doUpdate, string cacheObjectName, CacheDataType cacheDataType)
        {
            if (_cacheFactory.IsCacheEnabled)
                if (doUpdate)
                    _cacheFactory.UpdateCache(cacheObjectName, cachedResult, cacheDataType);
                else
                {
                    cachedResult = _cacheFactory.GetObjectFromCache(cacheObjectName, cacheDataType);
                    if (cachedResult != null)
                        doesExist = true;
                }
        }

        public static void RemoveCacheObject(string cacheObjectIdentifier, CacheDataType cacheDataType)
        {
            if (_cacheFactory.IsCacheEnabled)
                _cacheFactory.RemoveObjectFromCache(cacheObjectIdentifier, cacheDataType);
        }
    }
}