using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    public class ClearanceRejectReason
    {
        [DataMember]
        public long ReasonId { get; set; }
        [DataMember]
        public int Sequence_No { get; set; }
        [DataMember]
        public string RejectReason { get; set; }
        [DataMember]
        public bool IsMarktng { get; set; }
        [DataMember]
        public bool IsLegal { get; set; }
        [DataMember]
        public bool IsUMGI { get; set; }
        [DataMember]
        public string Archive_Flag { get; set; }
        [DataMember]
        public DateTime Modified_Dttm { get; set; }

    }

    [DataContract]
    public class ClearanceRejectReasonList
    {
        public List<ClearanceRejectReason> clearanceRejectReasonList = new List<ClearanceRejectReason>();
    }
}
