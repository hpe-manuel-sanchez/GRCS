/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IPreClearedResourcesModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
  * Author         : Team
 * Created on     : 1/3/2013
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

namespace UMGI.GRCS.UI.Interfaces.Report
{
    public interface IPreClearedResourcesModel
    {
        /// <summary>
        /// Gets or sets the ISRC .
        /// </summary>
        /// <value>The ISRC.</value>
        String ISRC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>
        /// <value>The Title.</value>
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the Version_Title .
        /// </summary>
        /// <value>The Version_Title.</value>
        String Version_Title { get; set; }

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

        ///// <summary>
        ///// Gets or sets output Rights Type .
        ///// </summary>
        ///// <value>The Rights Type</value>
        //String Type { get; set; }

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
        /// Gets or sets the PYear .
        /// </summary>
        /// <value>The PYear.</value>
        String PYear { get; set; }

        /// <summary>
        /// Gets or sets the Resource Genre .
        /// </summary>
        /// <value>The Resource Genre.</value>
        String ResourceGenre { get; set; }

        /// <summary>
        /// Gets or sets the Territorial Right .
        /// </summary>
        /// <value>The Territorial Right.</value>

        String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the PreClearance Type .
        /// </summary>
        /// <value>The PreClearance Type.</value>
        String PreClearanceType { get; set; }

        /// <summary>
        /// Gets or sets the resources
        /// </summary>
        List<IPreClearedResourcesModel> resources { get; set; }

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
       // string ClearanceCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryId
        /// </summary>
        string ExploitationCountryId { get; set; }

        /// <summary>
        /// Gets or sets the ExploitationCountryName
        /// </summary>
        string ExploitationCountryName { get; set; }

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
        /// Gets or sets the StartDate
        /// </summary>
        int YearFrom { get; set; }

        /// <summary>
        /// Gets or sets the EndDate
        /// </summary>
        int YearTo { get; set; }

        /// <summary>
        /// Gets or sets Total ResultCount .
        /// </summary>
        int Total { get; set; }
    }
}
