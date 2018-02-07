/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsReviewWorkQueueController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 04-02-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 * Karthik P                     14/02/2013                     Added methods
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using System.Web.Mvc;
using Syncfusion.Mvc.Grid;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.Resx.Resource.UIResources;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Interfaces.RepertoireRightsSearch;
using UMGI.GRCS.UI.Interfaces.RightsReviewWQ;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Models.RightsReviewWQ;
using UMGI.GRCS.Utilities;
using UMGI.GRCS.Utilities.logger;
using Filter = UMGI.GRCS.BusinessEntities.Entities.BaseEntities.Filter;


namespace UMGI.GRCS.UI.Controllers
{
    /// <summary>
    /// Rights Administration Controller
    /// </summary>
    public class RightsReviewWorkQueueController : BaseController
    {
        #region Private Variables

        private readonly IRightsReviewWorkQueueRepository _rightsReviewWorkQueueRepository;
        //private IResourceRightsWorkQueueModel _resourceRightsWorkQueueModel;
        //private IRightsReviewWorkQueueModel _rightsReviewWorkQueueModel;
        private readonly IContractRepository _contractRepository;
        private IConfigFactory _configFactory { get; set; }
        private ReviewRightsMasterInfo acquiredRightsMasterData;
        private DigitalRightsMasterData digitalRightsMasterData;
        private readonly IRepertoireRightsSearchRepository _repertoireRightsSearchRepository;
        private readonly IReleaseRepository _releaseRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IProjectRepository _projectRepository;

        #endregion

        #region Constructor

        public RightsReviewWorkQueueController() { }

        public RightsReviewWorkQueueController
            (IRightsReviewWorkQueueRepository rightsReviewWorkQueueRepository,
            IContractRepository contractRepository,
            ISessionWrapper sessionWrapper,
            ILogFactory logFactory,
            IConfigFactory configFactory,
            IRepertoireRightsSearchRepository repertoireRightsSearchRepository,
            IProjectRepository projectRepository,
            IReleaseRepository releaseRepository,
            IResourceRepository resourceRepository
            )
        {
            _configFactory = configFactory;
            _rightsReviewWorkQueueRepository = rightsReviewWorkQueueRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
            _contractRepository = contractRepository;
            _repertoireRightsSearchRepository = repertoireRightsSearchRepository;
            _projectRepository = projectRepository;
            _releaseRepository = releaseRepository;
            _resourceRepository = resourceRepository;

        }

        #endregion Constructor

        #region UC-18

        /// <summary>
        /// Returns the RightsReviewWorkQueue Page
        /// </summary>
        /// <returns></returns>
        public ActionResult RightsReviewWorkQueue()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                PermissionCheckNdRedirect(new[] { GrsTasks.RightsReviewWQ });
                //get master data
                GetAcquiredRightsMasterData();
                // construct Resource model


                var resourceModel = new ResourceRightsWorkQueueModel
                {
                    RightsPeriodDropDown = acquiredRightsMasterData.RightsPeriod.Values.Select(t => t.Replace(" ", Constants.HtmlSpace)).ToList(),
                    LostRightReasonDropDown =
                        acquiredRightsMasterData.LostRightsReason.Values.Select(t => t.Replace(" ", Constants.HtmlSpace)).ToList(),
                    ReviewReasonDropDown = acquiredRightsMasterData.ReviewReason.Select(
                    reason =>
                    new SelectListItem { Text = reason.Value, Value = reason.Key.ToString() }).ToList()
                };


