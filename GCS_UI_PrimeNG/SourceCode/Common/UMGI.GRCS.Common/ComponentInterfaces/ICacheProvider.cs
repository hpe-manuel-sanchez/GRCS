/* ************************************************************************ 
 * Copyrights ® 2013 UMGI 
 * ************************************************************************ 
 * File Name    : ICacheProvider.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 15-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      

****************************************************************************/
using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.CacheItems;
using UMGI.GRCS.BusinessEntities.Interfaces;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface ICacheProvider
    {
        void PopulateCache(CacheItems cacheItem);
        void GetCachedItems(out List<KeyValuePair<CacheDataType, CacheItems>> supportedCacheItems);
        bool IsCacheAvailable(CacheItems itemId);
        void RemoveCache(CacheItems cacheItem,CacheDataType cacheType);
    }

    public interface ICacheProviderMetaData { String MessageType { get; } }


}
