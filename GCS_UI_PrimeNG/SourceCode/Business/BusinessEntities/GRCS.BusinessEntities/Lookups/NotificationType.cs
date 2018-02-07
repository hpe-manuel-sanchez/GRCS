/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NotificationType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
                                 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
****************************************************************************/
using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Lookups
{
    public enum NotificationType
    {    
        /// <summary>
        /// Declare Enum for Work flow change
        /// </summary>
        [Description("Work flow change Notifications")]
        WorkFlow = 0,

        /// <summary>
        /// Declare Enum for Rights Expiry Notifications
        /// </summary>
        [Description("Rights Expiry Notifications")]
        RightsExpiry = 1,

        /// <summary>
        /// Declare Enum for End Of Term Notifications
        /// </summary>
        [Description("Rights Period")]
        EndOfTerm = 2,

        /// <summary>
        /// ProjectRepertoire Notifications
        /// </summary>
        [Description("ProjectRepertoire Notifications")]
        ProjectRepertoire = 3,
        
        /// <summary>
        /// Repertoire Notifications
        /// </summary>
        [Description("ReleaseFailureRepertoire Notifications")]
        ReleaseFailureRepertoire = 4,
        /// <summary>
        /// Repertoire Notifications
        /// </summary>
        [Description("ReleaseRepertoire Notifications")]
        ReleaseRepertoire = 5,

        /// <summary>
        /// Repertoire Notifications
        /// </summary>
        [Description("Lost Rights Date")]
        LostRightsDate = 6,

        /// <summary>
        /// Report Notifications
        /// </summary>
        [Description("Report Notifications")]
        Report = 7,

        /// <summary>
        /// Repertoire Notifications
        /// </summary>
        [Description("ResourceFailureRepertoire Notifications")]
        ResourceFailureRepertoire = 8,

    }
}
