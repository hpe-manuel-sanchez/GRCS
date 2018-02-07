/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GrsAuthentication.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * Rengaraj          03-Aug-2012     modified code for coding standard Review                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the GrsAuthentication entities.                                      
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    [DataContract]
    [Serializable]
    public class GrsAuthentication : EntityInformation
    {
        /// <summary>
        /// Gets or sets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        [DataMember]
        public string HashCode { get; set; }

        /// <summary>
        /// GRS permission
        /// </summary>
        [DataMember]
        public GrsPermission[] Permissions { get; set; }

        /// <summary>
        /// Target Application
        /// </summary>
        [DataMember]
        public AnaTargetApplication TargetApplication { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        [DataMember]
        public new string UserName { get; set; }

        /// <summary>
        /// check Authorized
        /// </summary>
        [DataMember]
        public bool IsAuthorized { get; set; }


        /// <summary>
        /// Gets or sets the application user.
        /// </summary>
        /// <value>The application user.</value>
        [DataMember]
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets the work flow ids by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public List<long> GetWorkFlowIdsByTask(GrsTasks task)
        {
            var workFlowIds = new List<long>();
            if (Permissions != null)
            {
                foreach (var workFlowId in from permission in Permissions
                                           where permission != null
                                           where (permission.Tasks & task) == task
                                           where permission.WorkFlowIds != null
                                           from workFlowId in permission.WorkFlowIds
                                           where !workFlowIds.Contains(workFlowId)
                                           select workFlowId)
                {
                    workFlowIds.Add(workFlowId);
                }
            }
            return workFlowIds;
        }

        /// <summary>
        /// Gets the contract status ids by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public List<long> GetContractStatusIdsByTask(GrsTasks task)
        {
            var contractStatusIds = new List<long>();
            if (Permissions != null)
            {
                foreach (var contractStatusId in from permission in Permissions
                                                 where permission != null
                                                 where (permission.Tasks & task) == task
                                                 where permission.ContractStatusIds != null
                                                 from contractStatusId in permission.ContractStatusIds
                                                 where !contractStatusIds.Contains(contractStatusId)
                                                 select contractStatusId)
                {
                    contractStatusIds.Add(contractStatusId);
                }
            }
            return contractStatusIds;
        }

        /// <summary>
        /// Gets the countries by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public List<long> GetCountryIdsByTask(GrsTasks task)
        {
            var countryList = new List<long>();
            if (Permissions != null)
            {
                foreach (var countries in from permission in Permissions
                                                 where permission != null
                                                 where (permission.Tasks & task) == task
                                                 where permission.CountryIds != null
                                                 from countryId in permission.CountryIds
                                                 where !countryList.Contains(countryId)
                                                 select countryId)
                {
                    countryList.Add(countries);
                }
                foreach (var country in from permission in Permissions
                                                 where permission != null
                                                 where (permission.Tasks & task) == task
                                                 where permission.CompanyIds != null
                                                 from companyId in permission.CompanyIds
                                                 where !countryList.Contains(companyId.Value)
                                                 select companyId.Value)
                {
                    countryList.Add(country);
                }
            }

            return countryList;
        }

        /// <summary>
        /// Gets the companies by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public Dictionary<long, long> GetCompanyIdsByAnyTask(GrsTasks task)
        {
            var companyList = new Dictionary<long, long>();
            if (Permissions != null)
            {
                foreach (var companyId in from permission in Permissions
                                          where permission != null
                                          where (permission.Tasks & task) != GrsTasks.None
                                          where permission.CompanyIds != null
                                          from company in permission.CompanyIds
                                          where !companyList.ContainsKey(company.Key)
                                          select company)
                {
                    companyList.Add(companyId.Key, companyId.Value);
                }
            }

            return companyList;
        }
        /// <summary>
        /// Gets the countries by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public List<long> GetCountryIdsByAnyTask(GrsTasks task)
        {
            var countryList = new List<long>();
            if (Permissions != null)
            {
                foreach (var contractStatusId in from permission in Permissions
                                                 where permission != null
                                                 where (permission.Tasks & task) != GrsTasks.None
                                                 where permission.CountryIds != null
                                                 from countryId in permission.CountryIds
                                                 where !countryList.Contains(countryId)
                                                 select countryId)
                {
                    countryList.Add(contractStatusId);
                }
            }

            return countryList;
        }

        /// <summary>
        /// Gets the companies by task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public Dictionary<long, long> GetCompanyIdsByTask(GrsTasks task)
        {
            var companyList = new Dictionary<long, long>();
            if (Permissions != null)
            {
                foreach (var companyId in from permission in Permissions
                                          where permission != null
                                          where (permission.Tasks & task) == task
                                          where permission.CompanyIds != null
                                          from company in permission.CompanyIds
                                          where !companyList.ContainsKey(company.Key)
                                          select company)
                {
                    companyList.Add(companyId.Key, companyId.Value);
                }
            }

            return companyList;
        }


        /// <summary>
        /// Gets the Role for the given country.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <returns></returns>
        private GrsPermission GetPermissionForCountry(long countryId)
        {
            if (Permissions != null)
            {
                return
                    Permissions.Where(permission => permission.CountryIds != null).FirstOrDefault(
                        permission => permission.CountryIds.Contains(countryId));
            }
            return null;
        }

        #region Methods that provides all Dags irrespective of Tasks

        /// <summary>
        /// Gets all companies in the DAG for all Roles.
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, long> GetCompanies()
        {
            var allCompanies = new Dictionary<long, long>();
            if (Permissions != null)
                foreach (var companyDag in from permission in Permissions
                                           where permission.CompanyIds != null
                                           from companyDag in permission.CompanyIds
                                           where !allCompanies.ContainsKey(companyDag.Key)
                                           select companyDag)
                {
                    allCompanies.Add(companyDag.Key, companyDag.Value);
                }
            return allCompanies;
        }

        /// <summary>
        /// Gets all countries in the DAG for all Roles.
        /// </summary>
        /// <returns></returns>
        public List<long> GetCountries()
        {
            var allCountries = new List<long>();
            if (Permissions != null)
                foreach (var country in from permission in Permissions
                                        where permission.CountryIds != null
                                        from country in permission.CountryIds
                                        where !allCountries.Contains(country)
                                        select country)
                {
                    allCountries.Add(country);
                }
            return allCountries;
        }

        /// <summary>
        /// Gets all work flow status.
        /// </summary>
        /// <returns></returns>
        public List<long> GetAllWorkFlowStatus()
        {
            var allWorkFlow = new List<long>();
            if (Permissions != null)
                foreach (var workFlow in from permission in Permissions
                                         where permission.WorkFlowIds != null
                                         from workFlowId in permission.WorkFlowIds
                                         where !allWorkFlow.Contains(workFlowId)
                                         select workFlowId)
                {
                    allWorkFlow.Add(workFlow);
                }
            return allWorkFlow;
        }

        /// <summary>
        /// Gets all contract status.
        /// </summary>
        /// <returns></returns>
        public List<long> GetAllContractStatus()
        {
            var allContractStatus = new List<long>();
            if (Permissions != null)
                foreach (var contractStatus in from permission in Permissions
                                               where permission.ContractStatusIds != null
                                               from contractStatusId in permission.ContractStatusIds
                                               where !allContractStatus.Contains(contractStatusId)
                                               select contractStatusId)
                {
                    allContractStatus.Add(contractStatus);
                }
            return allContractStatus;
        }

        #endregion

        /// <summary>
        /// Gets the Role for the given company.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns></returns>
        private GrsPermission GetPermissionForCompany(long companyId)
        {
            if (Permissions != null)
            {
                return
                    Permissions.Where(permission => permission.CompanyIds != null).FirstOrDefault(
                        permission => permission.CompanyIds.ContainsKey(companyId));
            }
            return null;
        }

        /// <summary>
        /// Gets the Role for the given company.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns>List of GrsPermission</returns>
        private IEnumerable<GrsPermission> GetPermissionsForCompany(long companyId)
        {
            var permissions = new List<GrsPermission>();

            if (Permissions != null)
            {
                foreach (var roles in Permissions.Where(roles => roles.CompanyIds.Any()).Where(roles => roles.CompanyIds.ContainsKey(companyId)))
                {
                    permissions.Add(roles);
                    break;
                }
            }
            return permissions;
        }


        /// <summary>
        /// Gets the tasks for company.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <returns>GrsTasks</returns>
        public GrsTasks GetTasksForCompany(long companyId)
        {
            var permissions = GetPermissionsForCompany(companyId);
            return permissions.Aggregate(GrsTasks.None, (current, grsPermission) => current | grsPermission.Tasks);

        }

        /// <summary>
        /// Gets the company ids by individual tasks.
        /// </summary>
        /// <param name="grsTasks">The GRS tasks.</param>
        /// <returns>Dictionary Of Tasks and companies</returns>
        public Dictionary<GrsTasks, List<long>> GetCompanyIdsByIndividualTasks(GrsTasks[] grsTasks)
        {
            var companiesOfTasks = new Dictionary<GrsTasks, List<long>>();
            if (Permissions != null)
            {
                foreach (var task in grsTasks)
                {
                    var companiesOfTask = (from permission in Permissions
                                           where permission.Tasks.HasFlag(task)
                                           where permission.CompanyIds != null
                                           from companies in permission.CompanyIds
                                           select companies.Key).Distinct().ToList();
                    companiesOfTasks.Add(task, companiesOfTask);
                }
            }
            return companiesOfTasks;
        }


        /// <summary>
        /// Gets the permitted tasks. (Irrespective of the DAG restriction)
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>the tasks which are only permitted</returns>
        public GrsTasks GetPermittedTasks(params GrsTasks[] tasks)
        {
            var inputTasks = tasks.Aggregate(GrsTasks.None, (current, grsTaskse) => current | grsTaskse);
            var allTasks = Permissions.Aggregate(GrsTasks.None, (current, permission) => current | permission.Tasks);
            return allTasks & inputTasks;
        }

        /// <summary>
        /// Determines whether the specified company id has permissions.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <param name="countryId">The country id.</param>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if the specified company id has permissions; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPermissions(long companyId, long countryId, params GrsTasks[] task)
        {
            var tasks = task.Aggregate(GrsTasks.None, (current, grsTasks) => current | grsTasks);
            return HasPermission(companyId, countryId, tasks);
        }


        /// <summary>
        /// Use HasAnyPermission. Determines whether the tasks are there, irrespective of the DAG
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if taks exist otherwise, <c>false</c>.
        /// </returns>
        [Obsolete]
        public bool HasPermissionsWithoutDag(params GrsTasks[] task)
        {
            var tasks = task.Aggregate(GrsTasks.None, (current, grsTasks) => current | grsTasks);
            if (Permissions.Any(permission => (permission.Tasks & tasks) == tasks))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines whether [has any permission] [the specified task].
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if [has any permission] [the specified task]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasAnyPermission(params GrsTasks[] task)
        {
            var tasks = task.Aggregate(GrsTasks.None, (current, grsTasks) => current | grsTasks);
            if (Permissions.Any(permission => (permission.Tasks & tasks) != GrsTasks.None))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified company id has permission.
        /// </summary>
        /// <param name="companyId">The company id.</param>
        /// <param name="countryId">The country id.</param>
        /// <param name="task">The task.</param>
        /// <returns>
        /// 	<c>true</c> if the specified company id has permission; otherwise, <c>false</c>.
        /// </returns>
        private bool HasPermission(long companyId, long countryId, GrsTasks task)
        {
            var permissionForCompany = GetPermissionForCompany(companyId);
            var permissionForCountry = GetPermissionForCountry(countryId);

            if (permissionForCompany != null)
            {
                if ((permissionForCompany.Tasks & task) == task) return true;
            }
            if (permissionForCountry != null)
            {
                if ((permissionForCountry.Tasks & task) == task) return true;
            }
            return false;
        }

        [Obsolete]
        public List<long> GetCountriesByTask(GrsTasks task)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public List<long> GetCompaniesByTask(GrsTasks task)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public List<DataAccessGroupData> GetWorkFlows(long companyId, long countryId)
        {
            throw new NotImplementedException();
        }

        [Obsolete]
        public List<DataAccessGroupData> GetContractstatus(long companyId, long countryId)
        {
            throw new NotImplementedException();
        }


        [Obsolete]
        public List<long> GetAllCompanies()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all countries irrespective of DAG type
        /// </summary>
        /// <returns></returns>
        public List<long> GetAllCountries()
        {
            var countryIds = GetCountries();

            var companyIds = GetCompanies();
            foreach (var company in companyIds)
            {
                if (!countryIds.Contains(company.Value))
                {
                    countryIds.Add(company.Value);
                }
            }

            return countryIds;
        }
    }
}