/* *************************************************************************** 
 * Copyrights ® 2012 Havas Media 
 * *************************************************************************** 
 * File Name    : CacheCollectionEnvelope<T>.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Sabarish M
 * Created on   : 20/07/2012
 * ***************************************************************************
 * Modification History
 * ***************************************************************************
 * Modified by       Modified on     Remarks
 * 
*************************************************************************** */

using System.Collections.ObjectModel;

namespace GRCS.Utilities.Caching
{
    /// <summary>
    /// Class to hold collections that needs to be cached
    /// </summary>
    /// <typeparam name="T">Type of the collection that needs to be cached</typeparam>
    public class CacheCollectionEnvelope<T>
    {
        public ObservableCollection<T> CachedCollection { get; set; }
    }
}