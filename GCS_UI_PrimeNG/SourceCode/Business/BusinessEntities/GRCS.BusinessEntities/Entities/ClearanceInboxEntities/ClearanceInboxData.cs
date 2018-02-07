using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxData
    {
        [DataMember]
        public ClearanceInboxFilterCriteria FilterCriteria { get; set; }

        [DataMember]
        public ClearanceInboxSearchCriteria SearchCriteria { get; set; }

        [DataMember]
        public ClearanceInboxSearchResult SearchResult { get; set; }
    }
}