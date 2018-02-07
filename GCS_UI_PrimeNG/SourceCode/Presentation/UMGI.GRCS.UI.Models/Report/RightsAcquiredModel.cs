
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsAcquiredModel.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vinod
 * Created on   : 07-March-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the Model for Active Roster Report Search Result
                  
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.UI.Interfaces.Report;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.UI.Models.Report
{
    /// <summary>
    /// RightsAcquired Repertoire search result
    /// </summary>
    [Serializable]
    public class RightsAcquiredModel: IRightsAcquiredModel
    {
        /// <summary>
        /// Gets or sets the Type to dispaly image.
        /// </summary>
        /// <value>The ISRC.</value>
        public String Type { get; set; }
        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The ISRC.</value>
        public String UPC { get; set; }
        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        /// <value>The Title.</value>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the Version_Title .
        /// </summary>
        /// <value>The Version_Title.</value>
        public String VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Configuration.
        /// </summary>
        /// <value>The Version_Title.</value>
        public String Configuration { get; set; }

        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        /// <value>The ISRC.</value>
        public String ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type .
        /// </summary>
        /// <value>The Resource Type.</value>
        public String ResourceType { get; set; }

        /// <summary>
        /// Gets or sets Rights Type .
        /// </summary>
        /// <value>The Rights Type</value>
        public String RightsType { get; set; }

        /// <summary>
        /// Gets or sets the RepertoireType .
        /// </summary>
        /// <value>The Artist.</value>
        public String RepertoireType { get; set; }

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
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        /// <value>The Contract Territorial Right.</value>
        public String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Territorial_Right_ToolTip .
        /// </summary>
        public string TerritorialRightToolTip { get; set; }

        /// <summary>
        /// Gets or sets the Physical Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String PhysicalExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the Digital Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String DigitalExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the Mobile  Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String MobileExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the DigitalExploitationRestrictions .
        /// </summary>
        public string DigitalExploitationRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the ContentType
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String ContentType { get; set; }

        /// <summary>
        /// Gets or sets the UseType 
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String USEType { get; set; }

        /// <summary>
        /// Gets or sets the CommercialModels
        /// </summary>
        /// <value>The Exploitation Type.</value>
        public String ComercialModel { get; set; }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        /// <value>The Notes.</value>
        public string Notes { get; set; }

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
        public string ClearanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the CountryId
        /// </summary>
        public string CountryId { get; set; }

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
        /// Gets or sets the contracts
        /// </summary>
        public List<IRightsAcquiredModel> Repertoire { get; set; }

        /// <summary>
        /// Get list of the Artists
        /// </summary>
        public ArtistDetail ArtistSearch { get; set; }

        /// <summary>
        /// ReleaseCommitmentModel constructor
        /// </summary>
        public RightsAcquiredModel()
        {
            ArtistSearch = new ArtistDetail();
        }

        /// <summary>
        /// Get or set the ContentTypeList
        /// </summary>
        public IEnumerable<SelectListItem> ContentTypeList { get; set; }

        /// <summary>
        /// Get or set the UseTypeList
        /// </summary>
        public IEnumerable<SelectListItem> UseTypeList { get; set; }

        /// <summary>
        /// Get or set the CommercialModelsList
        /// </summary>
        public IEnumerable<SelectListItem> CommercialModelsList { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryId
        /// </summary>
        public string ExploitationCountryId { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryName
        /// </summary>
        public string ExploitationCountryName { get; set; }

        /// <summary>
        /// Get or set combinations of Exploitation Rights
        /// </summary>
        public string ExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets Total
        /// </summary>
        public int Total { get; set; }
    }
}
