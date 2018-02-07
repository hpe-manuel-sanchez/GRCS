using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum ObjectType
    {
        [EnumMember]
        Rule = 1,

        [EnumMember]
        Variation = 2
    }
}
