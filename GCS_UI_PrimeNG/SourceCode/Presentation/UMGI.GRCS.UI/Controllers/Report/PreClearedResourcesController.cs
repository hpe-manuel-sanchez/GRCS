
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : PreClearedResourcesController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 12-4-2013 
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
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.UI.Models.Report;
using UMGI.GRCS.UI.Interfaces.Report;
using ModelConstant = UMGI.GRCS.UI.Models;
using Syncfusion.Mvc.Grid;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.Utilities.logger;
using UMGI.GRCS.UI.Helper;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UI.Controllers.Report
{
    /// <summary>
    /// Controller For Report Requests
    /// </summary>
    public partial class ReportController : BaseController
    {
        //
        // GET: /PreClearedResources/

        /// <summary>
        /// Display empty PreClearedResources screen 
        /// </summary>
        /// <returns></returns>
        public ActionResult PreClearedResources()
        {
            LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
            if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.ViewRepertoireRights))
            {
                RedirectToUnAuthorizedPage();
            }

            PreClearedResourcesModel model = new PreClearedResourcesModel();
            model.ResourceGenreList = _reportRepository.GetResourceGenre();
            model.ResourceTypeList = _reportRepository.GetResourceType();
            model.PreClearanceTypeList = _reportRepository.GetPreClearances();
            return View(ModelConstant.Constants.PreClearedResources, model);
        }

        /// <summary>
        ///  Display PreClearedResources screen 
        /// </summary>
        /// <param name="args">Paging Parameters</param>
        /// <param name="model">Pre-Cleared Resources Model</param>
        /// <param name="generateReport">Flag to generate Report </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PreClearedResources(PagingParams args, PreClearedResourcesModel model, string generateReport)
        {
            try
            {
                var resultCount = ModelConstant.Constants.ValueZero;
              //  model.resources = new List<IPreClearedResourcesModel>();

                /********************** Export Report***********************************/
                PreClearedResourceReportFilter parameters = new PreClearedResourceReportFilter
                {
                    ClearanceCompanyId = model.ClearanceCompanyId,
                    ArtistId = model.Artist,
                    genre = model.ResourceGenre,
                    PreClearanceType = model.PreClearanceType,
                    Title = model.Title,
                    ResourceType = model.ResourceType,
                    YearFrom = model.YearFrom,
                    YearTo = model.YearTo,
                    CountryId = model.ExploitationCountryId,
                    StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                    PageSize = args.PageSize,
                    SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[0].ColumnName,
                    IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[0].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                    ExportType = model.ExportType
                };

                if (!String.IsNullOrEmpty(model.ExportType))
                {
                    parameters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                    parameters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                    ExportReport_PreClearedResource(ModelConstant.Constants.PreClearedResources, parameters);
                    return View(ModelConstant.Constants.PreClearedResources, model);
                }

                /********************** End Export Report***********************************/

                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3},Sorting:{4}", model.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, 1, 1, null));


                if (generateReport == ModelConstant.Constants.FlagTrue)
                {
                    model.resources = this.GetData(args, parameters);
                    if (model.resources.Count > ModelConstant.Constants.ValueZero)
                    {
                        resultCount = Convert.ToInt32(model.resources[0].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                  
                }
                
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.resources.GridJSONActions<PreClearedResources>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<IPreClearedResourcesModel> GetData(PagingParams args, PreClearedResourceReportFilter parameters)
        {
            int pagesize = args.PageSize == ModelConstant.Constants.ValueZero ? 25 : Convert.ToInt32(args.PageSize);
            List<IPreClearedResourcesModel> model = new List<IPreClearedResourcesModel>();
            model = _reportRepository.GetPreClearedResourcesDetails(parameters);
            return model;
        }

        #region Export fuction 

        /// <summary>
        /// Method to export report for Pre-Cleared Resource
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_PreClearedResource(string reportName, PreClearedResourceReportFilter filters)
        {
            try
            {
                String dtE = DateTime.Now.ToString(ModelConstant.Constants.ReportDBdateFormat);
                string format = filters.ExportType; //Desired format goes here (PDF, Excel, or Image) 
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", filters.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, filters.StartIndex, filters.PageSize));
                
                var userName = SessionWrapper.CurrentPermissions.ApplicationUser.FirstName +String.Empty + SessionWrapper.CurrentPermissions.ApplicationUser.LastName;
                Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();
                rview.ServerReport.ReportServerUrl = new Uri(_configurationFactory.ReportServerUrl);
                
                rview.ServerReport.ReportServerCredentials = (IReportServerCredentials)new CustomReportCredentials(_configurationFactory.ReportUserName, _configurationFactory.ReportUserPassword, _configurationFactory.ReportUserDomain);
                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Resource_Type", filters.ResourceType));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ArtistId", filters.ArtistId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Genre", filters.genre));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Title", filters.Title));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClearanceCo", filters.ClearanceCompanyId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("P_YearFrom", filters.YearFrom.ToString()));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("P_YearTo", filters.YearTo.ToString()));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Country", filters.CountryId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("PreClearance", filters.PreClearanceType));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + ModelConstant.Constants.UrlPreClearedResources;
                rview.ServerReport.SetParameters(paramList);
                string mimeType, encoding, extension;
                string[] streamids;
                Microsoft.Reporting.WebForms.Warning[] warnings;



                byte[] bytes = rview.ServerReport.Render(format, ModelConstant.Constants.deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Clear();
                if (format == ModelConstant.Constants.ReportFormatPDF)
                {
                    //Response.ContentType = ModelConstant.Constants.ContentTypePDF;
                    Response.AddHeader(ModelConstant.Constants.ReportHeaderName, ModelConstant.Constants.ReportHeaderValuePDF);
                }
                else if (format == ModelConstant.Constants.ReportFormatExcel)
                {
                    // Response.ContentType = ModelConstant.Constants.ContentTypeExcel;
                    Response.AddHeader(ModelConstant.Constants.ReportHeaderName, ModelConstant.Constants.ReportHeaderValueExcel);
                }
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.End();
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
