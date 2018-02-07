using System.Collections.Generic;
using System.Data;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IGlobalData
    {
        /// <summary>
        /// Gets the label info.
        /// </summary>
        /// <returns></returns>
        LabelInfo GetLabelInfo();

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
        /// <param name="userCountryIds">The user country ids.</param>
        /// <returns></returns>
        List<CountryInfo> GetCountries(CountryInfo countryInfo, List<long> userCountryIds);

        /// <summary>
        /// Gets the countries.
        /// </summary>
        /// <returns></returns>
        Dictionary<long, string> GetCountries();

        /// <summary>
        /// Get the Companies
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <returns></returns>
        DataResponseInfo GetCompanies(DataRequestInfo companyInfo);

        /// <summary>
        /// Get the Divisions based on company id
        /// </summary>
        /// <param name="dataInfo">The data info.</param>
        /// <returns></returns>
        DataResponseInfo GetDivisionList(DataInfo dataInfo);

        /// <summary>
        /// Gets the name of the countries by territory.
        /// </summary>
        /// <param name="territoryName">Name of the territory.</param>
        /// <returns></returns>
        List<string> GetCountriesByTerritoryName(string territoryName);

        /// <summary>
        /// Get Label List based on the Company & Division Id
        /// </summary>
        /// <param name="companyInfo"></param>
        /// <param name="divisionInfo"></param>
        /// <returns></returns>
        DataResponseInfo GetLabelList(DataInfo companyInfo, DataInfo divisionInfo);

        /// <summary>
        /// Gets the clearance company country for user.
        /// </summary>
        /// <param name="companySearchTerm">The company search term.</param>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetCompanyCountries(string companySearchTerm);

        /// <summary>
        /// Gets the clearance admin companies.
        /// </summary>
        /// <returns></returns>
        List<CompanyInfo> GetClearanceAdminCompanies();

        /// <summary>
        /// Gets the umg signing companies.
        /// </summary>
        /// <returns></returns>
        List<UmgSigningCompany> GetUmgSigningCompanies();

        /// <summary>
        /// Gets the territories.
        /// </summary>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritories();

        /// <summary>
        /// Gets the clearance admin companies.
        /// </summary>
        /// <param name="clearanceAdminCompanyFilter">The clearance admin company filter.</param>
        /// <returns></returns>
        List<ClearanceAdminCompany> GetClearanceAdminCompanies(ClearanceAdminCompanyFilter clearanceAdminCompanyFilter);

        List<ClearanceAdminCompany> GetCompaniesForUser(long userId, CompanySearch searchCriteria);

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


        List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string sortValue);

        /// <summary>
        /// To validate the safe territories, this method is being used in CrossBorderSafeTerritoryCheckActivity.cs
        /// Added by Nilim
        /// </summary>
        /// <param name="countryIdList"></param>
        /// <returns></returns>
        List<long> GetSafeCountries(List<long> countryIdList);

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

        /// GetAuditTrailFilters
        /// </summary>
        /// <param name="auditObjectType"></param>
        /// <param name="type"> </param>
        /// <returns></returns>
        List<AuditTrailFilter> GetAuditTrailFilters(AuditObjectType auditObjectType);

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