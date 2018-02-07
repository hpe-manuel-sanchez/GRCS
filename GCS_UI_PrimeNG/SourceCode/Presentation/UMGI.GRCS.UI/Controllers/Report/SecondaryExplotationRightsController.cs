
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : SecondaryExplotationRightsController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Team
 * Created on     : 17-4-2013 
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
using System.Collections;
using Microsoft.Reporting.WebForms;
using UMGI.GRCS.UI.Utilities;

namespace UMGI.GRCS.UI.Controllers.Report
{
    public partial class ReportController : BaseController
    {
        #region Action Methods

        /// <summary>
        /// Displays initial view of report screen
        /// </summary>
        /// <returns></returns>
        public ActionResult SecondaryExploitationRights()
        {
            try
            {
                LoggerFactory.LogWriter.Debug(UMGI.GRCS.BusinessEntities.Constants.Constants.MethodStart);
                if (!SessionWrapper.CurrentPermissions.HasAnyPermission(GrsTasks.ViewRepertoireRights))
                {
                    RedirectToUnAuthorizedPage();
                }

                SecondaryExploitationRightsModel SecondaryExplotationRights = new SecondaryExploitationRightsModel { };
               
                SecondaryExplotationRights.SecondaryExploitationTypeList = _reportRepository.GetSecondaryExploitationType();
                return View(ModelConstant.Constants.SecondaryExploitationRights, SecondaryExplotationRights);
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
       [HttpPost]
        public ActionResult SecondaryExploitationRights(PagingParams args, string Artist, SecondaryExploitationRightsModel model, string generateReport)
        {
          
            try
            {
               
                long resultCount = ModelConstant.Constants.ValueZero;
                if (model.SecondaryExploitationType == null)
                {
                   return model.Repertoire.GridJSONActions<SecondaryExploitationRights>(resultCount);
                }
                else
                {
                    model.Repertoire = new List<ISecondaryExploitationRightsModel>();
                    
                    if (model.Artist == "0")
                    {
                        model.Artist = null;
                    }
                                   
                    if (model.Artist == null)
                    {
                        if (model.ArtistSearch.Info != null)
                        {
                            model.Artist = model.ArtistSearch.Info.Id.ToString();
                        }
                    }

                    /********************** Export Report***********************************/
                    SecondaryExploitationRightsReportFilter parameters = new SecondaryExploitationRightsReportFilter
                    { 
                        ArtistId = model.Artist,
                        SecondaryExploationType = model.SecondaryExploitationType,
                        CountriesId = model.CountryId,
                        StartIndex = args.StartIndex < ModelConstant.Constants.ValueZero ? ModelConstant.Constants.ValueZero : args.StartIndex,
                        PageSize = args.PageSize,
                        SortField = args.SortDescriptors == null ? ModelConstant.Constants.DefaultSortColumn : args.SortDescriptors[0].ColumnName,
                        IsAscendingOrder = args.SortDescriptors == null || args.SortDescriptors[0].SortDirection.ToString() == ModelConstant.Constants.AscendingValue,
                        ExportType = model.ExportType,
                     };
                    if (!String.IsNullOrEmpty(model.ExportType))
                    {
                        parameters.SortField = model.SortField ?? ModelConstant.Constants.DefaultSortColumn;
                        parameters.IsAscendingOrder = String.IsNullOrEmpty(model.IsAscending) || model.IsAscending == ModelConstant.Constants.AscendingValue;
                        ExportReport_SecondaryExploitationRights(ModelConstant.Constants.SecondaryExploitationRights, parameters,resultCount.ToString());
                        return View(ModelConstant.Constants.SecondaryExploitationRights, model);
                    }

                    ///********************** End Export Report***********************************/

                    LoggerFactory.LogWriter.Debug(string.Format("Artist:{0},UserName:{1},StartIndex:{2},PageSize:{3},Sorting:{4}", Artist, SessionWrapper.CurrentUserInfo.UserLoginName, 1, 1, null));
                    model.Repertoire = this.GetData(args, parameters);
                    if (model.Repertoire.Count > ModelConstant.Constants.ValueZero)
                    {
                        resultCount = Convert.ToInt32(model.Repertoire[0].Total);
                        args.PageSize = Convert.ToInt32(resultCount);
                    }
                    //else
                    //{
                    //    resultCount = 0;
                    //}
                    
                    LoggerFactory.LogWriter.Debug(UMGI.GRCS.UI.Models.Constants.MethodEnd);
                    return model.Repertoire.GridJSONActions<SecondaryExploitationRights>(resultCount);
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Method for pagination
        /// </summary>
        /// <param name="args"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
       private List<ISecondaryExploitationRightsModel> GetData(PagingParams args, SecondaryExploitationRightsReportFilter parameters)
       {
           int pagesize = args.PageSize == ModelConstant.Constants.ValueZero ? 25 : Convert.ToInt32(args.PageSize);
           List<ISecondaryExploitationRightsModel> model = new List<ISecondaryExploitationRightsModel>();
           model = _reportRepository.GetSecondaryExploitationRightsDetails(parameters);
          
           return model;
       }

       /// <summary>
       /// Method to export report 
       /// </summary>
       /// <param name="reportName">name of report</param>
       /// <param name="filters">filters to get report</param>
       public void ExportReport_SecondaryExploitationRights(string reportName, SecondaryExploitationRightsReportFilter filters, string resultCount )
       {
           try
           {
               String dtE = DateTime.Now.ToString(ModelConstant.Constants.ReportDBdateFormat);
               string format = filters.ExportType; //Desired format goes here (PDF, Excel, or Image) 
               LoggerFactory.LogWriter.Debug(string.Format("ClearanceAdminCompany:{0},UserName:{1},StartIndex:{2},PageSize:{3}", filters.ArtistId, SessionWrapper.CurrentUserInfo.UserLoginName, filters.StartIndex, filters.PageSize));
               var userName = SessionWrapper.CurrentPermissions.ApplicationUser.FirstName + String.Empty + SessionWrapper.CurrentPermissions.ApplicationUser.LastName;
               Microsoft.Reporting.WebForms.ReportViewer rview = new Microsoft.Reporting.WebForms.ReportViewer();
               rview.ServerReport.ReportServerUrl = new Uri(_configurationFactory.ReportServerUrl);
               IReportServerCredentials irsc = new CustomReportCredentials(_configurationFactory.ReportUserName, _configurationFactory.ReportUserPassword, _configurationFactory.ReportUserDomain);
               rview.ServerReport.ReportServerCredentials = (IReportServerCredentials)irsc;
               System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> paramList = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();
               paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("IsAscendingOrder", filters.IsAscendingOrder == true ? ModelConstant.Constants.FlagTrue : ModelConstant.Constants.FlagFalse));
               paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Artist", filters.ArtistId));
               paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("Countries", filters.CountriesId));
               paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("SortField", filters.SortField));
               paramList.Add(new Microsoft.Reporting.WebForms.ReportParameter("ExploitationType", filters.SecondaryExploationType));
               rview.ServerReport.ReportPath = _configurationFactory.ReportPath + ModelConstant.Constants.UrlSecondaryExploitationRights;
               rview.ServerReport.SetParameters(paramList);
              string mimeType, encoding, extension;
               string[] streamids;
               Microsoft.Reporting.WebForms.Warning[] warnings;

            //   deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" + "</DeviceInfo>";

               byte[] bytes = rview.ServerReport.Render(format, ModelConstant.Constants.deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
               Response.Clear();
               if (format == ModelConstant.Constants.ReportFormatPDF)
               {
                   Response.ContentType = ModelConstant.Constants.ContentTypePDF;
                   Response.AddHeader(ModelConstant.Constants.ReportHeaderName, ModelConstant.Constants.ReportHeaderValuePDF);
               }
               else if (format == ModelConstant.Constants.ReportFormatExcel)
               {
                   Response.ContentType = ModelConstant.Constants.ContentTypeExcel;
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
