/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MPReleaseResourceAssociaton.cs 
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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities
{
    /// <summary>
    /// Holds the Releases from MEdia Portal
    /// </summary>
    public class MPReleaseResourceAssociaton
    {
        [DataMember]
        public List<MPRelease> MPRelease { get; set; }
    }

    /// <summary>
    /// Media Portal Release
    /// </summary>
    public class MPRelease
    {
        [DataMember]
        public string Upc { get; set; }

        [DataMember]
        public List<MPResource> MPResource { get; set; }
    }

    /// <summary>
    /// Media Portal Resource
    /// </summary>
    public class MPResource
    {
        [DataMember]
        public string Isrc { get; set; }
    }
}
