
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsExpiryToBeDeterminedController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Report Team
 * Created on     : 16-2-2013 
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
using UMGI.GRCS.UI.Utilities;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Text;

namespace UMGI.GRCS.UI.Controllers.Report
{
    public partial class ReportController : BaseController
    {
        /// <summary>
        /// Displays initial view of report screen
        /// </summary>
        /// <returns></returns>
        public ActionResult RightsExpiryToBeDetermined()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
                if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.SearchContract))
                {
                    RedirectToUnAuthorizedPage();
                }

                RightsExpiryToBeDeterminedModel rightsExpiry = new RightsExpiryToBeDeterminedModel();

                return View(ModelConstant.Constants.RightsExpiryToBeDetermined, rightsExpiry);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        /// <summary>
        /// Default binding for the Rights Expiry To Be Determined report 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="AdminCompany"></param>
        /// <param name="model"></param>
        /// <param name="generateReport"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RightsExpiryToBeDetermined(PagingParams args, string AdminCompany, RightsExpiryToBeDeterminedModel model, string generateReport)
        {
            try
            {
                long resultCount = ModelConstant.Constants.ValueZero;
                RightsExpiryToBeDeterminedReportFilter filters;
                if (generateReport == ModelConstant.Constants.FlagTrue && (string.IsNullOrEmpty(AdminCompany) && string.IsNullOrEmpty(model.ClearanceCompanyId)) && String.IsNullOrEmpty(model.ExportType))
                {
                    AdminCompany = GetUserCompanyList(GrsTasks.SearchContract);
                    //var comp = SessionWrapper.CurrentPermissions.GetCompanyIdsByTask(GrsTasks.SearchContract);
                    //AdminCompany = string.Join(", ", comp.Select(pair => string.Format("{0}", pair.Key)));
                    model.ClearanceCompanyId = AdminCompany;
                }
                else
                {
                    AdminCompany = AdminCompany ?? model.ClearanceCompanyId;
                }
                filters = new RightsExpiryToBeDeterminedReportFilter
                {
                    ClearanceCompanyId = AdminCompany,
                    StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                    PageSize = args.PageSize,
                    SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[ModelConstant.Constants.ValueZero].ColumnName,
                    IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[ModelConstant.Constants.ValueZero].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                    ClearanceCompanyName = model.ClearanceCompanyName,
                    ExportType = model.ExportType
                };
                if (!String.IsNullOrEmpty(model.ExportType))
                {
                    filters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                    filters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                    ExportReport_RightsExpiryToBeDetermined(ModelConstant.Constants.RightsExpiryToBeDetermined, filters);
                    return View(ModelConstant.Constants.RightsExpiryToBeDetermined, model);
                }

                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", AdminCompany, SessionWrapper.CurrentUserInfo.UserLoginName, args.StartIndex, args.PageSize));

               
                if (!string.IsNullOrEmpty(AdminCompany))
                {
          
                    model.RightsExpiryToBeDeterminedContracts = this.GetData(args, filters);
                    if (model.RightsExpiryToBeDeterminedContracts.Count > ModelConstant.Constants.ValueZero)
                    {
                        resultCount = Convert.ToInt32(model.RightsExpiryToBeDeterminedContracts[0].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                   }
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.RightsExpiryToBeDeterminedContracts.GridJSONActions<RightsExpiryToBeDetermined>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<IRightsExpiryToBeDeterminedModel> GetData(PagingParams args, RightsExpiryToBeDeterminedReportFilter parameters)
        {
            int pagesize = args.PageSize == ModelConstant.Constants.ValueZero ? 25 : Convert.ToInt32(args.PageSize);
            List<IRightsExpiryToBeDeterminedModel> model = new List<IRightsExpiryToBeDeterminedModel>();
            model = _reportRepository.GetRightsExpiryToBeDeterminedDetails(parameters);
            return model;
        }

        #region Export Data

        /// <summary>
        /// Method to export report for Rights Expiry To Be Determined
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_RightsExpiryToBeDetermined(string reportName, RightsExpiryToBeDeterminedReportFilter filters)
        {
            try
            {
                String dtE = DateTime.Now.ToString(ModelConstant.Constants.ReportDBdateFormat);
                string format = filters.ExportType; //Desired format goes here (PDF, Excel, or Image) 
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", filters.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, filters.StartIndex, filters.PageSize));
                if (String.IsNullOrEmpty(filters.ClearanceCompanyId))
                {
                    filters.ClearanceCompanyId = filters.ClearanceCompanyName = filters.SensitiveCompanyId = String.Empty;
                    //var list = new List<ClearanceAdminCompany>();
                    //list = _globalRepository.AutoSearchClearanceCompCountry(String.Empty, GrsTasks.SearchContract);
                    var list = _reportRepository.GetClearanceAdminCompanies(GrsTasks.SearchContract);
                    StringBuilder CLearanceCompanyID = new StringBuilder(); // AutoSearchClearanceCompCountry Company ID
                    StringBuilder CLearanceCompanyName = new StringBuilder(); // AutoSearchClearanceCompCountry Company Name
                    foreach (BusinessEntities.Entities.BaseEntities.ClearanceAdminCompany comp in list)
                    {
                        CLearanceCompanyID.AppendFormat("{0} ,", comp.Id);
                        CLearanceCompanyName.AppendFormat("{0}  [  {1} ] , ", comp.Name, comp.CountryName);
                    }
                 

                    filters.ClearanceCompanyId = CLearanceCompanyID.ToString();
                    filters.ClearanceCompanyName = CLearanceCompanyName.ToString();
                    filters.ClearanceCompanyId = filters.ClearanceCompanyId.Substring(ModelConstant.Constants.ValueZero, filters.ClearanceCompanyId.Length - ModelConstant.Constants.ValueOne);
                    filters.ClearanceCompanyName = filters.ClearanceCompanyName.Substring(ModelConstant.Constants.ValueZero, filters.ClearanceCompanyName.Length - ModelConstant.Constants.ValueTwo);

                    if (filters.ClearanceCompanyName == null)
                        filters.ClearanceCompanyName = String.Empty;
                }

                var userName = SessionWrapper.CurrentPermissions.ApplicationUser.FirstName + " " + SessionWrapper.CurrentPermissions.ApplicationUser.LastName;
                Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();
                rview.ServerReport.ReportServerUrl = new Uri(_configurationFactory.ReportServerUrl);
                rview.ServerReport.ReportServerCredentials = (IReportServerCredentials)new CustomReportCredentials(_configurationFactory.ReportUserName, _configurationFactory.ReportUserPassword, _configurationFactory.ReportUserDomain);
                System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("NumberOfRows", ModelConstant.Constants.Pagesize));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClearanceCo", filters.ClearanceCompanyId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyList", filters.ClearanceCompanyName));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Username", userName));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + (format == ModelConstant.Constants.ReportFormatExcel ? ModelConstant.Constants.UrlRightsExpiryToBeDetemineExcel : ModelConstant.Constants.UrlRightsExpiryToBeDetermined);
                rview.ServerReport.SetParameters(paramList);
                string mimeType, encoding, extension;
                string[] streamids;
                Microsoft.Reporting.WebForms.Warning[] warnings;

                byte[] bytes = rview.ServerReport.Render(format, ModelConstant.Constants.deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                Response.Clear();
                if (format == ModelConstant.Constants.ReportFormatPDF)
                { 
                    Response.AddHeader(ModelConstant.Constants.ReportHeaderName, ModelConstant.Constants.ReportHeaderValuePDF);
                }
                else if (format == ModelConstant.Constants.ReportFormatExcel)
                {
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
