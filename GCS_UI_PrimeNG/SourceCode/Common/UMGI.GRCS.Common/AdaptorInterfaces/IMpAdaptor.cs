/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IR2Adapter.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Guna
 * Created on   : 23-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *    
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities;
using WSMediaPortalProxy;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    public interface IMpAdaptor
    {
        MPReleaseResourceAssociaton GetReleasesAndResources(string accountNumber, int companyId, int divisionId,
                                                            int labelId,
                                                            string upc);

        ReleaseRights GetReleaseRightsNotifications(DateTime from, DateTime toDate);

        
        MPReleaseRights GetResourceRights(string accountNumber, int companyId, int divisionId, int labelId, string upc,
                                          params string[] isrcCodes);

        MPReleaseRights GetReleaseRights(string accountNumber, int companyId, int divisionId, int labelId, string upc);

      

        MPResourceRights GetResourceRights(string accountNumber, int companyId, int divisionId, int labelId,
                                           string upc, string isrc);
    }
}