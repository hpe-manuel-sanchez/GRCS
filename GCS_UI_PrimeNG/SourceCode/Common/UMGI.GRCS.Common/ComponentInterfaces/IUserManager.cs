using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IUserManager
    {
        GrsAuthentication GetTasksAndRoles(string userName, AnaTargetApplication application);

        /// <summary>
        /// Gets the companies.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        List<long> GetCompanies(string userName, AnaTargetApplication application);

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        string GetDisplayName(string userLoginName);

        /// <summary>
        /// Gets the companies by task.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        List<long> GetCompaniesByTask(string userName, GrsTasks task);

        /// <summary>
        /// Gets the user permissions.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="applicationName">Name of the application.</param>
        /// <returns></returns>
        GrsAuthentication GetUserPermissions(string userName, AnaTargetApplication applicationName);

        /// <summary>
        /// Gets the country ids by task.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <param name="tasks">The tasks.</param>
        /// <returns></returns>
        List<long> GetCountryIdsByTask(string userLoginName, GrsTasks tasks);

        /// <summary>
        /// Resets the user hash key.
        /// </summary>
        void ResetUserHashKey();

        /// <summary>
        /// Determines whether [has super user role] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="anaTargetApplication">The ana target application.</param>
        /// <returns>
        /// 	<c>true</c> if [has super user role] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        bool HasSuperUserRole(string userName, AnaTargetApplication anaTargetApplication);

        /// <summary>
        /// Determines whether [has any task] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="anaTargetApplication">The ana target application.</param>
        /// <param name="grsTasks">The GRS tasks.</param>
        /// <returns>
        /// 	<c>true</c> if [has any task] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        bool HasAnyTask(string userName, AnaTargetApplication anaTargetApplication, GrsTasks grsTasks);

        /// <summary>
        /// Gets the user info.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        UserDetails GetUserInfo(string userLoginName);

        /// <summary>
        /// Gets the userId associated with provided user login name.
        /// </summary>
        /// <param name="userLoginName">The user login name.</param>
        /// <returns></returns>
        long GetId(string userLoginName);

        Dictionary<string, string> GetDisplayName(IEnumerable<string> loginNames);

        List<long> GetAnACompanies(string userName, GrsTasks grsTasks);

        List<long> GetAnACountries(string userName, GrsTasks grsTasks);

        GcsAuthentication GetGcsTasksAndRoles(string userName, AnaTargetApplication application);

        List<WorkGroupUser> Getusers(WorkGroupUserSearchCriteria userSearchCriteria, AnaTargetApplication targetApplication);
    }
}