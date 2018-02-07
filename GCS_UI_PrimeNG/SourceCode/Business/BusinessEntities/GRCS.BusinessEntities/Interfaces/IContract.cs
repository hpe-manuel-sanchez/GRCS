/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IContractService.cs 
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
 * Siva              08/08/2012      Reorganizing the code 
 * Siva              08/08/2012      Changeset id is 1525 
 * Vijayakumar.R     08/08/2012      Delete 3 Interface GetContracts methods 
 *                                   For Code Redundancy
 * Vijayakumar.R     08/08/2012      Changeset id is 1545 
 * Vaishnavi.L       22/08/2012      Commented unused methods
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Interface methods for Contract Service
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public partial interface IContract
    {
        #region Secondary Exploitation Restrictions

        /// <summary>
        /// Gets the default data secondary exploitations.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SecondaryExploitationDefaults GetDefaultDataSecondaryExploitations();

        #endregion Secondary Exploitation Restrictions

        #region Territory And Country

        /// <summary>
        /// Gets the territory data.
        /// </summary>
        /// <param name="templateInfo">The template info.</param>
        /// <returns></returns>
        [OperationContract]
        List<TerritorialDisplay> GetTerritoryData(TemplateDetails templateInfo);

        /// <summary>
        /// Gets the territory data.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <returns></returns>
        [OperationContract(Name = "GetContractTerritoryDetails")]
        List<TerritorialDisplay> GetContractTerritoryDetails(long contractId);


        #endregion Territory And Country

        #region Contract

        /// <summary>
        /// Saves the contract.
        /// </summary>
        /// <param name="contractDetails">The contract details.</param>
        /// <returns></returns>
        [OperationContract]
        ContractInfo SaveContract(ContractDetails contractDetails);

        /// <summary>
        /// Saves the contract template.
        /// </summary>
        /// <param name="contractDetails">The contract details.</param>
        /// <returns></returns>
        [OperationContract]
        string SaveContractTemplate(ContractTemplate contractDetails);


        /// <summary>
        /// Gets the contract templates.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        List<TemplateDetails> GetContractTemplates(string userName);

        /// <summary>
        /// Gets the contract template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        ContractDetails GetContractTemplate(long templateId, string userName);

        #endregion Contract

        #region ContractInfo

        /// <summary>
        /// Gets the master data for contract form.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MasterData GetMasterDataForContract(string userName);

        /// <summary>
        /// Gets the search contract master data.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        SearchContractMasterData GetSearchContractMasterData();

        /// <summary>
        /// Gets the contracts.
        /// </summary>
        /// <param name="contractSearch">The contract search.</param>
        /// <returns></returns>
        [OperationContract(Name = "SearchContract")]
        List<ContractInfo> GetContracts(ContractInfo contractSearch);

        [OperationContract]
        List<ContractInfo> GetContractMaintenanceWorkQueue(ContractInfo contractSearch);

        /// <summary>
        /// Searches the parent contracts.
        /// </summary>
        /// <param name="contractSearch">The contract search.</param>
        /// <returns></returns>
        [OperationContract]
        List<ContractInfo> SearchParentContracts(ContractInfo contractSearch);

        /// <summary>
        /// Gets the contract.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="tasks"> </param>
        /// <returns></returns>
        [OperationContract]
        ContractDetails GetContract(long contractId,string userName, GrsTasks tasks);

        /// <summary>
        /// Deletes the contract.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="requestDateTime">The request date time.</param>
        [OperationContract]
        void DeleteContract(long contractId, string userName, DateTime requestDateTime);

        #endregion

        #region Rights

        /// <summary>
        /// Gets the default data rights and restriction.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        RightsAndRestrictions  GetDefaultDataRightsAndRestriction();

        /// <summary>
        /// Gets the default data digital restriction.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictions GetDefaultDataDigitalRestriction();

      
        [OperationContract]
        DigitalRestrictionTemplateResults SaveDigitalRestrictionTemplate
            (DigitalRestrictionTemplate digitalRestrictionTemplateDetails);

        #endregion Rights

        #region Template Maintenance

        [OperationContract]
        ContractTemplatesResult LoadContractTemplates(FilterFields filterFields);

        /// <summary>
        /// Gets the digital templates.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        List<TemplateDetails>
        GetDigitalTemplates(string userName);

        /// <summary>
        /// Gets the digital template.
        /// </summary>
        /// <param name="digitalRestrictionTemplateId">The digital restriction template id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionTemplate GetDigitalTemplate(long digitalRestrictionTemplateId, string userName);

        /// <summary>
        /// Deletes the templates.
        /// </summary>
        /// <param name="templateDetail">The template detail.</param>
        /// <param name="userName"> </param>
        /// <returns></returns>
        [OperationContract]
        string DeleteTemplates(List<TemplateDetails> templateDetail, string userName);

        #endregion

        #region Split Deal

        /// <summary>
        /// Gets the split deal contracts.
        /// </summary>
        /// <param name="contractInfo">The contract info.</param>
        /// <returns></returns>
        [OperationContract]
        List<ContractInfo> GetSplitDealContracts(ContractInfo contractInfo);

        #endregion

        #region Project

        [OperationContract]
        void LinkProjectToContract(ProjectInfo linkProjectInfo, long contractId);

        #endregion

        #region LinkContract
        [OperationContract]
        List<long> IsAlreadyLinkedContract(List<long> ids, long contractId, string repertoireType);

        #endregion

        #region Email Groups
        /// <summary>
        /// Search criteria, Gets the user groups along with email Ids.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        /// <returns></returns>
        [OperationContract]
        List<EmailGroupDetails> GetEmailGroups(EmailGroupDetails emailGroupDetails);

        /// <summary>
        /// Saves the email group.
        /// </summary>
        /// <param name="emailGroupDetails">The email group details.</param>
        [OperationContract]
        void SaveEmailGroup(EmailGroupDetails emailGroupDetails);
        #endregion

        /// <summary>
        /// Deletes the email groups.
        /// </summary>
        /// <param name="emailGroupIds">The email group ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="modifiedDateTime">The modified date time.</param>
        [OperationContract]
        void DeleteEmailGroups(List<long> emailGroupIds, string userName, DateTime modifiedDateTime);

        [OperationContract]
        List<LinkedProjectInfo> GetLinkedProjectDetails(long contractId, FilterFields filterCriteria);

        [OperationContract]
        List<LinkedReleaseInfo> GetLinkedReleaseDetails(long contractId, FilterFields filterCriteria);

        [OperationContract]
        List<LinkedResourceInfo> GetLinkedResourceDetails(long contractId, FilterFields filterCriteria);


        [OperationContract]
        void UnlinkRepertoire(long contractId, List<long> projectIdList, List<long> releaseIdList, List<long> resourceIdList, string userName);

        [OperationContract]
        void UnlinkRepertoireFromWorkQueue(List<WorkQueue> workQueueItems, string userName, bool ChangeLink);

        [OperationContract]
        List<ContractInfo> GetContractsForSplitDeal(long contractId, ContractInfo contractFilter);

        [OperationContract]
        List<ContractInfo> GetChildContracts(ChildContractsRequest childContractsRequest);

        [OperationContract]
        void UnlinkRepertoireFromReleaseResource(List<ChangeLinkInfo> workQueueItems, string userName);

        [OperationContract]
        RepertoireFilterResults IsAlreadyUnlinked(long contractId, List<long> projectIdList, List<long> releaseIdList, List<long> resourceIdList);

         [OperationContract]
        RepertoireFilterResults IsAlreadyUnlinkFromWorkQueue(List<WorkQueue> workQueueItems);

         [OperationContract]
         List<long> GetLinkedRightSets(List<ChangeLinkInfo> changeLinkInfos);
    }
    
}
