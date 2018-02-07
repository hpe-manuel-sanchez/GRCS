/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IArtistData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rubini Raja       18-07-2012      Interface methods added for
 *                                   GetContracts and GetContract  
 * KrishnaPrasad     05-12-2012      UpdateArtist method Declared                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IArtistData
    {
        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="searchArtist">The search artist.</param>
        /// <returns></returns>
        List<ArtistInfo> GetArtists(ArtistInfo searchArtist);

        /// <summary>
        /// Saves the artist.
        /// </summary>
        /// <param name="artistInfo">The artist info.</param>
        /// <returns></returns>
        List<long> SaveArtist(List<ArtistInfo> artistInfo);

        /// <summary>
        /// Updates the artist.
        /// </summary>
        /// <param name="artistInfoList">The artist info list.</param>
        /// <param name="notificationType">Type of the notification.</param>
        void UpdateArtist(List<ArtistInfo> artistInfoList, string notificationType);
    }
}