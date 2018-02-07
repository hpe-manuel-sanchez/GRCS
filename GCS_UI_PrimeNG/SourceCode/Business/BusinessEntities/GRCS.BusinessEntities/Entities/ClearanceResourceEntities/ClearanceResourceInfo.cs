using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
     [DataContract]
    public class ClearanceResourceInfo: ResourceInfo
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
        public decimal SuggestedFee { get; set; }


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


       
    }
}
