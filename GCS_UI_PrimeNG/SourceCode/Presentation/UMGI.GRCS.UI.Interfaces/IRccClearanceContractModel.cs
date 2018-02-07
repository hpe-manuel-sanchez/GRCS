using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Interfaces
{
   public interface IRccClearanceContractModel
    {
        IEnumerable<SelectListItem> WorkFlowStatusList { get; set; }
        RccClearanceContract NewRccContract { get; set; }
        IEnumerable<SelectListItem> ContractStatusList { get; set; }
    }
}
