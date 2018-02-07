using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearenceUserTransferModel
    {

        IEnumerable<SelectListItem> WorkgroupUser { get; set; }
        List<ClearanceProject> listClearanceProjectTransfer { get; set; }

    }
}
