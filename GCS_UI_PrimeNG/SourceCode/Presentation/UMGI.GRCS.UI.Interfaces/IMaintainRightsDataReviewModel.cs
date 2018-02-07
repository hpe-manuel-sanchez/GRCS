using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.UI.Interfaces
{

    public interface IMaintainRightsDataReviewModel
    {
        MaintainRightsDataReview MaintainRightsDataReview { get; set; }
        IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
    }

}
