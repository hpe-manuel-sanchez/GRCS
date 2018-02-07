/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IAnaAdapter.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rubini Raja       18/07/2012      Interface methods added for
 *                                   GetContracts and GetContract   
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    public interface IAnaAdapter
    {
        GrsAuthentication GetTasksAndRoles(string userName, AnaTargetApplication application);

        GrsAuthentication GetTasksAndRolesWithHashCode(string windowsUserName, AnaTargetApplication targetApplication,
                                                       string hashCode);

        GcsAuthentication GetGcsTasksAndRoles(string windowsUserName, AnaTargetApplication targetApplication);

        AnaApplicationUsers GetAllUsersOfApplication(AnaTargetApplication targetApplication,string hashCode);

        List<R2Authentication> GetAuthorizationsForR2
            (string windowsUserName, Dictionary<AnaConfigurationType, List<string>> permission,
             AnaTargetApplication targetApplication, string hashCode);

        List<ApplicationUser> GetUsers(string username, string loginName, string countryName,
                                       AnaTargetApplication targetApplication, bool includeInactiveUsers);
    }
}