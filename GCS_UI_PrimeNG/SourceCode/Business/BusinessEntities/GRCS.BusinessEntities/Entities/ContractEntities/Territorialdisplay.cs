/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TerritorialDisplay.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 11-07-2012
 * ************************************************************************ 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Territorial Display
                  
****************************************************************************/

using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// TerritorialDisplay which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    [Serializable]
    public class TerritorialDisplay 
    {
        /// <summary>
        /// Property holding the Id
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        [DataMember]
        public long ParentId { get; set; }

        /// <summary>
        /// Check Territory
        /// </summary>
        [DataMember]
        public bool IsTerritory { get; set; }

        /// <summary>
        /// Territory Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Check NotApplicable
        /// </summary>
        [DataMember]
        public bool IsNotApplicable { get; set; }

        /// <summary>
        /// Check Included
        /// </summary>
        [DataMember]
        public bool IsIncluded { get; set; }

        /// <summary>
        /// Check Excluded
        /// </summary>
        [DataMember]
        public bool IsExcluded { get; set; }

        /// <summary>
        /// TerritoryComments
        /// </summary>
        [DataMember]
        public string TerritoryComments { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember]
        public TerritoryScope Status { get; set; }

        // territory Count 
        public string TerritoryCount { get; set; }

        [DataMember]
        public bool IsSafeTerritory { get; set; }
    }

    /// <summary>
    /// Enum values should map to DB values (Look ups)
    /// INCLD
    /// </summary>
    [DataContract]
    [Serializable]
    public enum TerritoryScope
    {
        [EnumMember] NotApplicable = 0,
        [EnumMember] Included = 1,
        [EnumMember] Excluded = 2
       
    }
}