using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.ClearanceResourceService;


namespace UMGI.GRCS.UI.Models
{
    public class ClearanceResourceRepository : IClearanceResourceRepository
    {
        public IClearanceResourceModel IClearanceResourceModel { get; set; }        
       
        readonly ILogFactory _logFactory;

        #region "Public Variables"

        public ISessionWrapper SessionWrapper { get; set; }
        public IConfigFactory ConfigurationFactory { get; set; }
        public IAddressBookModel AddressBookModel { get; set; }
        public IClearanceProjectModel IClearanceProjectModel { get; set; }
        public string Isparent { get; set; }

        const string _msgGetMasterData = "Get Master Data Method Initiated";
        const string _msgSearchR2Resource = "Search R2 Resource Method Initiated";        
        const string _msgGetResourceRights = "Search R2 Mock Data Resource Method Initiated";
        const string _msgRemoveResourceProject = "Remove Resource Project Method Initiated";

        #endregion

        

        #region "Project Repository Constructor"
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractRepository"/> class.
        /// </summary>
        public ClearanceResourceRepository(ILogFactory logFactory)
        {
            try
            {
                _logFactory = logFactory;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
        }

        #endregion

 

        [OutputCache(Duration = 0)]
        public ClearanceResourceSearchResult SearchR2Resource(ResourceSearchCriteria resourceSearch, LeanUserInfo userInfo)
        {
            ClearanceResourceSearchResult clrResourceSearchResult = new ClearanceResourceSearchResult();
            ClearanceResourceClient clearanceResourceClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgSearchR2Resource);
                clearanceResourceClient = new ClearanceResourceClient();
                clearanceResourceClient.Open();
                clrResourceSearchResult = clearanceResourceClient.SearchR2Resource(resourceSearch, userInfo);
                clearanceResourceClient.Close();
                return clrResourceSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
            }
        }

  

        //Get roles and rights for resource 
        public List<ContractDetails> GetResourceRights(long r2resourceId, LeanUserInfo userInfo)
        {
            ClearanceResourceClient clearanceResourceClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgGetResourceRights);
                List<ContractDetails> clrResourceSearchResult = new List<ContractDetails>();
                clearanceResourceClient = new ClearanceResourceClient();
                clearanceResourceClient.Open();
                clrResourceSearchResult = clearanceResourceClient.GetResourceRights(r2resourceId, userInfo);
                clearanceResourceClient.Close();
                return clrResourceSearchResult;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
            }
        }
        #region RemoveResourceProject
        /// Remove Resource Project
        /// <param name="clearanceresource"></param>
        /// <param name="userInfo"></param>
        /// </summary>
        /// 
        public bool RemoveResourceProject(string archiveFlag, List<long> listclrResourceId, LeanUserInfo userInfo)
        {
            ClearanceResourceClient clearanceResourceClient = null;
            try
            {
                _logFactory.LogWriter.Debug(_msgRemoveResourceProject);
                clearanceResourceClient = new ClearanceResourceClient();
                clearanceResourceClient.Open();
                bool result = clearanceResourceClient.RemoveResourceProject(archiveFlag, listclrResourceId, userInfo);
                clearanceResourceClient.Close();
                return result;

            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, MethodBase.GetCurrentMethod().Name), ex);
                throw;
            }
            finally
            {
                if (clearanceResourceClient != null && clearanceResourceClient.State == CommunicationState.Faulted)
                {
                    clearanceResourceClient.Abort();
                    clearanceResourceClient = null;
                }
            }
        }
        #endregion

    }
}
