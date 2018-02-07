/* ************************************************************************ 
 * Copyrights ® 2013 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceEmail.cs 
 * Project Code : UMG-GRCS
 * Author       : Arunagiri G
 * Created on   : 11-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the EmailInfo entities                                    
                  
****************************************************************************/
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    [DataContract]
    public class ClearanceEmail
    {
        [DataMember]
        public long ClrEmailQueueId { get; set; }

        [DataMember]
        public EmailType EmailType { get; set; }

        [DataMember]
        public long WorkgroupId { get; set; }

        [DataMember]
        public long EmailToUserId { get; set; }

        [DataMember]
        public string EmailId { get; set; }

        [DataMember]
        public string UserLoginName { get; set; }

        [DataMember]
        public long ClrProjectId { get; set; }
        [DataMember]
        public long ResourceId { get; set; }
        [DataMember]
        public long ReleaseId { get; set; }
        [DataMember]
        public int RoutedChannelType { get; set; }
        [DataMember]
        public long ContractId { get; set; }
        [DataMember]
        public string ActionUserLoginName { get; set; }
        [DataMember]
        public byte ActionType { get; set; }
        [DataMember]
        public string UPC { get; set; }
        [DataMember]
        public long FreeHandCompanyId { get; set; }
        [DataMember]
        public string OptionalData1 { get; set; }
        [DataMember]
        public string OptionalData2 { get; set; }

        [DataMember]
        public string CcEmailId { get; set; }

        [DataMember]
        public string BccEmailId { get; set; }
    }
}
