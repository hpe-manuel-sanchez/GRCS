/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TaskInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Ajitha R
 * Created on   : 26-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by      Modified on     Remarks  
 * Pavan Kumar      06-12-2012      Contract Id added (Change in Task Table)          
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for TaskInfo Details
                  
****************************************************************************/
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    /// <summary>
    /// Task table entities
    /// </summary>
    [DataContract]
    public class TaskInfo : EntityInformation
    {
        /// <summary>
        /// Gets or sets the type of the task.
        /// </summary>
        /// <value>The type of the task.</value>
        [DataMember]
        public string TaskType { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        /// <value>The name of the task.</value>
        [DataMember]
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the type of the stakus.
        /// </summary>
        /// <value>The type of the stakus.</value>
        [DataMember]
        public int ReviewreasonType { get; set; }

        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        /// <value>The item id.</value>
        [DataMember]
        public long ItemId { get; set; }

        /// <summary>
        /// Gets or sets the type of the status.
        /// </summary>
        /// <value>The type of the status.</value>
        [DataMember]
        public string StatusType { get; set; }

        /// <summary>
        /// Contract Id related to the Repertoire
        /// </summary>
        [DataMember]
        public long? ContractId { get; set; }

        /// <summary>
        /// Flag => Constants.Release, Resource
        /// </summary>
        [DataMember]
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets the type of the status.
        /// </summary>
        /// <value>The type of the status.</value>
        [DataMember]
        public string ItemType { get; set; }
    }
}
