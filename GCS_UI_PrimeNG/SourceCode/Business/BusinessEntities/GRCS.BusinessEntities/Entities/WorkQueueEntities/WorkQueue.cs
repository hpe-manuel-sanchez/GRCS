/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkQueue.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       :Mohammad
 * Created on   : 10/3/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Siva              23/10/2012      Changed the namespace
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for WorkQueue class
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    /// <summary>
    /// WorkQueue 
    /// </summary>
    [DataContract]
    public class WorkQueue : RepertoireBase
    {
        /// <summary>
        /// Gets or sets the type (Code from Task DB)
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the task id.
        /// </summary>
        /// <value>The task id.</value>
        [DataMember]
        public long? TaskId { get; set; }

        /// <summary>
        /// Gets or sets the R2 repertoire id. (ProjectId, R2_ReleaseId, R2_ResourceId)
        /// </summary>
        /// <value>The repertoire id.</value>
        [DataMember]
        public long? R2RepertoireId { get; set; }

        /// <summary>
        /// Gets or sets the Grcs repertoire id. (ProjectId, ReleaseId, ResourceId)
        /// </summary>
        /// <value>The repertoire id.</value>
        [DataMember]
        public long? GrcsRepertoireId { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public string ResourceType { get; set; }

        /// <summary>
        /// Type of resource type (Audio, Video).
        /// </summary>
        [DataMember]
        public string RepertoireSubType { get; set; }

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }


        /// <summary>
        /// Gets or sets the contract review reason.
        /// </summary>
        /// <value>The contract review reason.</value>
        [DataMember]
        public string ContractReviewReason { get; set; }

        /// <summary>
        /// Gets or sets the contract review reason id.
        /// </summary>
        /// <value>The contract review reason id.</value>
        [DataMember]
        public byte? ContractReviewReasonId { get; set; }

        /// <summary>
        /// Gets or sets the name of the linked contract.
        /// </summary>
        /// <value>The name of the linked contract.</value>
        [DataMember]
        public string LinkedContractName { get; set; }


        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        [DataMember]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the P year.
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public int? PYear { get; set; }

        /// <summary>
        /// Gets or sets the owned releases.
        /// </summary>
        /// <value>The owned releases.</value>
        [DataMember]
        public int? OwnedReleases { get; set; }

        /// <summary>
        /// Gets or sets the owned resources.
        /// </summary>
        /// <value>The owned resources.</value>
        [DataMember]
        public int? OwnedResources { get; set; }

        /// <summary>
        /// Gets or sets the contract description.
        /// </summary>
        /// <value>The contract description.</value>
        [DataMember]
        public string ContractDescription { get; set; }

        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long? ContractId { get; set; }

        [DataMember]
        public string ContractingParty { get; set; }

        [DataMember]
        public DateTime? CommencementDate { get; set; }

        [DataMember]
        public long AccountId { get; set; }

        [DataMember]
        public long? ContractArtistId { get; set; }

        [DataMember]
        public string ContractArtistName { get; set; }

        [DataMember]
        public byte? ScopeType { get; set; }

        [DataMember]
        public int? IsPackage { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public long ClearanceCompanyCountryId { get; set; }

        /// <summary>
        /// Gets or sets the MSG description.
        /// </summary>
        /// <value>The MSG description.</value>
        [DataMember]
        public string MsgDescription { get; set; }

        [DataMember]
        public long RepertoireId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can link.
        /// </summary>
        /// <value><c>true</c> if this instance can link; otherwise, <c>false</c>.</value>
        public bool CanLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can unlink.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can unlink; otherwise, <c>false</c>.
        /// </value>
        public bool CanUnlink { get; set; }


        /// <summary>
        /// Gets or sets the parameter pre defined.
        /// </summary>
        /// <value>The parameter pre defined.</value>
        public string parameterPreDefined { get; set; }
    }
}

