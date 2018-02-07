using System;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Interfaces;
using System.ServiceModel;
using UMGI.GRCS.UI.Proxies.UserService;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Models
{
    public partial class UserRepository : IUserRepository
    {
        #region Variable Declarations

        #region Private Variables

        /// <summary>
        /// Declaration of readonly IServiceFactory used to intialize the service
        /// </summary>
       // readonly IServiceFactory _serviceFactory;
        /// <summary>
        /// Declaration of readonly IUser used to Get all user service contracts
        /// </summary>
       // readonly IUser _userService;
        /// <summary>
        /// Used for logging
        /// </summary>
        readonly ILogFactory _logFactory;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Intializes a new instance of the class
        /// </summary>
        /// <param name="serviceFactory">The serviceFactory</param>
        /// <param name="logFactory">The logFactory</param>
        public UserRepository(ILogFactory logFactory)
        {
            try
            {
               // _serviceFactory = serviceFactory;
                _logFactory = logFactory;
               // _userService = _serviceFactory.GetService<IUser>(Constants.UserService);
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetGcsAuthorizationByLoginNameAndApplication"), ex);
                throw;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "UserRepository"), ex);
                throw;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the authorization by login name and application.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>GcsAuthentication</returns>
        public GrsAuthentication GetAuthorizationByLoginNameAndApplication(UserInfo userInfo)
        {
            UserClient userClient = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart, "GetAuthorizationByLoginNameAndApplication"));
                userClient = new UserClient();
                userClient.Open();
                var returnValue = userClient.GetAuthorizationByLoginNameAndApplication(userInfo.UserLoginName, AnaTargetApplication.Grcs);
                userClient.Close();
                return returnValue;
            }
            catch (EndpointNotFoundException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetAuthorizationByLoginNameAndApplication"), ex);
                throw new Exception(Constants.UserServiceFail);
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetAuthorizationByLoginNameAndApplication"), ex);
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
    }
    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial class UserRepository 
    {
        /// <summary>
        /// Gets the authorization by login name and application.
        /// </summary>
        /// <param name="userInfo">The user info.</param>
        /// <returns>GcsAuthentication</returns>
        public GcsAuthentication GetGcsAuthorizationByLoginNameAndApplication(UserInfo userInfo)
        {
            UserClient userClient = null;
            try
            {
                _logFactory.LogWriter.Debug(string.Format(Constants.MethodStart, "GetGcsAuthorizationByLoginNameAndApplication"));
                userClient = new UserClient();
                userClient.Open();
                var returnValue = userClient.GetGcsAuthorizationByLoginNameAndApplication(userInfo);
                userClient.Close();
                return returnValue;
            }
            catch (EndpointNotFoundException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetGcsAuthorizationByLoginNameAndApplication"), ex);
                throw new Exception(Constants.UserServiceFail);
            }
            catch (FaultException ex)
            {
                _logFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "GetGcsAuthorizationByLoginNameAndApplication"), ex);
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

    }

}
