/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AuditTrailFilter.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : GRCS Team
 * Created on   : 19-April-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
                                   
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Audit Trail Filter
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Audit Trail Filter
    /// </summary>
    [DataContract]
    public class AuditTrailFilter
    {
        /// <summary>
        /// Audit Configuration Id
        /// </summary>
        [DataMember]
        public long AuditConfigId { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Parent Audit Config Id
        /// </summary>
        [DataMember]
        public long? ParentAuditConfigId { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        [DataMember]
        public long Level { get; set; }
    }
}