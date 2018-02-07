using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxSearchResult
    {
        [DataMember]
        public long ReminderCount { get; set; }

        [DataMember]
        public long NotificationCount { get; set; }

        [DataMember]
        public List<ClearanceInboxFolder> Folders { get; set; }

        [DataMember]
        public ClearanceInboxState InboxState { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }
    }
}