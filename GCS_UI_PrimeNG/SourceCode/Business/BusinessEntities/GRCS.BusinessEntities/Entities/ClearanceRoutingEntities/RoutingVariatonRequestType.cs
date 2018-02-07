using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
	[DataContract]
	public enum RoutingVariatonRequestType
	{
		[EnumMember]
		NA,
		[EnumMember]
		Regular = 4,

		[EnumMember]
		Club = 5,

		[EnumMember]
		PriceReduction = 8,

		[EnumMember]
		TVPhysical = 10,

		[EnumMember]
		TVALaCarte = 11,

		[EnumMember]
		TVSubscription = 12,

		[EnumMember]
		TVMobileRealTone = 13,

		[EnumMember]
		TVMobileRingBackTone = 14,

		[EnumMember]
		TVStreaming = 15,

		[EnumMember]
		Promotional = 7,

		[EnumMember]
		NonTraditional = 6,

		[EnumMember]
		Master = 9
	}
}
