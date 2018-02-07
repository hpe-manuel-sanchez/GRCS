using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [Serializable]
    public class R2ReleaseResources
    {
        [DataMember]
        public long ResourceId { get; set; }

        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public long CompanyId { get; set; }

        [DataMember]
        public TrackInfo Track { get; set; }

        [DataMember]
        public string RightsType { get; set; }

        [DataMember]
        public string Isrc { get; set; }
    }
}