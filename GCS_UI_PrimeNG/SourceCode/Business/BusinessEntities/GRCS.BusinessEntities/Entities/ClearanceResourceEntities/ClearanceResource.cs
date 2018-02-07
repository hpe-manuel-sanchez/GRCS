using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceResource : ResourceInfo
    {

        [DataMember]
        public long ClearanceResourceId { get; set; }

        /// <summary>
        /// Resource Archive Flag Entities
        /// </summary>
        [DataMember]
        public string ArchiveFlag { get; set; }


        /// <summary>
        /// Resource Duration Entities
        /// </summary>
        [DataMember]
        public string Duration { get; set; }
        /// <summary>
        /// Resource SuggestedFee Entities
        /// </summary>
        [DataMember]
        public decimal? SuggestedFee { get; set; }


        /// <summary>
        /// Resource ExcerptTime Entities
        /// </summary>
        [DataMember]
        public string ExcerptTime { get; set; }
        /// <summary>
        /// Resource Comments Entities
        /// </summary>
        [DataMember]
        public string Comments { get; set; }


        /// <summary>
        /// SensitiveExplotation
        /// </summary>
        [DataMember]
        public bool SensitiveExplotation_ClearanceResource { get; set; }

        /// <summary>
        /// UMGI Condition Changed
        /// </summary>
        [DataMember]
        public bool HasUMGIConditionChanged { get; set; }
        
        /// <summary>
        /// IsGloballyCleared
        /// </summary>
        [DataMember]
        public bool IsGloballyCleared { get; set; }

        /// <summary>
        /// Globally Cleared Comments 
        /// </summary>
        [DataMember]
        public string GloballyClearedComments { get; set; }



        [DataMember]
        public List<TerritorialDisplay> TerritorialRights { get; set; }

        /// <summary>
        /// Resource RecordingTypeDesc Entities
        /// </summary>
        [DataMember]
        public string RecordingTypeDesc { get; set; }
        /// <summary>
        /// Resource ResourceTypeDesc Entities
        /// </summary>
        [DataMember]
        public string ResourceTypeDesc { get; set; }

        /// <summary>
        /// Resource MusicTypeDesc Entities
        /// </summary>
        [DataMember]
        public string MusicTypeDesc { get; set; }

        [DataMember]
        public string SequenceNo { get; set; }

        [DataMember]
        public string ResourceResubmitReasonComments { get; set; }

        [DataMember]
        public string Source_Upc { get; set; }

        public long AccountId { get; set; }

        [DataMember]
        public string Resource_Status { get; set; } // cancelled, Rejected

        [DataMember]
        public string isPushedToR2 { get; set; }

        [DataMember]
        public string Status_Type { get; set; }

        [DataMember]
        public ClearancePushR2Item ClrPushR2Item { get; set; }

        [DataMember]
        public long? R2ProjectId { get; set; }

        [DataMember]
        public string RightsTypeDesc { get; set; }

        [DataMember]
        public long ResourceIndexToUpdate { get; set; }

        /// <summary>
        /// Free Hand Resource Archive Flag Entities
        /// </summary>
        [DataMember]
        public string ReplaceFreeHandFlag { get; set; }

        /// <summary>
        /// Resource Archive Flag Entities
        /// </summary>
        [DataMember]
        public string FreeHandResourceStatus { get; set; }

        [DataMember]
        public bool IsNewlyAddedAfterSubmit { get; set; }

        /// <summary>
        /// Resource Update entity flag
        /// </summary>
        [DataMember]
        public long ResourceIdToUpdate { get; set; }

        [DataMember]
        public byte? Rights_TypeId { get; set; }

        [DataMember]
        public string IncludedTerritories { get; set; }

        [DataMember]
        public string ExcludedTerritories { get; set; }

        [DataMember]
        public bool? IsRouted { get; set; }

    }
}
