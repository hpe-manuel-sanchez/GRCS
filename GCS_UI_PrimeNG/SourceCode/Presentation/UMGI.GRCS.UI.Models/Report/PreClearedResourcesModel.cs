using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Models.Report
{
    public class PreClearedResourcesModel : IPreClearedResourcesModel
    {
        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        /// <value>The ISRC.</value>
        public String ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        /// <value>The Title.</value>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the Version_Title .
        /// </summary>
        /// <value>The Version_Title.</value>
        public String Version_Title { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type .
        /// </summary>
        /// <value>The Resource Type.</value>
        public String ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type .
        /// </summary>
        /// <value>The Resource Type.</value>
        public IEnumerable<SelectListItem> ResourceTypeList { get; set; }

        /// <summary>
        /// Gets or sets Rights Type .
        /// </summary>
        /// <value>The Rights Type</value>
        public String RightsType { get; set; }

        ///// <summary>
        ///// Gets or sets output Rights Type .
        ///// </summary>
        ///// <value>The Rights Type</value>
        //public String Type { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Clearance Admin Company .
        /// </summary>
        /// <value>The Clearance Admin Company.</value>
        public String ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the PYear .
        /// </summary>
        /// <value>The PYear.</value>
        public String PYear { get; set; }

        /// <summary>
        /// Gets or sets the Resource Genre .
        /// </summary>
        /// <value>The Resource Genre.</value>
        public String ResourceGenre { get; set; }

        /// <summary>
        /// Gets or sets the Resource Genre .
        /// </summary>
        /// <value>The Resource Genre.</value>
        public IEnumerable<SelectListItem> ResourceGenreList { get; set; }

        /// <summary>
        /// Gets or sets the Territorial Right .
        /// </summary>
        /// <value>The Territorial Right.</value>

        public String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the PreClearance Type .
        /// </summary>
        /// <value>The PreClearance Type.</value>
        public String PreClearanceType { get; set; }

             /// <summary>
        /// Gets or sets the PreClearance Type .
        /// </summary>
        /// <value>The PreClearance Type.</value>
        public IEnumerable<SelectListItem> PreClearanceTypeList { get; set; }

        /// <summary>
        /// Gets or sets the StartDate
        /// </summary>
        public int YearFrom { get; set; }

        /// <summary>
        /// Gets or sets the EndDate
        /// </summary>
        public int YearTo { get; set; }

        /// <summary>
        /// Gets or sets the resources
        /// </summary>
        public List<IPreClearedResourcesModel> resources { get; set; }

        /// <summary>
        /// Gets or sets the recordCount
        /// </summary>
        public long recordCount { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceCompanyId
        /// </summary>
        public string ClearanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceCompanyName
        /// </summary>
      //  public string ClearanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryId
        /// </summary>
        public string ExploitationCountryId { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryName
        /// </summary>
        public string ExploitationCountryName { get; set; }

        /// <summary>
        /// Gets or sets the ExportType
        /// </summary>
        public string ExportType { get; set; }

        /// <summary>
        /// Gets or sets Sort Field .
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        /// Gets or sets Ascending/Descending .
        /// </summary>
        public string IsAscending { get; set; }

        /// <summary>
        /// Get list of the Artists
        /// </summary>
        public ArtistDetail ArtistSearch { get; set; }

         /// <summary>
        /// PreClearedResourcesModel constructor
        /// </summary>
        public PreClearedResourcesModel()
        {
            ArtistSearch = new ArtistDetail();
        }
        /// <summary>
        /// Gets or sets Total
        /// </summary>
        public int Total { get; set; }
    }
}
