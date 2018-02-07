using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
    public class ClearenceUserTransferModel : IClearenceUserTransferModel
    {

        public List<ClearanceProject> listClearanceProjectTransfer { get; set; }

        public IEnumerable<SelectListItem> WorkgroupUser { get; set; }
    }
}
