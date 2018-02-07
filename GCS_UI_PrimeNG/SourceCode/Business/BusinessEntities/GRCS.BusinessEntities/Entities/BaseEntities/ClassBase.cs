using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class ClassBase : ClassInfo
    {
        [DataMember]
        public int StartIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public int TotalRows { get; set; }
    }
}
