/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IGlobal.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay Venkatesh Prasad.N
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      Interface Methods For Global Service

****************************************************************************/

using System.Collections.Generic;
using System.Data;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    /// <summary>
    /// IGlobal
    /// </summary>
    [ServiceContract]
    public partial interface IGlobal
    {
        /// <summary>
        /// Gets the label info.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        [OperationContract]
        LabelInfo GetLabelInfo(string searchTerm);


        /// <summary>
        /// Gets the companies for user.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [OperationContract]
        List<ClearanceAdminCompany> GetCompaniesForUser(CompanySearch searchCriteria);

    

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        [OperationContract]
        List<CountryIdentifier> GetCountriesList(string searchTerm);

        /// <summary>
        /// Get the Companies
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetCompanies(DataRequestInfo companyInfo);

        /// <summary>
        /// Gets the divisions based on the company id.
        /// </summary>
        /// <param name="dataInfo">company info</param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetDivisionList(DataInfo dataInfo);

        /// <summary>
        /// Get Label List based on the Company & Division Id
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <param name="divisionInfo"></param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetLabelList(DataInfo companyInfo, DataInfo divisionInfo);

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryInfo">The Country info.</param>
        /// <returns></returns>
        [OperationContract]
        DataResponseInfo GetCountries(DataRequestInfo countryInfo);
        
        /// <summary>
        /// Gets the territories.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TerritorialDisplay> GetTerritories();

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryInfo">The country info.</param>
        /// <param name="tasks">The tasks.</param>
        /// <returns></returns>
        [OperationContract(Name = "SearchUserCountries")]
        List<CountryInfo> GetCountries(CountryInfo countryInfo, GrsTasks tasks);

        [OperationContract]
        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string sortValue, string userLoginName);
    }

    /// <summary>
    /// Audit Trail
    /// </summary>
    public partial interface IGlobal
    {
        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
         [OperationContract(Name = "GetAuditTrailFilters")]
        List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType, int type);

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <param name="cdlType"> </param>
        /// <returns></returns>
        [OperationContract(Name = "GetAuditTrail")]
        DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId, int cdlType);
    }

    /// <summary>
    /// Audit Trail for GCS
    /// </summary>
    public partial interface IGlobal
    {
        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGCSAuditTrailFilters")]
        List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType);

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGCSAuditTrail")]
        DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId);
    }

    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IGlobal
    {
        /// <summary>
        /// Get Companies
        /// </summary>
        /// <param name="company"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetCompaniesForAutoComplete(string company, string userLoginName);

        /// <summary>
        /// Get Country
        /// </summary>
        /// <param name="country"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetCountriesForAutoComplete(string country, string userLoginName);
    }
}
