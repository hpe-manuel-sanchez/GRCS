using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
    [Serializable]
    public class ClearanceResourceModel : IClearanceResourceModel
    {
        public ClearanceResourceModel()
        {
            freeHandResource = new ClearanceResource();
            listClearanceResource = new List<ClearanceResource>();
            var pageItems = new List<StringIdentifier>
            {
           
            new StringIdentifier { Id = (int)PageValues.TwentyFive, Description = ((int)PageValues.TwentyFive).ToString() },
            new StringIdentifier { Id = (int)PageValues.Fifty, Description = ((int)PageValues.Fifty).ToString() },
            new StringIdentifier { Id = (int)PageValues.SeventyFive, Description = ((int)PageValues.SeventyFive).ToString() },
            new StringIdentifier { Id = (int)PageValues.Hundred, Description = ((int)PageValues.Hundred).ToString() }
            };
            ItemsPerPage = pageItems.Select(results => new SelectListItem 
            { 
                Text = results.Description, 
                Value = results.Id.ToString(CultureInfo.InvariantCulture) 
            });
        }

        public enum PageValues : int { Ten = 10, TwentyFive = 25, Fifty = 50, SeventyFive = 75, Hundred = 100 };
        public List<ClearanceResource> listClearanceResource { get; set; }
        public IEnumerable<SelectListItem> ItemsPerPage { get; set; }
        public IEnumerable<SelectListItem> ResourceType { get; set; }
        public IEnumerable<SelectListItem> ResourceTypeFreehand { get; set; }
        public IEnumerable<SelectListItem> RecordingType { get; set; }
        public IEnumerable<SelectListItem> MusicType { get; set; }
        public ClearanceResource freeHandResource { get; set; }
        public string SearchSelectedValues { get; set; }
        public int rowIndex = -1;

        public int ResourceTypeId
        {

            get { return (int)Constants.ResourceType.Audio; }



        }
        public int RecordingTypeId
        {

            get { return (int)Constants.LiveStudioType.Studio; }



        }
    }
}
