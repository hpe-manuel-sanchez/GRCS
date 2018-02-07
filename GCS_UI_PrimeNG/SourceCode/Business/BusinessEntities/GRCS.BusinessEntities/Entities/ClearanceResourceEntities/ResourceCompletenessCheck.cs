using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
    [DataContract]
    public class ResourceCompletenessCheck : EntityInformation
    {
        /// <summary>
        /// Clearance Resource
        /// </summary>
        public ResourceCompletenessCheck()
        {
           
        }

        /// <summary>
        /// ClrProjectId
        /// </summary>
        [DataMember]
        public long RoutedItemId { get; set; }
        /// <summary>
        /// ResourceId
        /// </summary>
        [DataMember]
        public long ResourceId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [DataMember]
        public byte Status { get; set; }
        /// <summary>
        /// R2Response
        /// </summary>
        [DataMember]
        public string R2Response { get; set; }
        /// <summary>
        /// RetryCount
        /// </summary>
        [DataMember]
        public int RetryCount { get; set; }

        /// <summary>
        /// R2ResourceCompanyId
        /// </summary>
        [DataMember]
        public long R2ResourceCompanyId{ get; set; }

        /// </summary>
        [DataMember]
        public string Reason { get; set; }

        /// <summary>
        /// WorkGroupId
        /// </summary>
        [DataMember]
        public long WorkGroupId { get; set; }





    }
}
