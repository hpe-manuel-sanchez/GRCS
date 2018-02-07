using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoutingType
    {
        [EnumMember] RoutingSource ,
        [EnumMember] RoutingAction 
    }
}
