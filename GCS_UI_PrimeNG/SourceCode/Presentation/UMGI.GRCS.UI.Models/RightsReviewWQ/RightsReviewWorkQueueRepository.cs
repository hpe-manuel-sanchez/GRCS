/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsReviewWorkQueueRepository.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Karthik P
 * Created on     : 12-02-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                 Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Interfaces.RightsReviewWQ;
using UMGI.GRCS.UI.Proxies.ContractService;
using UMGI.GRCS.UI.Proxies.RepertoireService;
using UMGI.GRCS.UI.Proxies.RightsService;
using UMGI.GRCS.UI.Proxies.WorkQueueService;
using UMGI.GRCS.Utilities.logger;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class RightsReviewWorkQueueRepository : IRightsReviewWorkQueueRepository
    {

        #region "Private Variables"

        private readonly ILogFactory _logFactory;

        #endregion

        #region RightsReviewWorkqueue Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RightsReviewWorkQueueRepository"/> class.
        /// </summary>
        /// <param name="logFactory">The log factory.</param>
        public RightsReviewWorkQueueRepository(ILogFactory logFactory)
        {
            _logFactory = logFactory;
        }

        #endregion RightsReviewWorkqueue Constructor

        #region MasterData for Uc-18,19,20

        /// <summary>
        /// Gets the acquired rights master data.
        /// </summary>
        /// <returns></returns>
        public ReviewRightsMasterInfo GetAcquiredRightsMasterData()
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                // _logFactory.LogWriter.Debug(string.Format("ResourceRights: {0}", resourceRights));
                return rightsService.GetRightsAcquiredMasterData();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Loads the pre clearance master data.
        /// </summary>
        /// <returns></returns>
        public PreClearanceMasterData LoadPreClearanceMasterData()
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                return rightsService.LoadPreClearanceMasterData();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Loads the secondary master data.
        /// </summary>
        /// <returns></returns>
        public SecondaryRightsMasterData LoadSecondaryMasterData()
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                return rightsService.LoadSecondaryRightsMasterData();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Loads the digital master data.
        /// </summary>
        /// <returns></returns>
        public DigitalRightsMasterData LoadDigitalMasterData()
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                return rightsService.LoadDigitalRightsMasterData();
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        #endregion MasterData for Uc-18,19,20

        #region Common Methods for UC-18,19,20

        public List<long> GetLinkedRightSets(List<ChangeLinkInfo> changeLinkItems)
        {
              ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                contractService.Open();
                return contractService.GetLinkedRightSets(changeLinkItems);
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
        /// <param name="term">The term.</param>
        /// <param name="userName">Name of the user.</param>
        public void UnlinkContract(string term, string userName)
        {
            ContractClient contractService = null;
            try
            {
                contractService = new ContractClient();
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                contractService.Open();

                if (string.IsNullOrEmpty(term) && string.IsNullOrEmpty(term.Split('^')[1]) && term.Split(',').Length == 0)
                {
                    return;
                }
                var repertoireType = term.Split('^')[1];
                var unlinkItems = term.Split('^')[0].Split(',');
                var contractId = long.Parse(unlinkItems[0]);
                if (repertoireType == "Resource")
                {
                    var resourceId = long.Parse(unlinkItems[1]);
                    contractService.UnlinkRepertoire(contractId, new List<long>(), new List<long>(), new List<long>() { resourceId }, userName);
                    return;
                }
                var releaseId = long.Parse(unlinkItems[1]);
                contractService.UnlinkRepertoire(contractId, new List<long>(), new List<long>() { releaseId }, new List<long>(), userName);

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
        /// Links the contract.
        /// </summary>
        /// <param name="contractId">The contract id.</param>
        /// <param name="releaseInfos">The release infos.</param>
        /// <param name="resourceInfos">The resource infos.</param>
        public void LinkContract(long contractId, List<ReleaseInfo> releaseInfos, List<ResourceInfo> resourceInfos)
        {
            RepertoireClient repertoireService = null;
            try
            {
                repertoireService = new RepertoireClient();
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                repertoireService.Open();
                repertoireService.LinkContractInBackGround(releaseInfos, resourceInfos, contractId);
                _logFactory.LogWriter.Debug(Constants.MethodEnd);

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (repertoireService != null && repertoireService.State == CommunicationState.Faulted)
                {
                    repertoireService.Abort();
                    repertoireService = null;
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
                if (repertoireService != null && repertoireService.State == CommunicationState.Opened)
                {
                    repertoireService.Close();
                    repertoireService = null;
                }
            }
        }

        /// <summary>
        /// Load territory data
        /// </summary>
        /// <param name="rightSetId"></param>
        /// <returns></returns>
        public List<TerritorialDisplay> LoadRightSetTerritory(long rightSetId)
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                rightsService.Open();
                var result = rightsService.LoadTerritoryData(rightSetId, false);
                rightsService.Close();
                _logFactory.LogWriter.Debug(Constants.MethodEnd);
                return result;

            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        #endregion  Common Methods for UC-18,19,20

        #region Load Methods for UC-18 & 19

    

        /// <summary>
        /// Loads the resource rights WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceRightsResult LoadResourceRightsWQ(ResourceFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
               workQueueService=new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.GetResourceRightsWQ(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }

        /// <summary>
        /// Loads the resource secondary rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceSecondaryRightsResult LoadResourceSecondaryRights
            (ResourceFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadResourceSecondaryRights(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }

        }

        /// <summary>
        /// Loads the resource pre clearance info.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourcePreClearanceResult LoadResourcePreClearanceInfo
            (ResourceFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadResourcePreClearanceInfo(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }


        /// <summary>
        /// Loads the resource digital rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceDigitalRightsResult LoadResourceDigitalRights
            (ResourceFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadResourceDigitalRights(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }

        /// <summary>
        /// Loads the resource rights predefined WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceRightsResult LoadResourceRightsPredefinedWQ(PreDefinedParametersWQ filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadResourceRightsWQPredefined(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }

        }

        /// <summary>
        /// Loads the resource digital rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceDigitalRightsResult LoadResourceDigitalRightsPredefined(PreDefinedParametersWQ filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.GetResourceDigitalRightsPredefined(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }

        }

        /// <summary>
        /// Loads the resource secondary rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourceSecondaryRightsResult LoadResourceSecondaryRightsPredefined(PreDefinedParametersWQ filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.GetResourceSecondaryRights(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }

        }

        /// <summary>
        /// Loads the resource pre clearance predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ResourcePreClearanceResult LoadResourcePreClearancePredefined(PreDefinedParametersWQ filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.GetResourcesPreclearancePredefined(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }


        #endregion Load Methods for UC-18 & 19

        #region Save Methods for UC-18 & 19

        /// <summary>
        /// Saves the reviewed resource rights.
        /// </summary>
        /// <param name="resourceRights">The resource rights.</param>
        /// <returns></returns>
        public List<long> SaveReviewedResourceRights(ResourceRightsSaveInfo resourceRights)
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("ResourceRights: {0}", resourceRights));
                return rightsService.SaveReviewedResourceRights(resourceRights);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Saves the resource digital rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <param name="modifierInfo">The modifier info.</param>
        /// <returns></returns>
        public List<long> SaveResourceDigitalRights
            (List<ResourceDigitalRestrictions> rights, ModifierInfo modifierInfo)
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", rights));
                var saveInformation = new ResourceDigitalSaveInfo { ModifierInfo = modifierInfo, Rights = rights };
                return rightsService.SaveResourceDigitalRights(saveInformation);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }
      

        /// <summary>
        /// Saves the resource secondary rights.
        /// </summary>
        /// <param name="secondaryRights">The secondary rights.</param>
        /// <returns></returns>
        public List<long> SaveResourceSecondaryRights
            (SecondaryRightsSaveInfo secondaryRights)
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", secondaryRights));
                return rightsService.SaveResourceSecondaryRights(secondaryRights);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Saves the resource pre clearance rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <returns></returns>
        public List<long> SaveResourcePreClearanceRights(PreClearanceSaveInfo rights)
        {
            RightsClient rightsService = null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", rights));
                return rightsService.SaveResourcePreClearanceRights(rights);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        #endregion Save Methods for UC-18 & 19

        #region Save Methods for UC-20

        /// <summary>
        /// Saves the reviewed release rights.
        /// </summary>
        /// <param name="releaseRights">The release rights.</param>
        /// <returns></returns>
        public List<long> SaveReviewedReleaseRights(ReleaseRightsSaveInfo releaseRights)
        {
            RightsClient rightsService=null;
            try
            {
                rightsService = new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", releaseRights));
                return rightsService.SaveReviewedReleaseRights(releaseRights);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        /// <summary>
        /// Saves the release digital rights.
        /// </summary>
        /// <param name="rights">The rights.</param>
        /// <param name="modifierInfo">The modifier info.</param>
        /// <returns></returns>
        public List<long> SaveReleaseDigitalRights
            (List<ReleaseDigitalRights> rights, ModifierInfo modifierInfo)
        {
            RightsClient rightsService = null;
            try
            {
               rightsService =new RightsClient();
                rightsService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", rights));
                var releaseDigitalSaveInfo = new ReleaseDigitalSaveInfo { Rights = rights, ModifierInfo = modifierInfo };
                return rightsService.SaveReleaseDigitalRights(releaseDigitalSaveInfo);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (rightsService != null && rightsService.State == CommunicationState.Faulted)
                {
                    rightsService.Abort();
                    rightsService = null;
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
                if (rightsService != null && rightsService.State == CommunicationState.Opened)
                {
                    rightsService.Close();
                    rightsService = null;
                }
            }
        }

        #endregion Save Methods for UC-20

        #region Load Methods UC-20

        /// <summary>
        /// Loads the release digital rights.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ReleaseDigitalRightsResult LoadReleaseDigitalRights
            (ReleaseFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService=new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadReleaseDigitalRights(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }

        /// <summary>
        /// Loads the release rights WQ.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ReleaseRightsResult LoadReleaseRightsWQ(ReleaseFilterParameters filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
                workQueueService = new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.GetReleaseRightsWQ(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }

       
        /// <summary>
        /// Loads the release digital rights predefined.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public ReleaseDigitalRightsResult LoadReleaseDigitalRightsPredefined(PreDefinedParametersWQ filter)
        {
            WorkQueueClient workQueueService = null;
            try
            {
              workQueueService=new WorkQueueClient();
                workQueueService.Open();
                _logFactory.LogWriter.Debug(string.Format("Filter: {0}", filter));
                return workQueueService.LoadReleaseDigitalRightsPredefined(filter);
            }
            catch (FaultException fex)
            {
                _logFactory.LogWriter.Error(Category.Service, fex);
                if (workQueueService != null && workQueueService.State == CommunicationState.Faulted)
                {
                    workQueueService.Abort();
                    workQueueService = null;
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
                if (workQueueService != null && workQueueService.State == CommunicationState.Opened)
                {
                    workQueueService.Close();
                    workQueueService = null;
                }
            }
        }

        #endregion Load Methods UC-20




    }
}
