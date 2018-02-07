/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractTabModel.cs
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
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
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.BusinessEntities.Responses;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.Resx.Resource.UIResources;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.ContractService;
using UMGI.GRCS.UI.Proxies.GlobalService;
using CountryInfo = UMGI.GRCS.BusinessEntities.Entities.ContractEntities.CountryInfo;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Repository where service calls are made and model data is filled
    /// </summary>
    public partial class ContractRepository : IContractRepository
    {
        #region "Private Variables"
        
        private readonly StringIdentifier _selectReason = new StringIdentifier { Id = 0, Description = Constants.SelectReason };
        private readonly StringIdentifier _select = new StringIdentifier { Id = 0, Description = Constants.Select };
        private readonly TemplateDetails _templateSelect = new TemplateDetails { TemplateId = 0, TemplateName = Constants.Select };
        private readonly StringIdentifier _emptyString = new StringIdentifier { Id = 0, Description = Constants.EmptyString };

        private ISessionWrapper _sessionWrapper { get; set; }
        private IConfigFactory _configurationFactory { get; set; }
        private ILogFactory _logFactory { get; set; }
        private UserInfo _userDetails { get; set; }

        #endregion

        #region "Public Variables"

        public IAddressBookModel AddressBookModel { get; set; }

        /// <summary>
        /// Gets or sets the obj contract model.
        /// </summary>
        /// <value>The obj contract model.</value>
        public IContractModel ContractPageModel { get; set; }

        /// <summary>
        /// Model for Contract Maintenance Work Queue.
        /// </summary>
        /// <value>The obj contract maintenance work queue model.</value>
        public IContractMaintenanceWorkQueueModel ContractMaintenanceWorkQueuePageModel { get; set; }

        #endregion


        #region "Contract Repository Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRepository"/> class.
        /// </summary>
        public ContractRepository(IConfigFactory configFactory, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _sessionWrapper = sessionWrapper;
                _userDetails = _sessionWrapper.CurrentUserInfo;
                _configurationFactory = configFactory;
                _logFactory = logFactory;

                ContractPageModel = new ContractModel { ContractTabModel = { ItemsPerPage = GetPageSizeList() } };
                AddressBookModel = new AddressBookModel { AddressBookPerPage = GetPageSizeList() };

                ContractMaintenanceWorkQueuePageModel = new ContractMaintenanceWorkQueueModel { ShowItemsPerPage = GetPageSizeList() };
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
            }
        }

        #endregion

        /// <summary>
        /// Gets the dropdown and default table values  .
        /// </summary>
        /// <param name="task"> </param>
        public void GetMasterDataForContract(GrsTasks task, bool isCRCContract)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug("GetMasterDataForContract");

                contractService = new ContractClient();
                contractService.Open();
                var defaultMasterDataValues = contractService.GetMasterDataForContract(_sessionWrapper.CurrentUserInfo.UserLoginName);
                contractService.Close();

                var contractTabModel = ContractPageModel.ContractTabModel;
                    SetCompanyDropDown(defaultMasterDataValues);
                    var contractDescriptionList =
                        defaultMasterDataValues.ContractDescriptions.Select(
                            p => new StringIdentifier {Description = p.Description, Id = p.Value});
                    contractTabModel.ContractDescriptionList =
                        contractDescriptionList.Select(
                            p => new SelectListItem {Text = p.Description, Value = p.Value.ToString()}).OrderBy(
                                ordDes => ordDes.Text);

                    defaultMasterDataValues.WorkflowStatus =
                       GetWorkFlowStatus(task != GrsTasks.ApproveNCRcontract ? Role.LegalEditor : Role.LegalReviwer);

                    contractTabModel.WorkFlowStatus =
                        defaultMasterDataValues.WorkflowStatus.Select(
                            p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() });
               
                //Assigning the values from value to id to avoid js validation Mismatch
                var contractStatus =
                    defaultMasterDataValues.ContractStatus.Select(
                        p => new StringIdentifier { Description = p.Description, Id = p.Value });

                if (isCRCContract == false)
                {
                    if (defaultMasterDataValues.ContractStatus.Any(status => status.Description == Constants.ClearanceRoutingContract))
                    {
                        defaultMasterDataValues.ContractStatus.Remove(defaultMasterDataValues.ContractStatus.First(status => status.Description == Constants.ClearanceRoutingContract));
                    }
                }
                contractTabModel.ContractStatusList = contractStatus.Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() });

              SetRightsDropDown(defaultMasterDataValues);

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch(FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Gets the dropdown and default table values  .
        /// </summary>
        /// <param name="task"> </param>
        public void GetSearchContractMasterData(GrsTasks task)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug("GetSearchContractMasterData");

                contractService = new ContractClient();
                contractService.Open();
                var defaultMasterDataValues = contractService.GetSearchContractMasterData();
                contractService.Close();

                var contractTabModel = ContractPageModel.ContractTabModel;

                    contractTabModel.WorkFlowStatus =
                        defaultMasterDataValues.WorkflowStatus.Select(
                            p => new SelectListItem { Text = p.Description, Value = p.Value.ToString() });
            
                  contractTabModel.ContractStatusList = defaultMasterDataValues.ContractStatus.Select(p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() });

                var rightsType = defaultMasterDataValues.RightTypes.Take(4).Select(
               p => new StringIdentifier() { Description = p.Description, Id = p.Value });//To Select the first 4 Values.

                contractTabModel.RightsType =
                    rightsType.Select(
                        p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() }).OrderByDescending(ordRightsType => ordRightsType.Text);

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Gets the work flow status.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        private List<StringIdentifier> GetWorkFlowStatus(Role role)
        {
            var workFlowStatus = new List<StringIdentifier>();
            switch (role)
            {
                case Role.LegalEditor:
                    workFlowStatus.Add(new StringIdentifier { Id = 1, Description = ContractResource.DataEntry });
                    workFlowStatus.Add(new StringIdentifier { Id = 2, Description = ContractResource.Pending });
                    break;
                case Role.LegalReviwer:
                    workFlowStatus.Add(new StringIdentifier { Id = 1, Description = ContractResource.DataEntry });
                    workFlowStatus.Add(new StringIdentifier { Id = 2, Description = ContractResource.Pending });
                    workFlowStatus.Add(new StringIdentifier { Id = 3, Description = ContractResource.Approved });
                    break;
                default:
                    workFlowStatus.Add(new StringIdentifier { Id = 1, Description = ContractResource.DataEntry });
                    workFlowStatus.Add(new StringIdentifier { Id = 2, Description = ContractResource.Pending });
                    break;
            }
            return workFlowStatus;
        }
        #region Master DataDropDown

        /// <summary>
        /// Sets the company dropdown.
        /// </summary>
        /// <param name="contractMasterDataValues">The contract master data values.</param>
        private void SetCompanyDropDown(MasterData contractMasterDataValues)
        {
            var contractTabModel = ContractPageModel.ContractTabModel;
            contractMasterDataValues.PCompanies.Insert(0, new NoticeCompany() { CompanyName = "", CompanyId = 0 });
            contractTabModel.PcCompanyCountryList =
               contractMasterDataValues.PCompanies.Select(
                   p => new SelectListItem { Text = p.CompanyName, Value = p.CompanyId.ToString() }).OrderBy(ordCompany => ordCompany.Text);
        }

        /// <summary>
        /// Sets the rights dropdowns.
        /// </summary>
        /// <param name="contractMasterDataValues">The contract master data values.</param>
        private void SetRightsDropDown(MasterData contractMasterDataValues)
        {
            var contractTabModel = ContractPageModel.ContractTabModel;
                var lostRightsReason = contractMasterDataValues.LostRightsReasons;

                lostRightsReason.Add(_emptyString);
                if (lostRightsReason != null)
                {
                    ContractPageModel.ContractTabModel.LostRightsReasonList =
                        lostRightsReason.Select(
                            p => new SelectListItem {Text = p.Description, Value = p.Value.ToString()}).OrderBy(
                                ordRights => ordRights.Text);
                }

                //Assigning the values from value to id to avoid js validation Mismatch        
                var rightsPeriod = contractMasterDataValues.RightsPeriods.Select(
                    p => new StringIdentifier {Description = p.Description, Id = p.Value});
             
                var onActiveRoster = new List<StringIdentifier>
                {
                    new StringIdentifier {Id = Constants.ValueOne, Description = Constants.NoInActive},
                    new StringIdentifier {Id = Constants.ValueTwo, Description = Constants.YesInActive}
                };

            contractTabModel.OnActiveRosterList =
                 onActiveRoster.Select(
                      p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() }).OrderByDescending(
                          i => i.Text);


                contractTabModel.RightsPeriodList =
                    rightsPeriod.Select(
                        p => new SelectListItem {Text = p.Description, Value = p.Id.ToString()}).OrderBy(
                            ordRightsPeriod => ordRightsPeriod.Text);

           
            var rightsType = contractMasterDataValues.RightTypes.Take(4).Select(
               p => new StringIdentifier() { Description = p.Description, Id = p.Value });//To Select the first 4 Values.

            contractTabModel.RightsType =
                rightsType.Select(
                    p => new SelectListItem { Text = p.Description, Value = p.Id.ToString() }).OrderByDescending(ordRightsType => ordRightsType.Text);
        }

        # endregion

        /// <summary>
        /// Searches the parent contract.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="localContractNumber">The local contract number.</param>
        /// <param name="contractingParty">The contracting party.</param>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterFields">Pagesize and Sorting Parameter </param>
        /// <returns></returns>
        public ContractSearchResult SearchParentContract(string artistName, string localContractNumber, string contractingParty, long contractId, FilterFields filterFields)
        {
            _logFactory.LogWriter.Debug(string.Format("ArtistName:{0},LocalContractNumber{1},ContractingParty{2},ContractId{3},FilterFields{4}",
                    artistName, localContractNumber, contractingParty, contractId, filterFields));
            ContractClient contractService = null;
            try
            {

                contractService = new ContractClient();
                contractService.Open();
                var contractSearchResult = new ContractSearchResult
                                               {
                                                   ContractSearchInfo =
                                                       contractService.SearchParentContracts(
                                                           new ContractInfo
                                                               {
                                                                   ArtistName = artistName,
                                                                   LocalContractRefNumber = localContractNumber,
                                                                   ContractingParty = contractingParty,
                                                                   ParentContractId = null,
                                                                   ContractId = contractId,
                                                                   HasSearchCriteria = true,
                                                                   HasPageDetails = true,
                                                                   PageDetails = filterFields,
                                                                   IsParentContractSearch = true,
                                                                   UserName = _sessionWrapper.CurrentUserInfo.UserLoginName
                                                               })
                                               };
                contractService.Close();

                if (contractSearchResult.ContractSearchInfo != null && contractSearchResult.ContractSearchInfo.Count > 0)
                {
                    contractSearchResult.ContractsCount = contractSearchResult.ContractSearchInfo.First().PageDetails.TotalRows;
                }

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                
                return contractSearchResult;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

      
        /// <summary>
        /// Edits the contract.
        /// </summary>
        /// <param name="contractId">The contractid.</param>
        public void EditContract(long contractId)
        {
            _logFactory.LogWriter.Debug("ContractId:"+contractId);
            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();

                Stopwatch sw = new Stopwatch();
                sw.Start();
                var contractDetails = contractService.GetContract(contractId,_userDetails.UserLoginName, GrsTasks.EditNCRContract|GrsTasks.SearchContract|GrsTasks.EditNCRAppContract|GrsTasks.EditCACContract);
               sw.Stop();
               _logFactory.LogWriter.Debug("Edit contract returned in :" + sw.Elapsed);
                if (contractDetails == null)
                {
                    return;
                }

                //Db migration issue fix
                contractDetails.WorkflowStatusId = contractDetails.WorkflowStatusId ?? 1;
                contractDetails.WorkflowStatus = contractDetails.WorkflowStatus ?? Constants.DataEntry;

                contractDetails.LastModifiedDate = contractDetails.LastModifiedTime.ToString(Constants.DateFormat);

                var contractTabModel = ContractPageModel.ContractTabModel;
                var rightsRestrictionTabModel = ContractPageModel.RightsRestrictionTabModel;

                contractTabModel.Contract = contractDetails;

                rightsRestrictionTabModel.RightsAcquired = contractDetails.RightsAndRestrictions.AcquisitableRights;
                rightsRestrictionTabModel.DigitalRestriction = contractDetails.RightsAndRestrictions.DigitalRestrictions;

                ContractPageModel.SecondaryExploitationTabModel.SecondaryExploitation = contractDetails.ContractExploitationRestrictionsList;

                ContractPageModel.TerritorialRightsPopup.SaveTerritorialRights = contractDetails.ContractTerritoryList.Where(s => s.IsExcluded || s.IsIncluded).ToList();
                ContractPageModel.TerritorialRightsPopup.GetTerritorialRights = contractDetails.ContractTerritoryList;

                contractTabModel.SplitContract = contractDetails.SplitDealContracts;
                contractTabModel.Contract.SplitDealContracts = contractDetails.SplitDealContracts;

                _sessionWrapper.ContractModelStore = ContractPageModel;

                // If parent contract is null return without filling below details.
                if (contractDetails.ParentContractId == null)
                {
                    return;
                }

                //TODO:Get parent in GetContract service method
                if (contractDetails.ParentContractId != -1)
                {
                    var parentContractId = contractDetails.ParentContractId;
                    ContractPageModel.ContractTabModel.AddParentContract = contractService.GetContract(parentContractId.Value, _userDetails.UserLoginName, GrsTasks.None);
                }

                contractService.Close();

                _logFactory.LogWriter.Debug(Constants.MethodEnd);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI,ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

        /// <summary>
        /// Copies the contract.
        /// </summary>
        /// <param name="contractId">The edit contract id.</param>
        public void CopyContract(long contractId)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug("ContractID :"+ contractId);

                contractService = new ContractClient();
                contractService.Open();
                var contractDetails = contractService.GetContract(contractId,_userDetails.UserLoginName,GrsTasks.CreateNCRContract|GrsTasks.ApproveNCRcontract);
                contractService.Close();

                if (contractDetails == null)
                {
                    return;
                }
                var contractTabModel = ContractPageModel.ContractTabModel;
                var rightsRestrictionTabModel = ContractPageModel.RightsRestrictionTabModel;


                contractTabModel.Contract = contractDetails;
                //Reset Contract Id
                if (contractTabModel.Contract != null)
                {
                    contractTabModel.Contract.ContractId = 0;
                    contractTabModel.Contract.WorkflowStatusId = 0;
                    contractTabModel.Contract.LocalContractRefNumber = "";
                    contractTabModel.Contract.IsRightsExceptionApplied = false;
					contractTabModel.Contract.ParentContractId = 0;
                    contractTabModel.Contract.DummyParentId = null;
                }

                rightsRestrictionTabModel.RightsAcquired =
                    contractDetails.RightsAndRestrictions.AcquisitableRights;

                rightsRestrictionTabModel.DigitalRestriction =
                    contractDetails.RightsAndRestrictions.DigitalRestrictions;

                ContractPageModel.SecondaryExploitationTabModel.SecondaryExploitation =
                    contractDetails.ContractExploitationRestrictionsList;

                ContractPageModel.TerritorialRightsPopup.SaveTerritorialRights =
                    contractDetails.ContractTerritoryList.Where(s => s.IsExcluded || s.IsIncluded).ToList();

                ContractPageModel.TerritorialRightsPopup.GetTerritorialRights = contractDetails.ContractTerritoryList;

                _sessionWrapper.ContractModelStore = ContractPageModel;

                _logFactory.LogWriter.Debug(Constants.MethodEnd);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Delete the Contract
        /// </summary>
        /// <param name="contractId">Deleting Contract Id</param>
        public void DeleteContract(long contractId)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(contractId.ToString());
                contractService = new ContractClient();
                contractService.Open();
                contractService.DeleteContract(contractId, _sessionWrapper.CurrentUserInfo.UserLoginName, DateTime.Now);
                contractService.Close();
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }


        /// <summary>
        /// Search for Artist
        /// </summary>
        /// <param name="searchOption">Search Parameters</param>
        /// <returns></returns>
        //public ArtistSearchResult SearchForArtist(ArtistSearchCriteria searchOption)
        //{
        //    ArtistClient artistService = null;

        //    try
        //    {
        //        _logFactory.LogWriter.Debug(string.Format("SearchOption:{0}", searchOption));

               
        //        if (searchOption.Criteria.StartIndex != 0)
        //        {
        //            var startIndex = GetStartIndex(searchOption.Criteria.StartIndex, searchOption.Criteria.PageSize);
        //            searchOption.Criteria.StartIndex = startIndex;
        //        }

        //        artistService = new ArtistClient();
        //        artistService.Open();
        //        var returnValue = artistService.SearchArtist(searchOption);
        //        artistService.Close();
        //        return returnValue;
        //    }
        //    catch (FaultException fex)
        //    {
        //        _logFactory.LogWriter.Error(Category.Service, fex);
        //        if (artistService != null && artistService.State == CommunicationState.Faulted)
        //        {
        //            artistService.Abort();
        //            artistService = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (artistService != null && artistService.State == CommunicationState.Opened)
        //        {
        //            artistService.Close();
        //            artistService = null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Search for PC Notice Companies
        ///// </summary>
        ///// <param name="searchOption">Search Parameters</param>
        ///// <returns></returns>
        //public NoticeCompanySearchResult SearchPcNoticeCompany(NoticeCompanySearch searchOption)
        //{
        //    PCompanyClient pCompanyService = null;

        //    try
        //    {
        //        _logFactory.LogWriter.Debug(string.Format("SearchOption :{0}", searchOption));

        //        if (searchOption.Criteria.StartIndex != 0)
        //        {
        //            var startIndex = GetStartIndex(searchOption.Criteria.StartIndex, searchOption.Criteria.PageSize);
        //            searchOption.Criteria.StartIndex = startIndex;
        //        }
        //        _logFactory.LogWriter.Debug(Constants.MethodEnd);
        //        pCompanyService = new PCompanyClient();
        //        pCompanyService.Open();
        //        var returnValue = pCompanyService.GetPcNoticeCompaniesFromR2(searchOption);
        //        pCompanyService.Close();
        //        return returnValue;
        //    }
        //    catch (FaultException fex)
        //    {
        //        _logFactory.LogWriter.Error(Category.Service, fex);
        //        if (pCompanyService != null && pCompanyService.State==CommunicationState.Faulted)
        //        {
        //            pCompanyService.Abort();
        //            pCompanyService = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (pCompanyService != null && pCompanyService.State == CommunicationState.Opened)
        //        {
        //            pCompanyService.Close();
        //            pCompanyService = null;
        //        }
        //    }
        //}

        private int GetStartIndex(int startIndex, int pageSize)
        {
            return startIndex / pageSize;
        }

        # region Search Contract Screen

        /// <summary>
        /// Auto  search for umg signing company.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public List<UmgSigningCompany> AutoSearchUmgSigningCompany(string searchTerm)
        {
            GlobalClient globalService = null;

            try
            {
                _logFactory.LogWriter.Debug(searchTerm);

                globalService = new GlobalClient();
                globalService.Open();
                var umgSigningCompanies = globalService.GetCompaniesForUser(new CompanySearch
                 {
                     IsCountryRequired = true,
                     IsFilterRequired = false,
                     SearchTerm = searchTerm,
                     UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                     HasPageDetails = false
                 });
                globalService.Close();
                _logFactory.LogWriter.Debug(Constants.MethodEnd);

                return
                    umgSigningCompanies.Select(
                        item => new UmgSigningCompany { CountryId = item.CountryId, CountryName = item.CountryName, CountryIsoCode = item.CountryIsoCode,Id = item.Id, Name = item.Name }).OrderBy(
                            comp => comp.Name).ToList();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }

        //public List<ArtistInfo> AutoSearchArtistName(string searchTerm)
        //{
        //    ArtistClient artistService = null;

        //    try
        //    {
        //        _logFactory.LogWriter.Debug(searchTerm);
        //        artistService = new ArtistClient();
        //        artistService.Open();
        //        var returnValue = new List<ArtistInfo>(artistService.SearchGRCSArtists(new ArtistInfo { Name = searchTerm }).Where(a => a.Name.ToLower().Contains(searchTerm.ToLower())));
        //        artistService.Close();
        //        return returnValue;
        //    }
        //    catch (FaultException fex)
        //    {
        //        _logFactory.LogWriter.Error(Category.Service, fex);
        //        if (artistService != null && artistService.State == CommunicationState.Faulted)
        //        {
        //            artistService.Abort();
        //            artistService = null;
        //        }
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logFactory.LogWriter.Error(Category.UI, ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (artistService != null && artistService.State == CommunicationState.Opened)
        //        {
        //            artistService.Close();
        //            artistService = null;
        //        }
        //    }
        //}

        /// <summary>
        /// Searches the contract.
        /// </summary>
        /// <returns></returns>        
        public ContractSearchResult SearchContract(ContractInfo searchContract, FilterFields filterFields)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format("SearchContract :{0},FilterFields :{1}", searchContract, filterFields));
                if (filterFields.SortField == null)
                {
                    filterFields.IsAscendingOrder = false;
                    filterFields.SortField = searchContract.PageDetails.SortField;
                }
                searchContract.UserName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                contractService = new ContractClient();
                contractService.Open();
                var contractSearchResult = new ContractSearchResult
                                               {
                                                   ContractSearchInfo = contractService.SearchContract(searchContract)
                                               };
                contractService.Close();

                if (contractSearchResult.ContractSearchInfo != null && contractSearchResult.ContractSearchInfo.Count > 0)
                {
                    contractSearchResult.ContractsCount = contractSearchResult.ContractSearchInfo.First().PageDetails.TotalRows;
                }

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                
                return contractSearchResult;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        # endregion

        /// <summary>
        /// Autos the search clearance comp country.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="tasks"> </param>
        /// <returns></returns>
        //public List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm, List<ClearanceAdminCompany> clearanceAdminCompanies)
        public List<ClearanceAdminCompany> AutoSearchClearanceCompCountry(string searchTerm, GrsTasks tasks)
        {
            GlobalClient globalService = null;

            _logFactory.LogWriter.Debug(searchTerm);
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.GetCompaniesForUser(new CompanySearch
                    {
                    UserName = _sessionWrapper.CurrentUserInfo.UserLoginName,
                    SearchTerm = searchTerm,
                    IsFilterRequired = true,
                    IsCountryRequired = true,
                    Tasks = tasks,
                    HasPageDetails = false
                });
                globalService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }
        }

        # region Secondary Exploitation

        /// <summary>
        /// Gets the default data secondary exploitations.
        /// </summary>
        public void GetDefaultDataSecondaryExploitations()
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);

                //need to enable after Services are up
                contractService = new ContractClient();
                contractService.Open();
                var secExpDefaultValues = contractService.GetDefaultDataSecondaryExploitations();
                contractService.Close();

                var secondaryExploitationTabModel = ContractPageModel.SecondaryExploitationTabModel;
                secondaryExploitationTabModel.SecondaryExploitation = secExpDefaultValues.SecondaryExploitations;
                secondaryExploitationTabModel.RestrictedList = secExpDefaultValues.RestrictionOptions.Select(restricted => new SelectListItem { Text = restricted.Name, Value = restricted.Id.ToString() }).Reverse();

                secExpDefaultValues.RestrictionTypes.Insert(0, new Restrictions { TypeId = 0, TypeName = Constants.Select });
                //Removed 'No restriction(4)' as per UAT requirement.
                secondaryExploitationTabModel.RestrictionList =
                    secExpDefaultValues.RestrictionTypes.Where(item => item.TypeId != 4).Select(
                        restriction =>
                        new SelectListItem { Text = restriction.TypeName, Value = restriction.TypeId.ToString() }).OrderBy(ordRestrictions => ordRestrictions.Text);

                secExpDefaultValues.ConsentPeriodTypes.Insert(0, new ConsentPeriod { TypeId = 0, TypeName = Constants.Select });

                secondaryExploitationTabModel.ConsentPeriodList = secExpDefaultValues.ConsentPeriodTypes.Select(consentPeriod => new SelectListItem { Text = consentPeriod.TypeName, Value = consentPeriod.TypeId.ToString() }).OrderBy(ordConsentPeriod => ordConsentPeriod.Text);

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }




        # endregion Secondary Exploitation


        # region Rights and Restriction

        /// <summary>
        /// Gets the digital restriction template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userName"> </param>
        /// <returns></returns>
        public DigitalRestrictionTemplate GetDigitalRestrictionTemplate(long templateId, string userName)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("TemplateId :{0}", templateId));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetDigitalTemplate(templateId, userName);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

      
        //TODO: need to make the single method after template maintence method redefined
        public DigitalRestrictionTemplateResults SaveDigitalRestrictionsTemplate(IRightsRestrictionTabModel digitalRestriction, string templateName, string clearenceAdminCompanyId)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("DigitalRestriction :{0},TemplateName :{1},ClearanceAdminCompanyID :{2}",
                    digitalRestriction, templateName, clearenceAdminCompanyId));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.SaveDigitalRestrictionTemplate(new DigitalRestrictionTemplate
                {
                    LastModifiedTime = digitalRestriction.LastModifiedTime,
                    DigitalRestrictions = digitalRestriction.DigitalRestriction,
                    TemplateName = templateName,
                    ClearanceAdmin = Convert.ToString(clearenceAdminCompanyId),
                    TemplateId = digitalRestriction.TemplateId,
                    TemplateType =
                        digitalRestriction.IsNewTemplate ? Constants.NewTemplate : Constants.UpdateTemplate,
                        RequestDateTime = DateTime.Now,
                    UserName = _sessionWrapper.CurrentUserInfo.UserLoginName
                         

                });
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        public void LoadDigitalRestrictionDropDownExtension(IRightsRestrictionTabModel digitalRestriction)
        {
            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var digitalData = contractService.GetDefaultDataDigitalRestriction();
                contractService.Close();

            digitalData.ContentTypes.Insert(0, _select);
            digitalRestriction.ContentTypeList = digitalData.ContentTypes.Select(contentType => new SelectListItem { Text = contentType.Description, Value = contentType.Value.ToString() });

            digitalData.UseTypes.Insert(0, _select);
            digitalRestriction.UseTypeList = digitalData.UseTypes.Select(useType => new SelectListItem { Text = useType.Description, Value = useType.Value.ToString() });

            digitalData.CommercialModelTypes.Insert(0, _select);
            digitalRestriction.CommercialModelsList = digitalData.CommercialModelTypes.Select(commercialType => new SelectListItem { Text = commercialType.Description, Value = commercialType.Value.ToString() });

            digitalData.RestrictionTypes.Insert(0, _select);
            //Remove Restriction - No Restriction(4) from digital restriction
            digitalRestriction.RestrictionList = digitalData.RestrictionTypes.Select(restriction => new SelectListItem { Text = restriction.Description, Value = restriction.Value.ToString() });

            digitalData.ConsentPeriodTypes.Insert(0, _select);
            digitalRestriction.ConsentPeriodList = digitalData.ConsentPeriodTypes.Select(consentPeriod => new SelectListItem { Text = consentPeriod.Description, Value = consentPeriod.Value.ToString() });

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        //TODO: Need to ask Service team  to expose one single method to get all value
        /// <summary>
        /// Gets the default data rightsand restriction.
        /// </summary>
        public void GetDefaultDataRightsandRestriction()
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                contractService = new ContractClient();
                contractService.Open();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var defaultValues = contractService.GetDefaultDataRightsAndRestriction();
                sw.Stop();
                _logFactory.LogWriter.Debug("R&R returned in :" + sw.Elapsed);
                //Closed in LoadDigitalTemplate
                //contractService.Close();

                var rightsRestrictionTabModel = ContractPageModel.RightsRestrictionTabModel;
      
                rightsRestrictionTabModel.RightsAcquired = defaultValues.AcquisitableRights;

                defaultValues.AcquiredOption.Insert(0, _select);
                defaultValues.AcquiredStatus.Insert(0, _select);
                rightsRestrictionTabModel.IsAcquiredList = defaultValues.AcquiredOption.Select(restricted =>
                            new SelectListItem { Text = restricted.Description, Value = restricted.Id.ToString(CultureInfo.InvariantCulture) });          
                rightsRestrictionTabModel.DealRestrictOption = defaultValues.AcquiredStatus.Select(dealstatus =>
                            new SelectListItem { Text = dealstatus.Description, Value = dealstatus.Id.ToString() }).OrderBy(ordDeal => ordDeal.Text);

                LoadDigitaltemplate(rightsRestrictionTabModel);

             
                SetDigitalRestrictionDropDownValues(rightsRestrictionTabModel);


                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }


        /// <summary>
        /// Loads the digitaltemplate.
        /// </summary>
        /// <param name="manager">The objmanager.</param>
        public void LoadDigitaltemplate(IRightsRestrictionTabModel manager)
        {
            _logFactory.LogWriter.Debug(string.Format("Manager :{0}", manager));
            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var digitalTemplate = contractService.GetDigitalTemplates( _sessionWrapper.CurrentUserInfo.UserLoginName);
                contractService.Close();

                if (digitalTemplate != null)
                {
                    digitalTemplate.Insert(0, _templateSelect);
                    manager.DigitalTemplate =
                        digitalTemplate.Select(
                            templateStatus =>
                            new SelectListItem { Text = templateStatus.TemplateName, Value = templateStatus.TemplateId.ToString() }).OrderBy(ordTemplate => ordTemplate.Text);
                }

                _logFactory.LogWriter.Debug(Constants.MethodEnd);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Loads the digitaltemplates.
        /// </summary>
        /// <param name="tempModel">Template Maintenance Model</param>
        public void LoadDigitaltemplates(ITemplateMaintenanceModel tempModel)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("TempModel :{0}", tempModel));
                contractService = new ContractClient();
                contractService.Open();
                tempModel.DigitalRestrictionTemplates = contractService.GetDigitalTemplates(_userDetails.UserLoginName);
                contractService.Close();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        
        /// <summary>
        /// Sets the digital restriction drop down values.
        /// </summary>
        /// <param name="digitalRestriction">The digital restriction.</param>
        public void SetDigitalRestrictionDropDownValues(IRightsRestrictionTabModel digitalRestriction)
        {
            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var defaultDigitalRestrictionData = contractService.GetDefaultDataDigitalRestriction();
                contractService.Close();

                defaultDigitalRestrictionData.ContentTypes.Insert(0, _select);
                digitalRestriction.ContentTypeList = defaultDigitalRestrictionData.ContentTypes.Select(
                        contentType => new SelectListItem { Text = contentType.Description, Value = contentType.Value.ToString() }).OrderBy(ordContentTypes => ordContentTypes.Text);

                defaultDigitalRestrictionData.UseTypes.Insert(0, _select);
                digitalRestriction.UseTypeList =
                    defaultDigitalRestrictionData.UseTypes.Select(
                        useType => new SelectListItem { Text = useType.Description, Value = useType.Value.ToString() }).OrderBy(ordUseTypes => ordUseTypes.Text);

                defaultDigitalRestrictionData.CommercialModelTypes.Insert(0, _select);
                digitalRestriction.CommercialModelsList =
                    defaultDigitalRestrictionData.CommercialModelTypes.Select(
                        commercialType => new SelectListItem { Text = commercialType.Description, Value = commercialType.Value.ToString() }).OrderBy(ordCommercialModel => ordCommercialModel.Value);

                defaultDigitalRestrictionData.RestrictionTypes.Insert(0, _select);
                //Remove Restriction - No Restriction(4) from digital restriction
                digitalRestriction.RestrictionList =
                    defaultDigitalRestrictionData.RestrictionTypes.Select(
                        restriction => new SelectListItem { Text = restriction.Description, Value = restriction.Value.ToString() }).OrderBy(ordRestrictions => ordRestrictions.Text);

                defaultDigitalRestrictionData.ConsentPeriodTypes.Insert(0, _select);
                digitalRestriction.ConsentPeriodList =
                    defaultDigitalRestrictionData.ConsentPeriodTypes.Select(
                        consentPeriod => new SelectListItem { Text = consentPeriod.Description, Value = consentPeriod.Value.ToString() }).OrderBy(ordConsentPeriod => ordConsentPeriod.Text);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        # endregion Rights and Restriction


        /// <summary>
        /// Saves the contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns></returns>
        public ContractInfo SaveContract(IContractModel contract)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("Contract :{0}" , contract));

                var contractDetails = SetValueForContract(contract);

                if (contractDetails.ParentContractId == 0)
                {
                    contractDetails.ParentContractId = null;
                }
     
                if (contractDetails.PcNoticeCountryCompanyId > 0 && string.IsNullOrEmpty(contractDetails.PcNoticeCountryCompany))
                {
                    contractDetails.PcNoticeCountryCompanyId = 0;
                }
                contractDetails.UserName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                contractDetails.RequestDateTime = DateTime.Now;
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.SaveContract(contractDetails);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Saves the contract template.
        /// </summary>
        /// <returns></returns>
        public string SaveContractTemplate(IContractModel contract)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("Contract :{0}", contract));
                var contractDetailsData = SetValueForContract(contract);

                if (contractDetailsData.UmgSigningCompanyId == null || contractDetailsData.UmgSigningCompanyId == 0)
                {
                    contractDetailsData.UmgSigningCompanyId = contractDetailsData.ClearanceCompanyCountryId;
                }
                var contractTemplate = new ContractTemplate
                                           {
                                               ContractDetailsData = contractDetailsData,
                                               TemplateId = contract.TemplateId,
                                               TemplateName = contract.TemplateName,
                                               UserName = _userDetails.UserLoginName,
                                               TemplateType =
                                                   contract.IsNewTemplate
                                                       ? Constants.NewTemplate
                                                       : Constants.UpdateTemplate
                                           };

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                if (contractTemplate.ContractDetailsData.PcNoticeCountryCompanyId > 0 && string.IsNullOrEmpty(contractTemplate.ContractDetailsData.PcNoticeCountryCompany))
                {
                    contractTemplate.ContractDetailsData.PcNoticeCountryCompanyId = 0;
                }
                contractTemplate.UserName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                contractTemplate.RequestDateTime = DateTime.Now;
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.SaveContractTemplate(contractTemplate);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Sets the value for contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns></returns>
        private ContractDetails SetValueForContract(IContractModel contract)
        {
            var contractDetailsData = contract.ContractTabModel.Contract;
            var rightsAndRestrictions = contractDetailsData.RightsAndRestrictions;
            var rightsRestrictionTabModel = contract.RightsRestrictionTabModel;

            contractDetailsData.ContractExploitationRestrictionsList = contract.SecondaryExploitationTabModel.SecondaryExploitation;

            rightsAndRestrictions.DigitalRestrictions = rightsRestrictionTabModel.DigitalRestriction??new List<ContractDigitalRestrictions>();
            rightsAndRestrictions.AcquisitableRights = rightsRestrictionTabModel.RightsAcquired;

            contractDetailsData.ContractTerritoryList = contract.TerritorialRightsPopup.SaveTerritorialRights.Where(s => s.IsIncluded || s.IsExcluded).ToList();

            if (contractDetailsData.UmgSigningCompanyId == null || contractDetailsData.UmgSigningCompanyId == 0)
            {
                contractDetailsData.UmgSigningCompanyId = contractDetailsData.ClearanceCompanyCountryId;
            }
            return contractDetailsData;

        }

        /// <summary>
        /// Gets the contract templates.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public void GetContractTemplates(string userName)
        {
            _logFactory.LogWriter.Debug(string.Format("UserInformation :{0}", userName));

            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var selectTemplateId = contractService.GetContractTemplates(userName);
                contractService.Close();

                if (selectTemplateId != null)
                {
                    selectTemplateId.Insert(0, _templateSelect);
                    ContractPageModel.ContractTabModel.SelectTemplate =
                        selectTemplateId.Select(
                            template =>
                            new SelectListItem
                                {
                                    Text = template.TemplateName,
                                    Value = template.TemplateId.ToString(CultureInfo.InvariantCulture)
                                }).OrderBy(ordTemplateName => ordTemplateName.Text);
                }

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Gets the contract template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userName">Name of the user.</param>
        public void GetContractTemplate(long templateId, string userName)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("TemplateId :{0}", templateId));

                var contractTabModel = ContractPageModel.ContractTabModel;
                
                contractService = new ContractClient();
                contractService.Open();
                var contractDetails = contractService.GetContractTemplate(templateId, userName);
                contractService.Close();

                contractDetails.WorkflowStatusId = null;

                contractTabModel.Contract = contractDetails;

                if (contractTabModel.Contract != null)
                {
                    contractTabModel.Contract.WorkflowStatusId = 0;
                }

                var rightsRestrictionTabModel = ContractPageModel.RightsRestrictionTabModel;
                rightsRestrictionTabModel.RightsAcquired = contractDetails.RightsAndRestrictions.AcquisitableRights;
                rightsRestrictionTabModel.DigitalRestriction = contractDetails.RightsAndRestrictions.DigitalRestrictions;

                if (ContractPageModel.SecondaryExploitationTabModel != null)
                {
                    ContractPageModel.SecondaryExploitationTabModel.SecondaryExploitation =
                        contractDetails.ContractExploitationRestrictionsList;
                }

                if (ContractPageModel.TerritorialRightsPopup != null)
                {
                    ContractPageModel.TerritorialRightsPopup.SaveTerritorialRights =
                        contractDetails.ContractTerritoryList.Where(s => s.IsExcluded || s.IsIncluded).ToList();
                }

                ContractPageModel.TemplateName = contractDetails.SecondaryExploitationTemplate.TemplateName;
                ContractPageModel.TemplateId = contractDetails.SecondaryExploitationTemplate.TemplateId;

                _logFactory.LogWriter.Debug(Constants.MethodEnd);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Gets the contract maintenance work queue.
        /// </summary>
        public ContractSearchResult ContractMaintenanceWorkQueues(UserInfo userInformation, FilterFields filterFields)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("UserInformation :{0},FilterFields :{1}", userInformation, filterFields));
                contractService = new ContractClient();
                contractService.Open();
                var contractSearchResult = new ContractSearchResult
                                               {
                                                   ContractSearchInfo =
                                                       contractService.GetContractMaintenanceWorkQueue(
                                                           new ContractInfo
                                                               {
                                                                   ParentContractId = 0,
                                                                   HasSearchCriteria = true,
                                                                   PageDetails = filterFields,
                                                                   HasPageDetails = true,
                                                                   IsWorkQueue = true,
                                                                   UserName = _sessionWrapper.CurrentUserInfo.UserLoginName
                                                               })
                                               };
                contractService.Close();

                if (contractSearchResult.ContractSearchInfo.Count > 0)
                {
                    contractSearchResult.ContractsCount = contractSearchResult.ContractSearchInfo[0].PageDetails.TotalRows;
                }

                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return contractSearchResult;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }


        /// <summary>
        /// Gets the territorial rights data.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="templateId">The Template Id</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public List<TerritorialDisplay> GetTerritorialRightsData(long contractId, long templateId, string userName)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractId :{0},TemplateId :{1}", contractId, templateId));

                TemplateDetails templateDetails = null;
                if (templateId != 0)
                {
                    templateDetails = new TemplateDetails
                                          {
                                              TemplateId = templateId,
                                              UserName = userName
                                          };
                }

                contractService = new ContractClient();
                contractService.Open();
                if (contractId == 0)
                {
                    var templateTerritories = contractService.GetTerritoryData(templateDetails);
                    contractService.Close();
                    return templateTerritories;
                }
                var contractTerritories = contractService.GetContractTerritoryDetails(contractId);
                contractService.Close();
                return contractTerritories;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }


        /// <summary>
        /// Gets the contract templates.
        /// </summary>
        /// <param name="filterFields">The filter fields.</param>
        /// <returns></returns>
        public ContractTemplatesResult GetContractTemplates(FilterFields filterFields)
        {
            ContractClient contractService = null;

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.LoadContractTemplates(filterFields);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }


        /// <summary>
        /// Delete Templates
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="templateType"></param>
        /// <param name="userName"> </param>
        /// <returns></returns>
        public string DeleteTemplates(string templateId, string templateType, string userName)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("templateId :{0},templateType :{1}", templateId, templateType));

                if (templateId == null)
                {
                    return null;
                }

                var templateIdCollection = templateId.Split(',');
                var templates = (from word in templateIdCollection
                                 where word.Trim().Length > 0
                                 select new TemplateDetails
                                 {
                                     TemplateId = Convert.ToInt64(word),
                                     TemplateType = templateType
                                 }).ToList();

                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.DeleteTemplates(templates,userName);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        #region Address Book

        /// <summary>
        /// Gets the Email Group Names.
        /// </summary>
        /// <returns></returns>        

        public List<EmailGroupDetails> GetEmailGroups(EmailGroupDetails emailGroupDetails)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("EmailGroupDetails :{0}", emailGroupDetails));
                emailGroupDetails.UserName = _sessionWrapper.CurrentUserInfo.UserLoginName;
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetEmailGroups(emailGroupDetails);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

        #endregion Address Book


        //TODO:Need to pass only contracting PArty and artist name
        /// <summary>
        /// Split Deal Search
        /// </summary>
        /// <param name="contractInfo">Contract Information</param>
        /// <returns></returns>
        public List<ContractInfo> SplitDealSearch(ContractInfo contractInfo)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractInfo :{0}", contractInfo));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetSplitDealContracts(contractInfo);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }
        

        /// <summary>
        /// Save Email Group
        /// </summary>
        /// <param name="emailGroupDetails">EmailGRoup Details</param>
       
        public void SaveEmailGroup(EmailGroupDetails emailGroupDetails)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("EmailGroupDetails :{0}", emailGroupDetails));
                //TODO:Remove the date
                emailGroupDetails.RequestDateTime = DateTime.Now;
                contractService = new ContractClient();
                contractService.Open();
                contractService.SaveEmailGroup(emailGroupDetails);
                contractService.Close();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Get Countries
        /// </summary>
        /// <param name="countryInfo">Country Information</param>
        /// <returns></returns>
        public List<CountryInfo> GetCountries(CountryInfo countryInfo)
        {
            GlobalClient globalService = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format("CountryInfo :{0}", countryInfo));
                globalService = new GlobalClient();
                globalService.Open();
                var returnValue = globalService.SearchUserCountries(countryInfo, GrsTasks.AddressBookMaintinance);
                globalService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }

        }

        /// <summary>
        /// Detale an EmailGroup
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <param name="userInfo">User Information</param>
        public void DeleteEmailGroup(string groupId, UserInfo userInfo)
        {
            _logFactory.LogWriter.Debug(string.Format("GroupId :{0},UserInfo :{1}", groupId, userInfo));

            ContractClient contractService = null;

            var emailGroupDetailsLst = groupId.Split(',');
            var emailIds = (from word in emailGroupDetailsLst
                            where word.Trim().Length > 0
                            select Convert.ToInt64(word)).ToList();

            try
            {
                contractService = new ContractClient();
                contractService.Open();
                //TODO:Remove DateTime
                contractService.DeleteEmailGroups(emailIds, userInfo.UserLoginName, DateTime.Now);
                contractService.Close();
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

        /// <summary>
        /// To get the Contract Information for the Word Export
        /// </summary>
        /// <param name="flag"> </param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ContractDetails GetContractInfoForContractId(bool flag,long contractId = 0)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractId :{0}", contractId));
                contractService = new ContractClient();
                contractService.Open();
               var returnValue = flag ? contractService.GetContract(contractId, _userDetails.UserLoginName,
                                                                GrsTasks.ViewUnapprovedcontract |
                                                                GrsTasks.SearchContract)
                                  : contractService.GetContract(contractId, _userDetails.UserLoginName,
                                                                GrsTasks.None);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }


        }


        public List<CountryIdentifier> AutoSearchCountry(string searchTerm)
        {
            GlobalClient globalService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("SearchTerm:{0}", searchTerm));
                globalService = new GlobalClient();
                globalService.Open();
                var result = globalService.GetCountriesList(searchTerm);
                globalService.Close();
               // _logFactory.LogWriter.Info(string.Format("Result:{0}", result[0]));
                return result;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (globalService != null && globalService.State == CommunicationState.Faulted)
                {
                    globalService.Abort();
                    globalService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (globalService != null && globalService.State == CommunicationState.Opened)
                {
                    globalService.Close();
                    globalService = null;
                }
            }

        }
        /// <summary>
        /// Gets the page size list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPageSizeList()
        {
            return _configurationFactory.PageSizeValues.Select(
                    item =>
                    new SelectListItem { Text = item.Value, Value = item.Key.ToString(CultureInfo.InvariantCulture) });
        }



        #region UnlinkRepertoire
        /// <summary>
        /// Gets the long list.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        private List<long> GetLongList(IEnumerable<string> ids)
        {
            return (from id in ids where id != string.Empty select long.Parse(id)).ToList();
        }
        /// <summary>
        /// Determines whether [is already unlinked] [the specified array repertoire].
        /// </summary>
        /// <param name="arrayRepertoire">The array repertoire.</param>
        /// <param name="records">The records.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public RepertoireCodeResults IsAlreadyUnlinked(string[] arrayRepertoire, string[] records, string userName, long id = 0)
        {
            ContractClient contractService = null;
            try
            {
                var projectRows = new List<string>();
                 var releaseRows = new List<string>();
                 var resourceRows = new List<string>();
                _logFactory.LogWriter.Debug(string.Format("ArrayRepertoire :{0},Id:{1}", arrayRepertoire, id));
              foreach (string record in records)
              {
                  if (record.Split(';')[2] == Constants.ProjectCode)
                  {
                      projectRows.Add(record);
                  }
                  if (record.Split(';')[2] == Constants.ReleaseCode)
                  {
                      releaseRows.Add(record);
                  }
                  if (record.Split(';')[2] == Constants.ResourceCode)
                  {
                      resourceRows.Add(record);
                  }
              }
                var projectIds = GetLongList(arrayRepertoire[0].Split(','));
                var releaseIds = GetLongList(arrayRepertoire[1].Split(','));
                var resourceIds = GetLongList(arrayRepertoire[2].Split(','));
                var repertoireCode = new RepertoireCodeResults();
                contractService = new ContractClient();
                contractService.Open();
                var result = contractService.IsAlreadyUnlinked(id, projectIds, releaseIds, resourceIds);
             
                if (result.ProjectIds.Count != 0 || result.ReleaseIds.Count != 0 || result.ResourceIds.Count != 0)
                {

                    foreach (var projectRow in projectRows)
                    {
                        foreach (var projectId in result.ProjectIds)
                        {
                            if (projectRow.Contains(projectId.ToString()))
                            {
                                
                                repertoireCode.ProjectIds.Add(projectRow.Split(';')[1]);
                                
                            }
                        }
                       
                    }
                    foreach (var releaseRow in releaseRows)
                    {
                        foreach (var releaseId in result.ReleaseIds)
                        {
                            if (releaseRow.Contains(releaseId.ToString()))
                            {
                               
                                repertoireCode.ReleaseIds.Add(releaseRow.Split(';')[1]);
                                
                            }
                        }

                    }
                    foreach (var resourceRow in resourceRows)
                    {
                        foreach (var resourceId in result.ResourceIds)
                        {
                            if (resourceRow.Contains(resourceId.ToString()))
                            {
                              
                                repertoireCode.ResourceIds.Add(resourceRow.Split(';')[1]);
                               
                            }
                        }

                    }

                    var unlinkedProjectIds = projectIds.Where(item => result.ProjectIds.Contains(item) == false).ToList();
                    var unlinkedReleaseIds = releaseIds.Where(item => result.ReleaseIds.Contains(item) == false).ToList();
                    var unlinkedResourceIds = resourceIds.Where(item => result.ResourceIds.Contains(item) == false).ToList();
                    if (unlinkedProjectIds.Count > 0 || unlinkedReleaseIds.Count > 0 || unlinkedResourceIds.Count > 0)
                    {
                        contractService.UnlinkRepertoire(id, unlinkedProjectIds, unlinkedReleaseIds, unlinkedResourceIds,
                                                         userName);
                        contractService.Close();
                    }

                    return repertoireCode;
                }
                else
                {
                   
                    contractService.UnlinkRepertoire(id, projectIds, releaseIds, resourceIds, userName);
                    contractService.Close();
                    return null;
                }
               
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }
        /// <summary>
        /// Unlinks the contract.
        /// </summary>
        /// <param name="arrayRepertoire">The array repertoire.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="id">The id.</param>
        public void UnlinkContract(string[] arrayRepertoire, string userName, long id = 0)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ArrayRepertoire :{0},UserName :{1},Id:{2}", arrayRepertoire, userName, id));
                var projectIds = GetLongList(arrayRepertoire[0].Split(','));
                var releaseIds = GetLongList(arrayRepertoire[1].Split(','));
                var resourceIds = GetLongList(arrayRepertoire[2].Split(','));
                contractService = new ContractClient();
                contractService.Open();  
                contractService.UnlinkRepertoire(id, projectIds, releaseIds, resourceIds, userName);
                contractService.Close();
               
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        /// <summary>
        /// Gets the linked project details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        public List<LinkedProjectInfo> GetLinkedProjectDetails(long contractId, FilterFields filterCriteria)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractId :{0},FilterFields :{1}", contractId, filterCriteria));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetLinkedProjectDetails(contractId, filterCriteria);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

        /// <summary>
        /// Gets the linked release details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        public List<LinkedReleaseInfo> GetLinkedReleaseDetails(long contractId, FilterFields filterCriteria)
        {
            ContractClient contractService = null;

            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractId :{0},FilterFields :{1}", contractId, filterCriteria));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetLinkedReleaseDetails(contractId, filterCriteria);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }

        /// <summary>
        /// Gets the linked resource details.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        public List<LinkedResourceInfo> GetLinkedResourceDetails(long contractId, FilterFields filterCriteria)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format("ContractId :{0},FilterFields :{1}", contractId, filterCriteria));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetLinkedResourceDetails(contractId, filterCriteria);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }

        }
        #endregion



        #region GetContractsForSplitDeal

        /// <summary>
        /// Gets the contracts for split deal.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="searchInfo">The search info.</param>
        /// <returns></returns>
        public List<ContractInfo> GetContractsForSplitDeal(long contractId, ContractInfo searchInfo)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "ContractId :{0},searchInfo :{1}", contractId, searchInfo));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetContractsForSplitDeal(contractId, searchInfo);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetContractsForSplitDeal"), ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        #endregion

        #region GetChildContracts

        public List<ContractInfo> GetChildContracts(ChildContractsRequest childContractsRequest)
        {
            ContractClient contractService = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "ContractId :{0}", childContractsRequest.ContractId));
                contractService = new ContractClient();
                contractService.Open();
                var returnValue = contractService.GetChildContracts(childContractsRequest);
                contractService.Close();
                return returnValue;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetContractsForSplitDeal"), ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        #endregion region
    }

    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial class ContractRepository
    {
        #region "Manage Artist Search"
        /// <summary>
        /// This method to get the ArtistContract information from R2 & GCS system.
        /// </summary>
        /// <param name="artistSearchCriteria">ArtistSearchCriteria</param>
        /// <param name="isPaging">bool</param>
        /// <returns>ArtistContractSearchResult</returns>
        public ArtistContractSearchResult SearchContractbyArtist(ArtistSearchCriteria artistSearchCriteria, bool isPaging, UserInfo userInfo)
        {
            ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                contractService.Open();
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "ArtistSearchCriteria :{1},isPaging :{2}", "SearchContractbyArtist", artistSearchCriteria, isPaging));
                var contractbyArtistList = contractService.SearchContractbyArtist(artistSearchCriteria, isPaging, userInfo);
                contractService.Close();
                return contractbyArtistList;
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "SearchContractbyArtist"), fex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }
        #endregion

        #region "Manage Resource Search"
        /// <summary>
        /// This method to get the ArtistContract information from R2 & GCS system.
        /// </summary>
        /// <param name="resourceSearchCriteria">ResourceSearchCriteria</param>
        /// <param name="isPaging">bool</param>
        /// <returns>ArtistContractSearchResult</returns>
        public ResourceContractSearchResult SearchContractbyResource(ResourceSearchCriteria resourceSearchCriteria, bool isPaging, UserInfo userInfo)
        {
            ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                contractService.Open();
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "ResourceSearchCriteria :{1},isPaging :{2}", "SearchContractbyResource", resourceSearchCriteria, isPaging));
                var contractbyResourceList = contractService.SearchContractbyResource(resourceSearchCriteria, isPaging, userInfo);
                contractService.Close();
                return contractbyResourceList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "SearchContractbyResource"), ex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }
        #endregion


        /// <summary>
        /// This method to get Resource Contract information from R2 & GCS system.
        /// </summary>
        /// <param name="contractIdList">List of Long</param>
        /// <returns>List of ResourceContract</returns>
        public List<ResourceContract> GetResourceContractByContractIdList(List<DeviationResourceContract> contractIdList, UserInfo userInfo)
        {
            ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                contractService.Open();
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "contractIdList :{1}", "GetResourceContractByContractIdList", contractIdList));
                var resourceContractList = contractService.GetResourceContractByContractIdList(contractIdList, userInfo);
                contractService.Close();
                return resourceContractList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetResourceContractByContractIdList"), ex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }

        #region "Get Artist by Contract"
        /// <summary>
        ///"Get Artist by Contract".
        /// </summary>
        ///  <param name="contractids">Contract Ids</param>
        /// <returns>True if success, return list of record </returns>
        public List<ArtistContract> GetArtistByContract(List<long> contractids, string userLoginName)
        {
            ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                contractService.Open();
                var artistList = contractService.GetArtistByContract(contractids, userLoginName);
                contractService.Close();
                return artistList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetArtistByContract"), ex);
                if (contractService != null && contractService.State == CommunicationState.Faulted)
                {
                    contractService.Abort();
                    contractService = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            finally
            {
                if (contractService != null && contractService.State == CommunicationState.Opened)
                {
                    contractService.Close();
                    contractService = null;
                }
            }
        }
        #endregion
    }
}
