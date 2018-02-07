/* ***************************************************************************  
* Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : WorkQueueFilters
 * Project Code :   
 * Author       : Parvath
 * Created on   : Sprint 4
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * Rengaraj          18/12/2012      Add New Data Member
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for workqueue specific Criteria fields
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities
{
    [DataContract]
   public class WorkQueueCriteria : EntityInformation
    {
       
        [DataMember]
        public string PcCompany { get; set; }

        [DataMember]
        public long  PcCompanyId { get; set; }

        [DataMember]
        public long ContractId { get; set; }

        [DataMember]
        public string RightsType { get; set; }



        [DataMember]
        public ReleaseInfo ReleaseInfo { get; set; }

        [DataMember]
        public ResourceInfo ResourceInfo { get; set; }

        [DataMember]
        public string ContractStatus { get; set; }

        // Add Data Member for UC 017
        [DataMember]
        public long ClearanceCompanyId{ get; set; }

        [DataMember]
        public long? SplitDealId { get; set; }

        [DataMember]
        public bool? isLegalReviewRequired { get; set; }
    }
}
