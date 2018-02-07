/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IClearanceProjectDataProvider.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 11-02-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *                
*************************************************************************** 
 * Description  : Interface for Providing Clearance Project Data
****************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [XmlSerializerFormatAttribute()]
    [ServiceContract]
    public interface IClearanceProjectDataProvider
    {
        [OperationContract]
        ClrProject GetGCSProject(string gcsProjectId);

        [OperationContract]
        List<ClrRelease> GetGCSRelease(string gcsProjectId);

        [OperationContract]
        List<ClrResource> GetGCSResource(string gcsProjectId);

        [OperationContract]
        bool ClearanceNotify(long clearanceId, NotificationType type);
    }
}
