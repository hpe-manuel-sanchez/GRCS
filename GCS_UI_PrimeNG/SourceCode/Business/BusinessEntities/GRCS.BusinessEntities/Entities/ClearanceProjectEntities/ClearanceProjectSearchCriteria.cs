using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class ClearanceProjectSearchCriteria : FilterCriteria
    {
        [DataMember]
        public string ProjectReferenceId { get; set; }

        [DataMember]
        public string ProjectTitle { get; set; }

        [DataMember]
        public string LocalReference { get; set; }


        [DataMember]
        public int ProjectTypeId { get; set; }


        [DataMember]
        public int RequestTypeID { get; set; }


        [DataMember]
        public string RequestingCompany { get; set; }


        [DataMember]
        public string ThirdPartyCompany { get; set; }


        [DataMember]
        public string Requestor { get; set; }

        //[DataMember]
        //public int Status_Type { get; set; }
        [DataMember]
        public List<byte> listStatus_Type { get; set; }

        [DataMember]
        public int WorkgroupID { get; set; }

        [DataMember]
        public string ReadOnly { get; set; }

        [DataMember]
        public long ProjectId { get; set; }

       [DataMember]
        public string RequestTypeDesc { get; set; }

    }

}