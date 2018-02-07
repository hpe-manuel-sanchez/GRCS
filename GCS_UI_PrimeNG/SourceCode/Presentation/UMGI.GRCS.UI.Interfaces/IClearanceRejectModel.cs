using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceRejectModel
    {
        List<ClearanceRejectReason> RejectReasonList { get; set; }
    }
}
