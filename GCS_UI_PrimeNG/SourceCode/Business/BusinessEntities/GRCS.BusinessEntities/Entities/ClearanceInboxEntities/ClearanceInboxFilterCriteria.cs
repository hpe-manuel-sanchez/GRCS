using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;


namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxFilterCriteria : PagingBase
    {
        [DataMember]
        public long UserId { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public List<ListItem> RequestType { get; set; }

        [DataMember]
        public List<ListItem> RccAdminRequestType { get; set; }

        [DataMember]
        public List<ListItem> RccHandler { get; set; }

        [DataMember]
        public List<ListItem> Requestor { get; set; }

        [DataMember]
        public List<ListItem> ScopeType { get; set; }

        [DataMember]
        public List<WorkgroupBase> Workgroup { get; set; }
    }
}