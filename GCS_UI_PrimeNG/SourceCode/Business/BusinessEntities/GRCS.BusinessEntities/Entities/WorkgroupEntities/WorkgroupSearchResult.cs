/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkgroupSearchResult.cs 
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

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class WorkgroupSearchResult : Workgroup
    {
        public WorkgroupSearchResult() { }

        public WorkgroupSearchResult(Workgroup baseData)
        {
            base.ID = baseData.ID;
            base.IsActive = baseData.IsActive;
            base.ParentID = baseData.ParentID;
            base.Name = baseData.Name;
            base.R2Contract = baseData.R2Contract;
            base.RoleID = baseData.RoleID;
            base.RoleName = baseData.RoleName;
            base.StatusType = baseData.StatusType;
            base.Users = baseData.Users;
            base.Countries = baseData.Countries;
            base.Companies = baseData.Companies;
            base.ChildWorkgroups = baseData.ChildWorkgroups;
            base.Status = baseData.Status;
            base.ModifiedDateTime = baseData.ModifiedDateTime;
        }

        public WorkgroupSearchResult(ChildWorkgroup baseData)
        {
            base.ID = baseData.Id;
            base.ParentID = baseData.ParentId;
            base.Name = baseData.Name;
            base.R2Contract = baseData.R2Contract;
            base.RoleID = (int)baseData.RoleId;
            base.RoleName = baseData.RoleName;
            base.StatusType = baseData.StatusType;
            base.Users = baseData.Users;
            base.Countries = baseData.Countries;
            base.Companies = baseData.Companies;
            base.ChildWorkgroups = baseData.ChildWorkgroups;
            base.ModifiedDateTime = baseData.ModifiedDateTime;
        }

        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string ChildWorkgroupNames { get; set; }
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
