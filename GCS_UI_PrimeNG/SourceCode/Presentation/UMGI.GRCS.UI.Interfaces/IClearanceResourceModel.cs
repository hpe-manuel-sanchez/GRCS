using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceResourceModel
    {
        List<ClearanceResource> listClearanceResource { get; set; }
        IEnumerable<SelectListItem> ResourceType { get; set; }
        IEnumerable<SelectListItem> ResourceTypeFreehand { get; set; }
        IEnumerable<SelectListItem> RecordingType { get; set; }
        IEnumerable<SelectListItem> MusicType { get; set; }
        ClearanceResource freeHandResource { get; set; }
        IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        string SearchSelectedValues { get; set; }
        int ResourceTypeId { get; }
    }
}