                var userPermittedTask = PermissionExtension.GetPermittedTasks(GrsTasks.EditRightsDataHeader, GrsTasks.EditRightsDataDigital,
                                                 GrsTasks.EditRightsDataSecondary, GrsTasks.EditRightsDataPrecleared);

                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataHeader))
                {
                    ViewBag.EditRightsDataHeader = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataDigital))
                {
                    ViewBag.EditRightsDataDigital = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataSecondary))
                {
                    ViewBag.EditRightsDataSecondary = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataPrecleared))
                {
                    ViewBag.EditRightsDataPrecleared = true;
                }

                //return model to view
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return View(resourceModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Returns the RightsAcquired Page
        /// </summary>
        /// <returns></returns>
        public ActionResult RightsAcquired()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                // get rights master data
                var acquiredRightMaterData = _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData();
                // construct Resource model
                var resourceModel = new ResourceRightsWorkQueueModel
                {
                    // RightsResult = rightsAcquiredResult,
                    RightsPeriodDropDown = acquiredRightMaterData.RightsPeriod.Values.ToList(),
                    LostRightReasonDropDown =
                        acquiredRightMaterData.LostRightsReason.Values.ToList(),
                    ReviewReasonDropDown =
                        (List<SelectListItem>)
                        acquiredRightMaterData.ReviewReason.Select(
                            reason =>
                            new SelectListItem { Text = reason.Value, Value = reason.Key.ToString() })
                };

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();
                return PartialView(Constants.RightsAcquired, resourceModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Rights review work queue Get Method.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">Filter Parameters.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RightsReviewWorkQueue(PagingParams args, string filterParams)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var result = new ResourceRightsResult();
                if (filterParams != null)
                {
                    var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                    result = GetResourceRightsAcquiredData(filterParameters);
                    ViewData[Constants.totalrightsAcquiredData] = result.TotalRows;
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Rights acquired grid Data load.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RightsAcquired(PagingParams args, string filterParams)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var result = new ResourceRightsResult();
                if (filterParams != null)
                {
                    var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                    result = GetResourceRightsAcquiredData(filterParameters);
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Gets the filled filter params.
        /// </summary>
        /// <param name="filterParams">Filter parameters.</param>
        /// <param name="args">The args.</param>
        /// <param name="editingTask">The editing task.</param>
        /// <returns></returns>
        private ResourceFilterParameters GetFilledFilterParams(string filterParams, PagingParams args, GrsTasks editingTask)
        {

            var filterCriteria = JsonSerializer(filterParams);

            //On Default Load of Grid
            if (!filterCriteria.Resource && !filterCriteria.Track)
            {
                filterCriteria.Resource = true;
                filterCriteria.Track = true;
            }

            var resourceFilter = new ResourceFilterParameters
            {
                IsResource = filterCriteria.Resource,
                ISRC = filterCriteria.Isrc,
                SampleExists = filterCriteria.SampleExists,
                AdminCompany = filterCriteria.ClearanceAdminCompany == 0 ? null : filterCriteria.ClearanceAdminCompany.ToString(),
                ResourceTitle = filterCriteria.Title,
                ReviewReason = filterCriteria.ReasonForReview,
                SideArtist = filterCriteria.SideArtistExists,
                UserName = SessionWrapper.CurrentUserInfo.UserLoginName,
                RepertoireFilter = new RepertoireFilter
                {
                    Artist = filterCriteria.ArtistName,
                    LinkedContract = filterCriteria.LinkedContract,
                    UPC = filterCriteria.Upc,

                },
                IsTrack = filterCriteria.Track,
                IsImage = filterCriteria.Images,
                IsVideo = filterCriteria.Video,
                IsAudio = filterCriteria.Audio,
                Status = GetFilterReviewStatus(filterCriteria),
                IsExactSearch = filterCriteria.IsExactSearch,
                StartIndex = args.StartIndex < Constants.ValueZero ? Constants.ValueZero : args.StartIndex + 1,
                PageSize = args.PageSize == Constants.ValueZero ? Constants.PagesizeIntegerValue25 : args.PageSize,
                Filters = ApplyDigitalFilters(filterCriteria),
                TaskBasedCompanies = SessionWrapper.CurrentPermissions.
                    GetCompanyIdsByIndividualTasks(new GrsTasks[]{GrsTasks.RightsReviewWQ ,                           
                                                                                         GrsTasks.UpdateReviewStatus,
                                                                                         GrsTasks.ViewContractSenstiveData,
                                                                                         GrsTasks.ViewSensitiveRightsData,
                                                                                         editingTask}),
                IsCustomWorkQueue = false,
                Referer = filterCriteria.IsNavigatedWq,
                RightSetList = SessionWrapper.RightSetIds
            };
            if (resourceFilter.Referer == Constants.ListOneValue)
            {
                resourceFilter.IsCustomWorkQueue = true;
            }

            var currentRequest = (RequestType)args.RequestType;

            if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
            {
                if (args.SortDescriptors != null)
                {
                    resourceFilter.SortField = args.SortDescriptors[0].ColumnName;
                    resourceFilter.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                }
            }
            return resourceFilter;
        }


        /// <summary>
        /// Gets the Custome filled filter params. - For UC19
        /// </summary>
        /// <param name="filterParams">Filter parameters.</param>
        /// <param name="args">The args.</param>
        /// <param name="editingTask"> </param>
        /// <returns></returns>
        private ResourceFilterParameters GetCustomFilledFilterParams(string filterParams, PagingParams args, GrsTasks editingTask)
        {

            var filterCriteria = JsonSerializer(filterParams);

            //On Default Load of Grid
            if (!filterCriteria.Resource && !filterCriteria.Track)
            {
                filterCriteria.Resource = true;
                filterCriteria.Track = true;
            }

            var resourceFilter = new ResourceFilterParameters
            {
                IsResource = filterCriteria.Resource,
                //FromDt = filterCriteria.FromDt,
                //PreDefinedID = 1,
                PreDefinedID = filterCriteria.PredefinedQuereyTypeId,
                ISRC = filterCriteria.Isrc,
                SampleExists = filterCriteria.SampleExists,
                AdminCompany = filterCriteria.ClearanceAdminCompany == 0 ? null : filterCriteria.ClearanceAdminCompany.ToString(),
                ResourceTitle = filterCriteria.Title,
                SideArtist = filterCriteria.SideArtistExists,
                UserName = SessionWrapper.CurrentUserInfo.UserLoginName,
                NoReleaseDate = filterCriteria.NoReleaseDate,
                RepertoireFilter = new RepertoireFilter
                {
                    Artist = filterCriteria.ArtistName,
                    LinkedContract = filterCriteria.LinkedContract,
                    UPC = filterCriteria.Upc,
                    ReleaseTitle = filterCriteria.ReleaseTitle
                },
                IsFinal = filterCriteria.Final,
                IsTrack = filterCriteria.Track,
                IsExactSearch = filterCriteria.IsExactSearch,
                Status = GetFilterReviewStatus(filterCriteria),
                StartIndex = args.StartIndex < Constants.ValueZero ? Constants.ValueZero : args.StartIndex + 1,
                PageSize = args.PageSize == Constants.ValueZero ? Constants.PagesizeIntegerValue25 : args.PageSize,
                Referer = filterCriteria.IsNavigatedWq,
                StartDate = filterCriteria.FromDt == "" ? (DateTime?)null : Convert.ToDateTime(filterCriteria.FromDt),
                EndDate = filterCriteria.ToDt == "" ? (DateTime?)null : Convert.ToDateTime(filterCriteria.ToDt),

                //Filters = ApplyDigitalFilters(filterCriteria),
                TaskBasedCompanies = SessionWrapper.CurrentPermissions.
                    GetCompanyIdsByIndividualTasks(new GrsTasks[]{GrsTasks.RightsReviewWQ ,                           
                                                                                         GrsTasks.UpdateReviewStatus,
                                                                                         GrsTasks.ViewContractSenstiveData,
                                                                                         GrsTasks.ViewSensitiveRightsData,
                                                                                         editingTask}),
                IsCustomWorkQueue = true
            };
            var currentRequest = (RequestType)args.RequestType;

            if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
            {
                if (args.SortDescriptors != null)
                {
                    resourceFilter.SortField = args.SortDescriptors[0].ColumnName;
                    resourceFilter.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                }
            }
            return resourceFilter;
        }

        /// <summary>
        /// Jsons the serializer.
        /// </summary>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        private RightsFilterModel JsonSerializer(string filterParams)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(RightsFilterModel));
            var filterMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(filterParams));
            var filterCriteria = (RightsFilterModel)jsonSerializer.ReadObject(filterMemoryStream);

            return filterCriteria;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterParams"></param>
        /// <param name="args"></param>
        /// <param name="editingTask"></param>
        /// <returns></returns>

        private PreDefinedParametersWQ GetCustomFilledPredefinedParams(string filterParams, PagingParams args, GrsTasks editingTask)
        {

            var filterCriteria = JsonSerializer(filterParams);

            //On Default Load of Grid


            var predefinedFilter = new PreDefinedParametersWQ
            {
                PreDefinedID = filterCriteria.PredefinedQuereyTypeId,
                NoReleaseDate = filterCriteria.NoReleaseDate,
                StartIndex = args.StartIndex < Constants.ValueZero ? Constants.ValueZero : args.StartIndex + 1,
                PageSize = args.PageSize == Constants.ValueZero ? Constants.PagesizeIntegerValue25 : args.PageSize,
                FromDt = filterCriteria.FromDt == "" ? (DateTime?)null : Convert.ToDateTime(filterCriteria.FromDt),
                ToDt = filterCriteria.ToDt == "" ? (DateTime?)null : Convert.ToDateTime(filterCriteria.ToDt),
                //Filters = ApplyDigitalFilters(filterCriteria),
                TaskBasedCompanies = SessionWrapper.CurrentPermissions.
                    GetCompanyIdsByIndividualTasks(new GrsTasks[]{GrsTasks.RightsReviewWQ ,                           
                                                                                         GrsTasks.UpdateReviewStatus,
                                                                                       GrsTasks.ViewContractSenstiveData,
                                                                                        GrsTasks.ViewSensitiveRightsData,
                                                                                         editingTask}),

            };
            var currentRequest = (RequestType)args.RequestType;

            if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
            {
                if (args.SortDescriptors != null)
                {
                    predefinedFilter.SortField = args.SortDescriptors[0].ColumnName;
                    predefinedFilter.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                }
            }
            return predefinedFilter;
        }

        /// <summary>
        /// ApplyDigitalFilters
        /// 
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>


        private List<Filter> ApplyDigitalFilters(RightsFilterModel filterCriteria)
        {
            var digitalFilters = new List<Filter>();


            string useType = "";
            if (filterCriteria.Download)
                //useType = Constants.ListOneValue;
                useType = string.Format(Constants.ListOneValue);
            if (filterCriteria.Streaming)
            {
                if (filterCriteria.Download)
                {
                    //useType += Constants.Comma;
                    useType += string.Format(Constants.Comma);
                }
                //useType += Constants.ListTwoValue;
                useType += string.Format(Constants.ListTwoValue);
            }
            digitalFilters.Add(new Filter() { FieldName = Constants.UseType, Value = useType });

            string commercialModel = "";
            if (filterCriteria.AlaCarte)
                //commercialModel = Constants.ListOneValue;
                commercialModel = string.Format(Constants.ListOneValue);
            if (filterCriteria.Subscription)
            {
                if (filterCriteria.AlaCarte)
                {
                    //commercialModel += Constants.Comma;
                    commercialModel += string.Format(Constants.Comma);
                }
                //commercialModel += Constants.ListTwoValue;
                commercialModel += string.Format(Constants.ListTwoValue);
            }
            if (filterCriteria.AdFunded)
            {
                if (filterCriteria.AlaCarte || filterCriteria.Subscription)
                {
                    //commercialModel += Constants.Comma;
                    commercialModel += string.Format(Constants.Comma);
                }
                //commercialModel += Constants.ListThreeValue;
                commercialModel += string.Format(Constants.ListThreeValue);
            }
            digitalFilters.Add(new Filter() { FieldName = Constants.CommercialModel, Value = commercialModel });
            string restrictionType = "";
            if (filterCriteria.NoRights)
                //restrictionType = Constants.ListOneValue;
                restrictionType = string.Format(Constants.ListOneValue);
            if (filterCriteria.ConsentRequired)
            {
                if (filterCriteria.NoRights)
                {
                    //restrictionType += Constants.Comma;
                    restrictionType += string.Format(Constants.Comma);
                }
                //restrictionType += Constants.ListTwoValue;
                restrictionType += string.Format(Constants.ListTwoValue);
            }
            if (filterCriteria.ReferToLegal)
            {
                if (filterCriteria.NoRights || filterCriteria.ConsentRequired)
                {
                    //restrictionType += Constants.Comma;
                    restrictionType += string.Format(Constants.Comma);
                }
                //restrictionType += Constants.ListThreeValue;
                restrictionType += string.Format(Constants.ListThreeValue);
            }
            if (filterCriteria.NoRestriction)
            {
                if (filterCriteria.NoRights || filterCriteria.ConsentRequired || filterCriteria.ReferToLegal)
                {
                    //restrictionType += Constants.Comma;
                    restrictionType += string.Format(Constants.Comma);
                }
                //restrictionType += Constants.ListFourValue;
                restrictionType += string.Format(Constants.ListFourValue);
            }
            if (filterCriteria.Consult)
            {
                if (filterCriteria.NoRights || filterCriteria.ConsentRequired || filterCriteria.ReferToLegal || filterCriteria.NoRestriction)
                {
                    //restrictionType += Constants.Comma;
                    restrictionType += string.Format(Constants.Comma);
                }
                //restrictionType += Constants.ListFiveValue;
                restrictionType += string.Format(Constants.ListFiveValue);
            }
            digitalFilters.Add(new Filter() { FieldName = Constants.RestrictionType, Value = restrictionType });
            string consentPeriod = "";
            if (filterCriteria.DuringHoldBackPeriod)
                //consentPeriod = Constants.ListOneValue;
                consentPeriod = string.Format(Constants.ListOneValue);
            if (filterCriteria.DuringTerm)
            {
                if (filterCriteria.DuringHoldBackPeriod)
                {
                    //consentPeriod += Constants.Comma;
                    consentPeriod += string.Format(Constants.Comma);
                }
                //consentPeriod += Constants.ListTwoValue;
                consentPeriod += string.Format(Constants.ListTwoValue);
            }
            if (filterCriteria.DuringAndAfterTerm)
            {
                if (filterCriteria.DuringHoldBackPeriod || filterCriteria.DuringTerm)
                {
                    //consentPeriod += Constants.Comma;
                    consentPeriod += string.Format(Constants.Comma);
                }
                //consentPeriod += Constants.ListThreeValue;
                consentPeriod += string.Format(Constants.ListThreeValue);
            }
            digitalFilters.Add(new Filter() { FieldName = Constants.ConsentPeriod, Value = consentPeriod });
            return digitalFilters;
        }

        /// <summary>
        /// Returns the filter review status.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        private ReviewStatusType GetFilterReviewStatus(RightsFilterModel filterCriteria)
        {

            var reviewStatus = ReviewStatusType.All;

            if (filterCriteria.NewForReview && filterCriteria.FinalForReview)
            {
                reviewStatus = ReviewStatusType.NewForReview | ReviewStatusType.FinalForReview;
            }
            else if (filterCriteria.NewForReview)
            {
                reviewStatus = ReviewStatusType.NewForReview;
            }
            else if (filterCriteria.FinalForReview)
            {
                reviewStatus = ReviewStatusType.FinalForReview;
            }


            return reviewStatus;
        }
        /// <summary>
        /// Returns the acquired rights mater data.
        /// </summary>
        /// <returns></returns>
        private ReviewRightsMasterInfo GetAcquiredRightsMasterData()
        {
            LoggerFactory.LogWriter.Debug(Constants.MethodStart);
            /*get master data*/
            if (SessionWrapper.AcquiredRightsMasterInfo == null)
            {
                acquiredRightsMasterData = _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData();
                acquiredRightsMasterData.LostRightsReason.Select(t => t.Value.Replace(" ", Constants.HtmlSpace)).ToList();

                SessionWrapper.AcquiredRightsMasterInfo = acquiredRightsMasterData;
            }
            else
            {
                acquiredRightsMasterData = SessionWrapper.AcquiredRightsMasterInfo;
            }
            if (!acquiredRightsMasterData.ReviewReason.ContainsKey(0))
                acquiredRightsMasterData.ReviewReason.Add(0, "");
            LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
            return acquiredRightsMasterData;
        }

        /// <summary>
        /// Returns the acquired rights mater data.
        /// </summary>
        /// <returns></returns>
        private DigitalRightsMasterData GetDigitalRightsMasterData()
        {
            LoggerFactory.LogWriter.Debug(Constants.MethodStart);
            /*get master data*/
            if (SessionWrapper.DigitalRightsMasterInfo == null)
            {
                digitalRightsMasterData = _rightsReviewWorkQueueRepository.LoadDigitalMasterData();
                SessionWrapper.DigitalRightsMasterInfo = digitalRightsMasterData;
            }
            else
            {
                digitalRightsMasterData = SessionWrapper.DigitalRightsMasterInfo;
            }
            LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
            return digitalRightsMasterData;
        }


        /// <summary>
        /// Saves the resource acquired rights.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="resourceRights">The resource rights.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="tabIndex"> </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveResourceAcquiredRights(PagingParams args, [Bind(Prefix = Constants.updatedRecords)]IEnumerable<ResourcesRight> resourceRights, string filterParams, string tabIndex)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                GetAcquiredRightsMasterData();
                acquiredRightsMasterData = acquiredRightsMasterData.ConstructSaveReverseInfo();
                resourceRights = UpdateMasterDataForSave(acquiredRightsMasterData, resourceRights.ToList());
                var resourceInfo = new ResourceRightsSaveInfo
                {
                    ResourceRights =
                        resourceRights.Select(right => right.ConstructSaveInfo()).ToList(),
                    ModifierInfo = { UserName = SessionWrapper.CurrentUserInfo.UserLoginName }
                };



                //save grid
                List<long> updatedRights = _rightsReviewWorkQueueRepository.SaveReviewedResourceRights(resourceInfo);
                // Load grid after save
                var result = new ResourceRightsResult();
                if (tabIndex == null)
                {
                    //UC 18 Part//
                    if (filterParams != null)
                    {
                        var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                        result = GetResourceRightsAcquiredData(filterParameters);


                    }
                }
                else
                {
                    //UC 19 Part//
                    if (filterParams != null)
                    {
                        if (tabIndex == Constants.ReleaseCustomTab)
                        {
                            var filterParameters = GetCustomFilledFilterParams(filterParams, args,
                                                                               GrsTasks.EditRightsDataHeader);
                            result = GetResourceRightsAcquiredData(filterParameters);

                        }
                        else if (tabIndex == Constants.ReleasePredefinedTab)
                        {
                            var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args,
                                                                              GrsTasks.EditRightsDataHeader);
                            result = GetResourceRightsAcquiredDataPredefinedParameters(filterPredefinedParameters);

                        }
                    }


                }
                ViewData[Constants.totalrightsAcquiredData] = result.TotalRows;
                result.ResourceRights.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                                                                                     (resultRight.Rights.
                                                                                          RightSetId)
                                                                                     ? Constants.Concurrencyproblems
                                                                                     : null);

                result.ResourceRights.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                                                                                       (resultRight.Rights.
                                                                                            RightSetId)
                                                                                       ? Constants.Concurrencyproblems
                                                                                       : null);
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }


        /// <summary>
        /// Returns the DigitalRestrictions Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResourceDigitalRestrictions()
        {
            LoggerFactory.MeasureLogWriter.Start();
            var digitalRightModel = new ResourceDigitalRightsReviewModel();
            digitalRightModel = ConvertResourceDigitalMasterData(digitalRightModel);
            LoggerFactory.MeasureLogWriter.Stop();
            return PartialView(Constants.ResourceDigitalRestrictions, digitalRightModel);
        }



        /// <summary>
        /// Resources the digital restrictions.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceDigitalRestrictions(PagingParams args, string filterParams)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                //create digital restriction grid model
                var digitalRightsModel = new ResourceDigitalRightsReviewModel();
                digitalRightsModel = LoadResourceDigitalRestrictions(digitalRightsModel, filterParams, args);
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                var data = digitalRightsModel.BindedDigitalRights;
                args.PageSize = digitalRightsModel.BindedDigitalRights.Count();
                ViewData[@RepertoireSearch.Digital] = digitalRightsModel;
                LoggerFactory.MeasureLogWriter.Stop();

                return data.GridJSONActions<List<ResourceDigitalRights>>
                            (digitalRightsModel.DigitalRightsResult.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Loads the resource digital master data.
        /// </summary>
        /// <param name="digitalRightsModel">The digital rights model.</param>
        /// <returns></returns>
        private ResourceDigitalRightsReviewModel ConvertResourceDigitalMasterData
            (ResourceDigitalRightsReviewModel digitalRightsModel)
        {
            digitalRightsModel.DigitalRightsMasterData =
               _rightsReviewWorkQueueRepository.LoadDigitalMasterData();
            digitalRightsModel.ConsentPeriodDropDown = digitalRightsModel.
                DigitalRightsMasterData.ConsentPeriodTypes.Values.Select(restriction => restriction.Replace(" ", Constants.HtmlSpace)).ToArray();
            digitalRightsModel.UseTypeDropDown = digitalRightsModel.
                DigitalRightsMasterData.UseTypes.Values.ToArray();
            digitalRightsModel.RestrictionDropDown = digitalRightsModel.
                DigitalRightsMasterData.RestrictionTypes.Values.Select(restriction => restriction.Replace(" ", Constants.HtmlSpace)).ToArray();
            digitalRightsModel.CommercialModelReasonDropDown = digitalRightsModel.
                DigitalRightsMasterData.CommercialModelTypes.Values.Select(restriction => restriction.Replace(" ", Constants.HtmlSpace)).ToArray();
            digitalRightsModel.RestrictionReasonDropDown = digitalRightsModel.
                DigitalRightsMasterData.RestrictionReason.Values.Select(restriction => restriction.Replace(" ", Constants.HtmlSpace)).ToArray();
            return digitalRightsModel;
        }


        /// <summary>
        /// Loads the resource digital restrictions.
        /// </summary>
        /// <param name="digitalRightsModel">The digital rights model.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        private ResourceDigitalRightsReviewModel LoadResourceDigitalRestrictions
            (ResourceDigitalRightsReviewModel digitalRightsModel,
            string filterParams, PagingParams args)
        {
            //assisgn Filter parameters
            if (filterParams != null)
            {
                var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataDigital);
                digitalRightsModel.DigitalRightsResult =
                    _rightsReviewWorkQueueRepository.LoadResourceDigitalRights(filterParameters);
                digitalRightsModel = ConvertResourceDigitalMasterData(digitalRightsModel);
                //construct/form linear digital restriction grid result
                digitalRightsModel.ConstructViewInfo();
                digitalRightsModel.BindedDigitalRights.ForEach(right =>
                {
                    right.RightsTypeLnr = RightsTypeDescriptions(right.RightsType);
                    right.ReviewReasonLnr = GetAcquiredRightsMasterData().
                            AppendReviewReason(right.ReviewReasons);
                });
                ViewBag.totalRecords = digitalRightsModel.DigitalRightsResult.TotalRows;
            }
            return digitalRightsModel;
        }


        /// <summary>
        /// Loads the resource digital restrictions.
        /// </summary>
        /// <param name="digitalRightsModel">The digital rights model.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex"> </param>
        /// <returns></returns>
        private ResourceDigitalRightsReviewModel LoadCustomResourceDigitalRestrictions
            (ResourceDigitalRightsReviewModel digitalRightsModel,
            string filterParams, PagingParams args, string tabIndex)
        {
            //assisgn Filter parameters
            if (filterParams != null)
            {

                if (tabIndex == Constants.ReleaseCustomTab)
                {
                    var filterParameters = GetCustomFilledFilterParams(filterParams, args,
                                                                       GrsTasks.EditRightsDataDigital);
                    //filterParameters.AdminCompany = "12404";
                    digitalRightsModel.DigitalRightsResult =
                        _rightsReviewWorkQueueRepository.LoadResourceDigitalRights(filterParameters);
                }
                if (tabIndex == Constants.ReleasePredefinedTab)
                {
                    var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args, GrsTasks.EditRightsDataDigital);
                    digitalRightsModel.DigitalRightsResult =
                        _rightsReviewWorkQueueRepository.LoadResourceDigitalRightsPredefined(filterPredefinedParameters);
                }
                digitalRightsModel = ConvertResourceDigitalMasterData(digitalRightsModel);
                //construct/form linear digital restriction grid result
                digitalRightsModel.ConstructViewInfo();
                digitalRightsModel.BindedDigitalRights.ForEach(right =>
                {
                    right.RightsTypeLnr = RightsTypeDescriptions(right.RightsType);
                    right.ReviewReasonLnr = GetAcquiredRightsMasterData().
                            AppendReviewReason(right.ReviewReasons);
                });
                ViewBag.totalRecords = digitalRightsModel.DigitalRightsResult.TotalRows;
            }
            return digitalRightsModel;
        }


        private string RightsTypeDescriptions(RightTypes rightType)
        {
            return EnumExtensions.GetDescription(rightType);
        }

        /// <summary>
        /// Saves the resource digital restrictions.
        /// </summary>
        /// <param name="digitalRightsUpdate">The digital rights update.</param>
        /// <param name="digitalRightsAdd">The digital rights add.</param>
        /// <param name="digitalRightsDelete">The digital rights delete.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex"> </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveResourceDigitalRestrictions
            ([Bind(Prefix = Constants.updatedRecords)]IEnumerable<ResourceDigitalRights> digitalRightsUpdate,
            [Bind(Prefix = Constants.addedRecords)]IEnumerable<ResourceDigitalRights> digitalRightsAdd,
            [Bind(Prefix = Constants.deletedRecords)]IEnumerable<ResourceDigitalRights> digitalRightsDelete,
            string filterParams, PagingParams args, string tabIndex)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                //linear to nested class values append
                //digital save method
                var updateDigitalRightsList = new List<ResourceDigitalRestrictions>();

                GetDigitalRightsMasterData();
                digitalRightsMasterData = digitalRightsMasterData.ConstructSaveReverseInfo();
                if (digitalRightsUpdate != null && digitalRightsUpdate.Any())
                {
                    updateDigitalRightsList.AddRange(digitalRightsUpdate.
                                                         Select
                                                         (right => ConstructDigitalSaveInfo(right, Constants.Updated)).
                                                         ToList());
                }
                if (digitalRightsAdd != null && digitalRightsAdd.Any())
                {
                    updateDigitalRightsList.AddRange(digitalRightsAdd.
                                                         Select
                                                         (right => ConstructDigitalSaveInfo(right, Constants.Added)).
                                                         ToList());
                }
                if (digitalRightsDelete != null && digitalRightsDelete.Any())
                {
                    updateDigitalRightsList.AddRange(digitalRightsDelete.
                                                         Select
                                                         (right => ConstructDigitalSaveInfo(right, Constants.Deleted)).
                                                         ToList());
                }

                updateDigitalRightsList =
                 ConstructDigitalRightForUpdate(updateDigitalRightsList);

                List<long> updatedRights = _rightsReviewWorkQueueRepository.
                    SaveResourceDigitalRights(updateDigitalRightsList, new ModifierInfo() { UserName = SessionWrapper.CurrentUserInfo.UserLoginName });

                // After Save load updated data
                //create digital grid model
                var digitalRightsModel = new ResourceDigitalRightsReviewModel();
                //assisgn Filter parameters

                if (tabIndex == null)
                {
                    //UC 18 Part//
                    LoadResourceDigitalRestrictions(digitalRightsModel, filterParams, args);
                }
                else
                {
                    //UC 19 Part//
                    if (tabIndex == Constants.ReleaseCustomTab)
                    {
                        LoadCustomResourceDigitalRestrictions(digitalRightsModel, filterParams, args, tabIndex);
                    }
                    else if (tabIndex == Constants.ReleasePredefinedTab)
                    {
                        LoadCustomResourceDigitalRestrictions(digitalRightsModel, filterParams, args, tabIndex);
                    }
                }

                var data = digitalRightsModel.BindedDigitalRights;
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                data.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                        (resultRight.DigitalRestriction.DigitalRestrictions.RightSetId) ? Constants.Concurrencyproblems : null);
                LoggerFactory.MeasureLogWriter.Stop();

                return data.GridJSONActions<List<ResourceDigitalRights>>
                       (digitalRightsModel.DigitalRightsResult.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private ResourceDigitalRestrictions ConstructDigitalSaveInfo
           (ResourceDigitalRights resourceDigitalRights, string status)
        {
            if (resourceDigitalRights.RestrictonReasonLnr != null)
            {
                var reason = resourceDigitalRights.RestrictonReasonLnr.Replace(" ", "").Replace(" ", "");
                if (reason != Constants.SideArtistIssues && reason != Constants.SampleIssues && reason != Constants.NoRights && reason != Constants.ArtistConsentRequired)
                {
                    if (reason != Constants.Other)
                    {
                        resourceDigitalRights.RestrictionReasonForOthers = resourceDigitalRights.RestrictonReasonLnr;//To Handle the scenario when free-text is given for new row     
                    }

                    resourceDigitalRights.RestrictonReasonLnr = Constants.Other;
                }
            }

            if (status == Constants.Deleted && resourceDigitalRights.RestrictionIdLnr == -1)
            {
                resourceDigitalRights.DigitalRestriction = null;
                return resourceDigitalRights.DigitalRestriction;
            }

            if (resourceDigitalRights.DigitalRestriction == null)
                resourceDigitalRights.DigitalRestriction =
                    new ResourceDigitalRestrictions();

            resourceDigitalRights.DigitalRestriction.DigitalRestrictions.ModifiedDateTime =
                resourceDigitalRights.ModifiedDate;
            resourceDigitalRights.DigitalRestriction.DigitalRestrictions.RightSetId =
                resourceDigitalRights.RightSetIdLnr;
            if (resourceDigitalRights.ConsentPeriodLnr == Constants.Null)
            {
                resourceDigitalRights.ConsentPeriodLnr = null;
            }

            if (resourceDigitalRights.ConsentPeriodLnr != null && resourceDigitalRights.ConsentPeriodLnr.Trim().Length < 1)
            {
                resourceDigitalRights.ConsentPeriodLnr = null;
            }

            Enum.TryParse(resourceDigitalRights.ReviewStatusLnr, true,
                              out resourceDigitalRights.DigitalRestriction.ReviewStatus.Status);
            if (resourceDigitalRights.CommercialModelsLnr != null &&
                resourceDigitalRights.UseTypeLnr != null)
            {
                resourceDigitalRights.DigitalRestriction.DigitalRestrictions.DigitalRestrictions.Add
                    (new ContractDigitalRestrictions()
                    {
                        DigitalRestrictionId = resourceDigitalRights.RestrictionIdLnr,
                        ConsentPeriodId = (resourceDigitalRights.ConsentPeriodLnr != null)
                                      ? Convert.ToByte(
                                          digitalRightsMasterData.ConsentPeriodTypesReverse[
                                              resourceDigitalRights.ConsentPeriodLnr.Replace(" ", "").Replace(" ", "")])
                                      : (byte?)null,
                        CommercialModelId = Convert.ToByte(resourceDigitalRights.CommercialModelsLnr != null
                                                               ? digitalRightsMasterData.
                                                                     CommercialModelTypesReverse[
                                                                         resourceDigitalRights.
                                                                             CommercialModelsLnr.Replace(" ",
                                                                                                         "")
                                                                             .Replace(" ", "")]
                                                               : 0),
                        RestrictionId = Convert.ToByte(resourceDigitalRights.RestrictionLnr != null
                                                           ? digitalRightsMasterData.RestrictionTypesReverse[
                                                               resourceDigitalRights.RestrictionLnr.Replace(
                                                                   " ", "").Replace(" ", "")]
                                                           : 0),
                        RestrictionReasonId = Convert.ToByte(resourceDigitalRights.RestrictonReasonLnr != null
                                        ? digitalRightsMasterData.RestrictionReasonReverse[
                                            resourceDigitalRights.RestrictonReasonLnr.Replace(
                                                " ", "").Replace(" ", "")]
                                        : 0),
                        IsDeleted = status == Constants.Deleted,
                        IsInserted =
                        (status == Constants.Added || resourceDigitalRights.RestrictionIdLnr == -1),
                        RestrictionExist =
                            (status == Constants.Updated && resourceDigitalRights.RestrictionIdLnr != -1),
                        UseTypeId = Convert.ToByte(resourceDigitalRights.UseTypeLnr != null
                                                       ? digitalRightsMasterData.UseTypesReverse[
                                                           resourceDigitalRights.UseTypeLnr.Replace(" ", "").
                                                               Replace(" ", "")]
                                                       : 0),
                        Notes = resourceDigitalRights.NotesLnr,
                        RightSetId = resourceDigitalRights.RightSetIdLnr,
                        RestrictionOtherReasonInfo = resourceDigitalRights.RestrictionReasonForOthers
                    });
            }



            return resourceDigitalRights.DigitalRestriction;
        }

        /// <summary>
        /// Method to construct digital rights
        /// </summary>
        /// <param name="updateDigitalRightsList"></param>
        /// <returns></returns>
        private List<ResourceDigitalRestrictions>
            ConstructDigitalRightForUpdate(List<ResourceDigitalRestrictions> updateDigitalRightsList)
        {

            var digitalRightsForSave = new List<ResourceDigitalRestrictions>();
            for (int rightCount = 0; rightCount < updateDigitalRightsList.Count(); rightCount++)
            {
                var result = updateDigitalRightsList[rightCount];
                if (result != null)
                {
                    ResourceDigitalRestrictions resourceRestriction = updateDigitalRightsList[rightCount];
                    if (resourceRestriction != null)
                    {
                        if (resourceRestriction.DigitalRestrictions != null)
                        {
                            if (digitalRightsForSave.Any(right => right.DigitalRestrictions.RightSetId ==
                                                                  resourceRestriction.DigitalRestrictions.RightSetId))
                            {

                                ResourceDigitalRestrictions toUpdateRestriction =
                                    digitalRightsForSave.First
                                        (right => right.DigitalRestrictions.RightSetId ==
                                                  resourceRestriction.DigitalRestrictions.RightSetId);
                                if (resourceRestriction.DigitalRestrictions.DigitalRestrictions.Any())
                                {
                                    toUpdateRestriction.DigitalRestrictions.DigitalRestrictions.Add
                                        (resourceRestriction.DigitalRestrictions.DigitalRestrictions[0]);
                                }
                            }
                            else
                            {
                                digitalRightsForSave.Add(resourceRestriction);
                            }
                        }
                    }
                }
            }
            return digitalRightsForSave;
        }


        /// <summary>
        /// Return the SecondaryExploitation Page
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult SecondaryExploitation()
        {
            return PartialView(Constants.SecondaryExploitation, new ResourceSecondaryRightsReviewModel());
        }


        /// <summary>
        /// Mock Data for SecondaryExploitation Grid.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SecondaryExploitation(PagingParams args, string filterParams)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                //create pre clearance grid model
                var secondaryRightsModel = new ResourceSecondaryRightsReviewModel();
                //assisgn Filter parameters
                if (filterParams != null)
                {

                    var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataSecondary);
                    secondaryRightsModel.RightsData =
                        _rightsReviewWorkQueueRepository.LoadResourceSecondaryRights(filterParameters);
                    var count = 0;
                    secondaryRightsModel.RightsData.RightsResult.ForEach(right =>
                    {
                        right.ConstructViewInfo();
                        right.RightsTypeLnr = RightsTypeDescriptions(right.RightsType);
                        right.ReviewReasonLnr = GetAcquiredRightsMasterData().AppendReviewReason(right.ReviewReasons);
                        right.RowId = count++.ToString();
                    });
                    ViewData[Constants.totalSecondaryData] = secondaryRightsModel.RightsData.TotalRows;
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return secondaryRightsModel.RightsData.RightsResult.GridJSONActions<List<ResourceSecondaryRights>>
                        (secondaryRightsModel.RightsData.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        /// <summary>
        /// Saves the resource secondary rights.
        /// </summary>
        /// <param name="resourceRights">The resource rights.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex"> </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveResourceSecondaryRights([Bind(Prefix = Constants.updatedRecords)]IEnumerable<ResourceSecondaryRights> resourceRights, string filterParams, PagingParams args, string tabIndex)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var secondaryRightsModel = new ResourceSecondaryRightsReviewModel();
                GetAcquiredRightsMasterData();
                var resourceSecondaryRights = resourceRights as ResourceSecondaryRights[] ?? resourceRights.ToArray();
                if (resourceSecondaryRights.Any())
                {
                    foreach (var resourceSecondaryRight in resourceSecondaryRights)
                    {
                        var jsonData = resourceSecondaryRight.JsonStringDatas;
                        var jsonSerializer = new DataContractJsonSerializer(typeof(ContractExploitationRestrictions[]));
                        var JsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));

                        var jsonArray = (ContractExploitationRestrictions[])jsonSerializer.ReadObject(JsonStream);
                        if (jsonArray != null)
                        {
                            // ReSharper disable RedundantEnumerableCastCall
                            var exploitationList = jsonArray.OfType<ContractExploitationRestrictions>().ToList();
                            // ReSharper restore RedundantEnumerableCastCall
                            resourceSecondaryRight.Exploitations = exploitationList;
                        }
                    }

                }

                var secondaryRight = new SecondaryRightsSaveInfo { Rights = new List<SecondaryRights>() };
                secondaryRight.Rights = resourceSecondaryRights.Select(right => right.ConstructSaveInfo()).ToList();
                secondaryRight.ModifierInfo = new ModifierInfo() { UserName = SessionWrapper.CurrentUserInfo.UserLoginName };

                List<long> updatedRights = _rightsReviewWorkQueueRepository.SaveResourceSecondaryRights(secondaryRight);
                if (filterParams != null)
                {
                    if (tabIndex == null)
                    {
                        //UC 18 Part//
                        var filterParameters = GetFilledFilterParams(filterParams, args,
                                                                     GrsTasks.EditRightsDataSecondary);
                        secondaryRightsModel.RightsData =
                            _rightsReviewWorkQueueRepository.LoadResourceSecondaryRights(filterParameters);
                    }
                    else
                    {
                        //UC 19 Part//
                        if (tabIndex == Constants.ReleaseCustomTab)
                        {
                            var filterParameters = GetCustomFilledFilterParams(filterParams, args,
                                                                               GrsTasks.EditRightsDataSecondary);
                            secondaryRightsModel.RightsData =
                                _rightsReviewWorkQueueRepository.LoadResourceSecondaryRights(filterParameters);
                        }
                        else if (tabIndex == Constants.ReleasePredefinedTab)
                        {
                            var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args,
                                                                               GrsTasks.EditRightsDataSecondary);
                            secondaryRightsModel.RightsData =
                                _rightsReviewWorkQueueRepository.LoadResourceSecondaryRightsPredefined(
                                    filterPredefinedParameters);
                        }

                    }

                    //construct/form linear secondary rights grid result
                    secondaryRightsModel.RightsData.RightsResult =
                        secondaryRightsModel.RightsData.RightsResult.Select(
                            secondaryRights => secondaryRights.ConstructViewInfo()).ToList();
                    var count = 0;
                    secondaryRightsModel.RightsData.RightsResult.ForEach(
                        secondaryRights => secondaryRights.RowId = count++.ToString());
                    secondaryRightsModel.RightsData.RightsResult.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                        (resultRight.Rights.RightSetId) ? Constants.Concurrencyproblems : null);
                    secondaryRightsModel.RightsData.RightsResult.Select
                       (secondaryRights => secondaryRights.RightsTypeLnr = RightsTypeDescriptions(secondaryRights.RightsType)).ToList();

                    ViewData[Constants.totalSecondaryData] = secondaryRightsModel.RightsData.TotalRows;
                    LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                    return secondaryRightsModel.RightsData.RightsResult.GridJSONActions<ResourcesRight>(secondaryRightsModel.RightsData.TotalRows);
                }
                LoggerFactory.MeasureLogWriter.Stop();
                return new List<ResourceSecondaryRights>().GridJSONActions<List<ResourceSecondaryRights>>();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }


        /// <summary>
        /// Return the PreClearance Page
        /// </summary>
        /// <returns></returns>
        public ActionResult PreClearance()
        {
            return PartialView(Constants.PreClearance, new ResourcePreClearanceReviewModel());
        }

        /// <summary>
        /// Pres the clearance.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PreClearance(PagingParams args, string filterParams)
        {
            LoggerFactory.LogWriter.Debug(Constants.MethodStart);
            LoggerFactory.MeasureLogWriter.Start();

            //create pre clearance grid model
            var preClearanceModel = new ResourcePreClearanceReviewModel();
            //assisgn Filter parameters
            if (filterParams != null)
            {

                var filterParameters = GetFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataPrecleared);

                //   preClearanceModel = GetPreclearanceData(filterParameters);
                preClearanceModel.PreClearanceResult = _rightsReviewWorkQueueRepository.LoadResourcePreClearanceInfo(
                    filterParameters);
                preClearanceModel = GetPreclearanceData(preClearanceModel);
                ViewBag.totalRecords = preClearanceModel.PreClearanceResult.TotalRows;
                ViewData[Constants.totalPreclearanceData] = preClearanceModel.PreClearanceResult.TotalRows;
            }
            LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
            LoggerFactory.MeasureLogWriter.Stop();

            return preClearanceModel.PreClearanceResult.Rights.GridJSONActions<List<PreClearanceResult>>
                    (preClearanceModel.PreClearanceResult.TotalRows);

        }


        private ResourcePreClearanceReviewModel GetPreclearanceData(ResourcePreClearanceReviewModel preClearanceModel)
        {
            //load pre clearance grid master data for drop down
            preClearanceModel.PreClearanceMasterData =
                _rightsReviewWorkQueueRepository.LoadPreClearanceMasterData();
            //assign pre clearance drop down values grid result
            //construct/form linear pre clearance grid result
            preClearanceModel.PreClearanceResult.Rights.ForEach(right =>
            {
                right.ConstructViewInfo();
                right.ReviewReasonLnr =
                    GetAcquiredRightsMasterData().AppendReviewReason(right.ReviewReasons);
                right.RightsTypeLnr =
                     RightsTypeDescriptions(right.RightsType);
            });

            foreach (var result in preClearanceModel.PreClearanceResult.Rights)
            {
                foreach (var country in result.PreClearanceInfo.ExcludedCountries)
                {
                    if (!string.IsNullOrEmpty(result.ExcludedCountries))
                        result.ExcludedCountries = result.ExcludedCountries + "," + country.ToString();
                    else
                        result.ExcludedCountries = country.ToString();
                }
                foreach (var country in result.PreClearanceInfo.IncludedCountries)
                {
                    if (!string.IsNullOrEmpty(result.IncludedCountries))
                        result.IncludedCountries = result.IncludedCountries + "," + country.ToString();
                    else
                        result.IncludedCountries = country.ToString();
                }
            }
            return preClearanceModel;
        }

        /// <summary>
        /// Return the PriorityWorkQueue Page
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PriorityWorkQueue()
        {
            return PartialView(new ResourceRightsWorkQueueModel());
        }

        /// <summary>
        /// Clearances the admin country popup.
        /// </summary>
        /// <param name="clearanceIds">The clearance ids.</param>
        /// <param name="clearanceNames">The clearance names.</param>
        /// <returns></returns>
        public ActionResult ClearanceAdminCountryPopup(string clearanceIds, string clearanceNames)
        {
            return PartialView(Constants.ClearanceAdminCountryPopup);
        }

        /// <summary>
        /// Secondaries the exploitation popup.
        /// </summary>
        /// <returns></returns>3
        [HttpPost]
        public ActionResult SecondaryExploitationPopup(string resourceSecondaryRights)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ContractExploitationRestrictions[]));
            var filterMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(resourceSecondaryRights));
            var filterCriterea = (ContractExploitationRestrictions[])jsonSerializer.ReadObject(filterMemoryStream);
            var model = new ResourceSecondaryRightsReviewModel { MaterData = _rightsReviewWorkQueueRepository.LoadSecondaryMasterData() };

            filterCriterea.Select(exploit => exploit.ExploitationType = model.MaterData.ExploitationTypes[exploit.ExploitationTypeId]).ToList();
            filterCriterea.Select(exp => exp.IsRestriction ? exp.RestrictionOptionId = Constants.ListOneValue : exp.RestrictionOptionId = Constants.ListTwoValue).ToList();
            model.right = new ResourceSecondaryRights { Exploitations = filterCriterea.ToList() };
            model.Restrictions = model.MaterData.Restrictions.Where(restriction => restriction.Key != 4).Select(restriction => new SelectListItem
                                                                                        {
                                                                                            Text = restriction.Value.ToString(),
                                                                                            Value = restriction.Key.ToString()
                                                                                        }).
                    ToList();
            model.Restrictions.Add(new SelectListItem() { Text = string.Empty, Value = Constants.ListZeroValue });
            model.RestrictedOptions = model.MaterData.RestrictedOptions.Select(restriction => new SelectListItem
                                                                                                  {
                                                                                                      Text = restriction.Value.ToString(),
                                                                                                      Value = restriction.Key.ToString()
                                                                                                  }).ToList();


            model.ConsentPeriod = model.MaterData.ConsentPeriod.Select(restriction => new SelectListItem
                                                                                          {
                                                                                              Text = restriction.Value.ToString(),
                                                                                              Value = restriction.Key.ToString()
                                                                                          }).ToList();
            model.ConsentPeriod.Add(new SelectListItem()
                                        {
                                            Text = string.Empty,
                                            Value = Constants.ListZeroValue
                                        });
            model.ExploitationTypes = model.MaterData.ExploitationTypes.Select(restriction => new SelectListItem
                                                                                                  {
                                                                                                      Text = restriction.Value.ToString(),
                                                                                                      Value = restriction.Key.ToString()
                                                                                                  }).ToList();
            return PartialView(Constants.SecondaryExploitationPopup, model);
        }


        #endregion UC-18


        #region UC-19

        /// <summary>
        /// Returns the resources rights work queue Page.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="tabNumber"> </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResourceRightsWorkQueue(string pageId, string tabNumber)
        {

            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                ViewBag.CustomTabIndex = tabNumber;
                ViewBag.ResourcePageId = pageId;
                ViewBag.rightsAcquiredGridId = Constants.RightsAcquireGrid + pageId;
                ViewBag.DigitalGridId = Constants.DigitalRestrictionGrid + pageId;
                ViewBag.SecondaryExpGridId = Constants.SecondaryExploitationGrid + pageId;
                ViewBag.PreClearanceGridId = Constants.PreClearanceGrid + pageId;

                if (acquiredRightsMasterData == null)
                {
                    acquiredRightsMasterData = GetAcquiredRightsMasterData();
                }

                var resourceModel = new ResourceRightsWorkQueueModel
                {
                    RightsPeriodDropDown = acquiredRightsMasterData.RightsPeriod.Values.ToList().Select(rightsPeriod => rightsPeriod.Replace(" ", Constants.HtmlSpace)).ToList(),
                    LostRightReasonDropDown =
                        acquiredRightsMasterData.LostRightsReason.Values.ToList().Select(rightsPeriod => rightsPeriod.Replace(" ", Constants.HtmlSpace)).ToList(),
                    RightsResult = new ResourceRightsResult() { ResourceRights = new List<ResourcesRight>() }
                };

                resourceModel.RightsResult.ResourceRights =
                    resourceModel.GetMasterDataValues(acquiredRightsMasterData,
                    resourceModel.RightsResult.ResourceRights);

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return PartialView(Constants.ResourceRightsWorkQueue, resourceModel);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Resources the rights work queue.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex"> </param>
        /// <param name="filterParams"> </param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceRightsWorkQueue(PagingParams args, string tabIndex, string filterParams)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                var result = new ResourceRightsResult();
                if (filterParams != null)
                {
                    if (tabIndex == Constants.ReleaseCustomTab)
                    {
                        var filterParameters = GetCustomFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                        result = GetResourceRightsAcquiredData(filterParameters);
                    }

                    if (tabIndex == Constants.ReleasePredefinedTab)
                    {
                        var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                        result = GetResourceRightsAcquiredDataPredefinedParameters(filterPredefinedParameters);
                    }



                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ResourceCustomWQRightsAcquired(string pageId, string tabNumber)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                ViewBag.CustomTabIndex = tabNumber;
                ViewBag.ResourcePageId = pageId;
                ViewBag.rightsAcquiredGridId = Constants.RightsAcquireGrid + pageId;
                ViewBag.DigitalGridId = Constants.DigitalRestrictionGrid + pageId;
                ViewBag.SecondaryExpGridId = Constants.SecondaryExploitationGrid + pageId;
                ViewBag.PreClearanceGridId = Constants.PreClearanceGrid + pageId;

                LoggerFactory.LogWriter.Debug(Constants.MethodStart);

                if (acquiredRightsMasterData == null)
                {
                    acquiredRightsMasterData = GetAcquiredRightsMasterData();
                }

                var resourceModel = new ResourceRightsWorkQueueModel
                {
                    RightsPeriodDropDown = acquiredRightsMasterData.RightsPeriod.Values.ToList(),
                    LostRightReasonDropDown =
                        acquiredRightsMasterData.LostRightsReason.Values.ToList(),
                    RightsResult = new ResourceRightsResult() { ResourceRights = new List<ResourcesRight>() }
                };

                resourceModel.RightsResult.ResourceRights =
                    resourceModel.GetMasterDataValues(acquiredRightsMasterData,
                    resourceModel.RightsResult.ResourceRights);

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return PartialView(Constants.RightsAcquired, resourceModel);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceCustomWQRightsAcquired(PagingParams args, string tabIndex, string filterParams)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                var result = new ResourceRightsResult();
                if (filterParams != null)
                {
                    if (tabIndex == Constants.ReleaseCustomTab)
                    {
                        LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                        if (filterParams != null)
                        {
                            var filterParameters = GetCustomFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                            result = GetResourceRightsAcquiredData(filterParameters);
                        }
                    }

                    if (tabIndex == Constants.ReleasePredefinedTab)
                    {
                        var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args, GrsTasks.EditRightsDataHeader);
                        result = GetResourceRightsAcquiredDataPredefinedParameters(filterPredefinedParameters);
                    }

                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ResourceCustomWQDigitalRestrictions(string pageId, string tabNumber)
        {
            try
            {
                ViewBag.CustomTabIndex = tabNumber;
                ViewBag.ResourcePageId = pageId;
                ViewBag.rightsAcquiredGridId = Constants.RightsAcquireGrid + pageId;
                ViewBag.DigitalGridId = Constants.DigitalRestrictionGrid + pageId;
                ViewBag.SecondaryExpGridId = Constants.SecondaryExploitationGrid + pageId;
                ViewBag.PreClearanceGridId = Constants.PreClearanceGrid + pageId;

                var digitalRightModel = new ResourceDigitalRightsReviewModel();
                digitalRightModel = ConvertResourceDigitalMasterData(digitalRightModel);
                return PartialView(Constants.ResourceDigitalRestrictions, digitalRightModel);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="tabIndex"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceCustomWQDigitalRestrictions(PagingParams args, string tabIndex, string filterParams)
        {
            try
            {
                var digitalRightsModel = new ResourceDigitalRightsReviewModel();
                if (filterParams != null)
                {
                    LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                    LoggerFactory.MeasureLogWriter.Start();

                    //create digital restriction grid model
                    digitalRightsModel = LoadCustomResourceDigitalRestrictions(digitalRightsModel, filterParams, args, tabIndex);
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                var data = digitalRightsModel.BindedDigitalRights;
                return data.GridJSONActions<List<ResourceDigitalRights>>
                            (digitalRightsModel.DigitalRightsResult.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ResourceCustomWQPreClearance(string pageId, string tabNumber)
        {
            try
            {
                ViewBag.CustomTabIndex = tabNumber;
                ViewBag.ResourcePageId = pageId;
                ViewBag.rightsAcquiredGridId = Constants.RightsAcquireGrid + pageId;
                ViewBag.DigitalGridId = Constants.DigitalRestrictionGrid + pageId;
                ViewBag.SecondaryExpGridId = Constants.SecondaryExploitationGrid + pageId;
                ViewBag.PreClearanceGridId = Constants.PreClearanceGrid + pageId;

                var resourcePreClearanceModel = new ResourcePreClearanceReviewModel();
                return PartialView(Constants.PreClearance, resourcePreClearanceModel);

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="tabIndex"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceCustomWQPreClearance(PagingParams args, string tabIndex, string filterParams)
        {
            try
            {
                var result = new ResourceRightsResult();
                if (filterParams != null)
                {
                    LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                    LoggerFactory.MeasureLogWriter.Start();

                    //create pre clearance grid model
                    var preClearanceModel = new ResourcePreClearanceReviewModel();
                    if (tabIndex == Constants.ReleaseCustomTab)
                    {
                        var filterParameters = GetCustomFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataPrecleared);

                        preClearanceModel.PreClearanceResult =
                            _rightsReviewWorkQueueRepository.LoadResourcePreClearanceInfo(filterParameters);
                    }

                    if (tabIndex == Constants.ReleasePredefinedTab)
                    {
                        var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args, GrsTasks.EditRightsDataPrecleared);
                        preClearanceModel.PreClearanceResult =
                                _rightsReviewWorkQueueRepository.LoadResourcePreClearancePredefined(filterPredefinedParameters);

                    }
                    //construct/form linear pre clearance grid result
                    preClearanceModel.PreClearanceResult.Rights =
                        preClearanceModel.PreClearanceResult.Rights.Select(preClearance => preClearance.ConstructViewInfo())
                            .ToList();
                    preClearanceModel.PreClearanceResult.Rights.Select(preClearance => preClearance.RightsTypeLnr =
                        RightsTypeDescriptions(preClearance.RightsType)).ToList();
                    //assign pre clearance drop down values grid result
                    preClearanceModel.PreClearanceResult.Rights =
                        preClearanceModel.FillDropDownValues
                            (preClearanceModel.PreClearanceResult.Rights, GetAcquiredRightsMasterData());
                    //load pre clearance grid master data for drop down
                    preClearanceModel.PreClearanceMasterData =
                        _rightsReviewWorkQueueRepository.LoadPreClearanceMasterData();
                    foreach (var countryResult in preClearanceModel.PreClearanceResult.Rights)
                    {
                        foreach (var country in countryResult.PreClearanceInfo.ExcludedCountries)
                        {
                            if (!string.IsNullOrEmpty(countryResult.ExcludedCountries))
                                countryResult.ExcludedCountries = countryResult.ExcludedCountries + "," + country.ToString();
                            else
                                countryResult.ExcludedCountries = country.ToString();
                        }
                        foreach (var country in countryResult.PreClearanceInfo.IncludedCountries)
                        {
                            if (!string.IsNullOrEmpty(countryResult.IncludedCountries))
                                countryResult.IncludedCountries = countryResult.IncludedCountries + "," + country.ToString();
                            else
                                countryResult.IncludedCountries = country.ToString();
                        }
                    }
                    ViewBag.totalRecords = preClearanceModel.PreClearanceResult.TotalRows;
                    ViewData[Constants.totalPreclearanceData] = preClearanceModel.PreClearanceResult.TotalRows;
                    LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                    LoggerFactory.MeasureLogWriter.Stop();

                    return preClearanceModel.PreClearanceResult.Rights.GridJSONActions<List<PreClearanceResult>>
                            (preClearanceModel.PreClearanceResult.TotalRows);


                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                return result.ResourceRights.GridJSONActions<List<ResourcesRight>>(result.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }



        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ResourceCustomWQSecExploitation(string pageId, string tabNumber)
        {
            try
            {
                ViewBag.CustomTabIndex = tabNumber;
                ViewBag.ResourcePageId = pageId;
                ViewBag.rightsAcquiredGridId = Constants.RightsAcquireGrid + pageId;
                ViewBag.DigitalGridId = Constants.DigitalRestrictionGrid + pageId;
                ViewBag.SecondaryExpGridId = Constants.SecondaryExploitationGrid + pageId;
                ViewBag.PreClearanceGridId = Constants.PreClearanceGrid + pageId;

                var resourceSecondaryRightsModel = new ResourceSecondaryRightsReviewModel();
                return PartialView(Constants.SecondaryExploitation, resourceSecondaryRightsModel);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="tabIndex"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResourceCustomWQSecExploitation(PagingParams args, string tabIndex, string filterParams)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                //create pre clearance grid model
                var secondaryRightsModel = new ResourceSecondaryRightsReviewModel();
                if (filterParams != null)
                {

                    if (tabIndex == Constants.ReleaseCustomTab)
                    {
                        //assisgn Filter parameters
                        var filterParameters = GetCustomFilledFilterParams(filterParams, args, GrsTasks.EditRightsDataSecondary);

                        //filterParameters.AdminCompany = "12404";//TODO Remove
                        secondaryRightsModel.RightsData =
                             _rightsReviewWorkQueueRepository.LoadResourceSecondaryRights(filterParameters);

                    }

                    if (tabIndex == Constants.ReleasePredefinedTab)
                    {
                        var filterPredefinedParameters = GetCustomFilledPredefinedParams(filterParams, args, GrsTasks.EditRightsDataSecondary);
                        secondaryRightsModel.RightsData = _rightsReviewWorkQueueRepository.LoadResourceSecondaryRightsPredefined(filterPredefinedParameters);
                    }

                    //TODO REP
                    //construct/form linear secondary rights grid result
                    secondaryRightsModel.RightsData.RightsResult =
                        secondaryRightsModel.RightsData.RightsResult.Select(secondaryRights => secondaryRights.ConstructViewInfo()).ToList();

                    secondaryRightsModel.RightsData.RightsResult.Select
                        (secondaryRights => secondaryRights.RightsTypeLnr = RightsTypeDescriptions(secondaryRights.RightsType)).ToList();



                    var count = 0;
                    secondaryRightsModel.RightsData.RightsResult.ForEach(
                        secondaryRights => secondaryRights.RowId = count++.ToString());
                    ViewData[Constants.totalSecondaryData] = secondaryRightsModel.RightsData.TotalRows;
                    //TODO REP

                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return secondaryRightsModel.RightsData.RightsResult.GridJSONActions<List<ResourceSecondaryRights>>
                        (secondaryRightsModel.RightsData.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }




        /// <summary>
        /// Returns the CreateCustomSetting Page
        /// </summary>
        /// <returns></returns>
        public PartialViewResult CreateCustomSetting(string referrer)
        {
            LoggerFactory.LogWriter.Debug(Constants.MethodStart);
            LoggerFactory.MeasureLogWriter.Start();

            ViewBag.referrerMessage = referrer;
            ViewData[Constants.lstDropDown] = _configFactory.PageSizeValues.Select(item => new SelectListItem() { Text = item.Value, Value = item.Key.ToString() });
            LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
            LoggerFactory.MeasureLogWriter.Stop();

            return PartialView(Constants.CreateCustomSetting, new ReleaseRightsWorkQueueModel());
        }

        #endregion UC-19

        #region UC20 Save Reviewed Release Rights

        /// <summary>
        /// Saves the release acquired rights.
        /// </summary>
        /// <param name="releaseUpdateGridItems">The release rights grid.</param>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <param name="upc">The upc.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="isExactSearch">The is exact search.</param>
        /// <param name="releaseTitle">The release title.</param>
        /// <param name="adminCompany">The admin company.</param>
        /// <param name="contractIds">The contract ids.</param>
        /// <param name="fromDt">From dt.</param>
        /// <param name="toDt">To dt.</param>
        /// <param name="noRlsDt">The no RLS dt.</param>
        /// <param name="queryType">Type of the query.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveReleaseAcquiredRights([Bind(Prefix = Constants.updatedRecords)]IEnumerable<ReleaseRightsAcquired> releaseUpdateGridItems, PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation, bool isDynamic = true)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                //PermissionCheckNdRedirect(new[] { GrsTasks.EditRightsDataHeader });
                if (releaseUpdateGridItems != null)
                {
                    ReleaseRightsSaveInfo rightsSaveInfo = GetReleaseRightsAcquiredSaveInfo(releaseUpdateGridItems);
                    //Call WCF Service Save Method//
                    List<long> updatedRights = _rightsReviewWorkQueueRepository.SaveReviewedReleaseRights(rightsSaveInfo);
                    var filterParams = new ReleaseFilterParameters();
                    if (isDynamic)
                    {
                        filterParams = GetReleaseAcquiredFilterData(args, tabIndex, upc, artist,
                                                                                        isExactSearch, releaseTitle,
                                                                                        adminCompany, contractIds, fromDt,
                                                                                        toDt, noRlsDt, queryType, linkIsrc, linkUpc, isLinkNavigation);
                    }
                    else
                    {
                        if (SessionWrapper.RightSetIds != null)
                        {
                            var rightSetId = "";
                            SessionWrapper.RightSetIds.ForEach(ids => rightSetId = rightSetId + Constants.Comma + ids.ToString());
                            filterParams = new ReleaseFilterParameters { RepertoireFilter = new RepertoireFilter { RightSetIds = rightSetId, IsRightsReviewWQ = true }, UserName = SessionWrapper.CurrentUserInfo.UserLoginName };
                        }
                    }
                    var gridResult = GetReleaseRightsData(filterParams);
                    long totalRecords = 0;
                    if (gridResult.Count > 0)
                    {
                        totalRecords = Convert.ToInt64(gridResult[0].TotalRows);
                    }
                    gridResult.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                        (resultRight.RightSetId) ? Constants.Concurrencyproblems : null);
                    return gridResult.GridJSONActions<ReleaseRightsAcquired>(totalRecords);
                }
                else
                {
                    var filterParams = GetReleaseAcquiredFilterData(args, tabIndex, upc, artist,
                                                                                    isExactSearch, releaseTitle,
                                                                                    adminCompany, contractIds, fromDt,
                                                                                    toDt, noRlsDt, queryType, linkIsrc, linkUpc, isLinkNavigation);
                    var gridResult = GetReleaseRightsData(filterParams);
                    long totalRecords = 0;
                    if (gridResult.Count > 0)
                    {
                        totalRecords = Convert.ToInt64(gridResult[0].TotalRows);
                    }
                    LoggerFactory.MeasureLogWriter.Stop();

                    return gridResult.GridJSONActions<ReleaseRightsAcquired>(totalRecords);
                }
            }
            catch (FaultException ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                if (ex.Message.Contains(GlobalConstants.ConcurrencyError))
                {
                    ViewBag.displayMessage = string.Format(ex.Message);
                }
                else
                {
                    ViewBag.displayMessage = ex.Message.Contains(ContractResource.AuthorizedMsg) ? ContractResource.ContractErrorMsg : ContractResource.UnableToSaveMsg;
                }

                return null;//PartialView(GlobalConstants.ContractTabView, _contractTabModel);
            }
            catch (Exception)
            {
                //have to implement
                throw;
            }
        }



        #endregion

        #region GetReleaseRightsAcquiredSaveInfo

        private ReleaseRightsSaveInfo GetReleaseRightsAcquiredSaveInfo(IEnumerable<ReleaseRightsAcquired> releaseUpdateGridItems)
        {
            var rightsData = new ReleaseRightsSaveInfo { ModifierInfo = { UserName = SessionWrapper.CurrentUserInfo.UserLoginName } };
            ReviewRightsMasterInfo masterData = _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData();
            masterData = masterData.ConstructSaveReverseInfo();
            foreach (ReleaseRightsAcquired release in releaseUpdateGridItems)
            {
                var releaseRights = new RepertoireRights
                {
                    RightSetId = release.RightSetId,
                    TerritorialRights = release.TerritorialRights
                };
                //Here Spl Chararacter Replacing with empty character Donot delete the below Line//
                var strRightsPeriod = release.RightsPeriod == null
                                          ? string.Empty
                                          : release.RightsPeriod.Replace(" ", "").Replace(" ", "");
                var strRightReason = release.LostReason == null
                                         ? string.Empty
                                         : release.LostReason.Replace(" ", "").Replace(" ", "");

                bool? blnException = null;
                if (release.ExceptionText != null && release.ExceptionText != "null" && release.ExceptionText != string.Empty)
                {
                    blnException = release.ExceptionText.Trim() == Constants.ExceptionApplied
                                                     ? true
                                                     : false;
                }

                //string strRightsPeriod = Regex.Replace(release.RightsPeriod,@"^[a-zA-Z0-9_@.-]*$","");
                //string strRightReason = release.LostReason == null? string.Empty : Regex.Replace(release.LostReason, "^[^A-Za-z0-9]*", "");

                releaseRights.RightsPeriod = strRightsPeriod == string.Empty
                                                 ? 0
                                                 : masterData.RightsPeriodReverse[strRightsPeriod];
                releaseRights.LostRights = release.LostRightsText == string.Empty
                                               ? (bool?)null
                                               : release.LostRightsText == Constants.YValue;
                releaseRights.LostReason = release.LostReason == string.Empty
                                               ? null
                                               : release.LostReason == null
                                                     ? (int?)null
                                                     : masterData.LostRightsReasonReverse[strRightReason];
                releaseRights.LostRightsDate = !string.IsNullOrEmpty(release.LostRightsDateLinear) ? Convert.ToDateTime(release.LostRightsDateLinear) : (DateTime?)null;
                releaseRights.Exception = blnException;
                releaseRights.PhysicallyExploited = release.PhysicallyExploitedText == string.Empty
                                                        ? (bool?)null
                                                        : release.PhysicallyExploitedText == Constants.YValue;
                releaseRights.DigitallyExploited = release.DigitallyExploitedText == string.Empty
                                                       ? (bool?)null
                                                       : release.DigitallyExploitedText == Constants.YValue;
                releaseRights.ExcludedCountry = saveReleaseInfo(release.ExcludedCountryLnr);
                releaseRights.IncludedCountry = saveReleaseInfo(release.IncludedCountryLnr);
                releaseRights.ExcludedTerritory = saveReleaseInfo(release.ExcludedTerritoryLnr);
                releaseRights.IncludedTerritory = saveReleaseInfo(release.IncludedTerritoryLnr);
                releaseRights.ModifiedDateTime = release.ModifiedDateTimeLnr;
                rightsData.ReleaseRights.Add(releaseRights);
            }
            return rightsData;
        }
        #endregion

        /// <summary>
        /// Returns the resource rights acquired data.
        /// </summary>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        #region GetResourceRightsAcquiredData
        private ResourceRightsResult GetResourceRightsAcquiredData(ResourceFilterParameters filterCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                /*get WQ result*/

                var rightsAcquiredResult = _rightsReviewWorkQueueRepository.LoadResourceRightsWQ(filterCriteria);
                ResourceRightsWorkQueueModel resourceModel =
                    ConstructAcquiredRightsModelWithMasterData(false, rightsAcquiredResult);
                //construct linear value for Syncfusion compatibility
                if (rightsAcquiredResult.ResourceRights != null)
                {
                    resourceModel.RightsResult.ResourceRights.ForEach(right =>
                    {
                        right.ConstructViewInfo();
                        right.LostRightsReasonLnr =
                            right.Rights.LostReason.HasValue ?
                            (right.Rights.LostReason.Value == 0 ? string.Empty :
                            acquiredRightsMasterData.LostRightsReason[right.Rights.LostReason.Value]) : string.Empty;
                        right.RightsPeriodLnr =
                        right.Rights.RightsPeriod != 0 ?
                        acquiredRightsMasterData.RightsPeriod[right.Rights.RightsPeriod] : string.Empty;
                        right.ReviewReasonLnr =
                        acquiredRightsMasterData.AppendReviewReason(right.ReviewReasons);
                        right.RightsTypeLnr = RightsTypeDescriptions(right.RightsType);
                    });
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                return rightsAcquiredResult;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }
        //Newly Added for UC 19//
        private ResourceRightsResult GetResourceRightsAcquiredDataPredefinedParameters(PreDefinedParametersWQ filterCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                /*get WQ result*/
                var rightsAcquiredResult = _rightsReviewWorkQueueRepository.LoadResourceRightsPredefinedWQ(filterCriteria);
                ResourceRightsWorkQueueModel resourceModel =
                    ConstructAcquiredRightsModelWithMasterData(false, rightsAcquiredResult);

                //construct linear value for Syncfusion compatibility
                if (rightsAcquiredResult.ResourceRights != null)
                    resourceModel.RightsResult.ResourceRights =
                        resourceModel.RightsResult.ResourceRights.Select(right => right.ConstructViewInfo()).ToList();

                //get value using ID for lost rights reason
                resourceModel.RightsResult.ResourceRights =
                    resourceModel.GetMasterDataValues(acquiredRightsMasterData,
                                                               resourceModel.RightsResult.ResourceRights);
                resourceModel.RightsResult.ResourceRights.Select(right => right.RightsTypeLnr = RightsTypeDescriptions(right.RightsType)).ToList();
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                return rightsAcquiredResult;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }



        private ResourceRightsWorkQueueModel ConstructAcquiredRightsModelWithMasterData
            (bool bulkEdit, ResourceRightsResult rightsAcquiredResult = null)
        {
            GetAcquiredRightsMasterData();
            // construct Resource model
            var resourceModel = new ResourceRightsWorkQueueModel
            {
                RightsResult = rightsAcquiredResult,
                RightsPeriodDropDown = acquiredRightsMasterData.RightsPeriod.Values.ToList(),
                LostRightReasonDropDown = acquiredRightsMasterData.LostRightsReason.Values.ToList()
            };

            if (bulkEdit)
            {

                resourceModel.RightsPeriodDropDownList =
                    acquiredRightsMasterData.RightsPeriod.Select(temp => new SelectListItem()
                    {
                        Text = temp.Value,
                        Value = temp.Key.ToString()
                    }).ToList();
                resourceModel.LostRightReasonDropDownList = acquiredRightsMasterData.LostRightsReason.Select(temp => new SelectListItem()
                {
                    Text = temp.Value,
                    Value = temp.Key.ToString()
                }).ToList();

                //resourceModel.LostRightReasonDropDownList = acquiredRightsMasterData.LostRightsReason.Select(temp => new SelectListItem()
                //{
                //    Text = temp.Value,
                //    Value = temp.Key.ToString()
                //}).ToList();


                resourceModel.RightsPeriodDropDownList =
                    AppendNoChangeRequiredSelect(resourceModel.RightsPeriodDropDownList);
                resourceModel.LostRightReasonDropDownList =
                    AppendNoChangeRequiredSelect(resourceModel.LostRightReasonDropDownList);
                resourceModel.BooleanDropDown =
                    AppendNoChangeRequiredSelect(resourceModel.BooleanDropDown);
                resourceModel.ReviewStatusDropDown.Remove(resourceModel.ReviewStatusDropDown.First(x => x.Value == Constants.NewForReview));
                resourceModel.ReviewStatusDropDown.Remove(resourceModel.ReviewStatusDropDown.First(x => x.Value == Constants.FinalForReview));
                resourceModel.ReviewStatusDropDown =
                    AppendNoChangeRequiredSelect(resourceModel.ReviewStatusDropDown);
                resourceModel.RightsExceptionDropdown.Remove(resourceModel.RightsExceptionDropdown.First(x => x.Value == "Not&nbsp;Applied"));
                resourceModel.RightsExceptionDropdown.Add(new SelectListItem() { Text = "Not Applied", Value = "Not Applied" });
                resourceModel.RightsExceptionDropdown =
                   AppendNoChangeRequiredSelect(resourceModel.RightsExceptionDropdown);
            }
            return resourceModel;
        }
        #endregion

        private List<SelectListItem>
            AppendNoChangeRequiredSelect(List<SelectListItem> modelData)
        {
            modelData.Add(new SelectListItem() { Text = WorkQueueResource.NoChangeRequired, Selected = true, Value = Constants.ListZeroValue });
            return modelData;
        }

        private List<SelectListItem>
           AppendNoChangeSecondaryRequiredSelect(List<SelectListItem> modelData)
        {
            modelData.Add(new SelectListItem() { Text = WorkQueueResource.NoChangeRequired, Selected = true, Value = Constants.ValueNoChange });
            return modelData;
        }

        /// <summary>
        /// Saves the digital release acquired rights.
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveReleaseDigitalRestriction([Bind(Prefix = Constants.updatedRecords)]IEnumerable<ReleaseDigitalLnr> releaseUpdateDigitalGridData, [Bind(Prefix = Constants.addedRecords)]IEnumerable<ReleaseDigitalLnr> releaseAddDigitalGridData,
            [Bind(Prefix = Constants.deletedRecords)]IEnumerable<ReleaseDigitalLnr> releaseDeleteDigitalGridData,
            PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation, bool isDynamic = true)
        {
            //Save Part//
            List<long> updatedRights = new List<long>();
            LoggerFactory.LogWriter.Debug(Constants.MethodStart);
            LoggerFactory.MeasureLogWriter.Start();

            DigitalRightsMasterData masterData = _rightsReviewWorkQueueRepository.LoadDigitalMasterData();
            var rightsDataCollection = new List<ReleaseDigitalRights>();
            var modifierInfo = new ModifierInfo { UserName = SessionWrapper.CurrentUserInfo.UserLoginName };
            masterData = masterData.ConstructSaveReverseInfo();

            //PermissionCheckNdRedirect(new[] { GrsTasks.EditRightsDataHeader });
            if (releaseUpdateDigitalGridData != null)
            {
                rightsDataCollection = ConstructReleaseDigitalRightForUpdate
                    (releaseUpdateDigitalGridData, Constants.Updated, masterData, rightsDataCollection);
            }
            if (releaseAddDigitalGridData != null)
            {
                rightsDataCollection = ConstructReleaseDigitalRightForUpdate
                    (releaseAddDigitalGridData, Constants.Added, masterData, rightsDataCollection);
            }
            //Digital Delete//
            if (releaseDeleteDigitalGridData != null)
            {
                rightsDataCollection = ConstructReleaseDigitalRightForUpdate
                    (releaseDeleteDigitalGridData, Constants.Deleted, masterData, rightsDataCollection);
            }

            updatedRights = _rightsReviewWorkQueueRepository.
                SaveReleaseDigitalRights(rightsDataCollection, modifierInfo);
            //Refresh Part//
            var releaseDigitalLnrCollection = GetReleaseRestrictionFilterData
                (args, tabIndex, upc, artist, isExactSearch, releaseTitle, adminCompany, contractIds,
                fromDt, toDt, noRlsDt, queryType, linkIsrc, linkUpc, isLinkNavigation, isDynamic);
            var data = releaseDigitalLnrCollection;

            long totalRecords = 0;
            if (data.Count > 0)
            {
                totalRecords = Convert.ToInt64(data[0].strTotalRows);
            }


            data.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                        (resultRight.RightSetIdLnr) ? Constants.Concurrencyproblems : null);
            LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
            LoggerFactory.MeasureLogWriter.Stop();

            return data.GridJSONActions<ReleaseDigitalLnr>(totalRecords);

        }

        private List<ReleaseDigitalRights> ConstructReleaseDigitalRightForUpdate
            (IEnumerable<ReleaseDigitalLnr> releaseDigitalGridData, string updateStatus,
            DigitalRightsMasterData masterData, List<ReleaseDigitalRights> rightsDataCollection)
        {
            bool dummyDelete = false;
            ReleaseDigitalRights rightsData = null;
            foreach (ReleaseDigitalLnr rlsDigitalVal in releaseDigitalGridData)
            {
                bool alreadyExist = false;
                if (rightsDataCollection.Where(right => right.RightSetId == rlsDigitalVal.RightSetIdLnr).Any())
                {
                    rightsData = rightsDataCollection.Where(right => right.RightSetId == rlsDigitalVal.RightSetIdLnr).FirstOrDefault();
                    alreadyExist = true;
                }
                else
                {
                    rightsData = new ReleaseDigitalRights();
                }

                if (rlsDigitalVal.RestrictonReasonLnr != null && rlsDigitalVal.RestrictonReasonLnr != Constants.Null)
                {
                    var reason = rlsDigitalVal.RestrictonReasonLnr.Replace(" ", "").Replace(" ", "");
                    if (reason != Constants.SideArtistIssues && reason != Constants.SampleIssues && reason != Constants.NoRights && reason != Constants.ArtistConsentRequired)
                    {
                        if (reason != Constants.Other)
                        {
                            rlsDigitalVal.RestrictionReasonForOthers = rlsDigitalVal.RestrictonReasonLnr;//To Handle the scenario when free-text is given for new row     
                        }
                        rlsDigitalVal.RestrictonReasonLnr = Constants.Other;
                    }
                }

                //Here Spl Chararacter Replacing with empty character Donot delete the below Line//
                var strUseType = string.Empty;
                if (rlsDigitalVal.UseTypeLnr != Constants.Null && rlsDigitalVal.UseTypeLnr != null)
                {
                    strUseType = rlsDigitalVal.UseTypeLnr.Replace(" ", "").Replace(" ", "");
                }
                var strCommercialModel = string.Empty;
                if (rlsDigitalVal.CommercialModelsLnr != Constants.Null && rlsDigitalVal.CommercialModelsLnr != null)
                {
                    strCommercialModel = rlsDigitalVal.CommercialModelsLnr.Replace(" ", "").Replace(" ", "");
                }
                var strRestriction = string.Empty;
                if (rlsDigitalVal.RestrictionLnr != Constants.Null && rlsDigitalVal.RestrictionLnr != null)
                {
                    strRestriction = rlsDigitalVal.RestrictionLnr.Replace(" ", "").Replace(" ", "");
                }

                var strRestrictionReason = string.Empty;
                if (rlsDigitalVal.RestrictonReasonLnr != Constants.Null && rlsDigitalVal.RestrictonReasonLnr != null)
                {
                    strRestrictionReason = rlsDigitalVal.RestrictonReasonLnr.Replace(" ", "").Replace(" ", "");
                }
                var strConsentPeriod = string.Empty;
                //Syncfusion return value//
                if (rlsDigitalVal.ConsentPeriodLnr != Constants.Null && rlsDigitalVal.ConsentPeriodLnr != null && rlsDigitalVal.ConsentPeriodLnr.Trim().Length > 0)
                {
                    strConsentPeriod = rlsDigitalVal.ConsentPeriodLnr.Replace(" ", "").Replace(" ", "");
                }

                if (strUseType != string.Empty &&
                strCommercialModel != string.Empty)
                {


                    var digRes = new ContractDigitalRestrictions
                    {
                        UseTypeId =
                            (byte)
                            (strUseType == string.Empty
                                 ? 0
                                 : masterData.UseTypesReverse[strUseType]),
                        CommercialModelId = (byte)
                                            (strCommercialModel == string.Empty
                                                 ? 0
                                                 : masterData.CommercialModelTypesReverse[
                                                     strCommercialModel]),
                        RestrictionId = (byte)(strRestriction == string.Empty
                                                    ? 0
                                                    : masterData.RestrictionTypesReverse[strRestriction
                                                          ]),
                        ConsentPeriodId = (byte)(strConsentPeriod == string.Empty
                                                      ? 0
                                                      : masterData.ConsentPeriodTypesReverse[
                                                          strConsentPeriod]),
                        RestrictionReasonId = (byte)(strRestrictionReason == string.Empty
                                                      ? 0
                                                      : masterData.RestrictionReasonReverse[strRestrictionReason]),

                        Notes = rlsDigitalVal.NotesLnr == Constants.Null ? string.Empty : rlsDigitalVal.NotesLnr,
                        RestrictionOtherReasonInfo = rlsDigitalVal.RestrictionReasonForOthers,
                        DigitalRestrictionId = rlsDigitalVal.RestrictionIdLnr,
                        RightSetId = rlsDigitalVal.RightSetIdLnr
                    };

                    if (updateStatus == Constants.Updated)
                    {
                        //Add Logic//
                        if (rlsDigitalVal.RestrictionIdLnr == -1)
                        {
                            digRes.IsInserted = true;
                        }
                        else
                        {
                            //Update//
                            digRes.RestrictionExist = true;
                        }
                    }
                    else if (updateStatus == Constants.Added)
                    {
                        digRes.IsInserted = true;
                    }
                    else if (updateStatus == Constants.Deleted)
                    {
                        if (rlsDigitalVal.RestrictionIdLnr == -1)
                        {
                            dummyDelete = true;
                        }

                        digRes.IsDeleted = true;
                    }

                    digRes.RightSetId = rlsDigitalVal.RightSetIdLnr;
                    rightsData.DigitalRights.RightSetId = rlsDigitalVal.RightSetIdLnr;
                    rightsData.DigitalRights.ModifiedDateTime = rlsDigitalVal.ModifiedDateTimeLnr;
                    if (!dummyDelete)
                        rightsData.DigitalRights.DigitalRestrictions.Add(digRes);
                }

                rightsData.RightSetId = rlsDigitalVal.RightSetIdLnr;
                if (!alreadyExist && rightsData.RightSetId > 0)
                    rightsDataCollection.Add(rightsData);

            }

            return rightsDataCollection;
        }

        //UC20
        /// <summary>
        /// Releases the rights work queue.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReleaseRightsWorkQueue(string pageId, string searchCriteria)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                searchCriteria = searchCriteria.Replace(Constants.strAmb, Constants.strAmbSymbol);
                ViewBag.SearchParams = searchCriteria;
                ViewBag.PageId = pageId;

                //Check Edit Permission
                ViewBag.EditRightsDataHeader = SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.EditRightsDataHeader);
                ViewBag.EditRightsDataDigital = SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.EditRightsDataDigital);

                ViewBag.releaseRightsAcquiredGridId = Constants.ReleaseRightsAcquireGrid + pageId;
                ViewBag.releaseDigitalGridId = Constants.ReleaseDigitalRestrictionGrid + pageId;
                var releaseModel = new ReleaseRightsWorkQueueModel
                {
                    ReleaseRightsInfo = new ReleaseRightsAcquired(),
                    ReleaseDigitalRightsInfo = new ReleaseDigitalLnr(),
                    RightsMasterData =
                        _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData()
                };
                releaseModel.RightsPeriodDropDown = releaseModel.RightsMasterData.RightsPeriod.Values.ToList();
                releaseModel.LostRightReasonDropDown = releaseModel.RightsMasterData.LostRightsReason.Values.ToList();

                releaseModel.LostRightReasonDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.LostRightsReason);
                releaseModel.RightsPeriodDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.RightsPeriod);
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return PartialView(Constants.ReleaseRightsWorkQueue, releaseModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        /// <summary>
        /// Releases the digital restrictions.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ReleaseRightsAcquired(string pageId)
        {
            ViewBag.releaseRightsAcquiredGridId = Constants.ReleaseRightsAcquireGrid + pageId;
            var releaseModel = new ReleaseRightsWorkQueueModel
            {
                ReleaseRightsInfo = new ReleaseRightsAcquired(),
                RightsMasterData =
                    _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData()
            };
            releaseModel.RightsPeriodDropDown = releaseModel.RightsMasterData.RightsPeriod.Values.ToList();
            releaseModel.LostRightReasonDropDown = releaseModel.RightsMasterData.LostRightsReason.Values.ToList();
            releaseModel.LostRightReasonDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.LostRightsReason);
            releaseModel.RightsPeriodDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.RightsPeriod);
            return PartialView(Constants.ReleaseRightsAcquired, releaseModel);
        }

        #region LoadReleaseRightsAcquired

        /// <summary>
        /// Releases the rights acquired.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <param name="upc">The upc.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="isExactSearch">The is exact search.</param>
        /// <param name="releaseTitle">The release title.</param>
        /// <param name="adminCompany">The admin company.</param>
        /// <param name="contractIds">The contract ids.</param>
        /// <param name="fromDt">From dt.</param>
        /// <param name="toDt">To dt.</param>
        /// <param name="noRlsDt">The no RLS dt.</param>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        /// <param name="rightsSetId">The rights set id.</param>
        /// <param name="linkIsrc">The link isrc.</param>
        /// <param name="linkUpc">The link upc.</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        /// <param name="isLinkNavigation">if set to <c>true</c> [is link navigation].</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReleaseRightsAcquired(PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string rightsSetId, string linkIsrc, string linkUpc, bool isDynamic = true, bool isLinkNavigation = false)
        {
            var gridResult = new List<ReleaseRightsAcquired>();
            var filterParams = new ReleaseFilterParameters();
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                if (isDynamic)
                {
                    filterParams = GetReleaseAcquiredFilterData(args, tabIndex, upc, artist,
                                                                isExactSearch, releaseTitle,
                                                                adminCompany, contractIds, fromDt,
                                                                toDt, noRlsDt, queryType, linkIsrc, linkUpc, isLinkNavigation);
                    gridResult = GetReleaseRightsData(filterParams);
                    LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                    return gridResult.GridJSONActions<ReleaseRightsAcquired>(0);
                }
                filterParams.RepertoireFilter = new RepertoireFilter
                {
                    RightSetIds = rightsSetId
                };
                gridResult = GetReleaseRightsData(filterParams);
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return gridResult.GridJSONActions<ReleaseRightsAcquired>(0);

            }
            catch (Exception ex)
            {
                return Json(new { Result = GlobalConstants.JsonError, ex.Message });
            }
        }

        #endregion


        #region FetchReleaseRightsAcquiredFilterData

        private ReleaseFilterParameters GetReleaseAcquiredFilterData(PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation = false)
        {
            var filterParams = new ReleaseFilterParameters { UserName = SessionWrapper.CurrentUserInfo.UserLoginName };
            // Get Search Critaria
            // Passing Custom Parameters//
            if (tabIndex == Constants.ReleaseCustomTab)
            {
                filterParams.RepertoireFilter.UPC = upc == string.Empty ? null : upc;
                filterParams.RepertoireFilter.Artist = artist == string.Empty ? null : artist;
                filterParams.IsExactSearch = isExactSearch == Constants.FlagTrue;
                filterParams.RepertoireFilter.ReleaseTitle = releaseTitle == string.Empty ? null : releaseTitle;
                filterParams.AdminCompany = adminCompany == string.Empty ? null : adminCompany;
                filterParams.RepertoireFilter.LinkedContract = contractIds == string.Empty ? null : contractIds;
                filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;
                filterParams.RepertoireFilter.LinkISRC = linkIsrc;
                filterParams.RepertoireFilter.LinkUPC = linkUpc;
                filterParams.RepertoireFilter.IsLinkNavigation = isLinkNavigation;
            }
            //Passing Predefined Parameters//
            else if (tabIndex == Constants.ReleasePredefinedTab)
            {
                if (queryType == Constants.ReleaseParamsMultiArtistValue)
                {
                    filterParams.IsMultiArtist = true;
                    filterParams.IsOst = null;
                }
                else if (queryType == Constants.ReleaseParamsOstValue)
                {
                    filterParams.IsOst = true;
                    filterParams.IsMultiArtist = null;
                }
                else if (queryType == Constants.ReleaseParamsNonMacValue)
                {
                    filterParams.IsMultiArtist = false;
                    filterParams.IsOst = null;
                }
                filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;

            }


            // Paging
            filterParams.StartIndex = args.StartIndex;
            filterParams.PageSize = args.PageSize;

            // Sorting
            var currentRequest = (RequestType)args.RequestType;
            if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
            {
                if (args.SortDescriptors != null)
                {
                    filterParams.SortField = args.SortDescriptors[0].ColumnName;
                    filterParams.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                }
            }
            return filterParams;
        }
        #endregion

        /// <summary>
        /// Releases the digital restrictions.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ReleaseDigitalRestrictions(string pageId)
        {


            try
            {
                ViewBag.releaseDigitalGridId = Constants.ReleaseDigitalRestrictionGrid + pageId;
                ViewBag.EditRightsDataDigital = SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.EditRightsDataDigital);
                ViewBag.HasDelete = false;
                var releaseModel = new ReleaseRightsWorkQueueModel
                {
                    digitalRightsMasterData =
                        _rightsReviewWorkQueueRepository.LoadDigitalMasterData()
                };
                //Digital RestrictionLnr Tab
                releaseModel.UserTypeDropDown = releaseModel.digitalRightsMasterData.UseTypes.Values.ToList();
                releaseModel.CommercialDropDown = releaseModel.digitalRightsMasterData.CommercialModelTypes.Values.ToList();
                releaseModel.RestrictionsDropDown = releaseModel.digitalRightsMasterData.RestrictionTypes.Values.ToList();
                //releaseModel.RestrictionReasonDropDown = releaseModel.digitalRightsMasterData.reas
                releaseModel.ConsentPeriodDropDown = releaseModel.digitalRightsMasterData.ConsentPeriodTypes.Values.ToList();

                releaseModel.UserTypeDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.digitalRightsMasterData.UseTypes);
                releaseModel.CommercialDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.digitalRightsMasterData.CommercialModelTypes);
                releaseModel.RestrictionsDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.digitalRightsMasterData.RestrictionTypes);
                releaseModel.RestrictionsReasonDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.digitalRightsMasterData.RestrictionReason);
                releaseModel.ConsentPeriodDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.digitalRightsMasterData.ConsentPeriodTypes);


                return PartialView(Constants.ReleaseDigitalRestrictions, releaseModel);

            }
            catch (Exception)
            {
                throw;
            }

        }

        #region LoadReleaseDigitalRestriction

        /// <summary>
        /// Releases the digital restrictions.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <param name="upc">The upc.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="isExactSearch">The is exact search.</param>
        /// <param name="releaseTitle">The release title.</param>
        /// <param name="adminCompany">The admin company.</param>
        /// <param name="contractIds">The contract ids.</param>
        /// <param name="fromDt">From dt.</param>
        /// <param name="toDt">To dt.</param>
        /// <param name="noRlsDt">The no RLS dt.</param>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="linkIsrc">The link isrc.</param>
        /// <param name="linkUpc">The link upc.</param>
        /// <param name="isLinkNavigation">if set to <c>true</c> [is link navigation].</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReleaseDigitalRestrictions(PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation = false, bool isDynamic = true)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var releaseDigitalLnrCollection = GetReleaseRestrictionFilterData(args, tabIndex, upc, artist, isExactSearch, releaseTitle, adminCompany, contractIds, fromDt, toDt, noRlsDt, queryType, linkIsrc, linkUpc, isLinkNavigation, isDynamic);
                args.PageSize = releaseDigitalLnrCollection.Count();
                var data = releaseDigitalLnrCollection;

                long totalRecords = 0;
                if (data.Count > 0)
                {
                    totalRecords = Convert.ToInt64(data[0].strTotalRows);
                }

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return data.GridJSONActions<ReleaseDigitalLnr>(totalRecords);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            // Get Search Critaria

        }

        #endregion

        #region FetchReleaseDigitalRestrictionFilterData

        private List<ReleaseDigitalLnr> GetReleaseRestrictionFilterData(PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation = false, bool isDynamic = true)
        {
            var filterParams = new ReleaseFilterParameters { UserName = SessionWrapper.CurrentUserInfo.UserLoginName };
            if (isDynamic)
            {
                //Custom Params
                if (tabIndex == Constants.ReleaseCustomTab)
                {
                    filterParams.RepertoireFilter.UPC = upc == string.Empty ? null : upc;
                    filterParams.RepertoireFilter.Artist = artist == string.Empty ? null : artist;
                    filterParams.IsExactSearch = isExactSearch == Constants.FlagTrue ? true : false;
                    filterParams.RepertoireFilter.ReleaseTitle = releaseTitle == string.Empty ? null : releaseTitle;
                    filterParams.AdminCompany = adminCompany == string.Empty ? null : adminCompany;
                    filterParams.RepertoireFilter.LinkedContract = contractIds == string.Empty ? null : contractIds;
                    filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                    filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                    filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;
                    filterParams.RepertoireFilter.IsLinkNavigation = isLinkNavigation;
                    filterParams.RepertoireFilter.LinkISRC = linkIsrc;
                    filterParams.RepertoireFilter.LinkUPC = linkUpc;
                } //Predefined Params
                else if (tabIndex == Constants.ReleasePredefinedTab)
                {
                    // filterParams.
                    if (queryType == Constants.ReleaseParamsMultiArtistValue)
                    {
                        filterParams.IsMultiArtist = true;
                        filterParams.IsOst = null;
                    }
                    else if (queryType == Constants.ReleaseParamsOstValue)
                    {
                        filterParams.IsOst = true;
                        filterParams.IsMultiArtist = null;
                    }
                    else if (queryType == Constants.ReleaseParamsNonMacValue)
                    {
                        filterParams.IsMultiArtist = false;
                        filterParams.IsOst = null;
                    }
                    filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                    filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                    filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;
                }
            }
            else
            {
                if (SessionWrapper.RightSetIds != null)
                {
                    var rightSetId = "";
                    SessionWrapper.RightSetIds.ForEach(ids => rightSetId = rightSetId + Constants.Comma + ids.ToString());
                    filterParams.RepertoireFilter = new RepertoireFilter { RightSetIds = rightSetId, IsRightsReviewWQ = true };
                }
            }
            // Paging
            filterParams.StartIndex = args.StartIndex;
            filterParams.PageSize = args.PageSize;

            // Sorting
            var currentRequest = (RequestType)args.RequestType;
            if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
            {
                if (args.SortDescriptors != null)
                {
                    filterParams.SortField = args.SortDescriptors[0].ColumnName;
                    filterParams.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                }
            }

            //Load Release Digital Restrictions WQ
            var releaseDigitalRights = _rightsReviewWorkQueueRepository.LoadReleaseDigitalRights(filterParams);
            var masterDigitalData = _rightsReviewWorkQueueRepository.LoadDigitalMasterData();

            var releaseDigitalLnrCollection = new List<ReleaseDigitalLnr>();


            releaseDigitalRights.Rights.All(rights =>
            {
                char sensitiveInfoPermitted = rights.SensitiveInfoPermitted;
                char rightsEditPermitted = rights.RightsEditPermitted;
                if (rights.DigitalRights.DigitalRestrictions.Count > 0)
                {

                    int childCount = rights.DigitalRights.DigitalRestrictions.Count;
                    if (childCount == 1)
                        childCount = 0;
                    int intCnt = 0;



                    rights.DigitalRights.DigitalRestrictions.All(digRights =>
                    {
                        releaseDigitalLnrCollection.Add(new ReleaseDigitalLnr()
                        {

                            strTotalRows = rights.TotalRows,
                            RightSetIdLnr = rights.RightSetId,
                            ModifiedDateTimeLnr = rights.ModifiedDateTime,
                            ReleaseId = rights.ReleaseId,
                            R2ReleaseId = rights.R2ReleaseId,
                            ContractId = rights.ContractId,
                            RestrictionIdLnr = digRights.DigitalRestrictionId,
                            ReleaseType = rights.ReleaseTypeInfo,
                            RightsSourceId = rights.RightsSourceId,
                            UPCId = rights.UPCId,
                            Artist = rights.Artist,
                            Title = rights.Title,
                            VersionTitle = rights.VersionTitle,
                            ReleaseDate = rights.ReleaseDate,
                            Configration = rights.Configration,
                            IsSplitDeal = rights.IsSplitDeal,
                            LinkedTooltip = rights.LinkedTooltip,
                            IsEditableRight = rights.DigitalRights.IsEditableRight,
                            AdminCompany = rights.AdminCompany,
                            TerritorialRights = rights.TerritorialRights == null
                                                                            ? string.Empty
                                                                            : rights.TerritorialRights == "null" ? string.Empty : rights.TerritorialRights,
                            UseTypeLnr = digRights.UseTypeId == 0 ? string.Empty : masterDigitalData.UseTypes[digRights.UseTypeId],
                            CommercialModelsLnr = digRights.CommercialModelId == 0 ? string.Empty : masterDigitalData.CommercialModelTypes[digRights.CommercialModelId],
                            RestrictionLnr = digRights.RestrictionId == 0 ? string.Empty : masterDigitalData.RestrictionTypes[digRights.RestrictionId],
                            //RestrictonReasonLnr = digRights.RestrictionReasonId,
                            RestrictonReasonLnr = digRights.RestrictionReasonId == null ? string.Empty : digRights.RestrictionReasonId == 0 ? string.Empty : masterDigitalData.RestrictionReason[int.Parse(digRights.RestrictionReasonId.ToString())],
                            ConsentPeriodLnr = digRights.ConsentPeriodId == null ? string.Empty : digRights.ConsentPeriodId == 0 ? string.Empty : masterDigitalData.ConsentPeriodTypes[int.Parse(digRights.ConsentPeriodId.ToString())],
                            NotesLnr = digRights.Notes,
                            SensitiveInfoPermitted = sensitiveInfoPermitted,
                            RightsEditPermitted = rightsEditPermitted,
                            LostRightsLnr = rights.LostRights,
                            RestrictionReasonForOthers = digRights.RestrictionOtherReasonInfo == null ? string.Empty : digRights.RestrictionOtherReasonInfo,

                            //Synfusion Logic//
                            MergeCount = intCnt == 0 ? childCount : 0
                            //MergeCount = childCount == 1 ? 0 : intCnt == 0 ? childCount : 0

                        });
                        intCnt++;
                        return true;
                    });
                }
                else
                {
                    releaseDigitalLnrCollection.Add(new ReleaseDigitalLnr()
                    {
                        strTotalRows = rights.TotalRows,
                        RestrictionIdLnr = -1,
                        ModifiedDateTimeLnr = rights.ModifiedDateTime,
                        RightSetIdLnr = rights.RightSetId,
                        ReleaseId = rights.ReleaseId,
                        R2ReleaseId = rights.R2ReleaseId,
                        ContractId = rights.ContractId,
                        ReleaseType = rights.ReleaseTypeInfo,
                        RightsSourceId = rights.RightsSourceId,
                        UPCId = rights.UPCId,
                        Artist = rights.Artist,
                        Title = rights.Title,
                        VersionTitle = rights.VersionTitle,
                        ReleaseDate = rights.ReleaseDate,
                        Configration = rights.Configration,
                        IsSplitDeal = rights.IsSplitDeal,
                        LinkedTooltip = rights.LinkedTooltip,
                        IsEditableRight = rights.DigitalRights.IsEditableRight,
                        AdminCompany = rights.AdminCompany,
                        TerritorialRights = rights.TerritorialRights == null
                                                                            ? string.Empty
                                                                            : rights.TerritorialRights == "null" ? string.Empty : rights.TerritorialRights,
                        SensitiveInfoPermitted = sensitiveInfoPermitted,
                        RightsEditPermitted = rightsEditPermitted,
                        LostRightsLnr = rights.LostRights,
                        MergeCount = 0,
                    });
                }
                return true;
            });
            return releaseDigitalLnrCollection;
        }

        #endregion


        /// <summary>
        /// Releases the rights work queue.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <param name="upc">The upc.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="isExactSearch">The is exact search.</param>
        /// <param name="releaseTitle">The release title.</param>
        /// <param name="adminCompany">The admin company.</param>
        /// <param name="contractIds">The contract ids.</param>
        /// <param name="fromDt">From dt.</param>
        /// <param name="toDt">To dt.</param>
        /// <param name="noRlsDt">The no RLS dt.</param>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        /// <param name="isLinkNavigation">if set to <c>true</c> [is link navigation].</param>
        /// <param name="linkIsrc">The link isrc.</param>
        /// <param name="linkUpc">The link upc.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReleaseRightsWorkQueue(PagingParams args, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isDynamic = true, bool isLinkNavigation = false)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var filterParams = new ReleaseFilterParameters { UserName = SessionWrapper.CurrentUserInfo.UserLoginName };
                // Get Search Critaria
                // Custom Params
                if (isDynamic)
                {
                    filterParams = GetFilterParametersForRelease(filterParams, tabIndex, upc, artist, isExactSearch,
                                                                 releaseTitle,
                                                                 adminCompany, contractIds, fromDt, toDt, noRlsDt,
                                                                 queryType, linkIsrc, linkUpc, isLinkNavigation);
                }
                else
                {
                    if (SessionWrapper.RightSetIds != null)
                    {
                        var rightSetId = "";
                        SessionWrapper.RightSetIds.ForEach(ids => rightSetId = rightSetId + Constants.Comma + ids.ToString());
                        filterParams.RepertoireFilter = new RepertoireFilter { RightSetIds = rightSetId, IsRightsReviewWQ = true };
                    }
                }


                // Paging
                filterParams.StartIndex = args.StartIndex < Constants.ValueZero ? Constants.ValueZero : args.StartIndex;
                filterParams.PageSize = args.PageSize == Constants.ValueZero ? Constants.PagesizeIntegerValue25 : args.PageSize;

                // Sorting
                var currentRequest = (RequestType)args.RequestType;
                if (currentRequest == RequestType.Sorting || currentRequest == RequestType.Paging)
                {
                    if (args.SortDescriptors != null)
                    {
                        filterParams.SortField = args.SortDescriptors[0].ColumnName;
                        filterParams.IsAscendingOrder = args.SortDescriptors[0].SortDirection.ToString() == Constants.AscendingValue;
                    }
                }

                var gridResult = GetReleaseRightsData(filterParams);
                long totalRecords = 0;
                if (gridResult.Count > 0)
                {
                    totalRecords = Convert.ToInt64(gridResult[0].TotalRows);
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return gridResult.GridJSONActions<ReleaseRightsAcquired>(totalRecords);
            }
            catch (Exception ex)
            {
                return Json(new { Result = GlobalConstants.JsonError, ex.Message });
            }
        }


        /// <summary>
        /// Gets the filter parameters for release.
        /// </summary>
        /// <param name="filterParams">The filter params.</param>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <param name="upc">The upc.</param>
        /// <param name="artist">The artist.</param>
        /// <param name="isExactSearch">The is exact search.</param>
        /// <param name="releaseTitle">The release title.</param>
        /// <param name="adminCompany">The admin company.</param>
        /// <param name="contractIds">The contract ids.</param>
        /// <param name="fromDt">From dt.</param>
        /// <param name="toDt">To dt.</param>
        /// <param name="noRlsDt">The no RLS dt.</param>
        /// <param name="queryType">Type of the query.</param>
        /// <param name="linkIsrc">The link isrc.</param>
        /// <param name="linkUpc">The link upc.</param>
        /// <param name="isLinkNavigation">if set to <c>true</c> [is link navigation].</param>
        /// <returns></returns>
        private ReleaseFilterParameters GetFilterParametersForRelease(ReleaseFilterParameters filterParams, string tabIndex, string upc, string artist, string isExactSearch, string releaseTitle, string adminCompany, string contractIds, string fromDt, string toDt, string noRlsDt, string queryType, string linkIsrc, string linkUpc, bool isLinkNavigation = false)
        {
            if (tabIndex == Constants.ReleaseCustomTab)
            {
                filterParams.RepertoireFilter.UPC = upc == string.Empty ? null : upc;
                filterParams.RepertoireFilter.Artist = artist == string.Empty ? null : artist;
                filterParams.IsExactSearch = isExactSearch == Constants.FlagTrue;
                filterParams.RepertoireFilter.ReleaseTitle = releaseTitle == string.Empty ? null : releaseTitle;
                filterParams.AdminCompany = adminCompany == string.Empty ? null : adminCompany;
                filterParams.RepertoireFilter.LinkedContract = contractIds == string.Empty ? null : contractIds;
                filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;
                filterParams.RepertoireFilter.IsLinkNavigation = isLinkNavigation;
                filterParams.RepertoireFilter.LinkISRC = linkIsrc;
                filterParams.RepertoireFilter.LinkUPC = linkUpc;
            }  //Predefined Params
            else if (tabIndex == Constants.ReleasePredefinedTab)
            {
                //filterParams.
                if (queryType == Constants.ReleaseParamsMultiArtistValue)
                {
                    filterParams.IsMultiArtist = true;
                    filterParams.IsOst = null;
                }
                else if (queryType == Constants.ReleaseParamsOstValue)
                {
                    filterParams.IsOst = true;
                    filterParams.IsMultiArtist = null;
                }
                else if (queryType == Constants.ReleaseParamsNonMacValue)
                {
                    filterParams.IsMultiArtist = false;
                    filterParams.IsOst = null;
                }
                filterParams.StartDate = fromDt == "" ? (DateTime?)null : Convert.ToDateTime(fromDt);
                filterParams.EndDate = toDt == "" ? (DateTime?)null : Convert.ToDateTime(toDt);
                filterParams.NoReleaseDate = noRlsDt == Constants.FlagFalse ? (bool?)null : true;
            }

            return filterParams;

        }


        /// <summary>
        /// Gets the release rights data.
        /// </summary>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        private List<ReleaseRightsAcquired> GetReleaseRightsData(ReleaseFilterParameters filterParams)
        {
            var gridResult = new List<ReleaseRightsAcquired>();
            try
            {
                //Load Release Rights WQ
                var releaseRightsResult = _rightsReviewWorkQueueRepository.LoadReleaseRightsWQ(filterParams);
                var masterData = _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData();

                //Linear Class implementation 
                foreach (ReleaseRight result in releaseRightsResult.ReleaseRights)
                {
                    var releaseRightsAcquired = new ReleaseRightsAcquired
                                                    {
                                                        AdminCompany = result.AdminCompany,
                                                        Artist = result.Artist,
                                                        Configration = result.Configration,
                                                        DigitallyExploitedText = result.Rights.DigitallyExploitedText,
                                                        Error = result.Rights.Error,
                                                        ExceptionText = result.Rights.Exception == null
                                                                            ? string.Empty
                                                                            : result.Rights.Exception == true
                                                                                  ? Constants.ExceptionApplied
                                                                                  : Constants.ExceptionNotApplied,
                                                        Notes = result.Rights.Notes,
                                                        RightsExpiryRule = result.Rights.RightsExpiryRule,
                                                        IsSplitDeal = result.IsSplitDeal,
                                                        LinkedTooltip = result.LinkedTooltip
                                                    };
                    if (result.Rights.LostReason != null)
                        releaseRightsAcquired.LostReason = result.Rights.LostReason == (int?)null ? string.Empty : result.Rights.LostReason == 0 ? string.Empty : masterData.LostRightsReason[result.Rights.LostReason.Value];
                    releaseRightsAcquired.LostRightsText = result.Rights.LostRightsText;
                    releaseRightsAcquired.LostRightsDate = result.Rights.LostRightsDate;
                    releaseRightsAcquired.PhysicallyExploitedText = result.Rights.PhysicallyExploitedText;
                    releaseRightsAcquired.ReleaseDate = result.ReleaseDate;
                    releaseRightsAcquired.ReleaseType = result.ReleaseTypeInfo;
                    releaseRightsAcquired.RightSetId = result.Rights.RightSetId;
                    releaseRightsAcquired.ReleaseId = result.ReleaseId;
                    releaseRightsAcquired.R2ReleaseId = result.R2ReleaseId;
                    releaseRightsAcquired.ContractId = result.ContractId;
                    releaseRightsAcquired.RightsPeriod = result.Rights.RightsPeriod == 0 ? string.Empty : masterData.RightsPeriod[result.Rights.RightsPeriod];
                    releaseRightsAcquired.RightsSourceId = result.RightsSourceId;
                    releaseRightsAcquired.TerritorialRights = result.Rights.TerritorialRights == null
                                                                            ? string.Empty
                                                                            : result.Rights.TerritorialRights == "null" ? string.Empty : result.Rights.TerritorialRights;
                    releaseRightsAcquired.Title = result.Title;
                    releaseRightsAcquired.UPCId = result.UPCId;
                    releaseRightsAcquired.VersionTitle = result.VersionTitle;
                    releaseRightsAcquired.TotalRows = result.TotalRows;
                    releaseRightsAcquired.SensitiveInfoPermitted = result.SensitiveInfoPermitted;
                    releaseRightsAcquired.RightsEditPermitted = result.RightsEditPermitted;
                    releaseRightsAcquired.ModifiedDateTimeLnr = result.Rights.ModifiedDateTime;
                    releaseRightsAcquired.LostRightsDateLinear = result.Rights.LostRightsDate.HasValue ? result.Rights.LostRightsDate.Value.ToString() : "";
                    releaseRightsAcquired.IsEditableRight = result.Rights.IsEditableRight;
                    gridResult.Add(releaseRightsAcquired);
                }
                if (gridResult.Count > 0)
                {
                    ViewData[Constants.totalreleaseRights] = gridResult[0].TotalRows;
                }
                return gridResult.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves the release info.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <returns></returns>
        private List<long> saveReleaseInfo(string jsonData)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<long>));
            var JsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            var jsonArray = (List<long>)jsonSerializer.ReadObject(JsonStream);
            return jsonArray;
        }

        /// <summary>
        /// Updates the master data for save.
        /// </summary>
        /// <param name="masterData">The master data.</param>
        /// <param name="rights">The rights.</param>
        /// <returns></returns>
        private List<ResourcesRight> UpdateMasterDataForSave
         (ReviewRightsMasterInfo masterData, List<ResourcesRight> rights)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();
                rights.Select(right => right.LostRightsReasonLnr != null ? (right.Rights.LostReason = masterData.LostRightsReasonReverse[right.LostRightsReasonLnr.Replace(" ", "").Replace(" ", "")]) : (int?)null).ToList();
                rights.Select(right => right.Rights.RightsPeriod = (right.RightsPeriodLnr != null ? masterData.RightsPeriodReverse[right.RightsPeriodLnr.Replace(" ", "").Replace(" ", "")] : 0)).ToList();
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();
                return rights;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        #region UC-18 SavePreClearanceEditedValues
        /// <summary>
        /// Save Preclearance Info U18 (Priority WQ) & UC 19 (Custom WQ)
        /// </summary>
        /// <param name="args"></param>
        /// <param name="preClearanceUpdate"></param>
        /// <param name="filterParams"></param>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        /// 
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SavePreClearanceEditedValues(PagingParams args, [Bind(Prefix = Constants.updatedRecords)]IEnumerable<PreClearanceResult> preClearanceUpdate, string filterParams, string tabIndex)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var preClearanceSaveInfo = new PreClearanceSaveInfo
                {
                    Rights =
                        preClearanceUpdate.Select(right => right.ConstructSaveInfo()).
                        ToList(),
                    ModifierInfo = { UserName = SessionWrapper.CurrentUserInfo.UserLoginName }
                };
                //linear to nested class values append
                //preclearance save method
                List<long> updatedRights = _rightsReviewWorkQueueRepository.SaveResourcePreClearanceRights(preClearanceSaveInfo);
                // After Save load updated data

                //create pre clearance grid model
                var preClearanceModel = new ResourcePreClearanceReviewModel();//assisgn Filter parameters

                if (filterParams != null)
                {


                    if (tabIndex == null)
                    {
                        //UC 18 Part//
                        var filterParameters = GetFilledFilterParams(filterParams, args,
                                                                     GrsTasks.EditRightsDataPrecleared);
                        preClearanceModel.PreClearanceResult =
                            _rightsReviewWorkQueueRepository.LoadResourcePreClearanceInfo(filterParameters);
                    }
                    else
                    {
                        //UC 19 Part//
                        if (tabIndex == Constants.ReleaseCustomTab)
                        {
                            var filterParameters = GetCustomFilledFilterParams(filterParams, args,
                                                                               GrsTasks.EditRightsDataPrecleared);
                            preClearanceModel.PreClearanceResult =
                                _rightsReviewWorkQueueRepository.LoadResourcePreClearanceInfo(filterParameters);
                        }
                        else if (tabIndex == Constants.ReleasePredefinedTab)
                        {
                            var filterParameters = GetCustomFilledPredefinedParams(filterParams, args,
                                                                               GrsTasks.EditRightsDataPrecleared);
                            preClearanceModel.PreClearanceResult =
                                _rightsReviewWorkQueueRepository.LoadResourcePreClearancePredefined(filterParameters);
                        }

                    }
                    preClearanceModel = GetPreclearanceData(preClearanceModel);
                    preClearanceModel.PreClearanceResult.Rights.ForEach(resultRight => resultRight.Error = updatedRights.Contains
                        (resultRight.PreClearanceInfo.RightInfo.RightSetId) ? Constants.Concurrencyproblems : null);
                    preClearanceModel.PreClearanceResult.Rights.Select(preClearance => preClearance.RightsTypeLnr =
                        RightsTypeDescriptions(preClearance.RightsType)).ToList();
                    ViewBag.totalRecords = preClearanceModel.PreClearanceResult.TotalRows;
                    ViewData[Constants.totalPreclearanceData] = preClearanceModel.PreClearanceResult.TotalRows;
                }

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return preClearanceModel.PreClearanceResult.Rights.GridJSONActions<PreClearanceResult>(preClearanceModel.PreClearanceResult.TotalRows);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }
        #endregion


        /// <summary>
        /// Loads the release digital rights.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="filterParams">The filter params.</param>
        private void LoadReleaseDigitalRights(PagingParams args, string filterParams)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();
                // _rightsReviewWorkQueueRepository.LoadReleaseDigitalRights(searchCriteria);

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();
                // return PartialView(releaseModel);
            }
            catch (Exception)
            {
                throw;
            }

        }


        //UC -18 BulkEdit Tabs
        #region Four Tabs Bulk Edit
        public ActionResult RightsAcquiredBulkEdit()
        {
            ResourceRightsWorkQueueModel resourceModel =
                   ConstructAcquiredRightsModelWithMasterData(true);

            return PartialView(resourceModel);
        }

        public ActionResult RightsAcquiredBulkEditTab()
        {
            return PartialView();
        }


        /// <summary>
        /// Pres the clearance Bulk Edit.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PreClearanceBulkEdit()
        {
            ResourcePreClearanceReviewModel preClearanceModel = new ResourcePreClearanceReviewModel();
            preClearanceModel.BooleanDropDown =
                AppendNoChangeRequiredSelect(preClearanceModel.BooleanDropDown);
            preClearanceModel.ReviewStatusDropDown.Remove(preClearanceModel.ReviewStatusDropDown.First(x => x.Value == Constants.NewForReview));
            preClearanceModel.ReviewStatusDropDown.Remove(preClearanceModel.ReviewStatusDropDown.First(x => x.Value == Constants.FinalForReview));
            preClearanceModel.ReviewStatusDropDown =
                AppendNoChangeRequiredSelect(preClearanceModel.ReviewStatusDropDown);
            return PartialView(Constants.PreClearanceBulkEdit, preClearanceModel);
        }

        [HttpPost]
        public ActionResult SecondaryExploitationBulkEdit()
        {
            var model = new ResourceSecondaryRightsReviewModel { MaterData = _rightsReviewWorkQueueRepository.LoadSecondaryMasterData() };

            _contractRepository.GetDefaultDataSecondaryExploitations();
            var filterCriterea = _contractRepository.ContractPageModel.SecondaryExploitationTabModel.SecondaryExploitation.ToArray();

            filterCriterea.Select(exploit => exploit.ExploitationType = model.MaterData.ExploitationTypes[exploit.ExploitationTypeId]).ToList();
            filterCriterea.Select(exp => exp.IsRestriction ? exp.RestrictionOptionId = Constants.ListOneValue : exp.RestrictionOptionId = Constants.ListTwoValue).ToList();
            model.right = new ResourceSecondaryRights { Exploitations = filterCriterea.ToList() };
            model.Restrictions = model.MaterData.Restrictions.Where(restriction => restriction.Key != 4).Select(restriction => new SelectListItem
                                                                                        {
                                                                                            Text = restriction.Value.ToString(),
                                                                                            Value = restriction.Key.ToString()
                                                                                        }).
                    ToList();

            //model.Restrictions.Add(new SelectListItem() { Text = string.Empty, Value = "0" });
            model.RestrictedOptions = model.MaterData.RestrictedOptions.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList();
            model.ConsentPeriod = model.MaterData.ConsentPeriod.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList();
            //model.ConsentPeriodLnr.Add(new SelectListItem() { Text = string.Empty, Value = "0" });
            model.ExploitationTypes = model.MaterData.ExploitationTypes.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList();
            model.Restrictions.Add(new SelectListItem() { Text = string.Empty, Value = Constants.ListZeroValue });
            model.ConsentPeriod.Add(new SelectListItem() { Text = string.Empty, Value = Constants.ListZeroValue });
            model.RestrictedOptions = AppendNoChangeSecondaryRequiredSelect(model.RestrictedOptions).OrderByDescending(value => Convert.ToInt32(value.Value)).ToList();
            model.ConsentPeriod = AppendNoChangeSecondaryRequiredSelect(model.ConsentPeriod).OrderByDescending(value => Convert.ToInt32(value.Value)).ToList();
            model.Restrictions = AppendNoChangeSecondaryRequiredSelect(model.Restrictions).OrderByDescending(value => Convert.ToInt32(value.Value)).ToList();
            model.ReviewStatusDropDown.Remove(model.ReviewStatusDropDown.First(x => x.Value == Constants.NewForReview));
            model.ReviewStatusDropDown.Remove(model.ReviewStatusDropDown.First(x => x.Value == Constants.FinalForReview));
            model.ReviewStatusDropDown = AppendNoChangeSecondaryRequiredSelect(model.ReviewStatusDropDown).OrderBy(value => value.Value).ToList();
            filterCriterea.Select(exploit => exploit.HoldBackPeriod = WorkQueueResource.NoChangeRequired);
            filterCriterea.Select(exploit => exploit.Notes = WorkQueueResource.NoChangeRequired);
            return PartialView(Constants.SecondaryExploitationBulkEdit, model);
        }


        public ActionResult DigitalRestrictionsBulkEdit(string HasDelete)
        {
            ViewBag.HasDelete = HasDelete == Constants.ListOneValue ? true : false;
            var digitalModel = (ResourceDigitalRightsReviewModel)ViewData[RepertoireSearch.Digital];
            if (digitalModel == null)
            {
                digitalModel = new ResourceDigitalRightsReviewModel { DigitalRightsMasterData = _rightsReviewWorkQueueRepository.LoadDigitalMasterData() };
                if (digitalModel.DigitalRestriction == null)
                    digitalModel.DigitalRestriction = new List<DigitalRightsMasterData>();
                digitalModel.DigitalRestriction.Add(new DigitalRightsMasterData());
                ViewData[@RepertoireSearch.Digital] = digitalModel;
                digitalModel.ReviewStatusDropDown.Remove(digitalModel.ReviewStatusDropDown.First(x => x.Value == Constants.FinalForReview));
                digitalModel.ReviewStatusDropDown.Remove(digitalModel.ReviewStatusDropDown.First(x => x.Value == Constants.NewForReview));
                // digitalModel.UseTypes = AppendNoChangeRequiredSelect(digitalModel.UseTypes.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList());
                // digitalModel.CommercialModelTypes = AppendNoChangeRequiredSelect(digitalModel.CommercialModelTypes.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList());
                // digitalModel.RestrictionTypes = AppendNoChangeRequiredSelect(digitalModel.RestrictionTypes.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList());
                // digitalModel.RestrictionReason = AppendNoChangeRequiredSelect(digitalModel.RestrictionReason.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList());
                // digitalModel.ConsentPeriodTypes = AppendNoChangeRequiredSelect(digitalModel.ConsentPeriodTypes.Select(restriction => new SelectListItem { Text = restriction.Value.ToString(), Value = restriction.Key.ToString() }).ToList());
            }
            return PartialView(Constants.DigitalRestrictionsBulkEdit, digitalModel);
        }
        #endregion

        #region "Territorial Rights"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trEditType">A <see cref="string"/> holds whether 
        /// edit type is Single or Bulk edit</param>
        /// <returns></returns>
        public PartialViewResult TerritorialRights(string trEditType, string rightSetId)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(string.Format("Contract ID:{0}", 0));
                LoggerFactory.MeasureLogWriter.Start();

                ModelState.Clear();

                ViewBag.Search = false;
                ViewBag.KeyId = 0;
                ViewBag.PageName = Constants.RightsWorkQueue;
                var territorial = new List<TerritorialDisplay>();
                if (trEditType == Constants.Bulk)
                {
                    territorial = _contractRepository.GetTerritorialRightsData(0, 0, SessionWrapper.CurrentUserInfo.UserLoginName);
                }
                else if (trEditType == Constants.Single)
                {
                    territorial = _rightsReviewWorkQueueRepository.LoadRightSetTerritory(Convert.ToInt64(rightSetId));
                }
                territorial = territorial.GroupBy(territorialDuplicate => new { territorialDuplicate.Id, territorialDuplicate.ParentId }).Select(territorialDuplicates => territorialDuplicates.FirstOrDefault()).ToList();
                ViewBag.Territories = GetTerritorialCount(territorial);

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return PartialView(TerritoryConstants.ManageTerritory, new ManageTerritoryModel());
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Gets the territorial count.
        /// </summary>
        /// <param name="territorialCount">The obj territorial count.</param>
        /// <returns></returns>
        private List<TerritorialDisplay> GetTerritorialCount(List<TerritorialDisplay> territorialCount)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                var territorialList = territorialCount.Where(territorial => territorial.IsTerritory);
                territorialList.All(total =>
                {
                    long countryCount = territorialCount.Count(territorialCounts => territorialCounts.ParentId == total.Id);
                    long includeCount = territorialCount.Count(territorialCounts => territorialCounts.ParentId == total.Id && territorialCounts.IsIncluded);
                    if (total.Id != 2 && total.Id != 0)//2- Universe, 0 - World
                        total.TerritoryCount = includeCount.ToString(CultureInfo.InvariantCulture) + GlobalConstants.Territory + countryCount.ToString(CultureInfo.InvariantCulture);
                    else
                    {
                        var allIncluded = territorialCount.Count(parent => parent.IsIncluded && !parent.IsTerritory);
                        long totalCountyCount = territorialCount.Count(totalCountyCounts => !totalCountyCounts.IsTerritory);
                        total.TerritoryCount = allIncluded.ToString(CultureInfo.InvariantCulture) + GlobalConstants.Territory + totalCountyCount.ToString(CultureInfo.InvariantCulture);
                    }
                    return true;
                });

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return territorialCount;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        #region Navigation From OtherUsecases

        /// <summary>
        /// Customs the rights review work queue.
        /// </summary>
        /// <param name="customWorkQueueType">Type of the custom work queue.</param>
        /// <returns></returns>
        public ActionResult CustomRightsReviewWorkQueue(string customWorkQueueType)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                PermissionCheckNdRedirect(new[] { GrsTasks.RightsReviewWQ });//Redirects To unauthorized page if permission in not there
                var userPermittedTask = PermissionExtension.GetPermittedTasks(GrsTasks.EditRightsDataHeader, GrsTasks.EditRightsDataDigital,
                                                     GrsTasks.EditRightsDataSecondary, GrsTasks.EditRightsDataPrecleared);

                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataHeader))
                {
                    ViewBag.EditRightsDataHeader = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataDigital))
                {
                    ViewBag.EditRightsDataDigital = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataSecondary))
                {
                    ViewBag.EditRightsDataSecondary = true;
                }
                if (userPermittedTask.HasFlag(GrsTasks.EditRightsDataPrecleared))
                {
                    ViewBag.EditRightsDataPrecleared = true;
                }

                if (customWorkQueueType == Constants.Release)  //For Releases (uc-20)
                {
                    ViewBag.referringType = Constants.Release;//Defines which page to open as a partial view
                    const string pageId = Constants.ListOneValue;
                    ViewBag.PageId = pageId;

                    //Check Edit Permission
                    ViewBag.EditRightsDataHeader =
                        SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.EditRightsDataHeader);
                    ViewBag.EditRightsDataDigital =
                        SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.EditRightsDataDigital);

                    ViewBag.releaseRightsAcquiredGridId = Constants.ReleaseRightsAcquireGrid + pageId;
                    ViewBag.releaseDigitalGridId = Constants.ReleaseDigitalRestrictionGrid + pageId;
                    var releaseModel = new ReleaseRightsWorkQueueModel
                    {
                        ReleaseRightsInfo = new ReleaseRightsAcquired(),
                        ReleaseDigitalRightsInfo = new ReleaseDigitalLnr(),
                        RightsMasterData =
                            _rightsReviewWorkQueueRepository.GetAcquiredRightsMasterData()
                    };
                    releaseModel.RightsPeriodDropDown = releaseModel.RightsMasterData.RightsPeriod.Values.ToList();
                    releaseModel.LostRightReasonDropDown =
                        releaseModel.RightsMasterData.LostRightsReason.Values.ToList();

                    releaseModel.LostRightReasonDropDown = releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.LostRightsReason);
                    releaseModel.RightsPeriodDropDown =
                        releaseModel.GetTrimmedValueforSyncfusionDropDown(releaseModel.RightsMasterData.RightsPeriod);

                    LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                    return View(Constants.CustomRightsReviewWorkQueue, new RightsReviewCustomWorkQueueModel { ReleaseRightsWorkQueueModel = releaseModel });
                }
                //For Resources (uc-19)

                ViewBag.referringType = Constants.Resource;//Defines which page to open as a partial view


                //UC-18 Pages  
                //get master data
                GetAcquiredRightsMasterData();
                // construct Resource model


                var resourceModel = new ResourceRightsWorkQueueModel
                {
                    RightsPeriodDropDown = acquiredRightsMasterData.RightsPeriod.Values.Select(t => t.Replace(" ", Constants.HtmlSpace)).ToList(),
                    LostRightReasonDropDown = acquiredRightsMasterData.LostRightsReason.Values.Select(t => t.Replace(" ", Constants.HtmlSpace)).ToList(),
                    ReviewReasonDropDown = acquiredRightsMasterData.ReviewReason.Select(reason =>
                        new SelectListItem { Text = reason.Value, Value = reason.Key.ToString() }).ToList()
                };



                //return model to view
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return View(Constants.CustomRightsReviewWorkQueue, new RightsReviewCustomWorkQueueModel { ResourceRightsWorkQueueModel = resourceModel });


            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }



        /// <summary>
        /// Customs the rights review work queue.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="isResourceCustom">if set to <c>true</c> [is resource custom].</param>
        /// <param name="filterParams">The filter params.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CustomRightsReviewWorkQueue(PagingParams args, string filterParams = "", bool isResourceCustom = false)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(Constants.MethodStart);
                LoggerFactory.MeasureLogWriter.Start();

                if (isResourceCustom)
                {
                    LoggerFactory.LogWriter.Debug(Constants.MethodEnd);

                    return RightsAcquired(args, filterParams);
                }
                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();

                return ReleaseRightsWorkQueue(args, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion Navigation From OtherUsecases


        #region Unlink and Linking functionalities

        /// <summary>
        /// Changes the contract.
        /// </summary>
        /// <param name="changeLinkItems">The Repertoire Rights items.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeContract(string changeLinkItems)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                LoggerFactory.LogWriter.Debug(string.Format("RepertoireRightsItem:{0}", changeLinkItems));

                //Unlink contract items
                LoggerFactory.MeasureLogWriter.Stop();
                var rightSets = GetLinkedRightSets(changeLinkItems);
                var unlinkedRightSet = new List<long>();
                if (rightSets.Any())
                {
                    unlinkedRightSet = UnlinkContract(changeLinkItems, true, rightSets);
                }
                return Json(new { Result = GlobalConstants.JsonOk, Record = unlinkedRightSet.ToList() });
                //Link process will start in javascript
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        public List<long> GetLinkedRightSets(string changeLinkItems)
        {

            try
            {
                LoggerFactory.MeasureLogWriter.Start();
                var changeLinkInfo = new DataContractJsonSerializer(typeof(ChangeLinkInfo[]));
                var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(changeLinkItems));
                var repertoireItems = (ChangeLinkInfo[])changeLinkInfo.ReadObject(memoryStream);
                return _rightsReviewWorkQueueRepository.GetLinkedRightSets(repertoireItems.ToList());

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        /// <summary>
        /// Unlinks the contract.
        /// </summary>
        /// <param name="unLinkItems">The work queue items.</param>
        /// <param name="isChangeLink">if set to <c>true</c> [is change link].</param>
        /// <param name="rightSets">The right sets.</param>
        /// <returns></returns>
        [HttpPost]
        public List<long> UnlinkContract(string unLinkItems, bool isChangeLink, List<long> rightSets)
        {
            try
            {
                var rightSetsUnlinked = new List<long>();
                if (isChangeLink)
                {
                    var result = new DataContractJsonSerializer(typeof(ChangeLinkInfo[]));
                    var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(unLinkItems));
                    var repertoireItems = (ChangeLinkInfo[])result.ReadObject(memoryStream);
                    rightSetsUnlinked = repertoireItems.ToList().Where(right => rightSets.Contains(Convert.ToInt64(right.RightSetId)) == false).Select(right => right.RightSetId).ToList();
                    var repertoire = repertoireItems.ToList().Where(right => rightSets.Contains(Convert.ToInt64(right.RightSetId))).ToArray();
                    _repertoireRightsSearchRepository.UnlinkContract(repertoire, SessionWrapper.CurrentUserInfo.UserLoginName);
                    return rightSetsUnlinked;
                }
                var repertoireType = unLinkItems.Split('^')[1];
                var unlinkItem = unLinkItems.Split('^')[0].Split(',');
                var contractId = long.Parse(unlinkItem[0]);
                var changeLinkInfo = new List<ChangeLinkInfo>
                                         {new ChangeLinkInfo(){
                                             ContractId = contractId,
                                             KeyId = long.Parse(unlinkItem[1]),
                                             RightSetId = long.Parse(unlinkItem[2]),
                                             LinkType = repertoireType
                                         }};
                var linkedRightSets = _rightsReviewWorkQueueRepository.GetLinkedRightSets(changeLinkInfo);
                if (linkedRightSets.Any() && linkedRightSets.First() == long.Parse(unlinkItem[2]))
                {
                    _rightsReviewWorkQueueRepository.UnlinkContract(unLinkItems,
                                                                    SessionWrapper.CurrentUserInfo.UserLoginName);
                    return null;
                }
                return rightSetsUnlinked;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Links the contract with Repertoire
        /// </summary>
        /// <param name="changeLinkItems">The repertorie Rights items.</param>
        /// <param name="contract">Contract list </param>
        /// <returns></returns>
        [HttpPost]
        public void LinkContract(string changeLinkItems, string contract)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();

                var result = new DataContractJsonSerializer(typeof(ChangeLinkInfo[]));
                var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(changeLinkItems));
                var repertorieItems = (ChangeLinkInfo[])result.ReadObject(memoryStream);

                result = new DataContractJsonSerializer(typeof(ContractInfo[]));
                memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(contract));
                var contractItems = (ContractInfo[])result.ReadObject(memoryStream);

                long contractId = contractItems[0].ContractId;

                LoggerFactory.LogWriter.Debug(changeLinkItems);

                //Link Release and Resource
                var releaseInfoItems = repertorieItems.Where(item => item.LinkType == Constants.Release).Select(release => release.KeyId != null ? new ReleaseInfo
                {
                    R2AccountId = release.R2KeyId.Value,
                    UserName = SessionWrapper.CurrentUserInfo.UserLoginName,
                    ReleaseId = release.KeyId.Value,
                    VersionTitle = release.VersionTitle,
                    ArtistName = release.ArtistName
                } : null).ToList();

                var resourceInfoItems = repertorieItems.Where(item => item.LinkType == Constants.Resource).Select(resource => resource.KeyId != null ? new ResourceInfo
                {
                    R2AccountId = resource.R2KeyId.Value,
                    UserName = SessionWrapper.CurrentUserInfo.UserLoginName,
                    ResourceId = resource.KeyId.Value
                } : null).ToList();

                var ownedProjectIdsForReleases = new List<ReleaseInfo>();
                var ownedProjectIdsForResources = new List<ResourceInfo>();
                // calling R2 Method for get the owned projectId for Release
                if (releaseInfoItems.Count > 0)
                {
                    var releaseInfo =
                        _releaseRepository.GetReleases(releaseInfoItems.Select(rel => rel.R2AccountId).ToList(),
                                                       SessionWrapper.CurrentUserInfo.UserLoginName);
                    ownedProjectIdsForReleases = releaseInfo.Values;

                    List<long> resourceIds = new List<long>();
                    foreach (var releaseInfoItem in releaseInfoItems)
                    {
                        var item = releaseInfoItem;
                        releaseInfoItem.OwnedProjectId =
                            ownedProjectIdsForReleases.Where(i => item != null && i.ReleaseId == item.ReleaseId).Select(
                                i => i.OwnedProjectId).FirstOrDefault();
                        resourceIds.Add(item.Id);
                    }
                }

                // calling R2 Method for get the owned projectId for Resource
                if (resourceInfoItems.Count > 0)
                {
                    var resourceInfo =
                        _resourceRepository.GetResources(resourceInfoItems.Select(res => res.R2AccountId).ToList(),
                                                         SessionWrapper.CurrentUserInfo.UserLoginName);
                    ownedProjectIdsForResources = resourceInfo.Values;

                    foreach (var resourceInfoItem in resourceInfoItems)
                    {
                        var item = resourceInfoItem;
                        resourceInfoItem.OwnedProjectId =
                            ownedProjectIdsForResources.Where(i => item != null && i.ResourceId == item.ResourceId).Select(
                                i => i.OwnedProjectId).FirstOrDefault();
                    }
                }

                var userRoleName = string.Empty;
                if (SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.LinkSplitContractToContract, GrsTasks.UnlinkSplitContractToContract))
                {
                    userRoleName = GlobalConstants.PowerUser;
                }

                ownedProjectIdsForReleases.All(item =>
                {
                    item.UserName = SessionWrapper.CurrentUserInfo.UserLoginName;
                    item.RoleName = userRoleName; return true;
                });
                ownedProjectIdsForResources.All(item => { item.UserName = SessionWrapper.CurrentUserInfo.UserLoginName; item.RoleName = userRoleName; return true; });


                _projectRepository.LinkReleaseResourceToContract(ownedProjectIdsForReleases, ownedProjectIdsForResources, contractId);
                SessionWrapper.ReleaseCollection = ownedProjectIdsForReleases;

                if (releaseInfoItems.Any())
                {
                    ViewBag.displayMessage = Constants.Release;
                }

                if (resourceInfoItems.Any())
                {
                    ViewBag.displayMessage = Constants.Resource;
                }

                LoggerFactory.LogWriter.Debug(Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion

    }


}



