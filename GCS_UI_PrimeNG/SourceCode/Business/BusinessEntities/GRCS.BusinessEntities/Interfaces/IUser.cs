/* *************************************************************************** 
 * Copyrights ® 2012 UMG 
 * *************************************************************************** 
 * FileName     : IUser
 * Project Code :  
 * Author       : Pavan Kumar K
 * Created on   : 13/07/2012
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 *Description :  Interface methods for User Service
                  
****************************************************************************/

using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public partial interface IUser
    {
        /// <summary>
        /// Authorizes the user by Login and responds back with respective responsibility/ Access to application.
        /// </summary>
        [OperationContract]
        GrsAuthentication GetAuthorizationByLoginNameAndApplication(string userLoginName, AnaTargetApplication applicationName);
    }
}