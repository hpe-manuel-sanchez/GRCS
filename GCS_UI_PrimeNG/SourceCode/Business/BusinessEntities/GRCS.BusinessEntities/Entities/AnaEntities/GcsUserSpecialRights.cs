/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : UserSpecialRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
	[DataContract][Serializable]
    public class GcsUserSpecialRights
    {
        [DataMember]
        public long UserId { get; set; }

		[DataMember]
		public long WorkgroupID { get; set; }

		[DataMember]
		public byte RoleType { get; set; }

		[DataMember]
        public bool IsInRole { get; set; }
        
		[DataMember]
        public bool CanManageWorkgroup { get; set; }
        
		[DataMember]
        public bool IsR2Authorized { get; set; }
        
		[DataMember]
        public bool CanAllocateUpc { get; set; }
    }
}
