using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities
{
    [DataContract]
    [Serializable]
    public class ListItem
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public bool Selected { get; set; }

        [DataMember]
        public int Order { get; set; }
    }
}