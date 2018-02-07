using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceInboxModel
    {
        ClearanceInboxFilterCriteria FilterCriteria { get; set; }

        ClearanceInboxSearchCriteria SearchCriteria { get; set; }

        ClearanceInboxSearchResult SearchResult { get; set; }





        /*THESE ALL SHOULD NOT BE HERE*/
        ClearanceInboxState InboxState { get; set; }
        Dictionary<GcsTasks, bool> TasksList { get; set; }
        List<long> RoutedItemId { get; set; }
        List<ContractDetails> ContractsSearch { get; set; }
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        List<long> ContractId { get; set; }
        long ResourceId { get; set; }
        string ErrorMessage { get; set; }
        string ResourceIdSelectedRequests { get; set; }
        ClearanceInboxResourceHistory resourceHistory { get; set; }
        ClearanceInboxProjectDetail projectDetails { get; set; }
    }
}
