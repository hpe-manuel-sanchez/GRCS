/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TerritoryController.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   :
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * R.MuthuKumar      02/11/2012      Code cleaning and commenting
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using System.Collections.ObjectModel;

namespace UMGI.GRCS.UI.Models
{
    public class WorkgroupModel : IWorkgroupModel
    {
        #region Variable Declarations

        #endregion

        #region Properties

        /// <summary>
        /// Used to Get/Set the list of searched company details
        /// </summary>
        public List<CompanyInfo> CompanySearch { get; set; }
        /// <summary>
        /// Used to Get/Set the company details
        /// </summary>
        public CompanyInfo CompanyDetails { get; set; }
        /// <summary>
        /// Get/Set the IEnumerable MVC SelectListItem
        /// This is used to show the row sizes for Jtable in the DropdownList
        /// </summary>
        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        /// <summary>
        /// Get/Set the IEnumerable MVC SelectListItem
        /// This is used to show the roles in the DropdownList
        /// </summary>
        public IEnumerable<SelectListItem> RolesList { get; set; }
        /// <summary>
        /// Used to Get/Set the list of company details
        /// </summary>
        public List<CompanyInfo> GetCompanies { get; set; }
        /// <summary>
        /// Used to Get/Set value of RequestReassignToWorkgroup or not
        /// </summary>
        public bool RequestReassignToWorkgroup { get; set; }
        /// <summary>
        /// Used to Get/Set Workgroup details with Child also
        /// </summary>
        public List<WorkgroupSearchResult> GetWorkgroupByChild { get; set; }
        /// <summary>
        /// Used to Get/Set Workgroup details
        /// </summary>
        public Workgroup WorkgroupDetails { get; set; }
        /// <summary>
        /// Used to Get/Set UserDetails
        /// Details fetch from ANA returned ObservableCollection
        /// </summary>
        public ObservableCollection<ApplicationUser> SearchUsers { get; set; }
        /// <summary>
        /// Used to Get/set Users details for the workgroup
        /// </summary>
        public List<WorkGroupUser> GetWorkgroupUserList { get; set; }
        /// <summary>
        /// Used to Get/Set Child work group details
        /// </summary>
        public ChildWorkgroup GetChildWorkgroup { get; set; }
        /// <summary>
        /// Used to Get/Set workgroup search result
        /// </summary>
        public WorkgroupSearchResult Workgroup { get; set; }
        /// <summary>
        /// Used to Get/Set List of workgroup roles
        /// </summary>
        public List<Workgroup> GetRoles { get; set; }
        /// <summary>
        /// Used to Get/Set Searched country details
        /// </summary>
        public List<CountryInfo> CountrySearch { get; set; }
        /// <summary>
        /// Used to Get/Set list of country details
        /// </summary>
        public List<CountryInfo> GetCountries { get; set; }
        /// <summary>
        /// Used to Get/Set list of countries for the workgroup
        /// </summary>
        public List<CountryInfo> CountriesForWorkGroup { get; set; }
        /// <summary>
        /// Used to Get/Set value of workgroup deleted or not
        /// </summary>
        public bool DeleteWorkgroup { get; set; }
        /// <summary>
        /// Used to Get/Set Default user name for the workgroup
        /// </summary>
        public string DefaultUserName { get; set; }
        /// <summary>
        /// Used to Get/Set the list of companies for the workgroup
        /// </summary>
        public List<CompanyInfo> GetCompaniesOfWorkgroup { get; set; }
        /// <summary>
        /// Used to Get/Set Workgroup search result
        /// </summary>
        public List<WorkgroupSearchResult> GetWorkgroups { get; set; }
        /// <summary>
        /// Used to Get/Set the value of LinkArtistContractToWorkgroup or not
        /// </summary>
        public List<long> LinkArtistContractToWorkgroup { get; set; }
        /// <summary>
        /// Used to Get/Set the value of LinkResourceContractToWorkgroup or not
        /// </summary>
        public List<long> LinkResourceContractToWorkgroup { get; set; }
        /// <summary>
        /// Get/Set the IEnumerable MVC SelectListItem
        /// Used to show the list of Request Types
        /// </summary>
        public IEnumerable<SelectListItem> RequestTypeList { get; set; }
        /// <summary>
        /// Used to Get/Set the list of workgroup request types
        /// </summary>
        public List<Workgroup> GetRequestType { get; set; }
        /// <summary>
        /// Used to Get/Set list of Contract details
        /// </summary>
        public List<LeanContractInfo> ContractsList { get; set; }
        /// <summary>
        /// Used to Get/set the contract details
        /// </summary>
        public LeanContractInfo LeanContract { get; set; }
        /// <summary>
        /// Get/Set the IEnumerable MVC SelectListItem
        /// Used to show the list of Resource Types
        /// </summary>
        public IEnumerable<SelectListItem> ResourceTypeList { get; set; }

