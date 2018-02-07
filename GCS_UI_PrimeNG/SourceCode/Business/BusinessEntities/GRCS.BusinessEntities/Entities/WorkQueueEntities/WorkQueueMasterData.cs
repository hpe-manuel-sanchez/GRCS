/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MasterDataInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       :Mohammad
 * Created on   : 10/3/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for MasterDataInfo class
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    /// <summary>
    /// MasterDataInfo 
    /// </summary>
    [DataContract]
    public class WorkQueueMasterData : EntityInformation
    {
        public WorkQueueMasterData()
        {
            Conditions = new List<StringIdentifier>();           
            ReasonForReview = new List<StringIdentifier>();
        }

        /// <summary>
        /// Conditions
        /// </summary>
        [DataMember]
        public List<StringIdentifier> Conditions { get; set; }

        /// <summary>
        /// ReasonForReview
        /// </summary>
        [DataMember]
        public List<StringIdentifier> ReasonForReview { get; set; }
    }
}
