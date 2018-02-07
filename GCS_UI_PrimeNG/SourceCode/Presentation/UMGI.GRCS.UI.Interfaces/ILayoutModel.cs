/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ILayoutModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 28-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 */

namespace UMGI.GRCS.UI.Interfaces
{
    public interface ILayoutModel
    {
        string AppBuildDate { get; set; }
        string AppEnvironment { get; set; }
        string AppVersion { get; set; }
        string UserName { get; set; }

    }
}
