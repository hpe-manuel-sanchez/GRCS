using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxFolder
    {
        [DataMember]
        public long? UserId { get; set; }

        [DataMember]
        public long FolderId { get; set; }

        [DataMember]
        public string FolderName { get; set; }

        [DataMember]
        public List<ClearanceInboxProject> Projects { get; set; }

        [DataMember]
        public long ProjectCount { get; set; }

        [DataMember]
        public bool? IsSystemFolder { get; set; }
    }
}