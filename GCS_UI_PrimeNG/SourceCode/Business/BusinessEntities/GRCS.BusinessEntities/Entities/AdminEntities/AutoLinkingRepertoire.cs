using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
   [DataContract]
  
    public class AutoLinkingRepertoire:EntityInformation
    {
       public AutoLinkingRepertoire()
       {
           Companies = new List<DataInfo>();
           Division = new List<DataInfo>();
           Label = new List<DataInfo>();
       }

       [DataMember]
       public List<DataInfo> Companies { get; set; }
       [DataMember]
       public List<DataInfo> Division { get; set; }
       [DataMember]
       public List<DataInfo> Label { get; set; }
    }
}
