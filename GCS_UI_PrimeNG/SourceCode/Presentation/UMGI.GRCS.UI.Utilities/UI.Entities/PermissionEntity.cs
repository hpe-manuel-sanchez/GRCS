/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : PermissionEntity.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 25-08-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Entity class for permissions
    /// </summary>
    public class PermissionEntity
    {
        public long TaskId { get; set; }

        public string TaskName { get; set; }

        public string Control { get; set; }

        public string Visibility { get; set; }
    }
}
