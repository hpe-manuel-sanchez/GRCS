/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IRollUpStatusModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 16/4/2013
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

namespace UMGI.GRCS.UI.Interfaces.Report
{
  public interface IRollUpStatusModel
    {
        /// <summary>
        /// 
        /// </summary>
        string UPC { get; set; }

        /// <summary>
        /// Gets or sets the Title .
        /// </summary>

        string Title { get; set; }

        /// <summary>
        /// Gets or sets the VersionTitle .
        /// </summary>

        string VersionTitle { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>

        string Artist { get; set; }

        /// <summary>
        /// Gets or sets the DataAdminCompany .
        /// </summary>

        string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Multi-Artist Compilation .
        /// </summary>

        string MultiArtistCompilation { get; set; }

        /// <summary>
        /// Gets or sets the Release Date .
        /// </summary>

        string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the ClearanceAdminCompany .
        /// </summary>

        string ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the Roll-up Status .
        /// </summary>

        string RollUp_Status { get; set; }

        /// <summary>
        /// Gets or sets the Last Roll-up Status .
        /// </summary>

        string LastRollUpDate { get; set; }

        /// <summary>
        /// Gets or sets the Release issuer .
        /// </summary>
        string Releaseissuer { get; set; }

        /// <summary>
        /// Gets or sets the Package .
        /// </summary>

        string Package { get; set; }

        /// <summary>
        /// Gets or sets the Package .
        /// </summary>

        string TerritorialRight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IRollUpStatusModel> releaseList { get; set; }

        /// <summary>
        /// Gets or sets Total ResultCount .
        /// </summary>
        int Total { get; set; }
    }
}
