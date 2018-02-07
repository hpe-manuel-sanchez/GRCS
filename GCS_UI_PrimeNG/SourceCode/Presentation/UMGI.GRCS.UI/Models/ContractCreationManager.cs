using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.Entities.ContractEntities;
using UMGI.GRCS.UI.Rights.Interfaces;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class ContractCreationManager : IContractCreationManager
    {
        public ContractCreationManager()
        {
            ContractInfo = new ContractInfoManager();
            RightsRestrictionsInfo = new RightsRestrictionManager();
            SecondaryExploitation = new SecondaryExploitationManager();
        }

        public IContractInfoManager ContractInfo { get; set; }

        public IRightsRestrictionManager RightsRestrictionsInfo { get; set; }

        public ISecondaryExploitationManager SecondaryExploitation { get; set; }
        
    }
}
