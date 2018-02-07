/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPReleaseRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : G Senthil Kumar
 * Created on   : 15-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by      Modified on     Remarks 
 */

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// Provides object representation of Media Portal Release Rights information
    /// </summary>    
    /// <remarks>
    /// This class shall be extended for the purpose of Explicit conversion of the ReleaseRigts Proxy object
    /// The extension class would be in name "MPReleaseRights.Casting.cs"
    /// </remarks>
    public partial class MPReleaseRights : EntityInformation
    {
        /// <summary>
        /// Gets or Sets the UPC code for the Release
        /// </summary>       
        public string Upc { get; set; }


        /// <summary>
        /// Gets or sets the exclusive info.
        /// </summary>
        /// <value>The exclusive info.</value>
        public string ExclusiveInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can delivery to you tube.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can delivery to you tube; otherwise, <c>false</c>.
        /// </value>
        public bool CanDeliveryToYouTube { get; set; }


        /// <summary>
        /// Gets or Sets the Account ID of the Release
        /// </summary>       
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or Sets the Company ID
        /// </summary>       
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or Sets the Company ID
        /// </summary>       
        public int DivisionId { get; set; }

        /// <summary>
        /// Gets or Sets the Label ID
        /// </summary>       
        public int LabelId { get; set; }

        /// <summary>
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        public string Grid { get; set; }


        /// <summary>
        /// Gets or sets the rights expiry date.
        /// </summary>
        /// <value>The rights expiry date.</value>
        public DateTime? RightsExpiryDate { get; set; }

        /// <summary>
        /// Gets or Sets collection of Resource Rights
        /// </summary>
        public List<MPResourceRights> ResourceRights { get; set; }

        /// <summary>
        /// Gets or sets the rights restrictions.
        /// </summary>
        /// <value>The rights restrictions.</value>
        public List<MPRightsRestrictions> RightsRestrictions { get; set; }
    }
}