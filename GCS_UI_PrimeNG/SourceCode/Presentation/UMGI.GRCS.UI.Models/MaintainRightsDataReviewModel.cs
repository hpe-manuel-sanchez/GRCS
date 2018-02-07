using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Interfaces;
namespace UMGI.GRCS.UI.Models
{

    public class MaintainRightsDataReviewModel : IMaintainRightsDataReviewModel
    {
        public MaintainRightsDataReview MaintainRightsDataReview { get; set; }
        public IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
    }
}
