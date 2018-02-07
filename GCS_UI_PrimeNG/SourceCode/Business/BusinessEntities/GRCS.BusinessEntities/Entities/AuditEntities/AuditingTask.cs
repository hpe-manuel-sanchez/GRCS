/* ************************************************************************ 
 * Copyrights ® 2013 UMGI 
 * ************************************************************************ 
 * File Name    : AuditingTask.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Anjali
 * Created on   : 12-03-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Sudhakar Paulraj  12-03-2013      Initial version
*************************************************************************** 
 * Reviewed by      Modified on     Remarks 

****************************************************************************
 * Description     Contains audit trail enumerations
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities
{
    /// <summary>
    /// Enum holds the type of auditing to be done.
    /// </summary>
    public enum AuditingTask
    {
        /// <summary>
        /// Company, Division and Label Mapping
        /// </summary>
        CDLMapping,

        /// <summary>
        /// Contract Mapping
        /// </summary>
        Contract,

        /// <summary>
        /// Contract Mapping
        /// </summary>
        ResourceTrackAuditMapping,

        /// <summary>
        /// Contract Mapping
        /// </summary>
        ReleaseAuditMapping,

        WorkgroupMapping,
        SafeTerritoriesMapping,
        RoutingVariationsMapping,
        MasterProject_ProjectMapping,
        MasterProject_ResourceMapping,
        MasterProject_ResourceFreeHandMapping,
        RegularProject_ProjectMapping,
        RegularProject_RequestTypeMapping,
        RegularProject_ReleaseNewMapping,
        RegularProject_ReleaseExistsMapping,
        RegularProject_ResourceMapping,
        RegularProject_ResourceFreeHandMapping
    }

}
