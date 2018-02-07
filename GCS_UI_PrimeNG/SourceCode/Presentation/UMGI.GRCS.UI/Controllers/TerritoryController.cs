using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.Globalization;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using BusinessConstants = UMGI.GRCS.BusinessEntities.Constants.Constants;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    public class TerritoryController : BaseController
    {
        #region Variable Declarartions

        readonly IGlobalRepository _globalRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes anew instance of the class
        /// </summary>
        public TerritoryController(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryController"/> class.
        /// </summary>
        /// <param name="globalRepository">The globalRepository</param>
        /// <param name="sessionWrapper">The sessionWrapper</param>
        public TerritoryController(IGlobalRepository globalRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _globalRepository = globalRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
        }

        #endregion

        #region User Defined Methods

        #region Public Methods

        /// <summary>
        /// Get all styles in the specified file
        /// </summary>
        /// <returns></returns>
        public string GetStyle()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                Response.ContentType = "text/css";
                if (SessionWrapper.CurrentUserInfo.UserApplicationName == AnaTargetApplication.Gcs)
                {
                    LoggerFactory.LogWriter.MethodExit();
                    return System.IO.File.ReadAllText(Server.MapPath("/GCS/Content/ManageTerritory.css"));
                }
                else
                {
                    LoggerFactory.LogWriter.MethodExit();
                    return System.IO.File.ReadAllText(Server.MapPath("/GCS/Content/SubPages/TerritorialRights.css"));
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Get list of territories and countries.
        /// </summary>
        /// <returns>Partial view</returns>
        [HttpPost]
        public ActionResult GetTerritories(List<TerritorialDisplay> territory, string id, bool IsCallFromWorkgroup = false)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                //Clear the state of the model.
                ModelState.Clear();
                //Set the search value is false for expand/collapse the treeview.
                ViewBag.Search = false;
                int keyId = 0;
                Int32.TryParse(id, out keyId);
                ViewBag.Territories = GetTerritoryDetails(territory);
                var model = new ManageTerritoryModel {TargetApplication = AnaTargetApplication.Gcs, IdForTerritory = keyId, IsCallFromWorkgroup = IsCallFromWorkgroup};

                LoggerFactory.LogWriter.MethodExit();

                return PartialView(BusinessConstants.TerritoryConstants.ManageTerritory, model);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        [HttpPost]
        public MvcHtmlString AutocompleteSuggestions(string territorialItemString, string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                LoggerFactory.LogWriter.Debug(string.Format("id:{0}", id));

                var jsonSerializer = new DataContractJsonSerializer(typeof(List<TerritorialDisplay>));
                var jsonMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(territorialItemString));
                var territorialItems = (List<TerritorialDisplay>)jsonSerializer.ReadObject(jsonMemoryStream);

                long topLevelParentId;
                ModelState.Clear();
                if (id != null)
                {
                    territorialItems = territorialItems.GroupBy(territorialDuplicate => new { territorialDuplicate.Id, territorialDuplicate.ParentId }).Select(territorialDuplicates => territorialDuplicates.FirstOrDefault()).ToList();
                    territorialItems = territorialItems.Where(territory => territory.Name != null).ToList();
                    ViewBag.Search = true;
                    topLevelParentId = TreeViewHelper.GetCountryParentId(territorialItems.ToList());
                }
                else
                {
                    ViewBag.Search = false;
                    topLevelParentId = TreeViewHelper.GetCountryParentId(territorialItems.ToList());
                }
                LoggerFactory.LogWriter.MethodExit();

                return MvcHtmlString.Create(CustomExtensions.BuildMenuItems(territorialItems.ToList(), topLevelParentId, ViewBag.Search, 0));
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Autocompletes the suggestions.
        /// </summary>
        /// <param name="territorialParent">The obj searching items.</param>
        /// <param name="id">The id.</param>
        /// <returns>MvcHtmlString</returns>
        [HttpPost]
        public MvcHtmlString SafeTerritoryAutocompleteSuggestions(List<TerritorialDisplay> territorialParent, string id)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "territorialParent:{1},id:{2}", "AutocompleteSuggestions", territorialParent, id));

                long topLevelParentId;
                ModelState.Clear();
                if (id != null)
                {
                    if (territorialParent == null)
                    {
                        territorialParent = new List<TerritorialDisplay>();
                    }
                    territorialParent = territorialParent.GroupBy(territorialDuplicate => new { territorialDuplicate.Id, territorialDuplicate.ParentId }).Select(territorialDuplicates => territorialDuplicates.FirstOrDefault()).ToList();
                    territorialParent = territorialParent.Where(territory => territory.Name != null).ToList();
                    ViewBag.Search = true;
                    topLevelParentId = TreeViewHelper.GetCountryParentId(territorialParent.ToList());
                }
                else
                {
                    ViewBag.Search = false;
                    topLevelParentId = TreeViewHelper.GetCountryParentId(territorialParent.ToList());
                }

                LoggerFactory.LogWriter.Debug(string.Format(Constants.MethodEnd, "AutocompleteSuggestions"));
                LoggerFactory.LogWriter.MethodExit();

                return MvcHtmlString.Create(CustomExtensions.BuildMenus(territorialParent.ToList(), topLevelParentId, ViewBag.Search, 0));
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Get the territory details from the specified service/session
        /// </summary>
        /// <param name="territory">included territories</param>
        /// <returns>collection of territory details</returns>
        private List<TerritorialDisplay> GetTerritoryDetails(List<TerritorialDisplay> territory)
        {
            List<TerritorialDisplay> territoryDisplay = null;
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (SessionWrapper.ManageTerritoryModel == null)
                {
                    //Get territories from service
                    var territoryCollection = _globalRepository.GetTerritories();
                    territoryCollection = territoryCollection.GroupBy(territorialDuplicate => new { territorialDuplicate.Id, territorialDuplicate.ParentId }).Select(territorialDuplicates => territorialDuplicates.FirstOrDefault()).ToList();
                    if (SessionWrapper.ManageTerritoryModel == null)
                    {
                        //Set the territory list in session
                        SessionWrapper.ManageTerritoryModel = new ManageTerritoryModel();
                        SessionWrapper.ManageTerritoryModel.Territories = territoryCollection;
                    }
                    if (territory == null)
                    {
                        territoryDisplay = GetTerritorialCount(territoryCollection);
                    }
                    else
                    {
                        territoryDisplay = GetTerritorialCount(SetIncludedTerritory(territoryCollection, territory));
                    }
                }
                else if (SessionWrapper.ManageTerritoryModel.Territories != null)
                {
                    if (territory == null)
                    {
                        territoryDisplay = GetTerritorialCount(SessionWrapper.ManageTerritoryModel.Territories);
                    }
                    else
                    {
                        territoryDisplay = GetTerritorialCount(SetIncludedTerritory(SessionWrapper.ManageTerritoryModel.Territories, territory));
                    }
                }
                LoggerFactory.LogWriter.MethodExit();
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            return territoryDisplay;
        }

        /// <summary>
        /// build the included/excluded countries as selected in the given inputs.
        /// </summary>
        /// <param name="globalValues">whole territory/country collection</param>
        /// <param name="includedCountries">included country collection</param>
        /// <returns>collection of territory details</returns>
        private List<TerritorialDisplay> SetIncludedTerritory(List<TerritorialDisplay> globalValues, List<TerritorialDisplay> includedCountries)
        {
            
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                List<TerritorialDisplay> GlobalCollection = new List<TerritorialDisplay>();

                GlobalCollection = globalValues.CloneDataContract<List<TerritorialDisplay>>();

                foreach (TerritorialDisplay territory in includedCountries)
                {
                    foreach (TerritorialDisplay item in GlobalCollection.Where(c => c.Id == territory.Id))
                    {
                        if (territory.IsIncluded)
                        {
                            item.IsIncluded = territory.IsIncluded;
                            if (item.IsExcluded == true)
                            {
                                item.IsIncluded = false;
                            }
                        }
                        else
                        {
                            item.IsExcluded = territory.IsExcluded;
                            if (item.IsIncluded == true)
                            {
                                item.IsIncluded = territory.IsIncluded;
                            }
                        }
                    }
                }

                LoggerFactory.LogWriter.MethodExit();
                return GlobalCollection;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
            
        }

        /// <summary>
        /// Gets the territorial count.
        /// </summary>
        /// <param name="objTerritorialCount">The obj territorial count.</param>
        /// <returns>collection of territory details</returns>
        private List<TerritorialDisplay> GetTerritorialCount(List<TerritorialDisplay> objTerritorialCount)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                var objTerritorialList = objTerritorialCount.Where(territorial => territorial.IsTerritory);
                objTerritorialList.All(total =>
                {
                    long countryCount = objTerritorialCount.Count(territorialCount => territorialCount.ParentId == total.Id);
                    long includeCount = objTerritorialCount.Count(territorialCount => territorialCount.ParentId == total.Id && territorialCount.IsIncluded);
                    if (total.Id != 2 && total.Id != 0)
                    {
                        if (total.Id == 0)
                            total.TerritoryCount = (includeCount - 1).ToString(CultureInfo.InvariantCulture) + BusinessConstants.TerritoryConstants.Territory + (countryCount - 1).ToString(CultureInfo.InvariantCulture);
                        else
                            total.TerritoryCount = includeCount.ToString(CultureInfo.InvariantCulture) + BusinessConstants.TerritoryConstants.Territory + countryCount.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        var allIncluded = objTerritorialCount.Count(parent => parent.IsIncluded && !parent.IsTerritory);
                        total.TerritoryCount = allIncluded.ToString(CultureInfo.InvariantCulture) + BusinessConstants.TerritoryConstants.Territory + objTerritorialCount.Count.ToString(CultureInfo.InvariantCulture);
                    }
                    return true;
                });

                LoggerFactory.LogWriter.MethodExit();
                return objTerritorialCount;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion Private Methods

        #endregion User Defined Methods
    }

}
