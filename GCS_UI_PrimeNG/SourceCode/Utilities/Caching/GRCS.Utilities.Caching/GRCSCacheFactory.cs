using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.Core.Utilities.Helper;

namespace GRCS.Utilities.Caching
{
    /// <summary>
    /// Provides access to AppFabric caching cluster for UMG GRCS applications
    /// This class acts as a Singleton, however due to MEF builder method must be public
    /// </summary>
    [Export(typeof(IGRCSCacheFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GrcsCacheFactory : IGRCSCacheFactory
    {
        private static readonly Lazy<GrcsCacheFactory> _cacheInstance = new Lazy<GrcsCacheFactory>(() => new GrcsCacheFactory());

        public GrcsCacheFactory()
        {
            _mIsCacheEnabled = Convert.ToBoolean(ConfigUtil.GetAppSettingsValue(Constants.IsCacheEnabled));
        }

        private readonly bool _mIsCacheEnabled;

        private readonly Dictionary<CacheDataType, string> _namedCacheCollection = new Dictionary<CacheDataType, string>{
            {CacheDataType.Reference,"umg_reference_data"},
            {CacheDataType.Resource,"umg_resource_data"},
            {CacheDataType.Activity,"umg_activity_data"}
        };

        /// <summary>
        /// Method to check Whether the cache is enabled.
        /// </summary>
        public bool IsCacheEnabled
        {
            get { return _mIsCacheEnabled; }
        }

        public static GrcsCacheFactory Instance
        {
            get { return _cacheInstance.Value; }
        }

        /// <summary>
        /// Method to update an object in to a named cache
        /// </summary>
        /// <param name="cacheObjectIdentifier">"Key"- Identifier to identify the cache object</param>
        /// <param name="objectToCache">The object that needs to be cached</param>
        /// <param name="cacheType"> </param>
        public void UpdateCache(string cacheObjectIdentifier, object objectToCache, CacheDataType cacheType)
        {
            if (_mIsCacheEnabled)
                lock (this)
                    CacheUtil.GetCache(_namedCacheCollection[cacheType]).Put(cacheObjectIdentifier, objectToCache);
        }

        /// <summary>
        /// Method to get the object from the cache based on the object identifier
        /// </summary>
        /// <param name="cacheObjectIdentifier">"Key"- Identifier to identify the cache object</param>
        /// <param name="cacheType"> </param>
        /// <returns>Returns the cache object if any for the identifier</returns>
        public object GetObjectFromCache(string cacheObjectIdentifier, CacheDataType cacheType)
        {
            return _mIsCacheEnabled
                ? CacheUtil.GetCache(_namedCacheCollection[cacheType]).Get(cacheObjectIdentifier)
                : null;
        }

        /// <summary>
        /// Removes and object from the cache
        /// </summary>
        /// <param name="cacheObjectIdentifier">"Key"- Identifier to identify the cache object</param>
        /// <param name="cacheType"> </param>
        public void RemoveObjectFromCache(string cacheObjectIdentifier, CacheDataType cacheType)
        {
            if (_mIsCacheEnabled)
                CacheUtil.GetCache(_namedCacheCollection[cacheType]).Remove(cacheObjectIdentifier);
        }
    }
}