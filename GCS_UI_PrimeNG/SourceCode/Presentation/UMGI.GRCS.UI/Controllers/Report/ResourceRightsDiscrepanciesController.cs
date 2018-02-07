
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      :ResourceRightsDiscrepanciesController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 16-04-2013 
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
using System.Web.Mvc;
using Syncfusion.Mvc.Grid;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.UI.Models.Report;
using UMGI.GRCS.Utilities.logger;
using ModelConstant = UMGI.GRCS.UI.Models;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UI.Controllers.Report
{
    /// <summary>
    /// Controller For Report Requests
    /// </summary>
    public partial class ReportController : BaseController
    {

        public ActionResult ResourceRightsDiscrepancies()
        {            
            LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
            if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.ViewRepertoireRights))
            {
                RedirectToUnAuthorizedPage();
            }

            ResourceRightsDiscrepanciesModel descripencyModel = new ResourceRightsDiscrepanciesModel();
            return View(ModelConstant.Constants.ResourceRightDescripency, descripencyModel);
        }

        [HttpPost]
        public ActionResult ResourceRightsDiscrepancies(PagingParams args,  ResourceRightsDiscrepanciesModel model, string generateReport)
        {
            try
            {
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", model.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, args.StartIndex, args.PageSize));

                long resultCount = ModelConstant.Constants.ValueZero;
                ResourceRightsDiscrepanciesReportFilter filters;               

                if (generateReport == ModelConstant.Constants.FlagTrue)
                {
                    var sYear = model.StartDate == DateTime.MinValue ? model.EndDate.AddMonths(-6).ToString().Split('/') : model.StartDate.ToString().Split('/');
                    var eYear = model.EndDate == DateTime.MinValue ? model.StartDate.AddMonths(6).ToString().Split('/') : model.EndDate.ToString().Split('/');
                    filters = new ResourceRightsDiscrepanciesReportFilter
                    {
                        ClearanceCompanyId = model.ClearanceCompanyId,
                        StartDate = sYear[1] + sYear[2].Split(' ')[ModelConstant.Constants.ValueZero],
                        EndDate = eYear[1] + eYear[2].Split(' ')[ModelConstant.Constants.ValueZero],
                        StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                        PageSize = args.PageSize,
                        SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[ModelConstant.Constants.ValueZero].ColumnName,
                        IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[ModelConstant.Constants.ValueZero].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                        ExportType = model.ExportType
                    };
                    if (!String.IsNullOrEmpty(model.ExportType))
                    {
                        filters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                        filters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                        ExportReport_ResourceRightsDiscrepancies(ModelConstant.Constants.ResourceRightDescripency, filters);
                        return View(ModelConstant.Constants.ResourceRightDescripency, model);
                    }

                    model.Discrepancies = this.GetData(args, filters);
                    if (model.Discrepancies.Count > ModelConstant.Constants.ValueZero)
                    {
                        resultCount = Convert.ToInt32(model.Discrepancies[ModelConstant.Constants.ValueZero].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                 }
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.Discrepancies.GridJSONActions<ResourceRightsDiscrepancies>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        private List<IResourceRightsDiscrepanciesModel> GetData(PagingParams args, ResourceRightsDiscrepanciesReportFilter parameters)
        {
            int pagesize = args.PageSize == ModelConstant.Constants.ValueZero ? 25 : Convert.ToInt32(args.PageSize);
            List<IResourceRightsDiscrepanciesModel> model = new List<IResourceRightsDiscrepanciesModel>();
            model = _reportRepository.GetResourceRightsDiscrepanciesDetail(parameters);
            return model;
        }

        #region EXPORT DATA

        /// <summary>
        /// Method to export report for Resource Rights Discrepancies
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_ResourceRightsDiscrepancies(string reportName, ResourceRightsDiscrepanciesReportFilter filters)
        {
            try
            {
                String dtE = DateTime.Now.ToString(ModelConstant.Constants.ReportDBdateFormat);
                string format = filters.ExportType; //Desired format goes here (PDF, Excel, or Image) 
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", filters.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, filters.StartIndex, filters.PageSize));
                 var userName = SessionWrapper.CurrentPermissions.ApplicationUser.FirstName + String.Empty + SessionWrapper.CurrentPermissions.ApplicationUser.LastName;
                Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();
                rview.ServerReport.ReportServerUrl = new Uri(_configurationFactory.ReportServerUrl);
                // IReportServerCredentials irsc = new CustomReportCredentials(_configurationFactory.ReportUserName, _configurationFactory.ReportUserPassword, _configurationFactory.ReportUserDomain);
                rview.ServerReport.ReportServerCredentials = (IReportServerCredentials)new CustomReportCredentials(_configurationFactory.ReportUserName, _configurationFactory.ReportUserPassword, _configurationFactory.ReportUserDomain);
                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();

                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClearanceCo", filters.ClearanceCompanyId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("StartMonthYear", filters.StartDate));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("EndDMonthYear", filters.EndDate));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + ModelConstant.Constants.UrlResourceRightsDataDescripency;
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
