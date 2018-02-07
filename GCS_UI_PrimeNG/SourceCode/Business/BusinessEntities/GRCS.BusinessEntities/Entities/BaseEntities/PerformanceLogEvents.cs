using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public enum PerformanceLogEvents
    {
       [EnumMember]
       [StringValue("WorkgroupSearch")]
       WorkgroupSearch,
       [EnumMember]
       [StringValue("WorkgroupSave")]
       WorkgroupSave,
       [EnumMember]
       [StringValue("ContractSearch")]
       ContractSearch,
       [EnumMember]
       [StringValue("ContractSave")]
       ContractSave,
       [EnumMember]
       [StringValue("ClrProjectSearch")]
       ClrProjectSearch,
       [EnumMember]
       [StringValue("ClrProjectSave")]
       ClrProjectSave
    }

    public class StringValueAttribute : System.Attribute
    {
        private readonly string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}