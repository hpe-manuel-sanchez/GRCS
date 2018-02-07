using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public enum StatusType
    {
        [EnumMember]
        Active = 1,
        [EnumMember]
        Deactive = 2,
        [EnumMember]
        Remove = 3

    }
}
