/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IArtistManager.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rubini Raja       18/07/2012      Interface methods added for
 *                                   GetContracts and GetContract   
 * Pavan Kumar K     30/10/2012      Code Refactored 
 * KrishnaPrasad     05-12-2012      UpdateArtist method Declared
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IArtistManager
    {
        void GetArtist();

        ArtistSearchResult SearchArtist(ArtistSearchCriteria searchOption);

        List<ArtistInfo> GetArtists(ArtistInfo searchArtists);

        List<long> SaveArtist(List<ArtistInfo> artistInfo);

        void UpdateArtist(string xmlData, long notificationUniqueId);

        ArtistRights GetArtistRightXml(string r2TalentNameId);
    }
}