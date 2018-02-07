/* ************************************************************************ 
 * Copyrights ® 2013 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceEmailDetails.cs 
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
* Description :  Defines the Email detail entities                                    
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    [DataContract]
    public class ClearanceEmailDetails
    {
        [DataMember]
        public long ClrEmailQueueDetailsId { get; set; }
        [DataMember]
        public long ClrEmailQueueId { get; set; }
        [DataMember]
        public long ClrProjectId { get; set; }
        [DataMember]
        public long ResourceId { get; set; }
        [DataMember]
        public long ReleaseId { get; set; }
        [DataMember]
        public byte RoutedChannelType { get; set; }
        [DataMember]
        public long ContractId { get; set; }
        [DataMember]
        public string ActionUserLoginName { get; set; }
        [DataMember]
        public byte ActionType { get; set; }
        [DataMember]
        public string UPC { get; set; }
        [DataMember]
        public long FreehandCompanyId { get; set; }
        [DataMember]
        public string OptionalData1 { get; set; }
        [DataMember]
        public string OptionalData2 { get; set; }

    }
}
