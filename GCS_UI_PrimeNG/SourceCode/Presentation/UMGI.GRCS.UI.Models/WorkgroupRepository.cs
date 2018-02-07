/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : WorkgroupRepository.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * R.MuthuKumar      11/27/2012      Code cleaning and commenting
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.BusinessEntities.Interfaces;
using UMGI.GRCS.UI.Interfaces;
using System.Text;
using UMGI.GRCS.UI.Utilities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.UI.Proxies.UserService;
using UMGI.GRCS.UI.Proxies.WorkgroupService;
using UMGI.GRCS.UI.Proxies.GlobalService;
using UMGI.GRCS.UI.Proxies.ContractService;
using UMGI.GRCS.UI.Proxies.ClearanceProjectService;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Models
{
    public class WorkgroupRepository : IWorkgroupRepository
    {
        #region Variable Declarations

        #region Private Variables

        /// <summary>
        /// Declaration of readonly IServiceFactory used to intialize the service
        /// </summary>
        //readonly IServiceFactory _serviceFactory;
        /// <summary>
        /// Declaration of readonly IWorkgroup used to Get all workgroup service contracts
        /// </summary>
       // readonly IWorkgroup _workgroupService;
        /// <summary>
        /// Declaration of readonly IGlobal used to Get all global service contracts
        /// </summary>
      //  readonly IGlobal _globalService;
        /// <summary>
        /// Declaration of readonly IUser used to Get all user service contracts
        /// </summary>
    //    readonly IUser _userService;
        /// <summary>
        /// Declaration of readonly IContract used to Get all contract service contracts
        /// </summary>
    //   readonly IContract _contractService;
        /// <summary>
        /// This Collection is Use to assign the workgroup roles
        /// </summary>
        static List<Workgroup> _roleList;
        /// <summary>
        /// This Collection is Use to assign the workgroup request types
        /// </summary>
        static List<Workgroup> _requestTypeList;
        /// <summary>
        /// This Collection is Use to assign the workgroup resource types
        /// </summary>
        static List<KeyValuePair<int, string>> _resourceTypeList;
        /// <summary>
        /// Used to log the given details
        /// </summary>
        readonly ILogFactory _logFactory;
        /// <summary>
        /// Append ; with space
        /// </summary>
        const string _semiColonWithSpace = "; ";
        /// <summary>
        /// Used to append comma
        /// </summary>
        const string _addComma = ",";

        static List<KeyValuePair<int, string>> _resourceTypeListForResource;
        const string _resourceTypeAudio = "Audio";
        const string _resourceTypeVideo = "Video";
        const string _delete = "Delete";
        const string _manageWorkgroup = "ManageWorkgroup";
        const string _deactivate = "Deactivate";
        const string _workgroupName = "WorkgroupName";
        const string _company = "Company";
        const string _user = "User";
        const string _country = "Country";
        const int _pageSize = 200;
        const int _startIndex = 0;
       // IClearanceProjectRepository _clearanceProjectRepository;
        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set SessionWrapper use to store objects in session
        /// </summary>
        private ISessionWrapper SessionWrapper { get; set; }

        /// <summary>
        /// Used to get /set the ConfigFactory instance
        /// </summary>
      //  private IConfigFactory ConfigurationFactory { get; set; }

        /// <summary>
        /// Gets or sets the obj contract model.
        /// </summary>
        /// <value>The obj contract model.</value>
        public IWorkgroupModel WorkgroupModel { get; set; }

        /// <summary>
        /// User Information
        /// </summary>
        /// <value>The obj user info.</value>
        private UserInfo ObjUserInfo { get; set; }

        private IClearanceProjectModel IClearanceProjectModel { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the WorkgroupRepository class.
        /// </summary>
        public WorkgroupRepository(ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            try
            {
                _logFactory = logFactory;
               // _serviceFactory = serviceFactory;
                ObjUserInfo = new UserInfo();
               // _workgroupService = _serviceFactory.GetService<IWorkgroup>(Constants.WorkgroupService);
             //   _globalService = _serviceFactory.GetService<IGlobal>(Constants.GlobalService);
               // _userService = _serviceFactory.GetService<IUser>(Constants.UserService);
              //  _contractService = _serviceFactory.GetService<IContract>(Constants.ContractService);
                SessionWrapper = sessionWrapper;
                ObjUserInfo = SessionWrapper.CurrentUserInfo;
              //  ConfigurationFactory = configFactory;
                GetWorkGroupRelatedDetails(ObjUserInfo);
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "WorkgroupRepository"), ex);
                throw;
            }
        }

        #endregion

        #region Public Methods

        #region Manage Company

        /// <summary>
        /// Get list of companies for the given company Ids.
        /// </summary>
        /// <param name="companyIds">The companyIds</param>
        /// <param name="userLoginName">The userLoginName</param>
        /// <returns>Collection o company info</returns>
        public List<CompanyInfo> GetCompanies(string companyIds, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Companies Information");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetCompanies(companyIds, userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetCompanies"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }

        }

        /// <summary>
        /// Search workgroup Companies with the given parameters
        /// </summary>
        /// <param name="companySearchCriteria">companySearchCriteria to search</param>       
        /// <returns>Collection of Company details</returns>
        public List<CompanyInfo> GetCompaniesOfWorkgroup(CompanySearchCriteria companySearchCriteria)
        {
             WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Workgroup Company Initiated for Given Parameters");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue =  workgroupClient.GetCompaniesOfWorkgroup(companySearchCriteria);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetCompaniesOfWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
             finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search Companies with the given parameters
        /// </summary>
        /// <returns>Collection of company info</returns>
        public List<CompanyInfo> CompanySearch(string companyName, string isacCode, string country, int startIndex, int pageSize, string jtSorting, string userLoginName)
        {
            GlobalClient globalClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Company Search Method Initiated");
                globalClient = new GlobalClient();
                globalClient.Open();
                var returnValue = globalClient.CompanySearch(companyName, isacCode, country, startIndex, pageSize, jtSorting, userLoginName);
                globalClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "CompanySearch"), ex);
                globalClient.Abort();
                globalClient = null;
                throw;
            }
            finally
            {
                if (globalClient != null && globalClient.State == CommunicationState.Opened)
                {
                    globalClient.Close();
                    globalClient = null;
                }
            }
        }

        #endregion

        #region Manage Users

        /// <summary>
        /// Get workgroup user list contains the given userIds
        /// </summary>
        /// <param name="userIds">The userIds</param>
        /// <param name="userName">The userName</param>
        /// <returns>List of workgroup users</returns>
        public List<WorkGroupUser> GetWorkgroupUserList(string userIds, string userName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Workgroup User List");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetUsersList(userIds, userName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetWorkgroupUserList"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search users for the given parameters
        /// </summary>
        /// <param name="userSearchCriteria">The userSearchCriteria</param>
        /// <param name="targetApplication">The targetApplication</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>WorkGroupUser details</returns>
        public List<WorkGroupUser> GetUsers(WorkGroupUserSearchCriteria userSearchCriteria, AnaTargetApplication targetApplication, string userLoginName)
        {
          UserClient userClient = null;
            try
            {
                _logFactory.LogWriter.Debug("User Search Initaited");
                userClient = new UserClient();
                userClient.Open();
                var returnValue = userClient.GetUsers(userSearchCriteria, AnaTargetApplication.Gcs, userLoginName);
                userClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetUsers"), ex);
                userClient.Abort();
                userClient = null;
                throw;
            }
            finally
            {
                if (userClient != null && userClient.State == CommunicationState.Opened)
                {
                    userClient.Close();
                    userClient = null;
                }
            }
        }

        #endregion

        #region Manage WorkGroup

        /// <summary>
        /// Get role details
        /// </summary>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>List of workgroup instance with role details</returns>
        public List<Workgroup> GetRoles(string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Roles Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetRoles(userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetRoles"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search workgroup with the specified parameters
        /// </summary>
        /// <param name="workGroupSearchCriteria">The workGroupSearchCriteria</param>
        /// <returns>List of WorkgroupSearchResult</returns>
        public List<WorkgroupSearchResult> SearchWorkgroup(WorkgroupSearchCriteria workGroupSearchCriteria)
        {
             WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Search Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.SearchWorgroup(workGroupSearchCriteria);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SearchWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get workgroup details for the given workgroup id.
        /// </summary>
        /// <param name="workGroupId">The workGroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>Workgroup</returns>
        public Workgroup Workgroup(long workGroupId, string userName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug(Constants.MethodStart);
                _logFactory.LogWriter.Debug(string.Format("workgroupId: {0}", workGroupId));
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                Workgroup data = workgroupClient.SearchSingleWorgroup(workGroupId, userName);
                workgroupClient.Close();
                var modelData = new WorkgroupSearchResult(data);
                modelData = SetWorkGroupDetails(modelData);
                WorkgroupModel.Workgroup = modelData;
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodEnd, "Successfully retrieved the data"));
                return data;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "Workgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "Workgroup"), ex);
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get child workgroup details for the given child workgroup id
        /// </summary>
        /// <param name="workGroupId">The workGroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>ChildWorkgroup</returns>
        public ChildWorkgroup GetChildWorkgroup(long workGroupId, string userName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("View Child Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                ChildWorkgroup data = workgroupClient.SearchSingleChildWorgroup(workGroupId, userName);
                workgroupClient.Close();
                var modelData = new WorkgroupSearchResult(data);
                modelData = SetWorkGroupDetails(modelData);

                WorkgroupModel.Workgroup = modelData;
                WorkgroupModel.GetChildWorkgroup = data;
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodEnd, "Successfully retrieved the data"));
                return data;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetChildWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetChildWorkgroup"), ex);
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// This method to Save Workgroup
        /// </summary>
        /// <param name="workgroupDetails">Workgroup Details</param>
        /// <param name="userLoginName">User Information</param>
        public string AddWorkgroup(Workgroup workgroupDetails, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Create Parent Workgroup Save Method Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.AddWorkGroup(workgroupDetails, userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "AddWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Delete the workgroup from the specified workgroup id
        /// </summary>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>True if successfully deletes, otherwise false</returns>
        public bool DeleteWorkgroup(long workgroupId, string userInfo, DateTime modifiedDate)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Delete Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.DeleteWorkgroup(workgroupId, userInfo, modifiedDate);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "DeleteWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search child workgroups for the specified parent workgroup and search parameters
        /// </summary>
        /// <param name="parentId">The parentId</param>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="workGroupSearchCriteria">The workGroupSearchCriteria</param>
        /// <returns>List of WorkgroupSearchResult</returns>
        public List<WorkgroupSearchResult> GetWorkgroupByChild(long parentId, long workgroupId, WorkgroupSearchCriteria workGroupSearchCriteria)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get WorkgroupByChild Method Initiated For Specified WorkgroupIds");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetWorkgroupByChild(parentId, workgroupId, workGroupSearchCriteria);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetWorkgroupByChild"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Update the Workgroup details
        /// </summary>
        /// <param name="workgroupDetails">Workgroup Details</param>
        /// <param name="userLoginName">User Information</param>
        /// <returns>True if successfully updates, otherwise false</returns>
        public bool UpdateWorkGroup(Workgroup workgroupDetails, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Update Workgroup Method Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.UpdateWorkGroup(workgroupDetails, userLoginName);
                workgroupClient.Close();
                return returnValue; 
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "UpdateWorkGroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Request re-assign to the specified workgroup
        /// </summary>
        /// <param name="expectWorkgroupId">The expectWorkgroupId</param>
        /// <param name="assignedgtWorkgroupId">The assignedgtWorkgroupId</param>
        /// <param name="requestIds">The requestIds</param>
        /// <param name="userName">The userName</param>
        /// <returns>True/False</returns>
        public bool RequestReassignToWorkgroup(long expectWorkgroupId, long assignedgtWorkgroupId, string requestIds, string userName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Workgroup Request Reassign To New Workgroup Initaited");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.RequestReassignToWorkgroup(expectWorkgroupId, assignedgtWorkgroupId, requestIds, userName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "RequestReassignToWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// This method to Save Child Workgroup
        /// </summary>
        /// <param name="childWorkgroupDetails">ChildWorkgroup Details</param>
        public string AddChildWorkgroup(ChildWorkgroup childWorkgroupDetails)
        { 
             WorkgroupClient workgroupClient = null;
            try
            {
               _logFactory.LogWriter.Debug("Create Child Workgroup Method Initiated");
               workgroupClient = new WorkgroupClient();
               workgroupClient.Open();
               var returnValue = workgroupClient.AddChildWorkGroup(childWorkgroupDetails);
               workgroupClient.Close();
               return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "AddChildWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }
        /// <summary>
        /// Update Child workGroup
        /// </summary>
        /// <param name="childWorkgroupDetails">The childWorkgroupDetails</param>
        /// <returns>True/false</returns>
        public bool UpdateChildWorkGroup(ChildWorkgroup childWorkgroupDetails)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
               _logFactory.LogWriter.Debug("Update Child Workgroup Method Initiated");
               workgroupClient = new WorkgroupClient();
               workgroupClient.Open();
               var returnValue = workgroupClient.UpdateChildWorkGroup(childWorkgroupDetails);
               workgroupClient.Close();
               return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "UpdateChildWorkGroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search workgroup with the given parameters including contract id.
        /// </summary>
        /// <param name="workGroupSearchCriteria">The workGroupSearchCriteria</param>
        /// <param name="contractIds">The contractIds</param>
        /// <param name="userInfo">The userInfo</param> 
        /// <returns>List of workgroup details</returns>
        public virtual List<WorkgroupSearchResult> GetWorkgroups(WorkgroupSearchCriteria workGroupSearchCriteria, string contractIds)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
               _logFactory.LogWriter.Debug("Get Workgroup Method Initiated For Specified ContractIds");
               workgroupClient = new WorkgroupClient();
               workgroupClient.Open();
               var returnValue = workgroupClient.SearchWorgroupForContract(workGroupSearchCriteria, contractIds);
               workgroupClient.Close();
               return returnValue; 
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetWorkgroups"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search specified input
        /// </summary>
        /// <param name="suggestiveInput">workgroupInput</param>
        /// <param name="workgroupElement">workgroupElement</param>
        /// <param name="pageName">The pageName</param>
        /// <param name="workgroupId">The workgroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>Suggestive Search Result For Workgroup</returns>
        public List<string> SuggestiveSearchForWorkgroup(string suggestiveInput, string workgroupElement, string pageName, string workgroupId, string userLoginName)
        {
            var suggestiveList = new List<string>();
            WorkgroupClient workgroupClient = null;
            GlobalClient globalClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Suggestive Workgroup Search Initiated");
                _logFactory.LogWriter.Debug(string.Format("suggestiveInput: {0},workgroupElement: {1},pageName: {2},workgroupId: {3},", suggestiveInput, workgroupElement, pageName, workgroupId));
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                globalClient = new GlobalClient();
                globalClient.Open();
                switch (workgroupElement)
                {
                    case _workgroupName:
                        suggestiveList = workgroupClient.GetWorkgroupNamesForAutoComplete(suggestiveInput, userLoginName);
                        break;
                    case _company:
                        if ((string.Compare(pageName, Utilities.Constants.MaintainParentWorkgroupView, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(pageName, Utilities.Constants.CreateParentWorkgroupView, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(pageName, _manageWorkgroup, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(pageName, _delete, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(pageName, _deactivate, StringComparison.OrdinalIgnoreCase) == 0) || (string.Compare(pageName, Utilities.Constants.AddRemoveUsers, StringComparison.OrdinalIgnoreCase) == 0))
                        {
                            suggestiveList = globalClient.GetCompaniesForAutoComplete(suggestiveInput, userLoginName);
                        }
                        else
                        {
                            var companySearchCriteria = new CompanySearchCriteria
                            {
                                Name = suggestiveInput,
                                IsacCode = string.Empty,
                                CountryName = string.Empty,
                                WorkGroupId = Convert.ToInt64(workgroupId),
                                StartIndex = _startIndex,
                                PageSize = _pageSize,
                                SortField = string.Empty,
                                UserLoginName = userLoginName
                            };
                            List<CompanyInfo> companyList = workgroupClient.GetCompaniesOfWorkgroup(companySearchCriteria);
                            foreach (var item in companyList)
                            {
                                suggestiveList.Add(item.Name);
                            }
                        }
                        break;
                    case _user:
                        suggestiveList = workgroupClient.GetUsersForAutoComplete(suggestiveInput, userLoginName);
                        break;
                    case _country:
                        suggestiveList = globalClient.GetCountriesForAutoComplete(suggestiveInput, userLoginName);
                        break;
                }
                _logFactory.LogWriter.Debug("Suggestive Workgroup Search Successfully Completed");
                workgroupClient.Close();
                globalClient.Close();
                return suggestiveList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SuggestiveSearchForWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                globalClient.Abort();
                globalClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
                if (globalClient != null && globalClient.State == CommunicationState.Opened)
                {
                    globalClient.Close();
                    globalClient = null;
                }
            }
        }

        /// <summary>
        /// Link Artist Contract To Workgroup
        /// </summary>
        ///  <param name="contractIds">Contract Ids</param>
        /// <param name="workgroupIds">Workgroup Ids</param>
        ///  <param name="userInfo">UserInfo</param>
        /// <returns>True if success, otherwise false</returns>
        public List<long> LinkArtistContractToWorkgroup(string contractIds, string workgroupIds, string userInfo)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Link Artist Contract To Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                List<long> returnValue = workgroupClient.LinkArtistContractToWorkgroup(contractIds, workgroupIds, userInfo);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "LinkArtistContractToWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Link Resource Contract To Workgroup
        /// </summary>
        ///  <param name="deviationResourceContract">DeviationResourceContract</param>
        /// <param name="workgroupIds">Workgroup Ids</param>
        ///  <param name="userInfo">UserInfo</param>
        /// <returns>True if success, otherwise false</returns>
        public List<long> LinkResourceContractToWorkgroup(List<DeviationResourceContract> deviationResourceContract, string workgroupIds, string userInfo)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Link Resource Contract To Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                List<long> returnValue = workgroupClient.LinkResourceContractToWorkgroup(deviationResourceContract, workgroupIds, userInfo);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "LinkResourceContractToWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get all request types
        /// </summary>
        /// <returns>List of request types</returns>
        public List<Workgroup> GetRequestType(string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Request Type Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetRequestType(userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetRequestType"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get pending request for the given workgroup id
        /// </summary>
        /// <param name="workGroupId">The workGroupId</param>     
        /// <returns>List of request</returns>
        public List<Request> DeactivateWorkGroup(RequestSearch requestSearch)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get WorkGroup Pending Request Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.DeactivateWorkGroup(requestSearch);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetPendingRequest"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search workgroup with the specified parameters
        /// </summary>
        /// <param name="workGroupSearchCriteria">The workGroupSearchCriteria</param>
        /// <param name="loginName">The loginName</param>
        /// <returns>List of WorkgroupSearchResult</returns>
        public List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string loginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Search Workgroup For Remove Users Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.SearchWorkgroupForRemoveUsers(workGroupSearchCriteria, loginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SearchWorkgroupForRemoveUsers"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Search workgroup with the specified parameters
        /// </summary>
        /// <param name="workGroupSearchCriteria">The workGroupSearchCriteria</param>
        /// <param name="userData">The userData</param>
        /// <returns>List of WorkgroupSearchResult</returns>
        public List<WorkgroupSearchResult> SearchWorkgroupToAddUsers(WorkgroupSearchCriteria workGroupSearchCriteria, string userLoginName, string searchLoginId)
        {
             WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Search Workgroup For Add Users Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.SearchWorkgroupToAddUsers(workGroupSearchCriteria, userLoginName, searchLoginId);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "SearchWorkgroupToAddUsers"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Add User In Multiple Workgroup
        /// </summary>
        /// <param name="workgroupDetails">The workgroupDetails</param>
        /// <param name="userData">The workgroupIdList</param>
        /// <param name="userLoginName">The userLoginName</param>
        /// <returns>True/False</returns>
        public bool AddUserInMultipleWorkgroup(List<Workgroup> workgroupDetails, WorkGroupUser userData, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Search Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.AddUserInMultipleWorkgroup(workgroupDetails, userData, userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "AddUserInMultipleWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }
        /// <summary>
        /// Remove User From Multiple Workgroup
        /// </summary>
        /// <param name="userid">The userid</param>
        /// <param name="workgroupIdList">The workgroupIdList</param>
        /// <param name="userLoginName">The userLoginName</param>
        /// <returns>Workgroup instance</returns>
        public List<string> RemoveUserFromMultipleWorkgroup(string userToRemove, string workgroupIdList, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Search Workgroup Initiated");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.RemoveUserFromMultipleWorkgroup(userToRemove, workgroupIdList, userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "RemoveUserFromMultipleWorkgroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        #endregion

        #region Manage Territories/Countries

        /// <summary>
        /// Get the collection of territories/countries
        /// </summary>
        /// <returns>TerritorialDisplay list</returns>
        public List<TerritorialDisplay> GetTerritories()
        {
            GlobalClient globalClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Territory Details Initiated");
                globalClient = new GlobalClient();
                globalClient.Open();
                var returnValue = globalClient.GetTerritories();
                globalClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetTerritories"), ex);
                throw;
            }
        }

        /// <summary>
        /// Get list of territories/countries for the specified workgroup
        /// </summary>
        /// <param name="workGroupId">The workGroupId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>TerritorialDisplay list</returns>
        public List<TerritorialDisplay> GetTerritoriesForWorkGroup(long workGroupId, string userLoginName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Territories Details For Given WorkGroupId");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetTerritories(workGroupId, userLoginName);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetTerritoriesForWorkGroup"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        #endregion

        #region Manage Contract

        /// <summary>
        /// Get contracts for the specified artist
        /// </summary>
        /// <param name="artistId">The artistId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>List of contracts</returns>
        public List<LeanContractInfo> GetContractsByArtist(long artistId, string userLoginName)
        {
            ContractClient contractClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Contract Information For Given ArtistId");
                contractClient = new ContractClient();
                contractClient.Open();
                List<LeanContractInfo> data = contractClient.GetContractsByArtist(artistId, userLoginName);
                contractClient.Close();
                WorkgroupModel.ContractsList = new List<LeanContractInfo>();

                foreach (var contractList in data)
                {
                    if (contractList.ContractTerritoryList.Count > 0)
                    {
                        contractList.TerritoryData = GetTerritoryNames(contractList.ContractTerritoryList);
                    }
                    WorkgroupModel.ContractsList.Add(contractList);
                }
                _logFactory.LogWriter.Debug("Successfully Retrieved The Contract Details For Given ArtistId");
                return WorkgroupModel.ContractsList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetContractsByArtist"), ex);
                contractClient.Abort();
                contractClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetContractsByArtist"), ex);
                throw;
            }
            finally
            {
                if (contractClient != null && contractClient.State == CommunicationState.Opened)
                {
                    contractClient.Close();
                    contractClient = null;
                }
            }
        }

        /// <summary>
        /// Get contract for the given contract id
        /// </summary>
        /// <param name="contractId">The contractId</param>
        /// <param name="userInfo">The userInfo</param>
        public void GetLeanContract(long contractId, string userLoginName)
        {
            ContractClient contractClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Contract Information For Given contractId");
                contractClient = new ContractClient();
                contractClient.Open();
                LeanContractInfo data = contractClient.GetLeanContract(contractId, userLoginName);
                contractClient.Close();
                WorkgroupModel.LeanContract = new LeanContractInfo();
                if (data.ContractTerritoryList.Count > 0)
                {
                    data.TerritoryData = GetTerritoryNames(data.ContractTerritoryList);
                }
                WorkgroupModel.ContractsList.Add(data);
                _logFactory.LogWriter.Debug("Successfully Retrieved The Contract Details For Given contractId");
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetLeanContract"), ex);
                contractClient.Abort();
                contractClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetLeanContract"), ex);
                throw;
            }
            finally
            {
                if (contractClient != null && contractClient.State == CommunicationState.Opened)
                {
                    contractClient.Close();
                    contractClient = null;
                }
            }
        }

        /// <summary>
        /// Get contracts for the given resource
        /// </summary>
        /// <param name="resourceId">The resourceId</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>List of Contracts</returns>
        public List<LeanContractInfo> GetContractsByResource(long resourceId, string userLoginName)
        {
            ContractClient contractClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Contract Information For Given ResourceId");
                contractClient = new ContractClient();
                contractClient.Open();
                List<LeanContractInfo> data = contractClient.GetContractsByResource(resourceId, userLoginName);
                contractClient.Close();
                WorkgroupModel.ContractsList = new List<LeanContractInfo>();

                foreach (var contractList in data)
                {
                    if (contractList.ContractTerritoryList.Count > 0)
                    {
                        contractList.TerritoryData = GetTerritoryNames(contractList.ContractTerritoryList);
                    }
                    WorkgroupModel.ContractsList.Add(contractList);
                }
                _logFactory.LogWriter.Debug("Successfully Retrieved The Contract Details For Given ResourceId");
                return WorkgroupModel.ContractsList;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetContractsByResource"), ex);
                contractClient.Abort();
                contractClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetContractsByResource"), ex);
                throw;
            }
            finally
            {
                if (contractClient != null && contractClient.State == CommunicationState.Opened)
                {
                    contractClient.Close();
                    contractClient = null;
                }
            }
        }

        #endregion


        #region User Preferences

        /// <summary>
        /// Get email preferences
        /// </summary>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>List of Preferences</returns>
        public List<Preferences> GetEmailPreference(string userInfo)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Companies Information");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetEmailPreferences(userInfo);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetCompanies"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get user prference for the userId
        /// </summary>
        /// <param name="userId">The userId</param>
        /// <returns>List of UserPreference</returns>
        public List<UserPreference> GetUserPreferences(long userId)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Companies Information");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetUserPreferences(userId);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetCompanies"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Add user Preferences
        /// </summary>
        /// <param name="UserPreferenceList">The UserPreferenceList</param>
        /// <param name="userInfo">The userInfo</param>
        /// <returns>True/false</returns>
        public bool InsertUserPrefernces(List<UserPreference> UserPreferenceList, long userId, string loginUserName)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("InsertUserPrefernces");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.InsertUserPreferences(UserPreferenceList, userId, loginUserName);
                workgroupClient.Close();
                return true;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "InsertUserPrefernces"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        #endregion

        # region "Get list of reviewer workgroup based on userId"

        /// <summary>
        /// Get list of reviewer workgroup based on userId
        /// </summary>
        /// <param name="userId">The userId</param>
        /// <returns>List of WorkgroupBase</returns>
        public List<WorkgroupBase> GetWorkgroups(long userId)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Companies Information");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                var returnValue = workgroupClient.GetWorkgroups(userId);
                workgroupClient.Close();
                return returnValue;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetCompanies"), ex);
                workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
        }

        /// <summary>
        /// Get GetClrPACompanyAndCurrencyUserList
        /// </summary>
        /// <returns>Company And Currency</returns>
        public IClearanceProjectModel GetClrPACompanyAndCurrencyUserList(string userLoginName)
        {
            ClearanceProjectClient clearanceProjectClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get ClrPA Company And Currency list for User");
                List<ClearanceMasterData> _dropDownList = null;
                IClearanceProjectModel = new ClearanceProjectModel();
                clearanceProjectClient = new ClearanceProjectClient();
                clearanceProjectClient.Open();
                //if (_dropDownList == null)
                _dropDownList = clearanceProjectClient.GetClearanceProjectDropDownByUserList(userLoginName);
                IClearanceProjectModel.CurrencyList = _dropDownList.Where(item => item.Type == "Currency").Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) }).OrderBy(item => item.Text);
                IClearanceProjectModel.CompanyList = _dropDownList.Where(item => item.Type == "Company").Select(ClearanceMasterData => new SelectListItem { Text = ClearanceMasterData.Description, Value = ClearanceMasterData.Value.ToString(CultureInfo.InvariantCulture) }).OrderBy(item => item.Text);
                clearanceProjectClient.Close();
                return IClearanceProjectModel;
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "Get ClrPA Company And Currency list for User"), ex);
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetClrPACompanyAndCurrencyUserList"), ex);
                throw;
            }
            finally
            {
                if (clearanceProjectClient != null && clearanceProjectClient.State == CommunicationState.Faulted)
                {
                    clearanceProjectClient.Abort();
                    clearanceProjectClient = null;
                }
            }
           
        }



        # endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Roles,Request and Resource types then set into WorkgroupModel
        /// </summary>
        /// <param name="userInfo">The userInfo</param>
        private void GetWorkGroupRelatedDetails(UserInfo userInfo)
        {
            WorkgroupClient workgroupClient = null;
            try
            {
                _logFactory.LogWriter.Debug("Get Workgroup Related Data");
                workgroupClient = new WorkgroupClient();
                workgroupClient.Open();
                WorkgroupModel = new WorkgroupModel();
                if (_roleList == null)
                    _roleList = workgroupClient.GetRoles(userInfo.UserName);
                var rccAdminMatch = _roleList.Find(rccAdmin => rccAdmin.RoleName == Constants.RCCAdmin);
                _logFactory.LogWriter.Debug("Get the RolesList");
                if (rccAdminMatch != null)
                    _roleList.Remove(rccAdminMatch);
                WorkgroupModel.RolesList = _roleList.Select(workgroup => new SelectListItem { Text = workgroup.RoleName, Value = workgroup.RoleID.ToString(CultureInfo.InvariantCulture) });
                _logFactory.LogWriter.Debug("Get the RequestType");
                if (_requestTypeList == null)
                {
                    _requestTypeList = workgroupClient.GetRequestType(userInfo.UserName);
                }
                WorkgroupModel.RequestTypeList = _requestTypeList.Select(workgroup => new SelectListItem { Text = workgroup.RequestTypeName, Value = workgroup.RequestTypeID.ToString(CultureInfo.InvariantCulture) });
                _logFactory.LogWriter.Debug("Get the ResourceType");
                if (_resourceTypeList == null)
                {
                    _resourceTypeList = new List<KeyValuePair<int, string>>();
                    _resourceTypeListForResource = workgroupClient.GetResourceType(userInfo.UserName);

                    foreach (var uniqueReourceType in _resourceTypeListForResource)
                    {
                        var listResource = new KeyValuePair<int, string>(uniqueReourceType.Key, uniqueReourceType.Value);
                        if (listResource.Value == _resourceTypeAudio || listResource.Value == _resourceTypeVideo)
                        {
                            _resourceTypeList.Add(listResource);
                        }
                    }
                }
                WorkgroupModel.ResourceTypeList =
                    _resourceTypeList.Select(
                   resourceType => new SelectListItem { Text = resourceType.Value, Value = (resourceType.Key.ToString()) });
                workgroupClient.Close();
                _logFactory.LogWriter.Debug("Successfully Retrieved The Workgroup Data");
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetWorkGroupRelatedDetails"), ex);
              workgroupClient.Abort();
                workgroupClient = null;
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetWorkGroupRelatedDetails"), ex);
                throw;
            }
            finally
            {
                if (workgroupClient != null && workgroupClient.State == CommunicationState.Opened)
                {
                    workgroupClient.Close();
                    workgroupClient = null;
                }
            }
           
        }

        /// <summary>
        /// Get territory/Country Names from the specified territories/countries
        /// </summary>
        /// <param name="territories">The territory/Country collection</param>
        /// <returns>concatenated territory/country names with comma</returns>
        private string GetTerritoryNames(List<TerritorialDisplay> territories)
        {
            string namesOfTerritory = string.Empty;
            try
            {
                _logFactory.LogWriter.Debug("Get Territory Names Initiated");
                var territoryNames = new StringBuilder();
                foreach (var territory in territories)
                {
                    territoryNames.Append(territory.Name).Append(_addComma);
                }
                var territoryNamesList = territoryNames.ToString();
                if (!string.IsNullOrEmpty(territoryNamesList))
                {
                    namesOfTerritory = territoryNamesList.Remove(territoryNamesList.Length - 1, 1);
                }
                _logFactory.LogWriter.Debug("Successfully Retrieved The Territory Names");
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.ExceptionWithinMethod, "GetTerritoryNames"), ex);
                throw;
            }
            return namesOfTerritory;
        }

        /// <summary>
        /// Set result to model
        /// </summary>
        /// <param name="modelData">The WorkgroupSearchResult</param>
        /// <returns>WorkgroupSearchResult</returns>
        private WorkgroupSearchResult SetWorkGroupDetails(WorkgroupSearchResult modelData)
        {
            try
            {
                _logFactory.LogWriter.Debug("Get the Countries");
                if (modelData.Countries.Any())
                {
                    var countryNames = new StringBuilder();
                    foreach (var singleCountry in modelData.Countries)
                    {
                        countryNames.Append(singleCountry.CountryDetails.CountryName).Append(_semiColonWithSpace);
                    }
                    modelData.Country = countryNames.ToString().Remove(countryNames.ToString().Length - 1, 1);
                }
                _logFactory.LogWriter.Debug("Get the User");
                if (modelData.Users.Any())
                {
                    var userNames = new StringBuilder();
                    var defaultUserNames = new StringBuilder();
                    foreach (var singleUser in modelData.Users)
                    {
                        userNames.Append(singleUser.Name).Append(_semiColonWithSpace);
                        if (singleUser.IsUserDefault)
                        {
                            defaultUserNames.Append(singleUser.Name).Append(_semiColonWithSpace);
                        }
                    }
                    modelData.User = userNames.ToString().Remove(userNames.ToString().Length - 1, 1);
                    WorkgroupModel.DefaultUserName = defaultUserNames.ToString();
                }
                _logFactory.LogWriter.Debug("Get Child Workgroups");
                if (modelData.ChildWorkgroups.Any())
                {
                    var childWorkGroupNames = new StringBuilder();
                    foreach (var childName in modelData.ChildWorkgroups)
                    {
                        childWorkGroupNames.Append(childName.Value).Append(_semiColonWithSpace);
                    }
                    modelData.ChildWorkgroupNames = childWorkGroupNames.ToString().Remove(childWorkGroupNames.ToString().Length - 1, 1);
                }
                return modelData;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
