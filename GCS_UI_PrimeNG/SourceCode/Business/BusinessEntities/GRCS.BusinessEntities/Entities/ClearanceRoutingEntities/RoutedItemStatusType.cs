using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoutedItemStatusType
    {
        [EnumMember]
        Waiting = 1,
        [EnumMember]
        Approved = 2,
        [EnumMember]
        AutoApproved = 3,
        [EnumMember]
        ConditionallyApproved = 4,
        [EnumMember]
        Rejected = 5,
        [EnumMember]
        AutoRejected = 6,
        [EnumMember]
        Cancelled = 7,
        [EnumMember]
        Excluded = 8,
        [EnumMember]
        Error = 9,
        [EnumMember]
        SystemCancel =10,
        [EnumMember]
        ReInstated = 11,
        [EnumMember]
        RoutingStopped = 12,
        [EnumMember]
        Deleted = 13,
        [EnumMember]
        SystemReInstated = 14
    }
}
