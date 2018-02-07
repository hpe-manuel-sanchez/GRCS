using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IContractManager
    {
        #region Secondary Exploitation Restrictions

        SecondaryExploitationDefaults GetDefaultDataSecondaryExploitations();

        #endregion Secondary Exploitation Restrictions

        #region Territory And Country

        List<TerritorialDisplay> GetTerritoryData(TemplateDetails templateInfo);

        List<TerritorialDisplay> GetContractTerritoryDetails(long contractId);

        #endregion Territory And Country

        #region Contract

        ContractInfo SaveContract(ContractDetails contractDetails);

        // bool SaveContractTemplate(ContractTemplate contractDetails);
        string SaveContractTemplate(ContractTemplate contractDetails);

        /// <summary>
        /// Gets the contracts.
        /// </summary>
        /// <param name="contractSearch">The contract search.</param>
        /// <returns></returns>
        List<ContractInfo> GetContracts(ContractInfo contractSearch);

        List<ContractInfo> GetContractMaintenanceWorkQueue(ContractInfo contractInfo);

        /// <summary>
        /// Gets the parent contracts.
        /// </summary>
        /// <param name="contractSearch">The contract search.</param>
        /// <returns></returns>
        List<ContractInfo> GetParentContracts(ContractInfo contractSearch);

        ContractDetails GetContract(long contractId, string userName, GrsTasks tasks);

        List<TemplateDetails> GetContractTemplates(string userName);

        /// <summary>
        /// Gets the contract template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        ContractDetails GetContractTemplate(long templateId, string userName);

        void DeleteContract(long contractId, string userName, DateTime requestDateTime);

        #endregion Contract

        #region Contract Info

        MasterData GetMasterDataForContract(string userName);

        SearchContractMasterData GetSearchContractMasterData();

        //NoticeCompanySearchResult GetPcNoticeCompany(NoticeCompanySearch searchOption);

        #endregion Contract Info

        #region Rights and Restrictions

        RightsAndRestrictions
            GetDefaultDataRightsAndRestriction();

        DigitalRestrictions GetDefaultDataDigitalRestriction();

        DigitalRestrictionTemplateResults SaveDigitalRestrictionTemplate(DigitalRestrictionTemplate digitalRestrictionTemplateDetails);

        #endregion Rights and Restrictions

        #region Template Maintenance

        ContractTemplatesResult LoadContractTemplates(FilterFields filterFields);

        List<TemplateDetails> GetDigitalTemplates(string userName);

        string DeleteTemplates(List<TemplateDetails> templateDetail, string userName);

        DigitalRestrictionTemplate GetDigitalRestrictionTemplate(long digitalRestrictionTemplateId, string userName);

        #endregion Template Maintenance

        #region Split Deal

        List<ContractInfo> GetSplitDealContracts(ContractInfo contractInfo);

        #endregion Split Deal

        #region Project

        void LinkProjectToContract(ProjectInfo linkProjectInfo, long contractId);

        #endregion Project

        #region Release

        RepertoireFilterResults IsAlreadyUnlinkFromWorkQueue(List<WorkQueue> workQueueItems);

        RepertoireFilterResults IsAlreadyUnlinked(long contractId, List<long> projectIdList, List<long> releaseIdList, List<long> resourceIdList);

        void IsExistingContract(long contractId, string request = null);

        List<long> IsAlreadyLinkedContract(List<long> ids, long contractId, string repertoireType);

        WorkQueueCriteria GetContractDetails(long contractId);

        long LinkReleaseContractRight(long releaseId, long rightSetId, long contractId, string userName, long r2ReleaseId);

        long SaveResourceContractRight(long resourceId, long rightSetId, long contractId, string userName);

        long LinkResourceContractRight(long resourceId, long rightSetId, long contractId, string userName, long r2ResourceId);

        List<long> GetSplitDealContractId(long contractId);

        List<LinkedProjectInfo> GetLinkedProjectDetails(long contractId, FilterFields filterCriteria);

        List<LinkedReleaseInfo> GetLinkedReleaseDetails(long contractId, FilterFields filterCriteria);

        List<LinkedResourceInfo> GetLinkedResourceDetails(long contractId, FilterFields filterCriteria);

        #endregion Release

        #region UnlinkRepertoire

        void UnlinkRepertoire(long contractId, List<long> projectIdList, List<long> releaseIdList,
                              List<long> resourceIdList, string userName);

        void UnlinkRepertoireFromWorkQueue(List<WorkQueue> workQueueItems, string userName);

        #endregion UnlinkRepertoire

        #region Company Division Label

        long? GetContractId(long companyId, long divisionId, long labelId);

        bool IsMediaPortalCdl(long companyId, long divisionId, long labelId);

        bool IsMac(string artistName);

        bool IsPcCompanyMismatch(long pcCompanyId, long? contractId);

        bool IsRightsTypeMismatch(long isRightsTypeSetId, long? contractId);

        long GetContractRightSetId(long? rightscontractId);

        bool IsNonExecMismatch(long isNonexecId, long? contractId);

        #endregion Company Division Label

        #region AutoLink CDL-Contract

        /// <summary>
        /// Save CDL-Contract mapping
        /// </summary>
        /// <param name="autoLinkCdlContract">The auto link CDL contract.</param>
        /// <returns></returns>
        bool SaveAutoLinkCdlContract(ContractMapping autoLinkCdlContract);

        /// <summary>
        /// Deletes the CDL contracts.
        /// </summary>
        /// <param name="dictIdTime">The auto link ids & dates deletion list.</param>
        /// <param name="userName">UserName</param>
        void DeleteCdlContracts(Dictionary<long, DateTime> dictIdTime, string userName);

        /// <summary>
        /// GetAutoMappedCompanies method will fetch the matched companies from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        DataResponseInfo GetLinkedCompanies(string companyName);

        /// <summary>
        /// GetLinkedDivisions method will fetch the matched divisions from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="divisionName">Name of the division.</param>
        /// <returns></returns>
        DataResponseInfo GetLinkedDivisions(string divisionName);

        /// <summary>
        /// GetLinkedLabels method will fetch the matched labels from the Cdl-Contract mapping table
        /// </summary>
        /// <param name="labelName">Name of the division.</param>
        /// <returns></returns>
        DataResponseInfo GetLinkedLabels(string labelName);

        /// <summary>
        /// FilerCdlMappings method will fetch the CDL-Contract mapping details
        /// </summary>
        /// <param name="mappingFilterCriteria">The mapping filter criteria.</param>
        /// <returns></returns>
        MappingFilterResults SearchCdlMappings(MappingFilterCriteria mappingFilterCriteria);

        #endregion AutoLink CDL-Contract

        List<ContractInfo> GetContractsForSplitDeal(long contractId, ContractInfo contractFilter);

        List<ContractInfo> GetChildContracts(BusinessEntities.Requests.ChildContractsRequest childContractsRequest);

        List<StringIdentifier> GetContractStatuses();

        List<StringIdentifier> GetWorkflowStatuses();

        List<StringIdentifier> GetContractDescriptions();

        LinkedInfo GetContractInfo(long contractId);

        void GetContractBatchProcess();

        List<long> GetLinkedRightSets(List<ChangeLinkInfo> changeLinkInfos);

        /// <summary>
        /// Saves the email group.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        void SaveEmailGroup(EmailGroupDetails emailGroupDetails);

        /// <summary>
        /// Deletes the email groups.
        /// </summary>
        /// <param name="emailGroupIds">The email group ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="modifiedDateTime">The modified date time.</param>
        void DeleteEmailGroups(List<long> emailGroupIds, string userName, DateTime modifiedDateTime);

        /// <summary>
        /// Gets the EmailGroup Information
        /// </summary>
        /// <param name="emailGroupDetails"></param>
        /// <returns></returns>
        string GetEmailGroupInfo(string emailGroupDetails);

        /// <summary>
        /// Method to get email groups
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        /// <returns></returns>
        List<EmailGroupDetails> GetEmailGroupInfo(EmailGroupDetails emailGroupDetails);

        void UnlinkRepertoireFromReleaseResource(List<ChangeLinkInfo> workQueueItems, string userName);
    }
}