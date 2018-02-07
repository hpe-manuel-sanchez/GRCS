using System.Collections.Generic;
using System.Data;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    /// <summary>
    /// IGlobalManager
    /// </summary>
    public partial interface IGlobalManager
    {
        /// <summary>
        /// Gets the name of the countries by territory.
        /// </summary>
        /// <param name="territoryName">Name of the territory.</param>
        /// <returns></returns>
        List<string> GetCountriesByTerritoryName(string territoryName);

        /// <summary>
        /// Gets the label info.
        /// </summary>
        /// <param name="searchTerm"> </param>
        /// <returns></returns>
        LabelInfo GetLabelInfo(string searchTerm);

        /// <summary>
        /// Gets the companies for user.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetCompaniesForUser(CompanySearch searchCriteria);

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        List<CountryIdentifier> GetCountries(string searchTerm);

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryInfo">The country info.</param>
        /// <param name="tasks">The tasks.</param>
        /// <returns></returns>
        List<CountryInfo> GetCountries(CountryInfo countryInfo, GrsTasks tasks);

        /// <summary>
        /// Get the Companies
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <returns></returns>
        DataResponseInfo GetCompanies(DataRequestInfo companyInfo);

        /// <summary>
        /// Gets the Divisions based on company id.
        /// </summary>
        /// <param name="dataInfo">Company Info.</param>
        /// <returns></returns>
        DataResponseInfo GetDivisionList(DataInfo dataInfo);

        /// <summary>
        /// Get Label List based on the Company & Division Id
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <param name="divisionInfo"></param>
        /// <returns></returns>
        DataResponseInfo GetLabelList(DataInfo companyInfo, DataInfo divisionInfo);

        /// <summary>
        /// Gets all the Clearance Admin Companies
        /// </summary>
        /// <returns></returns>
        //List<ClearanceAdminCompany> GetClearanceAdminCompanies();

        //List<ClearanceAdminCompany> GetClearanceAdminCompanies(ClearanceAdminCompanyFilter filterCriteria);

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <param name="countryInfo">The company info.</param>
        /// <returns></returns>
        DataResponseInfo GetCountries(DataRequestInfo countryInfo);

        /// <summary>
        /// Gets the territories.
        /// </summary>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritories();

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <returns></returns>
        Dictionary<long, string> GetCountries();

        /// <summary>
        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType, int type = 0);

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <param name="cdlType"> </param>
        /// <returns></returns>
        DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId, int cdlType);

        /// <summary>
        /// Get compny details for reports
        /// </summary>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetClearanceAdminCompaniesForNames(CompanySearch searchCriteria);

        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string sortValue);

        /// <summary>
        /// To validate the safe territories, this method is being used in CrossBorderSafeTerritoryCheckActivity.cs
        /// Added by Nilim
        /// </summary>
        /// <param name="countryIdList"></param>
        /// <returns></returns>
        List<long> GetSafeCountries(List<long> countryIdList);

        /// <summary>
        /// Get company info along with global flag information
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<long, bool>> GetCompanyInfoWithGlobalFlag();

        /// <summary>
        /// Get Companies
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        ///
        List<string> GetCompaniesForAutoComplete(string company);

        /// <summary>
        /// Get Country
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        ///
        List<string> GetCountriesForAutoComplete(string country);

        /// <summary>
        /// GetAuditTrail
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="selectedAuditConfiguration"></param>
        /// <param name="selectedItemId"></param>
        /// <param name="cdlType"> </param>
        /// <returns></returns>
        DataSet GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId);
    }
}