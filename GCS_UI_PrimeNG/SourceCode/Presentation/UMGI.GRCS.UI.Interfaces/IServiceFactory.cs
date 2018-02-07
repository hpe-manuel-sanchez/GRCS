/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IServiceFactory.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 23-08-2012 
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
    public interface IServiceFactory
    {
        T GetService<T>(string serviceName) where T : class;
    }
}
