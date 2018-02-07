using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
    public class ClearanceReleaseModel : IClearanceReleaseModel
    {

        public ClearanceReleaseModel()
        {
            clearanceRelease = new ClearanceRelease();         

            clearanceRelease = new ClearanceRelease();
            lstClearanceRelease = new List<ClearanceRelease>();

        }
        public ClearanceRelease clearanceRelease { get; set; }

        public List<ClearanceRelease> lstClearanceRelease { get; set; }

    }
}
