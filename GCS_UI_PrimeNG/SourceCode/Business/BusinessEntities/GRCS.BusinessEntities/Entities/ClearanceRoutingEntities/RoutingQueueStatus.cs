using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoutingQueueStatus
    {
        [EnumMember]
        Queued = 1,
        [EnumMember]
        InProgress = 2,
        [EnumMember]
        Completed = 3,
        [EnumMember]
        Error = 4
    }
}
