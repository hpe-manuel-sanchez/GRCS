/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GlobalAddress.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Global Address Search
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities
{
    /// <summary>
    /// GlobalAddressSearch which is inherited from BaseClass ClassInfo
    /// </summary>
    [DataContract]
    public class GlobalAddressSearch : EntityInformation
    {
        /// <summary>
        /// SearchCategoryId
        /// </summary>
        [DataMember]
        public long SearchCategoryId { get; set; }

        /// <summary>
        /// SearchValue
        /// </summary>
        [DataMember]
        public String SearchValue { get; set; }
    }
}