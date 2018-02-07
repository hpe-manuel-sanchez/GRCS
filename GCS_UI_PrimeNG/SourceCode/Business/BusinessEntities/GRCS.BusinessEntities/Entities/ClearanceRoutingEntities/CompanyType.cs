using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum CompanyType
    {
        [EnumMember]
        Requesting = 1,
        [EnumMember]
        Owning = 2

    }
}
