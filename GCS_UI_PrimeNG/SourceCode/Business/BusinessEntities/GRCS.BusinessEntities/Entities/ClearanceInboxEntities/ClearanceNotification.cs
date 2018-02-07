using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceNotification
    {
        [DataMember]
        public long NotificationId { get; set; }

        [DataMember]
        public string NotificationType { get; set; }

        [DataMember]
        public long RequestId { get; set; }

        [DataMember]
        public long ClearanceProjectID { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public long FromWorkgroupId { get; set; }

        [DataMember]
        public long ToWorkgroupId { get; set; }

        [DataMember]
        public string FromWorkgroupName { get; set; }

        [DataMember]
        public string ToWorkgroupName { get; set; }

        [DataMember]
        public string ProjectCode { get; set; }

        [DataMember]
        public string RequestType { get; set; }

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string PrimaryArtistName { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }

        
    }
}