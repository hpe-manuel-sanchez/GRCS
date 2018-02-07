using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    [DataContract]
    public class R2Companies
    {
        [DataMember]
        public WSCompanySearchServiceProxy.WSCompany[] Companies { get; set; }
    }
}