/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkgroupSearchCriteria.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class WorkgroupSearchCriteria : PagingBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Role { get; set; }

        [DataMember]
        public string Company { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public bool IsActiveOnly { get; set; }

        [DataMember]
        public bool IsRccAdmin { get; set; }

        [DataMember]
        public WorkGroupSearchOption WorkGroupSearchOptions { get; set; }

        [DataMember]
        public string UserLoginName { get; set; }

    }
    [DataContract]
    public enum WorkGroupSearchOption
    {
        [EnumMember]
        NoOPtionWg = 0,
        [EnumMember]
        IncludeInactiveWg = 1,
        [EnumMember]
        IgnoreSelectedUserWg = 2,
        [EnumMember]
        SearchSelectedUserWg = 3
    };
}
