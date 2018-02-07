/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IANAService.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 13 July 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************/

using System.ServiceModel;

namespace UMGI.GRCS.BusinessEntities.Interfaces.SimulatorInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IANAService
    {
        [OperationContract]
        Authorizations GetAuthorizationsByLoginNameAndApplication(string loginName, string applicationName);
    }
}