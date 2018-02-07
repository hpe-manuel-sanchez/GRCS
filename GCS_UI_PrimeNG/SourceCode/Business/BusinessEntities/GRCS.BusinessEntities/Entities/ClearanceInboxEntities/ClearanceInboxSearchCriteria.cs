using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxSearchCriteria : PagingBase
    {
        [DataMember]
        public long UserId { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public string SearchText { get; set; }

        [DataMember]
        public List<ListItem> SearchType { get; set; }

        [DataMember]
        public ClearanceInboxState InboxState { get; set; }
    }
}