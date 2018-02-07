/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Workgroup.cs 
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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    [Serializable]
    public class Workgroup : PagingBase
    {
        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public long ParentID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string R2Contract { get; set; }

        [DataMember]
        public int RoleID { get; set; }

        [DataMember]
        public List<WorkGroupUser> Users { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public List<CompanyInfo> Companies { get; set; }

        [DataMember]
        public List<TerritoryInfo> Countries { get; set; }

        [DataMember]
        public string StatusType { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }

        [DataMember]
        public List<KeyValuePair<long, string>> ChildWorkgroups { get; set; }

        [DataMember]
        public List<TerritorialDisplay> Territories { get; set; }

        [DataMember]
        public int RequestTypeID { get; set; }

        [DataMember]
        public string RequestTypeName { get; set; }

        public void Method()
        {
            throw new System.NotImplementedException();
        }

        [DataMember]
        public string ExpectWorkgroupID { get; set; }


        [DataMember]
        public string AssignedgtWorkgroupID { get; set; }

        [DataMember]
        public List<KeyValuePair<long, string>> ResourceType { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public DateTime ModifiedDateTime { get; set; }

        [DataMember]
        public string IncludedTerritories { get; set; }

        [DataMember]
        public string ExcludedTerritories { get; set; }
    }
}
