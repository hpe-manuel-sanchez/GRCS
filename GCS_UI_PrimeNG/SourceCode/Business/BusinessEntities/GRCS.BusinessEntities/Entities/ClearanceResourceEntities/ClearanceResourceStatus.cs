using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
    [DataContract]
    public class ClearanceResourceStatus
    {
        public ClearanceResourceStatus()
        {

        }

        public ClearanceResourceStatus(long pResourceId, string pSaleChannel, string pStatus)
        {
            this.Resource_ID = pResourceId;
            this.Sale_Channel_Description = pSaleChannel;
            this.Status_Description = pStatus;
        }

        [DataMember]
        public long Resource_ID  { get ; set ;}         

        [DataMember]
        public string Sale_Channel_Description { get ; set ;} 

        [DataMember]
        public string Status_Description { get ; set ;}        
    }
}
