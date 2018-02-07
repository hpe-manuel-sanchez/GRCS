using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceResourceRepository
    {
        IClearanceResourceModel IClearanceResourceModel { get; set; }        
        ClearanceResourceSearchResult SearchR2Resource(ResourceSearchCriteria resourceSearch, LeanUserInfo userInfo);        
        List<ContractDetails> GetResourceRights(long r2resourceId, LeanUserInfo userInfo);
        bool RemoveResourceProject(string archiveFlag, List<long> listclrResourceId, LeanUserInfo userInfo);
    }
}
