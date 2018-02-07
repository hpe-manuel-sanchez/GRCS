/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractRightsAcquired.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 Rubini Raja        17/07/2012       added member for active/passive status
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for ContractRightsAcquired class
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// ContractRightsAcquired which is inherited from ClassInfo
    /// </summary>
    [DataContract]
    [Serializable]
    public class ContractRightsAcquired : EntityInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRightsAcquired"/> class.
        /// </summary>
        public ContractRightsAcquired()
        {
            AcquiredOption = new List<StringIdentifier>();
        }

        /// <summary>
        /// RightAcquiredId
        /// </summary>
        [DataMember]
        public long RightAcquiredId { get; set; }

        /// <summary>
        /// RightAcquiredType
        /// </summary>
        [DataMember]
        public string RightAcquiredType { get; set; }

        /// <summary>
        /// RightSetId
        /// </summary>
        [DataMember]
        public long RightSetId { get; set; }

        /// <summary>
        /// Check Acquired
        /// </summary>
        [DataMember]
        public bool IsAcquired { get; set; }

        /// <summary>
        /// RightAcquiredTypeId
        /// </summary>
        [DataMember]
        public byte RightAcquiredTypeId { get; set; }

        /// <summary>
        /// AcquiredStatus
        /// </summary>
        [DataMember]
        public byte? AcquiredStatus { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [DataMember]
        public string Notes { get; set; }

        /// <summary>
        /// AcquiredOption
        /// </summary>
        [DataMember]
        public List<StringIdentifier> AcquiredOption { get; set; }
    }
}