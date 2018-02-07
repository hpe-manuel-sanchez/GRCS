using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public enum TerritoryType
    {
        [EnumMember]
        Release = 1,
        [EnumMember]
        Owning = 2,
        [EnumMember]
        Requesting = 3
    }
}
