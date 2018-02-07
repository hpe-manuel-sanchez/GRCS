/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : IContractDataManager.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
 * Rubini Raja       16/07/2012      Methods for secondary exploitations
 *                                   updated
 * Rubini Raja       18/07/2012      Interface methods added for
 *                                   GetContracts and GetContract
 * Siva              07/08/2012      Reorganizing the code
 * Siva              08/08/2012      Changeset id is 1525
 * Vijayakumar.R     08/08/2012      Delete 3 Interface GetContracts methods
 *                                   For Code Redundancy
 *
 * Vijayakumar.R     08/08/2012      Changeset id is 1545
 * Vaishnavi.L       22/08/2012      Commented unused methods
 * Siva              17/09/2012      Removed unused mocking methods
 * Pavan Kumar       30/10/2012      Code Refactored
***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

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
    /// <summary>
    /// This partial interface comprises of only implemented methods as on Sprint 2 development
    /// </summary>
    public partial interface IContractData
    {
        #region Contracts Info

        List<ContractInfo> GetContracts(ContractInfo contractSearch, Dictionary<GrsTasks, List<long>> userRoleCompanyIds);

        List<ContractInfo> GetContracts(BusinessEntities.Requests.ContractSearch contractSearch);

        List<ContractInfo> GetParentContracts(ContractInfo contractSearch, List<long> userCompanyIds);

        ContractDetails LoadContract(long contractId, string userRoleCompanyIds);

        List<TemplateDetails> GetContractTemplates(string userName, long userId, GrsTasks tasks);

        ContractDetails LoadContractTemplate(long templateId, string userName);

        ContractInfo SaveContract(ContractDetails contractDetails);

        void IsTemplateNameExists(string templateName, long clearanceAdminCompanyId);

        string SaveContractTemplate(ContractTemplate contractDetails);

        void DeleteContract(long contractId, string userName, DateTime requestDateTime);

        #endregion Contracts Info

        #region Rights and Restrictions

        DigitalRestrictionTemplateResults SaveDigitalRestrictionTemplate
             (DigitalRestrictionTemplate digitalRestrictionTemplateDetails);

        bool IsDigitalTemplateNameAlreadyExists(string templateName, long clearanceAdminCompanyId);

        #endregion Rights and Restrictions

        #region Territory

        List<TerritoryDataInfo> GetTerritoryDataInfo(long templateId);

        #endregion Territory

        #region Template Maintenance

        ContractTemplatesResult GetContractTemplates(FilterFields filterFields, GrsTasks tasks);

        List<TemplateDetails> GetDigitalTemplates(string userName, long userId, GrsTasks tasks);

        string DeleteTemplates(List<TemplateDetails> templateDetail, string userName);

        DigitalRestrictionTemplate GetDigitalRestrictionTemplate(long digitalRestrictionTemplateId, string userName);

        #endregion Template Maintenance

        #region Split Deal

        List<ContractInfo> GetSplitDealContracts(ContractInfo contractInfo);

        #endregion Split Deal

        #region Release

        List<long> IsAlreadyLinkedToContract(List<long> ids, long contractId, string repertoireType);

        WorkQueueCriteria GetContractDetails(long contractId);

        long SaveReleaseContractRight(long releaseId, long rightSetId, long contractId, string userName);

        long SaveResourceContractRight(long resourceId, long rightSetId, long contractId, string userName);

        List<long> GetSplitDealContractId(long contractId);

        #endregion Release

        #region Unlink

        List<LinkedProjectInfo> GetLinkedProjectDetails(long contractId, FilterFields filterCriteria);

        List<LinkedReleaseInfo> GetLinkedReleaseDetails(long contractId, FilterFields filterCriteria);

        List<LinkedResourceInfo> GetLinkedResourceDetails(long contractId, FilterFields filterCriteria);

        List<long> GetProjectReleaseId(long projectId);

        List<long> GetProjectResourceId(long projectId);

        bool UnlinkProjectContract(long projectId, long contractId, string userName);

        #endregion Unlink

        #region UC 014

        /// <summary>
        /// Method that gets the email group information
        /// </summary>
        /// <param name="emailGroupDetails">Email group id and name
        /// for <see cref="GetEmailGroupInfo"/>
        /// </param>
        /// <returns>email ids of group details in comma seperated
        ///     fromat</returns>
        string
            GetEmailGroupInfo(string emailGroupDetails);

        #endregion UC 014

        #region Company Division Label

        long? GetContractId(long companyId, long divisionId, long labelId);

        bool IsMediaPortalCdl(long companyId, long divisionId, long labelId);

        bool IsPcCompanyMismatch(long pcCompanyId, long? contractId);

        bool IsRightsTypeMismatch(long isRightsTypeSetId, long? contractId);

        long GetContractrRightSetId(long? rightscontractId);

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
        /// Updates the auto link CDL contract.
        /// </summary>
        /// <param name="autoLinkCdlContract">The auto link CDL contract.</param>
        /// <returns></returns>
        bool UpdateAutoLinkCdlContract(ContractMapping autoLinkCdlContract);

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

        bool IsCdlCombinationExists(long companyId, long divisionId, long labelId, long contractId, out long lookupAutolinkId);

        #endregion AutoLink CDL-Contract

        #region Sprint5

        List<ContractInfo> GetChildContracts(BusinessEntities.Requests.ChildContractsRequest childContractsRequest);

        #endregion Sprint5

        #region Scheduler

        void GetContractBatchProcess();

        #endregion Scheduler

        // Lookups
        List<StringIdentifier> GetContractStatuses();

        List<StringIdentifier> GetWorkflowStatuses();

        List<StringIdentifier> GetContractDescriptions();

        LinkedInfo GetContractInfo(long contractId);
    }

    /// <summary>
    /// This partial interface comprises of only unimplemented methods as on Sprint 2 development
    /// </summary>
    public partial interface IContractData
    {
        /// <summary>
        /// Search criteria, Gets the user groups along with email Ids.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        /// <param name="userCountryIds">The user country ids.</param>
        /// <returns></returns>
        List<EmailGroupDetails> GetEmailGroups(EmailGroupDetails emailGroupDetails, List<long> userCountryIds);

        /// <summary>
        /// Updates the email group.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        void UpdateEmailGroup(EmailGroupDetails emailGroupDetails);

        /// <summary>
        /// Adds the email group.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        void AddEmailGroup(EmailGroupDetails emailGroupDetails);

        /// <summary>
        /// Determines whether the email group already exists for the specified country id.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <param name="emailGroupName">Name of the email group.</param>
        /// <param name="emailGroupId">The email group id.</param>
        /// <returns>
        /// 	<c>true</c> if [is email group already exists] [the specified country id]; otherwise, <c>false</c>.
        /// </returns>
        bool IsExistingEmailGroup(long countryId, string emailGroupName, out long emailGroupId);

        /// <summary>
        /// Deletes the email groups.
        /// </summary>
        /// <param name="emailGroupIds">The email group ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="modifiedDateTime">The modified date time.</param>
        void DeleteEmailGroups(List<long> emailGroupIds, string userName, DateTime modifiedDateTime);

        void SaveEmailNotification(Email emailContent, ContractDetails contractDetails, string reasonTypedesc);

        DateTime? GetLastWorkflowDate(long contractId);

        long IsAlreadyUnlinkFromWorkQueue(long contractId, long repertoireId, string repertoireType);

        RepertoireFilterResults IsAlreadyUnlinked(long contractId, List<long> projectIdList, List<long> releaseIdList, List<long> resourceIdList);

        void IsExistingContracts(List<long> ids);

        void IsExistingContract(long contractId, string request = null);

        void GetContractRights(long rightSetId, ContractDetails contractDetails);

        bool IsMac(string artistName);
    }
}