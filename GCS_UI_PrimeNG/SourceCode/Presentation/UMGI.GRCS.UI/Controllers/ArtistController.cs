using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using Constants = UMGI.GRCS.BusinessEntities.Constants.Constants;


namespace UMGI.GRCS.UI.Controllers
{
    public class ArtistController : BaseController
    {
        
        #region Private Variable

        private IArtistRepository _objArtistRepository;

        #endregion Private Variable

        #region Artist Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController"/> class.
        /// </summary>
        public ArtistController(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractController"/> class.
        /// </summary>
        /// <param name="contractRepository">The artist repository.</param>
        /// <param name="sessionWrapper"> </param>
        public ArtistController(IArtistRepository artistRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _objArtistRepository = artistRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
        }

        private void InitilizeObjects(IArtistRepository artistRepository, ISessionWrapper sessionWrapper)
        {
            _objArtistRepository = artistRepository;
            SessionWrapper = sessionWrapper;
        }

        #endregion Artist Constructor

        #region Search For Artist

        /// <summary>
        /// Search for artist screen.
        /// </summary>
        /// <returns></returns>
        /// 
        public PartialViewResult SearchForArtist()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                this.ViewData.Add("OpenFrom", "");
                return PartialView(_objArtistRepository.ObjArtistModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult SearchForArtist(ArtistModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                //hadnling for firefox
                this.ViewData.Add("OpenFrom", "");
                if (model.existingArtist != null)
                {
                    if (model.existingArtist.Trim() != string.Empty)
                        _objArtistRepository.ObjArtistModel.GetArtistList = ParseString(model.existingArtist);
                }
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(_objArtistRepository.ObjArtistModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult SearchArtistForRegularResource(ArtistModel model)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                //hadnling for firefox
                this.ViewData.Add("OpenFrom", "RegularResource");
                if (model.existingArtist != null)
                {
                    if (model.existingArtist.Trim() != string.Empty)
                        _objArtistRepository.ObjArtistModel.GetArtistList = ParseString(model.existingArtist);
                }
                LoggerFactory.LogWriter.MethodExit();
                return PartialView("SearchForArtist", _objArtistRepository.ObjArtistModel);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }

        }
        
        /// <summary>
        /// Searches for artist.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <param name="firstName">First Name.</param>
        /// <param name="lastName">Last Name.</param>
        /// <param name="artistId">The artistid.</param>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <param name="jtStartIndex">Start index of the jt.</param>
        /// <param name="jtPageSize">Size of the jt page.</param>
        /// <param name="jtSorting">The jt sorting.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ArtistSearch(string ArtistName, int jtStartIndex, long ArtistID = 0, int PageSize = 0, long rowIndex = 0, string userName = "", string jtSorting = null)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();

                if (string.IsNullOrEmpty(ArtistName))
                {
                    Page page = new Page();
                    page.PageSize = PageSize;
                    FilterFields criteria = new FilterFields();
                    criteria.PageSize = page.PageSize;
                    criteria.RowIndex = rowIndex;
                    criteria.UserName = userName;
                    criteria.QualificationCriteria = true;
                    var searchOption = new ArtistSearchCriteria
                    {
                        Criteria = criteria,
                        ArtistName = ArtistName,
                        ArtistId = ArtistID,
                    };
                    var artistResult = _objArtistRepository.SearchArtist(searchOption, criteria.QualificationCriteria);
                    var result = artistResult.Details.ToList();
                    LoggerFactory.LogWriter.MethodExit();
                    return Json(new { Result = Constants.JsonOk, Records = result, TotalRecordCount = result.Count });
                }
                else
                {
                    if (ArtistName.Contains('='))
                    {
                        var result = ParseString(ArtistName);
                        return Json(new { Result = Constants.JsonOk, Records = result });
                    }
                    else
                    {
                        Page page = new Page();
                        page.PageSize = PageSize;
                        FilterFields criteria = new FilterFields();
                        criteria.PageSize = page.PageSize;
                        criteria.RowIndex = rowIndex;
                        criteria.UserName = userName;
                        criteria.QualificationCriteria = true;
                        var searchOption = new ArtistSearchCriteria
                        {
                            Criteria = criteria,
                            ArtistName = ArtistName,
                            ArtistId = ArtistID
                        };

                        searchOption.Criteria.StartIndex = jtStartIndex;
                        var artistResult = _objArtistRepository.SearchArtist(searchOption, criteria.QualificationCriteria);
                        var result = artistResult.Details.ToList();
                        LoggerFactory.LogWriter.MethodExit();
                        return Json(new { Result = Constants.JsonOk, Records = result, TotalRecordCount = artistResult.RowsRetreived, RowIndex = artistResult.RowIndex });
                    }
                }

            }
            catch (Exception ex)
            {

                return Json(new { Result = Constants.JsonError, ex.Message });
            }
        }

        private List<ArtistDetail> ParseString(string text)
        {
            LoggerFactory.LogWriter.MethodStart();
            string[] _arrReslist = text.Split('=');
            List<ArtistDetail> listSelectedItems = new List<ArtistDetail>();
            for (int i = 0; i < _arrReslist.Length; i++)
            {                
                ArtistSearch artistSearch = new ArtistSearch();
                ArtistInfo artistInfo = new ArtistInfo();
                string[] listResources = _arrReslist[i].ToString().Split(':');

                //check the project user id is not null or empty
                if (listResources[0].ToString() != "")
                {
                    artistInfo.Id = Convert.ToInt64(listResources[1]);
                    artistInfo.NameId = Convert.ToInt64(listResources[2]);
                    artistSearch.Info = artistInfo;
                    listSelectedItems.Add(new ArtistDetail()
                    {
                        FirstName = listResources[0].ToString(),
                        Info = artistSearch.Info
                    });
                }

            }

            LoggerFactory.LogWriter.MethodExit();
            return listSelectedItems;
        }

        #endregion Search For Artist

    }
}
