/* ***************************************************************************
 * Copyrights ® 2012 HCL Technologies Limited
 * ***************************************************************************
 * File Name    : IGRCSCacheFactory.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Sabarish M
 * Created on   : 20/07/2012
 * ***************************************************************************
 * Modification History
 * ***************************************************************************
 * Modified by       Modified on     Remarks
 * Pavan Kumar       30-10-2012     Code Refactored
*************************************************************************** */


namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    public interface IGRCSCacheFactory
    {

        bool IsCacheEnabled { get; }
        void UpdateCache
            (string cacheObjectIdentifier, object objectToCache, CacheDataType cacheType);
        object GetObjectFromCache
            (string cacheObjectIdentifier, CacheDataType cacheType);
        void RemoveObjectFromCache
            (string cacheObjectIdentifier, CacheDataType cacheType);

    }

    /// <summary>
    /// Enum to represent the type of the cache region that
    /// the object to be inserted
    /// </summary>
    public enum CacheDataType
    {
        Reference,
        Resource,
        Activity
    }
  
}
