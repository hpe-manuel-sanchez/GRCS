/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IContractRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-07-2012 
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;
using CountryInfo = UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo;


namespace UMGI.GRCS.UI.Interfaces
{
    public partial interface IContractRepository
    {

        IContractModel ContractPageModel { get; set; }

        IContractMaintenanceWorkQueueModel ContractMaintenanceWorkQueuePageModel { get; set; }
                        

        #region Contract Tab

        void GetMasterDataForContract(GrsTasks task, bool isCRCContract);
        void GetSearchContractMasterData(GrsTasks task);

        ContractSearchResult SearchParentContract(string artistName, string localContractNumber, string contractingParty, long contractId, FilterFields filterFields);

       // ArtistSearchResult SearchForArtist(ArtistSearchCriteria searchOption);
       // NoticeCompanySearchResult SearchPcNoticeCompany(NoticeCompanySearch searchOption);
        ContractSearchResult SearchContract(ContractInfo searchContract, FilterFields filterFields);       

        void EditContract(long contractId);
        void CopyContract(long contractId);
        void DeleteContract(long contractId);

        List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm, GrsTasks tasks);
        List<UmgSigningCompany> AutoSearchUmgSigningCompany(string searchTerm);
      //  List<ArtistInfo> AutoSearchArtistName(string searchTerm);

        ContractInfo SaveContract(IContractModel contract);

        void GetContractTemplates(string userName);
        void GetContractTemplate(long templateId, string userName);
        string SaveContractTemplate(IContractModel contract);
        
        List<ContractInfo> GetContractsForSplitDeal(long contractId, ContractInfo searchInfo);

        #endregion


        # region Secondary Exploitation

        void GetDefaultDataSecondaryExploitations();



        # endregion Secondary Exploitation

        # region Rights and Restriction

        void GetDefaultDataRightsandRestriction();

        void LoadDigitalRestrictionDropDownExtension(IRightsRestrictionTabModel digitalRestriction);

        DigitalRestrictionTemplate GetDigitalRestrictionTemplate(long templateId, string userName);

      
        DigitalRestrictionTemplateResults SaveDigitalRestrictionsTemplate(IRightsRestrictionTabModel digitalRestrictionModel, string templateName, string clearenceAdminCompanyId);
       // void LoadDigitalRestrictionDropDown(IRightsRestrictionTabModel objDigitalRestriction);

        void LoadDigitaltemplate(IRightsRestrictionTabModel objmanager);
       
        void SetDigitalRestrictionDropDownValues(IRightsRestrictionTabModel digitalRestriction);
        void LoadDigitaltemplates(ITemplateMaintenanceModel tempModel);
        # endregion Rights and Restriction

        #region "Contract Maintenance Work Queue"

        ContractSearchResult ContractMaintenanceWorkQueues(UserInfo userInformation, FilterFields filterFields);

        #endregion

        #region "Territorial Rights"
        List<TerritorialDisplay> GetTerritorialRightsData(long contractId, long templateId, string userName);
        #endregion

      

        

        #region Contract Template Maintenance

                string DeleteTemplates(string templateId, string templateType, string userName);
        #endregion


        #region Address Book
        IAddressBookModel AddressBookModel { get; set; }
        /// <summary>
        /// Autofill text to filter Email Group names
        /// </summary>
        /// <returns></returns>
        List<EmailGroupDetails> GetEmailGroups(EmailGroupDetails emailGroupDetails);

        #endregion Address Book

        List<ContractInfo> SplitDealSearch(ContractInfo contractInfo);
       
       

        #region address book
        void SaveEmailGroup(EmailGroupDetails emailGroupDetails);

        List<CountryInfo> GetCountries(CountryInfo countryInfo);

        void DeleteEmailGroup(string groupId, UserInfo userInfo);

      

        #endregion

        ContractDetails GetContractInfoForContractId(bool flag,long contractId = 0);

        IEnumerable<SelectListItem> GetPageSizeList();

        List<CountryIdentifier> AutoSearchCountry(string searchTerm);

        #region UnlinkRepertoire


        /// <summary>
        /// Determines whether [is already unlinked] [the specified array repertoire].
        /// </summary>
        /// <param name="arrayRepertoire">The array repertoire.</param>
        /// <param name="records">The records.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        RepertoireCodeResults IsAlreadyUnlinked(string[] arrayRepertoire, string[] records, string userName, long id = 0);
        /// <summary>
        /// Unlinks the contract.
        /// </summary>
        /// <param name="arrayRepertoire">The array repertoire.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="id">The id.</param>
        void UnlinkContract(string[] arrayRepertoire, string userName,long id = 0);

        /// <summary>
        /// Gets the linked project details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        List<LinkedProjectInfo> GetLinkedProjectDetails(long contractId, FilterFields filterCriteria);

        /// <summary>
        /// Gets the linked release details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        List<LinkedReleaseInfo> GetLinkedReleaseDetails(long contractId, FilterFields filterCriteria);

        /// <summary>
        /// Gets the linked resource details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        List<LinkedResourceInfo> GetLinkedResourceDetails(long contractId, FilterFields filterCriteria);

        #endregion
       

        ContractTemplatesResult GetContractTemplates(FilterFields filterFields);

        #region GetChildContracts

        List<ContractInfo> GetChildContracts(ChildContractsRequest childContractsRequest);

        #endregion

    }

    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IContractRepository
    {
        #region "SearchContractbyArtist"
        ArtistContractSearchResult SearchContractbyArtist(ArtistSearchCriteria artistSearchCriteria, bool isPaging, UserInfo userInfo);
        #endregion

        #region "SearchContractbyResource"
        ResourceContractSearchResult SearchContractbyResource(ResourceSearchCriteria resourceSearchCriteria, bool isPaging, UserInfo userInfo);
        #endregion

        #region "GetResourceContractByContractIdList"
        List<ResourceContract> GetResourceContractByContractIdList(List<DeviationResourceContract> contractIdList, UserInfo userInfo);
        #endregion

        #region "GetArtistByContract"
        List<ArtistContract> GetArtistByContract(List<long> contractids, string userLoginName);
        #endregion

    }
}
