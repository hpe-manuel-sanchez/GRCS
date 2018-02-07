using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    [Serializable]
    public class RoutedRequest
    {

        [DataMember]
        public long RequestId { get; set; }

        [DataMember]
        public int ApprovalStatus { get; set; }

        [DataMember]
        public long RoutedItemId { get; set; }

        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public long ProjectId { get; set; }

        [DataMember]
        public int SalesChannelId { get; set; }

        [DataMember]
        public string ReviewDate { get; set; }

        [DataMember]
        public short RequestRead { get; set; }

        [DataMember]
        public string AvailableDate { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public RequestStatus RequestStatus { get; set; }

        [DataMember]
        public int SequenceNo { get; set; }

        public bool IsUmgiWorkgroup { get; set; }

        [DataMember]
        public DateTime LastModifiedDateTime { get; set; }

        [DataMember]
        public long? AssignedToUserId { get; set; }

        [DataMember]
        public string AssignedToUser { get; set; }
    }
}
