using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.UI.Models
{
    public class UserPreferenceViewModel
    {
        public IEnumerable<SelectListItem> RqstngCmpny { get; set; }
        public IEnumerable<SelectListItem> Currency { get; set; }
        public IEnumerable<SelectListItem> listItem { get; set; }
        public UserPreferenceInfo userPreferenceInfo { get; set; }
        public string InboxRole { get; set; }
        public string ConsolidatedEmail { get; set; }
        public bool DisplayWorkgroupList { get; set; }
        public bool initialLoad { get; set; }
        public string RequestingCompanyId { get; set; }
        public string CurrencyId { get; set; }
    }
}
