/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ReleaseCommitmentController.cs
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Syncfusion.Mvc.Grid;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.UI.Models.Report;
using UMGI.GRCS.UI.Utilities;
using UMGI.GRCS.Utilities.logger;
using ModelConstant = UMGI.GRCS.UI.Models;
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
        public ActionResult ReleaseCommitment()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
                if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.SearchContract))
                {
                    RedirectToUnAuthorizedPage();
                }

                ReleaseCommitmentModel releaseCommitment = new ReleaseCommitmentModel();
                releaseCommitment.WorkflowStatuses = _reportRepository.GetWorkflowStatuses();
                releaseCommitment.LinkedRepertoire = true;
                return View(ModelConstant.Constants.ReleaseCommitment, releaseCommitment);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }
        /// <summary>
        /// Action method invoked on submit
        /// </summary>
        /// <param name="args"></param>
        /// <param name="AdminCompany"></param>
        /// <param name="model"></param>
        /// <param name="WorkFlowStatus"></param>
        /// <param name="LinkedRepertoire"></param>
        /// <param name="generateReport"></param>
        /// <param name="artist"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReleaseCommitment(PagingParams args, string AdminCompany, ReleaseCommitmentModel model, string WorkFlowStatus, string LinkedRepertoire, string generateReport, string artist)
        {
            try
            {
                long resultCount = 0;
                ReleaseCommitmentReportFilter filters;
                if (generateReport == ModelConstant.Constants.FlagTrue && (string.IsNullOrEmpty(AdminCompany) && string.IsNullOrEmpty(model.ClearanceCompanyId)) && String.IsNullOrEmpty(model.ExportType))
                {
                    //var comp = SessionWrapper.CurrentPermissions.GetCompanyIdsByTask(GrsTasks.SearchContract);
                    //AdminCompany = string.Join(", ", comp.Select(pair => string.Format("{0}", pair.Key)));
                    AdminCompany = GetUserCompanyList(GrsTasks.SearchContract);
                    model.ClearanceCompanyId = AdminCompany;
                }
                else
                {
                    AdminCompany =  AdminCompany ?? model.ClearanceCompanyId;
                }
                
                filters = new ReleaseCommitmentReportFilter
                {
                    ClearanceCompanyId = AdminCompany,
                    WorkFlowStatus = String.IsNullOrEmpty(WorkFlowStatus) ? ModelConstant.Constants.workflow : WorkFlowStatus,
                    RepertoireLinked = String.IsNullOrEmpty(LinkedRepertoire) || LinkedRepertoire == ModelConstant.Constants.TrueFlag || LinkedRepertoire == ModelConstant.Constants.FlagTrue,
                    StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                    PageSize = args.PageSize,
                    SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[ModelConstant.Constants.ValueZero].ColumnName,
                    IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[ModelConstant.Constants.ValueZero].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                    ClearanceCompanyName = model.ClearanceCompanyName,
                    ExportType = model.ExportType,
                    ArtistId = model.ArtistSearch.Info != null ? model.ArtistSearch.Info.Id.ToString() : artist,
                    ArtistName = model.ArtistSearch.Info != null ? model.ArtistSearch.Info.Name : null
                };
                if (!String.IsNullOrEmpty(model.ExportType))
                {
                    filters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                    filters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                    ExportReport_ReleaseCommitment(ModelConstant.Constants.ReleaseCommitment, filters);
                    return View(ModelConstant.Constants.ReleaseCommitment, model);
                }

                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", AdminCompany, SessionWrapper.CurrentUserInfo.UserLoginName, args.StartIndex, args.PageSize));


                if (!string.IsNullOrEmpty(AdminCompany))
                {
                    model.ReleaseCommitmentModelContracts = this.GetData(args, filters);
                    if (model.ReleaseCommitmentModelContracts.Count > 0)
                    {
                        resultCount = Convert.ToInt32(model.ReleaseCommitmentModelContracts[0].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                }
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.ReleaseCommitmentModelContracts.GridJSONActions<ActiveRoster>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<IReleaseCommitmentModel> GetData(PagingParams args, ReleaseCommitmentReportFilter parameters)
        {
            int pagesize = args.PageSize == 0 ? 25 : Convert.ToInt32(args.PageSize);
            List<IReleaseCommitmentModel> model = new List<IReleaseCommitmentModel>();
            model = _reportRepository.GetReleaseCommitmentDetails(parameters);
            return model;
        }

        #region EXPORT DATA 
        /// <summary>
        /// Method to export report for Release Commitment
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_ReleaseCommitment(string reportName, ReleaseCommitmentReportFilter filters)
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
                    var list = _reportRepository.GetClearanceAdminCompanies(GrsTasks.SearchContract);
                    //list = _globalRepository.AutoSearchClearanceCompCountry(String.Empty, GrsTasks.SearchContract);
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
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ClearanceCo", filters.ClearanceCompanyId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("CompanyList", filters.ClearanceCompanyName));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("LinkedRepetoire", filters.RepertoireLinked ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Artist", filters.ArtistId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("WorFlowStatus", filters.WorkFlowStatus ?? ModelConstant.Constants.workflow));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Username", userName));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + ModelConstant.Constants.UrlReleaseCommitment;
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
