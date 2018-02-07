using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.UI.Controllers;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UnitTestUIProject.Presentation.UI.Controller
{
    [TestClass]
    public class ClearanceProjectUnitTest
    {
        ISessionWrapper _sessionWrapper;
        ILogFactory _logFactory;
        IConfigFactory _configFactory;
        IUserRepository _userRepository;
        IClearanceResourceRepository _clearanceResourceRepository;
        IClearanceReleaseRepository _clearanceReleaseRepository;
        IClearanceProjectRepository _clearanceProjectRepository;

        [TestInitialize]
        public void Initialize()
        {
            _logFactory = Utilities.Initialize.GetLogFactory();
            _userRepository = new UserRepository(_logFactory);
            _configFactory = new ConfigFactory(_logFactory);
            _sessionWrapper = Utilities.Initialize.GetSessionWrapper(_userRepository);
            _clearanceResourceRepository = new ClearanceResourceRepository(_logFactory);
            _clearanceReleaseRepository = new ClearanceReleaseRepository(_logFactory);
            _clearanceProjectRepository = new ClearanceProjectRepository(_configFactory, _logFactory);
        }


        #region AutoCompleteSearch
        [TestMethod]
        public void AutoComplete_ThirdParyCompany_Search()
        {
            ClearanceProjectController clearanceProjectController = new ClearanceProjectController(_clearanceProjectRepository, _clearanceReleaseRepository, _clearanceResourceRepository, _sessionWrapper, _logFactory);
            clearanceProjectController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceProjectController);

            SearchCriteria searchCriteria = new SearchCriteria();
            searchCriteria.searchBy = "ThirdParyCompanyName";
            searchCriteria.term = "";

            JsonResult result = clearanceProjectController.AutoCompleteSearch(searchCriteria);

            List<string> list = (List<string>)result.Data;

            foreach (string item in list)
            {
                searchCriteria.term = item;
                List<string> itemsFiltered = (List<string>)clearanceProjectController.AutoCompleteSearch(searchCriteria).Data;
                
                foreach (string itemFiltered in itemsFiltered)
                    Assert.IsTrue(itemFiltered.ToUpper().Contains(item.ToUpper()));
                
                Assert.IsTrue(itemsFiltered.Count > 0);
            }
        }
        #endregion


    }
}