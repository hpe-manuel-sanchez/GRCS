using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceInboxState
    {
        [DataMember]
        public long? FolderSize { get; set; }
        
        [DataMember]
        public long? SelectedFolderId { get; set; }

        [DataMember]
        public bool? ShowAllProjects { get; set; }

        [DataMember]
        public string SortByDirection { get; set; }

        [DataMember]
        public string SortByColumnName { get; set; }
        
        [DataMember]
        public long? SelectedProjectId { get; set; }
        
        [DataMember]
        public bool? ProjectReadStatus { get; set; }
    }
}