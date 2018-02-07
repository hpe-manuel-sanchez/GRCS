/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : GlobalController.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 27-10-2012 
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Syncfusion.XlsIO;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Models;
using UMGI.GRCS.UI.Utilities;
using BLConstant = UMGI.GRCS.BusinessEntities.Constants.Constants;
using Constants = UMGI.GRCS.UI.Models.Constants;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    public partial class GlobalController : BaseController
    {
        #region Private Variable

        private IGlobalRepository _globalRepository;

        #endregion

        public GlobalController(IGlobalRepository globalRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _globalRepository = globalRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
        }

        /// <summary>
        /// For AutoSearching clearance comp country.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public JsonResult AutoSearchClearanceCompCountry(string term)
        {
            try
            {
                //term = term.Split(';').Last().Trim();
                LoggerFactory.LogWriter.Debug(string.Format(Constants.MethodStart + "term:{1}", "AutoSearchClearanceCompCountry", term));

                var list = new List<AutoSuggestionEntity>();
                if (term != string.Empty)
                {
                    list =
                        _globalRepository.AutoSearchClearanceCompCountry(term).Select(
                            item =>
                            new AutoSuggestionEntity { id = Convert.ToInt32(item.Id), value = item.Name, label = item.Name }).OrderBy(ordCompCountry => ordCompCountry.label).ToList();
                }

                LoggerFactory.LogWriter.Debug(string.Format(Constants.MethodEnd, "AutoSearchClearanceCompCountry"));
                if (list.Count > 200)
                    return Json(list.Take(200).ToList(), JsonRequestBehavior.AllowGet);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "AutoSearchClearanceCompCountry"), ex);
                throw;
            }
        }

        public ActionResult ClearanceAdminCompany(string clearanceIds, string clearanceNames)
        {

            var model = new PriorityWorkQueueModel();

            if (clearanceIds != null && clearanceNames != null)
            {
                var ids = clearanceIds.Split(',');
                var names = clearanceNames.Split(',');
                if (ids.Length > 0)
                {
                    for (int index = 0; index < ids.Length; index++)
                    {
                        model.ClearanceAdminList.Add(new SelectListItem { Text = names[index], Value = ids[index] });
                    }
                }
            }

            //model.ClearanceAdminList.Add(new SelectListItem { Text = "asdfd", Value = "Asdf" });
            //model.ClearanceAdminList.Add(new SelectListItem { Text = "asdfd", Value = "asdf" });

            return PartialView(model);
        }

    }

    /// <summary>
    /// Audit Trail
    /// </summary>
    public partial class GlobalController
    {
        [HttpGet]
        public ActionResult GetAuditTrailFilters(AuditObjectType auditObjectType)
        {
            try
            {
                var auditTrailFilters = _globalRepository.GetAuditTrailFilters(auditObjectType);

                return View("AuditTrail", auditTrailFilters);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);

                return Json(new { Error = true, Message = "Error loading Audit Trail Filters." });
            }
        }

        [HttpGet]
        public ActionResult GetWGAuditTrailFilters(AuditObjectType auditObjectType, string selectedWorkgroupRole, bool isParent)
        {
            try
            {
                var auditTrailFilters = _globalRepository.GetWGAuditTrailFilters(auditObjectType, selectedWorkgroupRole, isParent);

                return View("AuditTrail", auditTrailFilters);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);

                return Json(new { Error = true, Message = "Error loading Audit Trail Filters." });
            }
        }

        [HttpPost]
        public ActionResult GetAuditTrail(AuditObjectType auditObjectType, List<long> selectedAuditConfiguration, List<long> selectedItemId)
        {
            try
            {
                var auditTrail = _globalRepository.GetAuditTrail(auditObjectType, selectedAuditConfiguration, selectedItemId);

                ViewBag.SelectedAuditConfiguration = selectedAuditConfiguration;

                return PartialView("AuditTrailResult", auditTrail);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, MethodBase.GetCurrentMethod().Name, ex);

                return Json(new { Error = true, Message = "Error loading Audit Trail." });
            }
        }

        [HttpPost]
        public ActionResult ExportToExcel(AuditObjectType auditObjectType, string selectedAuditConfiguration, string selectedItemId, string displayTitle)
        {
            var selectedAuditConfigurationList = selectedAuditConfiguration.Split(BLConstant.StringDelimiter.ToCharArray()).Select(long.Parse).ToList();

            var selectedItemIdList = selectedItemId.Split(BLConstant.StringDelimiter.ToCharArray()).Select(long.Parse).ToList();

            var auditTrail = _globalRepository.GetAuditTrail(auditObjectType, selectedAuditConfigurationList, selectedItemIdList);

            using (var excelEngine = new ExcelEngine())
            {
                var excel = excelEngine.Excel;

                var workbook = excel.Workbooks.Create(1);
                workbook.StandardFontSize = 8;

                var workbookProperties = workbook.BuiltInDocumentProperties;
                workbookProperties.Author = "UMG";
                workbookProperties.Company = "UMG";
                workbookProperties.Title = "Audit Trail";
                workbookProperties.Subject = "Audit Trail";

                var worksheetTitleStyle = workbook.Styles.Add("WorksheetTitleStyle");
                worksheetTitleStyle.BeginUpdate();
                workbook.SetPaletteColor(8, Color.FromArgb(255, 170, 170, 170));
                worksheetTitleStyle.Color = Color.FromArgb(255, 170, 170, 170);
                worksheetTitleStyle.Font.Bold = true;
                worksheetTitleStyle.Font.Size = 16;
                worksheetTitleStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                worksheetTitleStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                worksheetTitleStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheetTitleStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheetTitleStyle.EndUpdate();

                var tableTitleStyle = workbook.Styles.Add("TableTitleStyle");
                tableTitleStyle.BeginUpdate();
                workbook.SetPaletteColor(9, Color.FromArgb(255, 72, 84, 114));
                tableTitleStyle.Color = Color.FromArgb(255, 72, 84, 114);
                tableTitleStyle.Font.Bold = true;
                tableTitleStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                tableTitleStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                tableTitleStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                tableTitleStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                tableTitleStyle.EndUpdate();

                var tableHeaderStyle = workbook.Styles.Add("TableHeaderStyle");
                tableHeaderStyle.BeginUpdate();
                workbook.SetPaletteColor(10, Color.FromArgb(255, 207, 207, 207));
                tableHeaderStyle.Color = Color.FromArgb(255, 207, 207, 207);
                tableHeaderStyle.Font.Bold = true;
                tableHeaderStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                tableHeaderStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                tableHeaderStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                tableHeaderStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                tableHeaderStyle.EndUpdate();

                var worksheet = workbook.Worksheets.Create("Audit Trail");

                workbook.Worksheets.Remove(0);

                var startRowIndex = 4;

                var maxColumnCount = 0;

                foreach (DataTable dataTable in auditTrail.Tables)
                {
                    var columnCount = dataTable.Columns.Count;

                    if (columnCount > maxColumnCount) { maxColumnCount = columnCount; }

                    var tableTitle = worksheet.Range[startRowIndex, 1, startRowIndex, columnCount];
                    tableTitle.Merge();
                    tableTitle.Text = dataTable.ExtendedProperties["ATTRIBUTE_NAME"].ToString();
                    tableTitle.CellStyleName = "TableTitleStyle";
                    tableTitle.CellStyle.Font.RGBColor = Color.FromArgb(255, 255, 255, 255);
                    tableTitle.RowHeight = 15;

                    worksheet.ImportDataTable(dataTable, true, startRowIndex + 1, 1, -1, -1, true);
                    worksheet.Range[startRowIndex + 1, 1, startRowIndex + 1, columnCount].CellStyleName = "TableHeaderStyle";

                    startRowIndex = worksheet.UsedRange.LastRow + 4;
                }

                worksheet.UsedRange.AutofitRows();
                worksheet.UsedRange.ColumnWidth = 25;

                var worksheetTitle = worksheet.Range[1, 1, 1, maxColumnCount];
                worksheetTitle.Merge();
                worksheetTitle.Text = displayTitle;
                worksheetTitle.CellStyleName = "WorksheetTitleStyle";
                worksheetTitle.RowHeight = 20;

                workbook.SaveAs(string.Format("{0} - {1}.{2}", "Audit Trail", displayTitle, "xls"), ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog);

                return null;
            }
        }
    }
}