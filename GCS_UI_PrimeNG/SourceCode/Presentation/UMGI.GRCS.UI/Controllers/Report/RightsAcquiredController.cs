/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsAcquiredController.cs
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
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.UI.Interfaces.Report;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models.Report;
using UMGI.GRCS.BusinessEntities.Constants;
using UMGI.GRCS.UI.Extensions;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.UI.Utilities;
using Syncfusion.Mvc.Grid;
using Syncfusion.Mvc.Shared;
using UMGI.GRCS.BusinessEntities.Entities.ReportEntities;
using ModelConstant = UMGI.GRCS.UI.Models;
using UMGI.GRCS.Utilities.logger;
using Microsoft.Reporting.WebForms;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.UI.Controllers.Report
{
    public partial class ReportController : BaseController
    {
        /// <summary>
        /// Displays initial view of report screen
        /// </summary>
        /// <returns></returns>
        public ActionResult RightsAcquired()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
                if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.ViewRepertoireRights))
                {
                    RedirectToUnAuthorizedPage();
                }
                RightsAcquiredModel rightsAcquired = new RightsAcquiredModel();
                var digitalEx = _reportRepository.getDigitalExpoliatation();

                digitalEx.UseTypes.Insert(0, new StringIdentifier { Value = 0, Description = ModelConstant.Constants.ALL });

                rightsAcquired.UseTypeList = digitalEx.UseTypes.Select(restricted =>
                            new SelectListItem { Text = restricted.Description, Value = restricted.Value.ToString(CultureInfo.InvariantCulture) });
                digitalEx.ContentTypes.Insert(0, new StringIdentifier { Value = 0, Description = ModelConstant.Constants.ALL });

                rightsAcquired.ContentTypeList = digitalEx.ContentTypes.Select(restricted =>
                            new SelectListItem { Text = restricted.Description, Value = restricted.Value.ToString(CultureInfo.InvariantCulture) });
                rightsAcquired.UseTypeList = _reportRepository.UseTypeList();
                rightsAcquired.CommercialModelsList = digitalEx.CommercialModelTypes.Select(restricted =>
                            new SelectListItem { Text = restricted.Description, Value = restricted.Value.ToString(CultureInfo.InvariantCulture) });
               
                return View(ModelConstant.Constants.RightsAcquired, rightsAcquired);
               
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
        /// <param name="args">Paging Parameters</param>
        /// <param name="AdminCompany"></param>
        /// <param name="model">Rights Acquired Model</param>
        /// <param name="WorkFlowStatus">The Work Flow Status</param>
        /// <param name="LinkedRepertoire">The Flag Linked-Repertoire</param>
        /// <param name="generateReport">Flag to generate Report</param>
        /// <param name="artist">The Artist</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RightsAcquired(PagingParams args, RightsAcquiredModel model, string generateReport)
        {
            try
            {
                long resultCount = ModelConstant.Constants.ValueZero;
                //RequestType currentRequest = (RequestType)Convert.ToInt32(args.RequestType);
                RightsAcquiredReportFilter filters;
                StringBuilder ExploationRights = new StringBuilder();
                ExploationRights.AppendFormat(" {0} , {1} , {2}", model.ContentType, model.USEType,model.ComercialModel);
                //model.Repertoire = new List<IRightsAcquiredModel>();
                filters = new RightsAcquiredReportFilter
                {
                    ClearanceCompanyId = model.ClearanceCompanyId,
                    CountriesId = model.ExploitationCountryId,
                    StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                    PageSize = args.PageSize,
                    SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[0].ColumnName,
                    IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[0].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                    ExportType = model.ExportType,
                    ArtistId = model.Artist,
                    RepertoireType = (model.RepertoireType == ModelConstant.Constants.FlagTrue || model.RepertoireType == "1") ? ModelConstant.Constants.RepertoireRelease : ModelConstant.Constants.RepertoireResource,
                    PhysicalExploitationRights = model.ExploitationRights,
                    DigitalExploitationRestrictions = model.DigitalExploitationrights == null ? model.DigitalExploitationrights : ExploationRights.ToString()

                };
                if (!String.IsNullOrEmpty(model.ExportType))
                {
                    filters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                    filters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                    ExportReport_RightsAcquired(ModelConstant.Constants.RightsAcquired, filters);
                    return View(ModelConstant.Constants.RightsAcquired, model);
                }

                LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", model.ClearanceAdminCompany, SessionWrapper.CurrentUserInfo.UserLoginName, args.StartIndex, args.PageSize));


                if (generateReport == ModelConstant.Constants.FlagTrue)
                {
                    model.Repertoire = this.GetData(args, filters);
                    if (model.Repertoire.Count > ModelConstant.Constants.ValueZero)
                    {
                        resultCount = Convert.ToInt32(model.Repertoire[0].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                 }
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                return model.Repertoire.GridJSONActions<RightsAcquired>(resultCount);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        private List<IRightsAcquiredModel> GetData(PagingParams args, RightsAcquiredReportFilter parameters)
        {
            int pagesize = args.PageSize == ModelConstant.Constants.ValueZero ? 25 : Convert.ToInt32(args.PageSize);
            List<IRightsAcquiredModel> model = new List<IRightsAcquiredModel>();
            model = _reportRepository.GetRightsAcquiredDetails(parameters);
            return model;
        }

        #region Export Data

        /// <summary>
        /// Method to export report for Rights Acquired
        /// </summary>
        /// <param name="reportName">name of report</param>
        /// <param name="filters">filters to get report</param>
        public void ExportReport_RightsAcquired(string reportName, RightsAcquiredReportFilter filters)
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
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Username", userName));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("RepertoireType", filters.RepertoireType));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Artist", filters.ArtistId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Countries", filters.CountriesId));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExploitationRight", filters.PhysicalExploitationRights));
                paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("DigitalExpRestriction", filters.DigitalExploitationRestrictions));
                rview.ServerReport.ReportPath = _configurationFactory.ReportPath + ModelConstant.Constants.UrlRightsAquired;
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
