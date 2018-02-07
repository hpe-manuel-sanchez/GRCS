/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPResourceRights.cs 
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

namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// Provides object represeantation of Release Resource Right Information
    /// </summary>
    
    public class MPResourceRights
    {
        
        /// <summary>
        /// Gets or Sets the Release Resource ISRC Code
        /// </summary>       
        public string Isrc { get; set; }

        /// <summary>
        /// Gets or Sets the Resource Type
        /// </summary>       
        public MPResourceType ResourceType { get; set; }   

        /// <summary>
        /// Gets or Sets the Rights Expiry Date
        /// </summary>       
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Determines whether [is add supported straming allowed].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is add supported straming allowed]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddSupportedStramingAllowed()
        {
            return CountriesAllowedForStreaming.Count > 0;
        }
        /// <summary>
        /// Gets or sets the countries allowed for streaming.
        /// </summary>
        /// <value>The countries allowed for streaming.</value>
        public List<string> CountriesAllowedForStreaming { get; set; }

        /// <summary>
        /// Gets or sets the rights restrictions.
        /// </summary>
        /// <value>The rights restrictions.</value>
        public List<MPRightsRestrictions> RightsRestrictions { get; set; }
        
    }

    /// <summary>
    /// Enum to define Release Resource Type
    /// </summary>    
    public enum MPResourceType
    {
        /// <summary>
        /// Resource of Type Audio
        /// </summary>       
        Audio,

        /// <summary>
        /// Resource of Type Video
        /// </summary>       
        Video       

    }

    
   
}
