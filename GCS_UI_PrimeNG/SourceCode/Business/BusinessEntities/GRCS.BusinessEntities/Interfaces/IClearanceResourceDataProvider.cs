/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IClearanceResourceDataProvider.cs
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 22-02-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *                
*************************************************************************** 
 * Description  : Interface for Providing Clearance Resource Data
****************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [XmlSerializerFormatAttribute()]
    [ServiceContract]
    public interface IClearanceResourceDataProvider
    {
        [OperationContract]
        List<ClrResource> GetGCSResource(string ResourceId, string GCSProjectID, ResourceSearchOption resourceSearchOption);
    }
}
