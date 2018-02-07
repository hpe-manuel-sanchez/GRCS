using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IContractTerritorialRightsModel
    {     
        List<TerritorialDisplay> SaveTerritorialRights { get; set; }
        List<TerritorialDisplay> GetTerritorialRights { get; set; }
    }

}
