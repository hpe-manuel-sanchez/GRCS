using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Responses;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Proxies.BackgroundService;

namespace UMGI.GRCS.UI.Models
{
    public class BackgroundRepository : IBackground
    {
        public List<BackgroundContractData> GetBackgroundProcessRecords(string query, FilterFields filter)
        {
            var client = new BackgroundServiceClient();
            return client.GetBackgroundProcessDetails(query, filter);
        }
    }
}
