using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class ClearancePushR2Item
    {

        public enum StatusTypeBackground
        {

            Pending=1,
            InProcess=2,
            Success=3,
            Fail=4

        };

        [DataMember]
        public long ProcessId { get; set; }

        [DataMember]
        public long? ClrProjectId { get; set; }

        [DataMember]
        public long ItemId { get; set; }

        [DataMember]
        public byte? ItemType { get; set; }

        [DataMember]
        public byte? Status_Type { get; set; }

        [DataMember]
        public string Error_Description { get; set; }

        [DataMember]
        public string CreatedUser { get; set; }

        [DataMember]
        public DateTime CreatedDttm { get; set; }

        [DataMember]
        public string ModifiedUser { get; set; }

        [DataMember]
        public DateTime ModifiedDttm { get; set; }

        [DataMember]
        public string ArchiveFlag { get; set; }

        [DataMember]
        public byte? No_Of_Retries { get; set; }

        [DataMember]
        public long? R2ReleaseID { get; set; }

    }

    [DataContract(Name = "StatusTypeBackgroundR2Push")]
    public enum StatusTypeBackgroundR2Push
    {
        Pending = 1,
        InProcess = 2,
        Success = 3,
        Fail = 4
    }


    [DataContract(Name = "StatusTypeR2PushItem")]
    public enum StatusTypeR2PushItem
    {
        Release_Type = 1,
        Resource_Type = 2,
    }

}
