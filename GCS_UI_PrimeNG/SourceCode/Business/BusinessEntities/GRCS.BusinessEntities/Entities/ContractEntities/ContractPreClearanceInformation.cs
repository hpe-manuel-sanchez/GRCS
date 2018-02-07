using System;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;


namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    [DataContract]
    [Serializable]
    public class ContractPreClearanceInformation : EntityInformation
    {
        /// <summary>
        /// PreclearanceType
        /// </summary>
        [DataMember]
        public int PreclearanceType { get; set; }

        /// <summary>
        /// PreCleared
        /// </summary>
        [DataMember]
        public string PreCleared { get; set; }

        /// <summary>
        /// PreClearedTerritoryExclusion
        /// </summary>
        [DataMember]
        public string PreClearedTerritoryExclusion { get; set; }

        /// <summary>
        /// PreclearanceTypeDesc
        /// </summary>
        [DataMember]
        public string PreclearanceTypeDesc { get; set; }

        /// <summary>
        /// PreclearanceTypeDesc
        /// </summary>
        [DataMember]
        public long PreclearanceRightSetId { get; set; }

        /// <summary>
        /// IsPreclearance
        /// </summary>
        [DataMember]
        public bool IsPreclearance { get; set; }


    }
}
