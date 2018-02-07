using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoutingCaller
    {
        [EnumMember] ReviewerInbox,
        [EnumMember] RCCAdminInbox,
        [EnumMember] PASubmit,
        [EnumMember] PARequestSummary,
        [EnumMember] PAResubmit 

    }
}