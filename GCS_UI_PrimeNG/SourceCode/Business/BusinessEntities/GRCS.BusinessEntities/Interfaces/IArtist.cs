/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IArtist.cs
 * Project Code :   
 * Author       : Pavan Kumar K
 * Created on   : 13 July 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 *Description : Interface methods for Artist Service
                  
****************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public partial interface IArtist
    {

        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        [OperationContract]
        ArtistSearchResult SearchArtist(ArtistSearchCriteria searchOption);


        /// <summary>
        /// Gets the artists.
        /// </summary>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        [OperationContract(Name = "SearchGRCSArtists")]
        List<ArtistInfo> GetArtists(ArtistInfo searchOption);

      
    }
    public partial interface IArtist
    {
        [OperationContract(Name = "GCSSearchArtist")]
        ArtistSearchResult SearchArtist(ArtistSearchCriteria searchCriteria, bool applyQualifyingCriteria);
    }
}