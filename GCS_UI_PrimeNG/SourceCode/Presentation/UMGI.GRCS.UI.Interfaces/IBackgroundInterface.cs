using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IBackground
    {
        List<BackgroundContractData> GetBackgroundProcessRecords(string query, FilterFields filter);
    }
}
