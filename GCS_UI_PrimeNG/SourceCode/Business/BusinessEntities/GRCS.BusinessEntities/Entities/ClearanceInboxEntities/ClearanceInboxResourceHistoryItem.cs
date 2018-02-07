using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxResourceHistoryItem
    {
        [DataMember]
        public string ProjectReferenceNumber { get; set; }

        [DataMember]
        public string ProjectTitle { get; set; }

        [DataMember]
        public string ReleaseType { get; set; }

        [DataMember]
        public string RequestType { get; set; }

        [DataMember]
        public string ReviewStatus { get; set; }

        [DataMember]
        public string ReviewComments { get; set; }
    }
}