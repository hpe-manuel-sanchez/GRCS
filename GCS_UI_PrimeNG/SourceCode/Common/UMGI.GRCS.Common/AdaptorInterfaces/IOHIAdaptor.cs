/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IOHIAdaptor.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : senthil kumar G
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using UMGI.GRCS.BusinessEntities.Entities.CprsEntities;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    public interface IOHIAdaptor
    {
        CprsReleaseDetail GetCprsPhysicalSchedule(int productId);
        bool PostGrcsNotification(long primaryKey, string xml, string entityName);
        //List<XmlDocument> GetCPRSReleaseScheduleXml(List<int> productIds);
    }
}