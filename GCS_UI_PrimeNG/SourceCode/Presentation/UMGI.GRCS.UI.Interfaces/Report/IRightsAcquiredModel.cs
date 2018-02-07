/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IRightsAcquiredModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
  * Author         : Team
 * Created on     : 7/3/2013
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.UI.Interfaces.Report
{
    public interface IRightsAcquiredModel
    {
        /// <summary>
        /// Gets or sets the Type to dispaly image.
        /// </summary>
        /// <value>The ISRC.</value>
        String Type { get; set; }
        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The ISRC.</value>
        String UPC { get; set; }
        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        /// <value>The Title.</value>
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the Version_Title .
        /// </summary>
        /// <value>The Version_Title.</value>
        String VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Configuration.
        /// </summary>
        /// <value>The Version_Title.</value>
        String Configuration { get; set; }

        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        /// <value>The ISRC.</value>
        String ISRC { get; set; }
        
        /// <summary>
        /// Gets or sets the Resource Type .
        /// </summary>
        /// <value>The Resource Type.</value>
        String ResourceType { get; set; }

        /// <summary>
        /// Gets or sets Rights Type .
        /// </summary>
        /// <value>The Rights Type</value>
        String RightsType { get; set; }

        /// <summary>
        /// Gets or sets the RepertoireType .
        /// </summary>
        /// <value>The Artist.</value>
        String RepertoireType { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>
        String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Clearance Admin Company .
        /// </summary>
        /// <value>The Clearance Admin Company.</value>
        String ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        /// <value>The Contract Territorial Right.</value>
        String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Physical Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String PhysicalExploitationrights  { get; set; }

        /// <summary>
        /// Gets or sets the Digital Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String DigitalExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the Mobile  Exploitation Rights
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String MobileExploitationrights { get; set; }

        /// <summary>
        /// Gets or sets the ContentType
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String ContentType { get; set; }

        /// <summary>
        /// Gets or sets the UseType 
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String USEType { get; set; }

        /// <summary>
        /// Gets or sets the CommercialModels
        /// </summary>
        /// <value>The Exploitation Type.</value>
        String ComercialModel { get; set; }
        
        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        /// <value>The Notes.</value>
        string Notes { get; set; }

        /// <summary>
        /// Gets or sets the recordCount
        /// </summary>
        long recordCount { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceCompanyId
        /// </summary>
        string ClearanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceCompanyName
        /// </summary>
        string ClearanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the CountryId
        /// </summary>
        string CountryId { get; set; }

        /// <summary>
        /// Gets or sets the ExportType
        /// </summary>
        string ExportType { get; set; }

        /// <summary>
        /// Gets or sets Sort Field .
        /// </summary>
        string SortField { get; set; }

        /// <summary>
        /// Gets or sets Ascending/Descending .
        /// </summary>
        string IsAscending { get; set; }

        /// <summary>
        /// Gets or sets the contracts
        /// </summary>
        List<IRightsAcquiredModel> Repertoire { get; set; }

        /// <summary>
        /// Get or set the ArtistSearch
        /// </summary>
        ArtistDetail ArtistSearch { get; set; }

        /// <summary>
        /// Get or set the ContentTypeList
        /// </summary>
        IEnumerable<SelectListItem> ContentTypeList { get; set; }

        /// <summary>
        /// Get or set the UseTypeList
        /// </summary>
        IEnumerable<SelectListItem> UseTypeList { get; set; }

        /// <summary>
        /// Get or set the CommercialModelsList
        /// </summary>
        IEnumerable<SelectListItem> CommercialModelsList { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryId
        /// </summary>
        string ExploitationCountryId { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryName
        /// </summary>
        string ExploitationCountryName { get; set; }

        /// <summary>
        /// Get or set combinations of Exploitation Rights
        /// </summary>
        string ExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets Total ResultCount .
        /// </summary>
        int Total { get; set; }
    }
}
