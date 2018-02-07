﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMGI.GRCS.UI.Proxies.GlobalService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GlobalService.IGlobal")]
    public interface IGlobal {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetLabelInfo", ReplyAction="http://tempuri.org/IGlobal/GetLabelInfoResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.LabelInfo GetLabelInfo(string searchTerm);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCompaniesForUser", ReplyAction="http://tempuri.org/IGlobal/GetCompaniesForUserResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UMGI.GRCS.BusinessEntities.Entities.ContractEntities.FilterFields))]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.ClearanceAdminCompany> GetCompaniesForUser(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.CompanySearch searchCriteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCountriesList", ReplyAction="http://tempuri.org/IGlobal/GetCountriesListResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.CountryIdentifier> GetCountriesList(string searchTerm);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCompanies", ReplyAction="http://tempuri.org/IGlobal/GetCompaniesResponse")]
        UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetCompanies(UMGI.GRCS.BusinessEntities.Requests.DataRequestInfo companyInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetDivisionList", ReplyAction="http://tempuri.org/IGlobal/GetDivisionListResponse")]
        UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetDivisionList(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo dataInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetLabelList", ReplyAction="http://tempuri.org/IGlobal/GetLabelListResponse")]
        UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetLabelList(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo companyInfo, UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo divisionInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCountries", ReplyAction="http://tempuri.org/IGlobal/GetCountriesResponse")]
        UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetCountries(UMGI.GRCS.BusinessEntities.Requests.DataRequestInfo countryInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetTerritories", ReplyAction="http://tempuri.org/IGlobal/GetTerritoriesResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.TerritorialDisplay> GetTerritories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/SearchUserCountries", ReplyAction="http://tempuri.org/IGlobal/SearchUserCountriesResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo> SearchUserCountries(UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo countryInfo, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GrsTasks tasks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/CompanySearch", ReplyAction="http://tempuri.org/IGlobal/CompanySearchResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string sortValue, string userLoginName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetAuditTrailFilters", ReplyAction="http://tempuri.org/IGlobal/GetAuditTrailFiltersResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.AuditTrailFilter> GetAuditTrailFilters(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, int type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetAuditTrail", ReplyAction="http://tempuri.org/IGlobal/GetAuditTrailResponse")]
        System.Data.DataSet GetAuditTrail(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, System.Collections.Generic.List<long> selectedAuditConfiguration, System.Collections.Generic.List<long> selectedItemId, int cdlType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetGCSAuditTrailFilters", ReplyAction="http://tempuri.org/IGlobal/GetGCSAuditTrailFiltersResponse")]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.AuditTrailFilter> GetGCSAuditTrailFilters(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetGCSAuditTrail", ReplyAction="http://tempuri.org/IGlobal/GetGCSAuditTrailResponse")]
        System.Data.DataSet GetGCSAuditTrail(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, System.Collections.Generic.List<long> selectedAuditConfiguration, System.Collections.Generic.List<long> selectedItemId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCompaniesForAutoComplete", ReplyAction="http://tempuri.org/IGlobal/GetCompaniesForAutoCompleteResponse")]
        System.Collections.Generic.List<string> GetCompaniesForAutoComplete(string company, string userLoginName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGlobal/GetCountriesForAutoComplete", ReplyAction="http://tempuri.org/IGlobal/GetCountriesForAutoCompleteResponse")]
        System.Collections.Generic.List<string> GetCountriesForAutoComplete(string country, string userLoginName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGlobalChannel : UMGI.GRCS.UI.Proxies.GlobalService.IGlobal, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GlobalClient : System.ServiceModel.ClientBase<UMGI.GRCS.UI.Proxies.GlobalService.IGlobal>, UMGI.GRCS.UI.Proxies.GlobalService.IGlobal {
        
        public GlobalClient() {
        }
        
        public GlobalClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GlobalClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GlobalClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GlobalClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.LabelInfo GetLabelInfo(string searchTerm) {
            return base.Channel.GetLabelInfo(searchTerm);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.ClearanceAdminCompany> GetCompaniesForUser(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.CompanySearch searchCriteria) {
            return base.Channel.GetCompaniesForUser(searchCriteria);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.CountryIdentifier> GetCountriesList(string searchTerm) {
            return base.Channel.GetCountriesList(searchTerm);
        }
        
        public UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetCompanies(UMGI.GRCS.BusinessEntities.Requests.DataRequestInfo companyInfo) {
            return base.Channel.GetCompanies(companyInfo);
        }
        
        public UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetDivisionList(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo dataInfo) {
            return base.Channel.GetDivisionList(dataInfo);
        }
        
        public UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetLabelList(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo companyInfo, UMGI.GRCS.BusinessEntities.Entities.BaseEntities.DataInfo divisionInfo) {
            return base.Channel.GetLabelList(companyInfo, divisionInfo);
        }
        
        public UMGI.GRCS.BusinessEntities.Responses.DataResponseInfo GetCountries(UMGI.GRCS.BusinessEntities.Requests.DataRequestInfo countryInfo) {
            return base.Channel.GetCountries(countryInfo);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.TerritorialDisplay> GetTerritories() {
            return base.Channel.GetTerritories();
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo> SearchUserCountries(UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo countryInfo, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GrsTasks tasks) {
            return base.Channel.SearchUserCountries(countryInfo, tasks);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string sortValue, string userLoginName) {
            return base.Channel.CompanySearch(companyName, isacCode, country, startIndex, pageSize, sortValue, userLoginName);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.AuditTrailFilter> GetAuditTrailFilters(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, int type) {
            return base.Channel.GetAuditTrailFilters(auditObjectType, type);
        }
        
        public System.Data.DataSet GetAuditTrail(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, System.Collections.Generic.List<long> selectedAuditConfiguration, System.Collections.Generic.List<long> selectedItemId, int cdlType) {
            return base.Channel.GetAuditTrail(auditObjectType, selectedAuditConfiguration, selectedItemId, cdlType);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.BaseEntities.AuditTrailFilter> GetGCSAuditTrailFilters(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType) {
            return base.Channel.GetGCSAuditTrailFilters(auditObjectType);
        }
        
        public System.Data.DataSet GetGCSAuditTrail(UMGI.GRCS.BusinessEntities.Lookups.AuditObjectType auditObjectType, System.Collections.Generic.List<long> selectedAuditConfiguration, System.Collections.Generic.List<long> selectedItemId) {
            return base.Channel.GetGCSAuditTrail(auditObjectType, selectedAuditConfiguration, selectedItemId);
        }
        
        public System.Collections.Generic.List<string> GetCompaniesForAutoComplete(string company, string userLoginName) {
            return base.Channel.GetCompaniesForAutoComplete(company, userLoginName);
        }
        
        public System.Collections.Generic.List<string> GetCountriesForAutoComplete(string country, string userLoginName) {
            return base.Channel.GetCountriesForAutoComplete(country, userLoginName);
        }
    }
}