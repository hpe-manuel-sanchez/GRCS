/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ReportController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 5-3-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.UI.Utilities;
using UMGI.GRCS.Utilities.logger;
using ModelConstant = UMGI.GRCS.UI.Models;
using System.Web.Mvc;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.BusinessEntities.Requests;
using UMGI.GRCS.UI.Interfaces.RepertoireRightsSearch;
using UMGI.GRCS.UI.Helper;
using System.Globalization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Text;
namespace UMGI.GRCS.UI.Controllers.Report
{
    public partial class ReportController : BaseController
    {
        #region Private Variable
        private readonly IReportRepository _reportRepository;
        private IGlobalRepository _globalRepository;
        private IRepertoireRightsSearchRepository _repertoireRightsSearchRepository;
        private IConfigFactory _configurationFactory { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="reportRepository">The report repository.</param>
        /// <param name="sessionWrapper">The session Wrapper. </param>
        /// <param name="logFactory"></param>
        public ReportController(IReportRepository reportRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory, IGlobalRepository globalRepository, IRepertoireRightsSearchRepository repertoireRightsSearchRepository, IConfigFactory configFactory)
        {
            _reportRepository = reportRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
            _globalRepository = globalRepository;
            _configurationFactory = configFactory;
            _repertoireRightsSearchRepository = repertoireRightsSearchRepository;
        }

        /// <summary>
        /// get user's company list from user profile.
        /// </summary>
        /// <param name="tasks">the user task</param>
        /// <returns></returns>
        private string GetUserCompanyList(GrsTasks tasks)
        {
            try
            {
                StringBuilder AdminCompany = new StringBuilder();
                String AdminCo = string.Empty;
                //var list = new List<ClearanceAdminCompany>();
                //list = this._globalRepository.AutoSearchClearanceCompCountry(String.Empty, tasks);
                //foreach (ClearanceAdminCompany comp in list)
                //{
                //    AdminCompany.AppendFormat("{0}  ,", comp.Id);
                //}
                //AdminCo = AdminCompany.ToString();
                //AdminCo = AdminCo.Remove(AdminCo.Length - 1);

                var list = SessionWrapper.CurrentPermissions.GetCompanyIdsByTask(GrsTasks.SearchContract);
                AdminCo = string.Join(" ,", list.Select(pair => string.Format("{0}", pair.Key)));              
             
                return AdminCo;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #region country

        /// <summary>
        /// search country for pop-up
        /// </summary>
        /// <param name="clearanceIds">The clearance Ids</param>
        /// <param name="clearanceNames">The clearance company Names</param>
        /// <returns></returns>
        public ActionResult Country(string clearanceIds, string clearanceNames)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(string.Format("clearanceIds:{0},clearanceNames{1}", clearanceIds, clearanceNames));
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return PartialView(ModelConstant.Constants.CountryPopUpView);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Autoes the search clearance comp country.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public JsonResult SelectTerritorialCountries(string term)
        {
            // Added for input type values as No change required in Bulk Edit
            term = term == UMGI.GRCS.UI.Models.Constants.NoChangeRequired ? String.Empty : term;

            LoggerFactory.LogWriter.Debug(string.Format("term:{0}", term));
            var dataRequestInfo = new DataRequestInfo { Name = term };
            var result = _repertoireRightsSearchRepository.GetCountries(dataRequestInfo).ResponseInfos;
            if (result.Count != ModelConstant.Constants.ValueZero)
                result = result.OrderByDescending(ddd => ddd.Name).ToList();
            LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion country

        #region company

        /// <summary>
        /// search company for pop-up
        /// </summary>
        /// <param name="clearanceIds">The clearance Ids</param>
        /// <param name="clearanceNames">The clearance company Names</param>
        /// <returns></returns>
        public ActionResult Company()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return PartialView(ModelConstant.Constants.CompanyPopUpView);

            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// For AutoSearching clearance comp country.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns>Result in json format</returns>
        public JsonResult AutoSearchClearanceCompCountry(string term, string reportType)
        {
            try
            {
                LoggerFactory.MeasureLogWriter.Start();
                LoggerFactory.LogWriter.Debug(string.Format("term:{0}", term));

                var list = new List<AutoSuggestionEntity>();
                if (term != string.Empty)
                {
                    if (reportType == ModelConstant.Constants.ContractReportType)
                    {
                        list =
                            _globalRepository.AutoSearchClearanceCompCountry(term, GrsTasks.SearchContract).Select(
                                item =>
                                new AutoSuggestionEntity { id = Convert.ToInt32(item.Id), value = string.Concat(item.Name, GlobalConstants.OpenSquareWithSpace, item.CountryIsoCode, GlobalConstants.CloseSquareWithSpace), label = string.Concat(item.Name, GlobalConstants.OpenSquareWithSpace, item.CountryIsoCode, GlobalConstants.CloseSquareWithSpace) }).OrderBy(ordCompCountry => ordCompCountry.label).ToList();
                    }
                    else
                    {
                        list =
                         _globalRepository.AutoSearchClearanceAdminCompany(term).Select(
                             item =>
                             new AutoSuggestionEntity { id = Convert.ToInt32(item.Id), value = string.Concat(item.Name, GlobalConstants.OpenSquareWithSpace, item.CountryIsoCode, GlobalConstants.CloseSquareWithSpace), label = string.Concat(item.Name, GlobalConstants.OpenSquareWithSpace, item.CountryIsoCode, GlobalConstants.CloseSquareWithSpace) }).OrderBy(ordCompCountry => ordCompCountry.label).ToList();
                    }
                }

                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Utilities.Constants.MethodEnd);
                LoggerFactory.MeasureLogWriter.Stop();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        #endregion company

        /// <summary>
        /// Action Method invoked on Artist search
        /// </summary>
        /// <param name="term">the term</param>
        /// <returns>Result in json format</returns>
        [OutputCache(Duration = 0)]
        public JsonResult AutoSearchArtistList(string term)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(term);

                var list = _reportRepository.AutoSearchArtistName(term).Select(item => new AutoSearchArtistEntity { id = item.Id.ToString(CultureInfo.InvariantCulture), value = item.Name, label = item.Name }).ToList();

                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }


        

    }
}


