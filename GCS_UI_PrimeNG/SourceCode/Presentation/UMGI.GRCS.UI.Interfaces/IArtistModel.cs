using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IArtistModel
    {
        ArtistDetail Artist { get; set; }
        string existingArtist { get; set; }
        List<ArtistDetail> GetArtistList { get; set; }
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }
    }
}
