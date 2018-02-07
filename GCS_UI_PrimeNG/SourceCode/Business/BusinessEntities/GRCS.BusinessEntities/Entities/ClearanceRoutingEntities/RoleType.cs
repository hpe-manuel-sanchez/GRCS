using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoleType
    {
        [EnumMember]
        LocalLabelReviewer = 1,
        [EnumMember]
        NationalMarketingReviewer = 2,
        [EnumMember]
        NationalLegalReviewer = 3,
        [EnumMember]
        InternationalMarketingReviewer = 4,
        [EnumMember]
        InternationalLegalReviewer = 5,
        [EnumMember]
        UmgiMarketingReviewer = 6,
        [EnumMember]
        UmgiGlobalClearance = 7,
        [EnumMember]
        Requestor = 8,
        [EnumMember]
        Inquiry = 9,
        [EnumMember]
        RccAdmin = 10
    }

}
