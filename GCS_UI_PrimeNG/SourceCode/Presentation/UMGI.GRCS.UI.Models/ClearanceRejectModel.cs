using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
    public class ClearanceRejectModel : IClearanceRejectModel
    {
        public List<ClearanceRejectReason> RejectReasonList { get; set; }
    }    
}
