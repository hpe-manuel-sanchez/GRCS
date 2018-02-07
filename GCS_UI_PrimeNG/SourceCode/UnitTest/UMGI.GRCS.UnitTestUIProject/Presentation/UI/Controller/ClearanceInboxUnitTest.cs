using Microsoft.VisualStudio.TestTools.UnitTesting;
using Syncfusion.Mvc.Grid;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceInboxEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.UI.Controllers;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;
using UMGI.GRCS.UI.ViewModels.ClearanceInbox;
using System.Linq;
using System.Web;
using Moq;
using UMGI.GRCS.UnitTestUIProject.Utilities;
using System.Web.Routing;

namespace UMGI.GRCS.UnitTestUIProject.Presentation.UI.Controller
{
    [TestClass]
    public class ClearanceInboxUnitTest
    {
        IClearanceInboxRepository _clearanceInboxRepository;
        ISessionWrapper _sessionWrapper;
        ILogFactory _logFactory;
        IGlobalRepository _globalRepository;
        IUserRepository _userRepository;
        IClearanceProjectRepository _projectRepository;
        IConfigFactory _configFactory;
        IClearanceReleaseRepository _clearanceReleaseRepository;
        IClearanceResourceRepository _clearanceResourceRepository;

        private const int projectCount = 10;

        #region PRIVATE_GENERIC_METHODS
        private InboxViewModel.InboxProject getProjectRadomByRol(RoleGroup roleGroup)
        {
            ClearanceInboxController clearanceInboxController = new ClearanceInboxController(_clearanceInboxRepository, _sessionWrapper, _logFactory, _globalRepository);
            clearanceInboxController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceInboxController);

            PartialViewResult result = clearanceInboxController.GetInbox(roleGroup) as PartialViewResult;
            InboxViewModel InboxViewModel = (InboxViewModel)result.Model;

            Random rnd = new Random();
            int index = rnd.Next(0, InboxViewModel.Projects.Count);

            InboxViewModel.InboxProject inboxProject = InboxViewModel.Projects[index];

            if (inboxProject.ClearanceProjectId == 0)
                return null;
            else
                return InboxViewModel.Projects[index];

        }

        private List<InboxViewModel.InboxProject> getProjectsByRol(RoleGroup roleGroup)
        {
            ClearanceInboxController clearanceInboxController = new ClearanceInboxController(_clearanceInboxRepository, _sessionWrapper, _logFactory, _globalRepository);
            clearanceInboxController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceInboxController);
            PartialViewResult result = clearanceInboxController.GetInbox(roleGroup) as PartialViewResult;
            InboxViewModel InboxViewModel = (InboxViewModel)result.Model;

            List<InboxViewModel.InboxProject> inboxProject = InboxViewModel.Projects;

