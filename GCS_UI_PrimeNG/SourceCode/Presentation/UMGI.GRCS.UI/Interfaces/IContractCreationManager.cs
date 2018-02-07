using System;
using UMGI.GRCS.UI.Rights.Interfaces;

namespace UMGI.GRCS.UI.Rights.Interfaces
{
    public interface IContractCreationManager
    {
        IContractInfoManager ContractInfo { get; set; }
        IRightsRestrictionManager RightsRestrictionsInfo { get; set; }
        ISecondaryExploitationManager SecondaryExploitation { get; set; }
    }
}
