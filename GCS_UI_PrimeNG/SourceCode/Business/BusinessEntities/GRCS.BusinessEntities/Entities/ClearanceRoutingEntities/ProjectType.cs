using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
	[DataContract]
    public enum ProjectType
    {
		[EnumMember]
		NA,
        [EnumMember]
        Master = 1,

        [EnumMember]
        Regular = 2
    }
}
