using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxResourceHistory
    {
        [DataMember]
        public long ResourceId { get; set; }

        [DataMember]
        public string Isrc { get; set; }

        [DataMember]
        public string PrimaryArtistName { get; set; }

        [DataMember]
        public string ResourceTitle { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }

        [DataMember]
        public List<ClearanceInboxResourceHistoryItem> Records { get; set; }

        [DataMember]
        public int TotalRecordCount { get; set; }
    }
}