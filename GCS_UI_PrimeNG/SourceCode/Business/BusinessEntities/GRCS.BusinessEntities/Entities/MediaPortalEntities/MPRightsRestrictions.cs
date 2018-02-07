/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPRightsRestrictions.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : G Senthil Kumar
 * Created on   : 04-03-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by      Modified on     Remarks 
 */


using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class MPRightsRestrictionsComparer
    {
        /// <summary>
        /// Equalses the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool Equals(MPRightsRestrictions source, MPRightsRestrictions target)
        {
            return source.IsAlbumOnly == target.IsAlbumOnly && source.IsDownload == target.IsDownload &&
                   source.IsMakeOwnRingtone == target.IsMakeOwnRingtone && source.IsStreamable == target.IsStreamable &&
                   source.IsAddSupportedStraming == target.IsAddSupportedStraming;
        }
    }


    /// <summary>
    /// Represents the Rights restrictions Repertoire from Media Portal
    /// </summary>
    public class MPRightsRestrictions
    {
        /// <summary>
        ///  Gets or Sets the Album Right Existence
        /// </summary>        
        public bool IsAlbumOnly { get; set; }

        /// <summary>
        ///  Gets or Sets the Download Right Existence
        /// </summary>        
        public bool IsDownload { get; set; }

        /// <summary>
        ///  Gets or Sets the Streamable Right Existence
        /// </summary>        
        public bool IsStreamable { get; set; }

        /// <summary>
        ///  Gets or Sets the MakeOwnRingtone Right Existence
        /// </summary>        
        public bool IsMakeOwnRingtone { get; set; }

        /// <summary>
        /// Gets or Sets the AddSupportedStraming Right Existence
        /// </summary>
        public bool IsAddSupportedStraming { get; set; }

        /// <summary>
        /// Gets or Sets the Territory/Country Iso code
        /// </summary>        
        public List<string> CountryIsoCodes { get; set; }
    }
}