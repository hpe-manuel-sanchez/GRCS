/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IUserRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-10-2012 
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
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public partial interface IUserRepository
    {
        GrsAuthentication GetAuthorizationByLoginNameAndApplication(UserInfo userInfo);
    }
    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IUserRepository
    {
        GcsAuthentication GetGcsAuthorizationByLoginNameAndApplication(UserInfo userInfo);
    }
}
