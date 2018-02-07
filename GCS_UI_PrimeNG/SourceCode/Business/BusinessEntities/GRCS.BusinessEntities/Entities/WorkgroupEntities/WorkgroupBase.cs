/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkgroupBase.cs 
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
    public class WorkgroupBase : PagingBase
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public long ParentId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string StatusType { get; set; }

        [DataMember]
        public bool Selected { get; set; }

        [DataMember]
        public int Order { get; set; }
    }
}
