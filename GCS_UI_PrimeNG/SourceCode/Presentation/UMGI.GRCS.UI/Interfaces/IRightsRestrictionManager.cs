using System;
using System.Collections.Generic;
using UMGI.GRCS.Entities.ContractEntities;



namespace UMGI.GRCS.UI.Rights.Interfaces
{
   public interface IRightsRestrictionManager
    {
        List<ContractRightsAcquired> RightsAcquired { get; set; }

        List<ContractDigitalRestrictions> DigitalRestriction { get; set; }
    }
}
