using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IArtistManager
    {
        /// <summary>
        /// Added by vivek on dated 15-Nov-2012
        /// </summary>
        ArtistSearchResult SearchArtist(ArtistSearchCriteria searchCriteria, bool applyQualifyingCriteria);
    }
}