        /// <summary>
        /// Collection of page values used to load specified count Jtable
        /// </summary>
        public enum PageValues : int { Ten = 10, TwentyFive = 25, Fifty = 50, SeventyFive = 75, Hundred = 100 };

        /// <summary>
        /// Get/Set workgroup name
        /// </summary>
        public List<string> SuggestiveSearchForWorkgroup { get; set; }

        /// <summary>
        /// Get/Set workgroup name
        /// </summary>
        public List<WorkgroupSearchResult> SearchWorkgroupForRemoveUsers { get; set; }
        public List<WorkgroupSearchResult> SearchWorkgroupToAddUsers { get; set; }
        public List<string> RemoveUserFromMultipleWorkgroup { get; set; }
        public bool AddUserInMultipleWorkgroup { get; set; }
        public IClearanceProjectModel GetClrPACompanyAndCurrencyUserList { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        public WorkgroupModel()
        {
            try
            {
                //Initializes a new instance of the List<CompanyInfo> class.
                CompanySearch = new List<CompanyInfo>();
                //Initializes a new instance of the CompanyInfo class.
                CompanyDetails = new CompanyInfo();
                //Initializes a new instance of the Workgroup class.
                WorkgroupDetails = new Workgroup();
                //Initializes a new instance of the WorkgroupSearchResult class.
                Workgroup = new WorkgroupSearchResult();
                //Initializes a new instance of the List<LeanContractInfo> class.
                ContractsList = new List<LeanContractInfo>();
                //Initializes a new instance of the List<CountryInfo> class.
                CountrySearch = new List<CountryInfo>();
                //Initializes a new instance of the List<CountryInfo> class.
                GetCountries = new List<CountryInfo>();
                //Initializes a new instance of the List<CountryInfo> class.
                CountriesForWorkGroup = new List<CountryInfo>();
                //Initializes a new instance of the ChildWorkgroup class.
                GetChildWorkgroup = new ChildWorkgroup();
                //Initializes a new instance of the List<StringIdentifier> class with specified values.
                var pageItems = new List<StringIdentifier>
            {
            new StringIdentifier { Id = (int)PageValues.Ten, Description = ((int)PageValues.Ten).ToString() },
            new StringIdentifier { Id = (int)PageValues.TwentyFive, Description = ((int)PageValues.TwentyFive).ToString() },
            new StringIdentifier { Id = (int)PageValues.Fifty, Description = ((int)PageValues.Fifty).ToString() },
            new StringIdentifier { Id = (int)PageValues.SeventyFive, Description = ((int)PageValues.SeventyFive).ToString() },
            new StringIdentifier { Id = (int)PageValues.Hundred, Description = ((int)PageValues.Hundred).ToString() }
            };
                //Set the ListItems in the MVC SelectList class property
                ItemsPerPage = pageItems.Select(results => new SelectListItem
                {
                    Text = results.Description,
                    Value = results.Id.ToString(CultureInfo.InvariantCulture)
                });
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
