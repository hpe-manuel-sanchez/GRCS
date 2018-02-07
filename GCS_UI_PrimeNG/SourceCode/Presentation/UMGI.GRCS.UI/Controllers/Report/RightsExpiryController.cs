
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsExpiryController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 10-2-2013 
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
using UMGI.GRCS.UI.Extensions;
using ModelConstant = UMGI.GRCS.UI.Models;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.UI.Utilities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Text;
using System.Globalization;

namespace UMGI.GRCS.UI.Controllers.Report
{
    public partial class ReportController : BaseController
    {
        #region Action Methods

        /// <summary>
        /// Displays initial view of report screen
        /// </summary>
        /// <returns></returns>
        public ActionResult RightsExpiry()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
                if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.SearchContract))
                {
                    RedirectToUnAuthorizedPage();
                }

                RightsExpiryModel rightsExpiry = new RightsExpiryModel { LostRightDateList = _reportRepository.GetLostRightDateList() };


                return View(ModelConstant.Constants.RightsExpiry, rightsExpiry);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Default binding for the Rights Expiry report 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="AdminCompany"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RightsExpiry(PagingParams args, string AdminCompany, RightsExpiryModel model, string generateReport, FormCollection formCollectionValues)
        {
            try
            {
                long resultCount = 0;
                if (generateReport == ModelConstant.Constants.FlagTrue && (string.IsNullOrEmpty(AdminCompany) && string.IsNullOrEmpty(model.ClearanceCompanyId)) && String.IsNullOrEmpty(model.ExportType))
                {
                    AdminCompany = GetUserCompanyList(GrsTasks.SearchContract);                   
                    model.ClearanceCompanyId = AdminCompany;
                }
                else
                {
                    //export
                    AdminCompany = AdminCompany ?? model.ClearanceCompanyId;
                }
                /********************** Export Report***********************************/
                RightsExpiryReportFilters parameters = new RightsExpiryReportFilters
                {
                    ClearanceCompanyId = AdminCompany,
                    StartIndex = args.StartIndex < 0 ? 0 : args.StartIndex,
                    PageSize = args.PageSize,
                    SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[0].ColumnName,
                    IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[ModelConstant.Constants.ValueZero].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                    ClearanceCompanyName = model.ClearanceCompanyName,
                    LostRightsMonths = model.LostRightDate,
                    ExportType = model.ExportType,
                    SensitiveCompanyId = GetUserCompanyList(GrsTasks.ViewContractSenstiveData)
                };
                if (parameters.LostRightsMonths != 0)
                {
                    parameters.StartDate = DateTime.Now.ToString(ModelConstant.Constants.ReportdateFormat);
                    parameters.EndDate = DateTime.Now.AddMonths(parameters.LostRightsMonths).ToString(ModelConstant.Constants.ReportdateFormat);
                }
                else
                {                    
                    parameters.StartDate = model.StartDate.ToString(ModelConstant.Constants.ReportdateFormat);
                    parameters.EndDate = model.EndDate.ToString(ModelConstant.Constants.ReportdateFormat);
                   
                }
                if (!String.IsNullOrEmpty(model.ExportType))
                {
                    parameters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                    parameters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                    ExportReport_RightsExpiry(ModelConstant.Constants.RightsExpiry, parameters);
                    return View(ModelConstant.Constants.RightsExpiry, model);
                }

                /********************** End Export Report***********************************/

                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3},Sorting:{4}", AdminCompany, SessionWrapper.CurrentUserInfo.UserLoginName, 1, 1, null));


                if (!string.IsNullOrEmpty(AdminCompany))
                {

                    model.RightsExpiryContracts = this.GetData(args, parameters);
                    if (model.RightsExpiryContracts.Count > 0)
                    {
                        resultCount = Convert.ToInt32(model.RightsExpiryContracts[ModelConstant.Constants.ValueZero].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }

                }
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.RightsExpiryContracts.GridJSONActions<RightsExpiry>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<IRightsExpiryModel> GetData(PagingParams args, RightsExpiryReportFilters parameters)
        {
            int pagesize = args.PageSize == 0 ? 25 : Convert.ToInt32(args.PageSize);
            List<IRightsExpiryModel> model = new List<IRightsExpiryModel>();
            model = _reportRepository.GetRightsExpiryDetails(parameters);
            return model;
        }

        #region Export Data

        /// <summary>
        /// Method to export report for Rights Expiry
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_RightsExpiry(string reportName, RightsExpiryReportFilters filters)
        {
            try
            {
                String dtF, dtE = DateTime.Now.ToString(ModelConstant.Constants.ReportDBdateFormat);
                string format = filters.ExportType; //Desired format goes here (PDF, Excel, or Image) 
                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", filters.ClearanceCompanyId, SessionWrapper.CurrentUserInfo.UserLoginName, filters.StartIndex, filters.PageSize));
                if (String.IsNullOrEmpty(filters.ClearanceCompanyId))
                {
                    filters.ClearanceCompanyId = filters.ClearanceCompanyName = filters.SensitiveCompanyId = String.Empty;                   
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

                dtF = filters.StartDate;
                dtE = filters.EndDate;

                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("NumberOfRows", ModelConstant.Constants.Pagesize));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClearanceCo", filters.ClearanceCompanyId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyList", filters.ClearanceCompanyName));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("StartDate", dtF));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("EndDate", dtE));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Username", userName));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SensitiveCompanyId", filters.SensitiveCompanyId));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + (format == ModelConstant.Constants.ReportFormatExcel ? ModelConstant.Constants.UrlRightsExpiryExcel : ModelConstant.Constants.UrlRightsExpiry);

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

        #endregion

    }
}
