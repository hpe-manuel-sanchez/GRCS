/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkGroupUser.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    [Serializable]
    public class WorkGroupUser : ApplicationUser
    {
        [DataMember]
        public System.Collections.Generic.List<Workgroup> WorkGroups { get; set; }
        [DataMember]
        public bool IsUserDefault { get; set; }
        [DataMember]
        public string UserWorkgroupNames { get; set; }
        [DataMember]
        public int TotalRows { get; set; }
        [DataMember]
        public DateTime ModifiedDateTime { get; set; }
        [DataMember]
        public string UserInactiveWorkgroupNames { get; set; }
        [DataMember]
        public bool IsInRole { get; set; }
        [DataMember]
        public bool CanManageWorkgroup { get; set; }
        [DataMember]
        public bool IsR2Authorized { get; set; }
        [DataMember]
        public bool CanAllocateUpc { get; set; }
    }

	[DataContract]
	public class WorkGroupUserSearchCriteria 
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string LoginId { get; set; }

        [DataMember]
        public string CountryName { get; set; }

		[DataMember]
		public string UserToExclude { get; set; }

		[DataMember]
		public PagingBase GridFilterlterCriteria { get; set; }
    }
}