            return inboxProject;
        }
        #endregion

        [TestInitialize]
        public void Initialize()
        {
            _logFactory = Utilities.Initialize.GetLogFactory();
            _userRepository = new UserRepository(_logFactory);
            _sessionWrapper = Utilities.Initialize.GetSessionWrapper(_userRepository);
            _globalRepository = new GlobalRepository(_sessionWrapper, _logFactory);
            _clearanceInboxRepository = new ClearanceInboxRepository(_logFactory, _sessionWrapper);
            _configFactory = new ConfigFactory(_logFactory);
            _projectRepository = new ClearanceProjectRepository(_configFactory, _logFactory);
            _clearanceReleaseRepository = new ClearanceReleaseRepository(_logFactory);
            _clearanceResourceRepository = new ClearanceResourceRepository(_logFactory);
        }

        #region GetInboxProjectDetail

        #region RC-8210
        [TestMethod]
        public void GetInboxProjectDetail_Reviewer()
        {
            //VARIABLE INITIALIZATION
            RoleGroup roleGroup = RoleGroup.Reviewer;
            string viewName = "RightPanel-Reviewer";
            string trackGridData = "TblReviewerTrackGridData";
            string resourceGridData = "TblReviewerResourceGridData";

            //GET PROJECT
            List<InboxViewModel.InboxProject> inboxProjects = getProjectsByRol(roleGroup);

            TestInboxProjectDetail_SubmittedDate(inboxProjects, roleGroup, viewName, trackGridData, resourceGridData);
        }

        [TestMethod]
        public void GetInboxProjectDetail_Requestor()
        {
            //VARIABLE INITIALIZATION
            RoleGroup roleGroup = RoleGroup.Requestor;
            string viewName = "RightPanel-Requestor";
            string trackGridData = "TblRequestorTrackGridData";
            string resourceGridData = "TblRequestorResourceGridData";

            //GET PROJECT
            List<InboxViewModel.InboxProject> inboxProjects = getProjectsByRol(roleGroup);

            TestInboxProjectDetail_SubmittedDate(inboxProjects, roleGroup, viewName, trackGridData, resourceGridData);
        }

        private void TestInboxProjectDetail_SubmittedDate(List<InboxViewModel.InboxProject> inboxProjects, RoleGroup roleGroup, string viewName, string trackGridData, string resourceGridData)
        {

            PagingParams args = null;
            byte gridType = 0;

            //CONTROLLERS REQUERIEDS
            ClearanceInboxController clearanceInboxController =
                new ClearanceInboxController(_clearanceInboxRepository, _sessionWrapper, _logFactory, _globalRepository);
            clearanceInboxController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceInboxController);

            ClearanceProjectController clearanceProjectController =
                new ClearanceProjectController(_projectRepository, _clearanceReleaseRepository, _clearanceResourceRepository, _sessionWrapper, _logFactory);
            clearanceProjectController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceProjectController);

            foreach (InboxViewModel.InboxProject inboxProject in inboxProjects.Take(projectCount))
            {
                ClearanceInboxRequestAction clearanceInboxRequestAction = new ClearanceInboxRequestAction();
                clearanceInboxRequestAction.FolderId = inboxProject.FolderId;
                clearanceInboxRequestAction.ProjectId = inboxProject.ClearanceProjectId;
                clearanceInboxRequestAction.ProjectType = 0;
                clearanceInboxRequestAction.RoleName = inboxProject.RoleName;
                clearanceInboxRequestAction.WorkgroupId = inboxProject.WorkgroupId;

                //GET PROJECT DETAILL                    
                PartialViewResult result = clearanceInboxController.GetInboxProjectDetail(args, roleGroup, clearanceInboxRequestAction, gridType) as PartialViewResult;

                object requests;
                result.ViewData.TryGetValue("Error", out requests);
                if (requests == null)
                {
                    //ASSERT
                    Assert.AreEqual(viewName, result.ViewName);
                    Assert.IsNotNull(result.Model);
                    Assert.IsInstanceOfType(result.Model, typeof(ClearanceInboxModel));

                    ClearanceInboxModel clearanceInboxModel = (ClearanceInboxModel)result.Model;

                    Assert.AreEqual(inboxProject.ClearanceProjectId, clearanceInboxModel.projectDetails.clrProjectId);
                    Assert.AreEqual(inboxProject.FolderId, clearanceInboxModel.projectDetails.FolderId);
                    Assert.AreEqual(inboxProject.ProjectReferenceNumber, clearanceInboxModel.projectDetails.ProjectReferenceNumber);
                    Assert.AreEqual(inboxProject.RoleName, clearanceInboxModel.projectDetails.RoleName);
                    Assert.AreEqual(inboxProject.ProjectTypeId, clearanceInboxModel.projectDetails.ProjectType);
                    Assert.AreEqual(inboxProject.ProjectStatusId, clearanceInboxModel.projectDetails.ProjectStatus);

                    object requestsTrack;
                    result.ViewData.TryGetValue(trackGridData, out requestsTrack);

                    object requestsResource;
                    result.ViewData.TryGetValue(resourceGridData, out requestsResource);

                    List<ClearanceInboxRequest> clearanceInboxRequestList = new List<ClearanceInboxRequest>();

                    if (((List<ClearanceInboxRequest>)requestsTrack).Count > 0)
                    {
                        clearanceInboxRequestList = (List<ClearanceInboxRequest>)requestsTrack;
                    }
                    if (((List<ClearanceInboxRequest>)requestsResource).Count > 0)
                    {
                        clearanceInboxRequestList = (List<ClearanceInboxRequest>)requestsResource;
                    }

                    if (clearanceInboxRequestList.Count > 0)
                    {
                        PartialViewResult requestResult = new PartialViewResult();
                        foreach (ClearanceInboxRequest request in clearanceInboxRequestList)
                        {
                            //GET ROUTING DETAILL
                            requestResult = (clearanceProjectController.RoutingDetailsOnRequestSummary(request.RoutedItemId)) as PartialViewResult;
                            ClearanceRoutingDetails routingDetail = (
                                (List<ClearanceRoutingDetails>)requestResult.Model)
                                    .Where(i => i.Status.ToUpper() == ("Submitted").ToUpper()).First();
                            
                            if (routingDetail != null)
                                Assert.AreEqual(request.CreatedDate.Date, routingDetail.ReviewDate.Value.Date);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void GetInboxProjectSummaryDetails()
        {
            //VARIABLE INITIALIZATION
            RoleGroup roleGroup = RoleGroup.Requestor;

            //GET PROJECT
            List<InboxViewModel.InboxProject> inboxProjects = getProjectsByRol(roleGroup);

            ClearanceProjectController clearanceProjectController = new ClearanceProjectController(_projectRepository, _clearanceReleaseRepository, _clearanceResourceRepository, _sessionWrapper, _logFactory);

            clearanceProjectController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceProjectController);

            foreach (InboxViewModel.InboxProject inboxProject in inboxProjects.Take(projectCount))
            {
                if (inboxProject.ProjectTypeId == 1)
                {
                    ViewResult result = clearanceProjectController.GetProjectDetails(inboxProject.ProjectType, (int)inboxProject.ClearanceProjectId) as ViewResult;

                    ClearanceProjectModel clearanceProjectModel = (ClearanceProjectModel)result.Model;

                    PartialViewResult requestResult = new PartialViewResult();

                    foreach (ClearanceInboxRequest request in clearanceProjectModel.MasterProjectDetails.RequestInfoList)
                    {
                        //GET ROUTING DETAILL
                        requestResult = (clearanceProjectController.RoutingDetailsOnRequestSummary(request.RoutedItemId)) as PartialViewResult;
                        ClearanceRoutingDetails routingDetail = (
                            (List<ClearanceRoutingDetails>)requestResult.Model)
                                .Where(i => i.Status.ToUpper() == ("Submitted").ToUpper()).First();
                        
                        if (routingDetail != null)
                            Assert.AreEqual(request.CreatedDate.Date, routingDetail.ReviewDate.Value.Date);
                    }
                }
                else
                {
                    string clearanceProjectId = Convert.ToString(inboxProject.ClearanceProjectId);
                                                            
                    List<ClearanceInboxRequest> clearanceInboxRequestList = new List<ClearanceInboxRequest>();
                    PartialViewResult requestResult = new PartialViewResult();

                    //RESOURCES
                    JsonResult jsonResultResources = clearanceProjectController.GetNewResources(clearanceProjectId, 0, 100000) as JsonResult;
                    var jsonResultResourcesProperty = Utilities.Objects.GetReflectedProperty(jsonResultResources.Data, "Records");

                    if (jsonResultResourcesProperty != null)
                    {
                        clearanceInboxRequestList =
                            ((EnumerableQuery<ClearanceInboxRequest>)(jsonResultResourcesProperty)).ToList<ClearanceInboxRequest>();

                        foreach (ClearanceInboxRequest request in clearanceInboxRequestList)
                        {
                            //GET ROUTING DETAILL
                            requestResult = (clearanceProjectController.RoutingDetailsOnRequestSummary(request.RoutedItemId)) as PartialViewResult;
                            ClearanceRoutingDetails routingDetail = (
                                (List<ClearanceRoutingDetails>)requestResult.Model)
                                    .Where(i => i.Status.ToUpper() == ("Submitted").ToUpper()).FirstOrDefault();

                            if (routingDetail != null)
                                Assert.AreEqual(request.CreatedDate.Date, routingDetail.ReviewDate.Value.Date);
                        }
                    }

                    //TRACKS
                    JsonResult jsonResultTracks = clearanceProjectController.GetExistingTracks(clearanceProjectId, 0, 100000) as JsonResult;
                    jsonResultResourcesProperty = Utilities.Objects.GetReflectedProperty(jsonResultTracks.Data, "Records");

                    if (jsonResultResourcesProperty != null)
                    {
                        clearanceInboxRequestList =
                            ((EnumerableQuery<ClearanceInboxRequest>)(jsonResultResourcesProperty)).ToList<ClearanceInboxRequest>();

                        foreach (ClearanceInboxRequest request in clearanceInboxRequestList)
                        {
                            //GET ROUTING DETAILL
                            requestResult = (clearanceProjectController.RoutingDetailsOnRequestSummary(request.RoutedItemId)) as PartialViewResult;
                            ClearanceRoutingDetails routingDetail = (
                                (List<ClearanceRoutingDetails>)requestResult.Model)
                                    .Where(i => i.Status.ToUpper() == ("Submitted").ToUpper()).FirstOrDefault();
                            
                            if (routingDetail != null)
                                Assert.AreEqual(request.CreatedDate.Date, routingDetail.ReviewDate.Value.Date);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        [TestMethod]
        public void GetInbox()
        {
            ClearanceInboxController clearanceInboxController = new ClearanceInboxController(_clearanceInboxRepository, _sessionWrapper, _logFactory, _globalRepository);
            clearanceInboxController.ControllerContext = new ControllerContext(Utilities.Initialize.GenerateContext(), new RouteData(), clearanceInboxController);
            PartialViewResult result = clearanceInboxController.GetInbox(BusinessEntities.Lookups.RoleGroup.RCCAdmin) as PartialViewResult;
            Assert.AreEqual("Inbox", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(InboxViewModel));
        }

    }
}