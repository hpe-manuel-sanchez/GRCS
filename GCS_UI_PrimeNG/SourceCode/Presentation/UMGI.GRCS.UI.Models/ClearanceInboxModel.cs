using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
    public class ClearanceInboxModel : IClearanceInboxModel
    {
        public ClearanceInboxFilterCriteria FilterCriteria { get; set; }

        public ClearanceInboxSearchCriteria SearchCriteria { get; set; }

        public ClearanceInboxSearchResult SearchResult { get; set; }
            
  

        /*THESE ALL SHOULD NOT BE HERE*/

        public ClearanceInboxState InboxState { get; set; }

        public ClearanceInboxModel()
        {

            var pageItems = new List<StringIdentifier>
            {
           
            new StringIdentifier { Id = (int)PageValues.TwentyFive, Description = ((int)PageValues.TwentyFive).ToString(CultureInfo.InvariantCulture) },
            new StringIdentifier { Id = (int)PageValues.Fifty, Description = ((int)PageValues.Fifty).ToString(CultureInfo.InvariantCulture) },
            new StringIdentifier { Id = (int)PageValues.SeventyFive, Description = ((int)PageValues.SeventyFive).ToString(CultureInfo.InvariantCulture) },
            new StringIdentifier { Id = (int)PageValues.Hundred, Description = ((int)PageValues.Hundred).ToString(CultureInfo.InvariantCulture) }
            };
            ItemsPerPage = pageItems.Select(results => new SelectListItem
            {
                Text = results.Description,
                Value = results.Id.ToString(CultureInfo.InvariantCulture)
            });
        }
        public enum PageValues : int { Ten = 10, TwentyFive = 25, Fifty = 50, SeventyFive = 75, Hundred = 100 };

        public Dictionary<GcsTasks, bool> TasksList { get; set; }
        public List<ContractDetails> ContractsSearch { get; set; }
        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        public List<long> ContractId { get; set; }
        public List<long> RoutedItemId { get; set; }
        public long ResourceId { get; set; }
        public string ResourceIdSelectedRequests { get; set; }
        public string ErrorMessage { get; set; }
        public ClearanceInboxResourceHistory resourceHistory { get; set; }
        public ClearanceInboxProjectDetail projectDetails { get; set; }

    }
}
