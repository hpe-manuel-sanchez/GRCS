using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IUserData
    {
        /// <summary>
        /// Gets the company Ids for the given country ids.
        /// </summary>
        /// <param name="countryIds">The country ids.</param>
        /// <returns></returns>
        List<long> GetCompanies(List<long> countryIds);

        /// <summary>
        /// Gets the companies filtered with existing database.
        /// </summary>
        /// <param name="roleSpecificCompanyIds">The role specific company ids.</param>
        /// <returns></returns>
        List<long> GetCompaniesFilteredWithExistingDatabase(List<long> roleSpecificCompanyIds);

        /// <summary>
        /// Gets the user hash key.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="hashKey">The hash key.</param>
        /// <returns></returns>
        string GetUserHashKeyWithSecurityObject(string userName, out string hashKey);

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        string GetDisplayName(string userLoginName);


        /// <summary>
        /// Gets the user info.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        UserDetails GetUserInfo(string userLoginName);

        /// <summary>
        /// Logs the user.
        /// </summary>
        /// <param name="userInformation">The user information.</param>
        void UpdateUser(UserDetails userInformation);

        /// <summary>
        /// Determines whether [is user exists] [the specified login name].
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns>
        /// 	<c>true</c> if [is user exists] [the specified login name]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserExists(string userLoginName);

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="userDetails">The user details.</param>
        void AddUser(UserDetails userDetails);



        /// <summary>
        /// Gets the serialized GRS authentication from DB
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        string GetSerializedGrsAuthentication(string userLoginName);

        /// <summary>
        /// Gets the countries filtered with existing database.
        /// </summary>
        /// <param name="countryIds">The country ids.</param>
        /// <returns></returns>
        List<long> GetCountriesFilteredWithExistingDatabase(List<long> countryIds);

        /// <summary>
        /// Resets the User Hash Key
        /// </summary>
        void ResetUserHashKey();

        /// <summary>
        /// Gets the id of the user identified by user login name.
        /// </summary>
        /// <param name="userLoginName">Name of the user login.</param>
        /// <returns></returns>
        long GetUserId(string userLoginName);

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="loginNames">The login names.</param>
        /// <returns></returns>
        Dictionary<string, string> GetDisplayName(IEnumerable<string> loginNames);

        /// <summary>
        /// Gets AnA companies.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="grsTasks">The GRS tasks.</param>
        /// <returns></returns>
        List<long> GetAnACompanies(string userName, GrsTasks grsTasks);

        List<long> GetAnACountries(string userName, GrsTasks grsTasks);
    }
}
