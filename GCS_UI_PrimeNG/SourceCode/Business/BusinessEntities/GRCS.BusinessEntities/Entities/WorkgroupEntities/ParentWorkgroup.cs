/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ParentWorkgroup.cs 
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
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class ParentWorkgroup : WorkgroupBase
    {
        [DataMember]
        public string R2Contract { get; set; }

        [DataMember]
        public List<WorkGroupUser> Users { get; set; }

        [DataMember]
        public List<CompanyInfo> Companies { get; set; }

        //To be removed
        [DataMember]
        public List<TerritoryInfo> Countries { get; set; }

        [DataMember]
        public List<TerritorialDisplay> Territories { get; set; }

        [DataMember]
        public List<KeyValuePair<long, string>> ChildWorkgroups { get; set; }

        [DataMember]
        public DateTime ModifiedDateTime { get; set; }

    }
}
