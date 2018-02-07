/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IGlobalRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 27-10-2012 
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


using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using System.Data;

namespace UMGI.GRCS.UI.Interfaces
{
    public partial interface IGlobalRepository
    {
        List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm);

        List<TerritorialDisplay> GetTerritories();

    }

    /// <summary>
    /// GCS
    /// </summary>
    public partial interface IGlobalRepository
    {
        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string jtSorting, string userLoginName);

        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <returns></returns>
        List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType);

        /// <summary>
        /// GetWGAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <returns></returns>
        List<AuditTrailFilter> GetWGAuditTrailFilters(AuditObjectType auditObjectType, string selectedWorkgroupRole, bool isParent);

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <returns></returns>
        DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId);
    }
}
