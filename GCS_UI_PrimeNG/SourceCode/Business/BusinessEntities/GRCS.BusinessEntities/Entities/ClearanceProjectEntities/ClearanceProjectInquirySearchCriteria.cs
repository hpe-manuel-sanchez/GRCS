using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class ClearanceProjectInquirySearchCriteria : FilterCriteria
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string ProjectReferenceId { get; set; }

        [DataMember]
        public string ProjectTitle { get; set; }

        [DataMember]
        public string LocalReference { get; set; }

        [DataMember]
        public byte ProjectTypeId { get; set; }

        [DataMember]
        public int RequestTypeID { get; set; }

        [DataMember]
        public int RequestingCompany { get; set; }

        [DataMember]
        public string ThirdPartyCompany { get; set; }

        [DataMember]
        public string Requestor { get; set; }

        [DataMember]
        public string UPC { get; set; }

        [DataMember]
        public string ISRC { get; set; }

        [DataMember]
        public string ArtistName { get; set; }

        [DataMember]
        public string ReleaseTitle { get; set; }

        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }

        [DataMember]
        public bool ReadOnlyMode { get; set; }

        [DataMember]
        public string ProjectStatus { get; set; }

        [DataMember]
        public RoleGroup RoleGroup { get; set; }

        [DataMember]
        public string RequestTypeDesc { get; set; }

        [DataMember]
        public bool ArtistExactMatch { get; set; }

        [DataMember]
        public bool IsProjectBlocked{ get; set; }
    }

}