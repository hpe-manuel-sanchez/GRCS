using System.Collections.Generic;
using System.Web.Mvc;
using DynamicControlsCreation.ViewModels;
using UMGI.GRCS.Resx.Resource.UIResources;

namespace UMGI.GRCS.UI.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Test(string pageName)
        {
            ViewBag.pageName = pageName;
            var model = new DefaultControlsViewModel(pageName);
            model.Dropdownlist = GetDropDownValuesForWqLevel();
            return View(model);
        }
        
        private List<SelectListItem> GetDropDownValuesForWqLevel()
        {
                return new List<SelectListItem>
                            {
                                new SelectListItem {Text = AdminResource.Global, Value = "0", Selected = true},
                                new SelectListItem {Text = AdminResource.Country, Value = "1"}
                            };

        }
        [HttpPost]
        public ActionResult Test(DefaultControlsViewModel model)
        {
            return RedirectToAction("Test");        
        }
    }

    public class ControlKeys
    {
        public string ID { get; set; }
        public string ParentClass { get; set; }
        public string ChildClass1 { get; set; }
        public string ChildClass2 { get; set; }
        public string ChildClass3 { get; set; }
        public string ChildClass4 { get; set; }
        public string ChildClass5 { get; set; }
    }

}
