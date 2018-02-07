using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceReleaseModel
    {
        ClearanceRelease clearanceRelease { get; set; }
        List<ClearanceRelease> lstClearanceRelease { get; set; }
    }
}
