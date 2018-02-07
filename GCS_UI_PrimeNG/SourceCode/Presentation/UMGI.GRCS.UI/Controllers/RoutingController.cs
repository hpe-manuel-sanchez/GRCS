using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.UI.Utilities;
using Syncfusion.Mvc.Grid;
using Syncfusion.XlsIO;
using System.Data;
using System.Drawing;
using ConstantsEntities = UMGI.GRCS.BusinessEntities.Constants.Constants;
using UMGI.GRCS.Core.Utilities.logger;

namespace UMGI.GRCS.UI.Controllers
{
    public class RoutingController : BaseController
    {
        #region Variable Declarartions

        IRoutingRepository _routingRepository;
		
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        public RoutingController(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutingController"/> class.
        /// </summary>
        /// <param name="routingRepository">The routingRepository</param>
        /// <param name="sessionWrapper">The sessionWrapper</param>
        /// <param name="logFactory">The logFactory</param>
        public RoutingController(IRoutingRepository routingRepository, ISessionWrapper sessionWrapper, ILogFactory logFactory)
        {
            _routingRepository = routingRepository;
            SessionWrapper = sessionWrapper;
            LoggerFactory = logFactory;
        }

        #endregion

        #region User Defined Methods

        #region Public Methods

        /// <summary>
        ///  Get list of safe territories and countries.
        /// </summary>
        /// <returns>Partial View</returns>
		public PartialViewResult GetSafeAreaTerritory()
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.Territories = _routingRepository.GetSafeAreaTerritory();
                LoggerFactory.LogWriter.MethodExit();
                return PartialView(Constants.SafeTerritory);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Add Safe Territories
        /// </summary>
        /// <param name="territories">The territories</param>
        /// <returns>True/False</returns>
        [HttpPost]
        public bool AddSafeTerritories(List<TerritorialDisplay> territories)
        {
            try
            {
                LoggerFactory.LogWriter.MethodStart();
                UserInfo userInfo = SessionWrapper.CurrentUserInfo;
                LoggerFactory.LogWriter.MethodExit();
                return _routingRepository.AddSafeTerritories(territories, userInfo);
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

		#region Routing Variations
		public ActionResult ManageRoutingRules()
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();
                ViewBag.PageName = "MangeRules";
                LoggerFactory.LogWriter.MethodExit();
                return View();
			}
			catch (Exception ex)
			{
				LoggerFactory.LogWriter.Error(Category.UI, ex);
				throw;
			}
		}

		/// <summary>
		///  Get list of Routing Rules.
		/// </summary>
		/// <param name="args">table paging and sorting information from table querystring</param>
		/// <param name="jtPageSize">table pageSize from page sizr deopdown</param>
		/// <returns>GridJSONActions user details</returns>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult ManageRoutingRules(PagingParams args, string pageName, Int64 ruleNumber = 0, bool ruleVariationMode = false)
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();

                int totalRowCount = 0;
				if (pageName == "Rules")
				{
					var routingRules = _routingRepository.GetRoutingRules();
					totalRowCount = routingRules.Count;
                    LoggerFactory.LogWriter.MethodExit();
                    return routingRules.GridJSONActions<RoutingRule>(totalRowCount);
				}
				else
				{
					var ruleVariations = new List<RoutingVariations>();

					if (!ruleVariationMode && ruleNumber != 0)
					{
						var RoutingRuleVariations = _routingRepository.LoadRuleVariations(ruleNumber);

						foreach (RoutingVariations variation in RoutingRuleVariations.RoutingVariation)
						{
							string companyIds = string.Empty; string talentIds = string.Empty;
							if (variation.Companies.Any())
							{
								companyIds = string.Join(",", (from comp in variation.Companies
															   where comp.CompanyType.Equals(CompanyType.Owning)
															   select comp.CompanyInfo.Id.ToString()).Distinct().ToArray());
								variation.OwningCompany = (companyIds.IndexOf(ConstantsEntities._commaSymbol) != -1) ? companyIds : (companyIds != "") ? companyIds + ConstantsEntities._commaSymbol : "";

								companyIds = string.Join(",", (from comp in variation.Companies
															   where comp.CompanyType.Equals(CompanyType.Requesting)
															   select comp.CompanyInfo.Id.ToString()).Distinct().ToArray());
								variation.RequestingCompany = (companyIds.IndexOf(ConstantsEntities._commaSymbol) != -1) ? companyIds : (companyIds != "") ? companyIds + ConstantsEntities._commaSymbol : "";
							}
							if (variation.ArtistList.Any())
							{
								talentIds = string.Join(",", (from talent in variation.ArtistList
															  select talent.Id.ToString()).Distinct().ToArray());
								variation.variationTalent = (talentIds.IndexOf(ConstantsEntities._commaSymbol) != -1) ? talentIds : (talentIds != "") ? talentIds + ConstantsEntities._commaSymbol : "";
							}
							ruleVariations.Add(variation);
						}
						totalRowCount = ruleVariations.Count;
					}
					else if (ruleVariationMode)
					{
                        //Added for displaying default variation 
						RoutingVariations variation = new RoutingVariations();
						variation.IsParent = true;
						ruleVariations.Add(variation);
						totalRowCount = ruleVariations.Count;
					}

                    LoggerFactory.LogWriter.MethodExit();

                    return ruleVariations.GridJSONActions<RoutingVariations>(totalRowCount);
				}
			}
			catch (Exception ex)
			{
				LoggerFactory.LogWriter.Error(Category.UI, ex);
				throw;
			}
		}

		[HttpGet]
		public JsonResult GetRequestTypeLookUp()
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();
                var roles = new List<KeyValuePair<Byte, string>>();
				roles = _routingRepository.GetRoutingVariationRequestTypes();
                LoggerFactory.LogWriter.MethodExit();
                return Json(roles, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new {ex.Message });
			}
		}

		[HttpPost]
		public JsonResult ChangeRuleVariationStatus(Int64 Id, string statusType, string objectType)
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();
                LoggerFactory.LogWriter.Debug(string.Format("Id: {0}, statusType: {1},objectType: {2}", Id, statusType, objectType));

				var userInfo = new UserInfo { UserLoginName = GetCurrentLoginName() };
				StatusType ruleStatusType = (StatusType)Enum.Parse(typeof(StatusType), statusType);
				ObjectType ruleObjectType = (ObjectType)Enum.Parse(typeof(ObjectType), objectType);

				string resultMsg = _routingRepository.ChangeRuleStatus(Id, ruleStatusType, ruleObjectType, userInfo.UserLoginName);
				
				LoggerFactory.LogWriter.Debug("Change Rule or Variation Status Successfully Completed");

                LoggerFactory.LogWriter.MethodExit();
                return Json(resultMsg, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				LoggerFactory.LogWriter.Error(Category.UI, ex);
				throw;
			}
		}

		[HttpPost]
		public JsonResult SaveRuleVariations(RoutingRuleSaveInfo ruleVariationDetails)
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();
                long result = 0;
				if (ruleVariationDetails.RoutingRule != null && ruleVariationDetails.RoutingVariationSaveInfo.Count > 0)
				{
					ruleVariationDetails.RoutingRule.LoginName = GetCurrentLoginName();
                    result = _routingRepository.SaveRuleAndVariation(ruleVariationDetails);
                }
                LoggerFactory.LogWriter.MethodExit();
                return Json(result, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				LoggerFactory.LogWriter.Error(Category.UI, ex);
				throw;
			}
		}

		[HttpPost]
		public JsonResult GetRoutingVariationTerritories(long variationID, string territoryType)
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();

                TerritoryType variationTerritoryType = (TerritoryType)Enum.Parse(typeof(TerritoryType), territoryType);

				List<TerritorialDisplay> variationTerritoryList = _routingRepository.LoadTerritories(variationID, variationTerritoryType);

                LoggerFactory.LogWriter.MethodExit();

                return Json(variationTerritoryList, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				LoggerFactory.LogWriter.Error(Category.UI, ex);
				throw;
			}
		}

		[HttpPost]
		public ActionResult ExportRoutingVariations(string ExportRuleNumber = "")
		{
            LoggerFactory.LogWriter.MethodStart();

            List<RoutingVariations> ruleVariations = new List<RoutingVariations>();
			var RoutingRuleVariations = _routingRepository.LoadRuleVariations(Convert.ToInt64(ExportRuleNumber));
			ruleVariations = RoutingRuleVariations.RoutingVariation;
							
			#region Create Data Table
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Rule #"); 
			dataTable.Columns.Add("Project Type");
			dataTable.Columns.Add("Request Type"); 
			dataTable.Columns.Add("Sensitive Artist");
			dataTable.Columns.Add("Active Artist");
			dataTable.Columns.Add("Multi-artist");
			dataTable.Columns.Add("Compilation");
			dataTable.Columns.Add("Included Release Territoty");
			dataTable.Columns.Add("Excluded Release Territoty");
			dataTable.Columns.Add("Included Owning Territory");
			dataTable.Columns.Add("Excluded Owning Territory");
			dataTable.Columns.Add("Included Requesting Territory");
			dataTable.Columns.Add("Excluded Requesting Territory");
			dataTable.Columns.Add("Owning Company");
			dataTable.Columns.Add("Requesting Company");
			dataTable.Columns.Add("Talent Id");
			dataTable.Columns.Add("Skip International Marketing");
			dataTable.Columns.Add("Skip National Marketing");
			dataTable.Columns.Add("Skip Local Label Marketing");
			dataTable.Columns.Add("Comments");
				
			int variationIndex = 0;
			foreach (RoutingVariations variation in ruleVariations)
			{
				DataRow row = dataTable.NewRow(); 
				variationIndex++;
				row["Rule #"] = variationIndex;
				row["Project Type"] = ((byte)variation.ProjectType!=0) ? variation.ProjectType.ToString() : string.Empty;
				row["Request Type"] =  ((byte)variation.RvRequestType != 0) ? variation.RvRequestType.ToString(): string.Empty;
				row["Sensitive Artist"] = (variation.IsSensitiveArtist.ToString() == "True") ? "Y" : (variation.IsSensitiveArtist.ToString() == "") ? string.Empty : "N";
				row["Active Artist"] = (variation.IsActiveArtist.ToString() == "True") ? "Y" : (variation.IsActiveArtist.ToString() == "") ? string.Empty : "N";;
				row["Multi-artist"] = (variation.IsMultiArtist.ToString() == "True") ? "Y" : (variation.IsMultiArtist.ToString() == "") ? string.Empty : "N";
				row["Compilation"] = (variation.IsCompilation.ToString() == "True") ? "Y" : (variation.IsCompilation.ToString() == "") ? string.Empty : "N";
				row["Included Release Territoty"] = variation.IncludedReleaseTerritories;
				row["Excluded Release Territoty"] = variation.ExcludedReleaseTerritories;
				row["Included Owning Territory"] = variation.IncludedOwningTerritories;
				row["Excluded Owning Territory"] = variation.ExcludedOwningTerritories;
				row["Included Requesting Territory"] = variation.IncludedRequestingTerritories;
				row["Excluded Requesting Territory"] = variation.ExcludedRequestingTerritories;

				string companyIds = string.Empty;
				companyIds = string.Join(";", (from comp in variation.Companies
												where comp.CompanyType.Equals(CompanyType.Owning)
												select comp.CompanyInfo.CountryName + " (" + comp.CompanyInfo.ISACCode + ")").Distinct().ToArray());
				companyIds = (companyIds.IndexOf(';') != -1) ? companyIds : (companyIds != "") ? companyIds + ';' : string.Empty;
				row["Owning Company"] = companyIds;

				companyIds = string.Empty;
				companyIds = string.Join(";", (from comp in variation.Companies
												where comp.CompanyType.Equals(CompanyType.Requesting)
												select comp.CompanyInfo.CountryName + " (" + comp.CompanyInfo.ISACCode + ")").Distinct().ToArray());
				companyIds = (companyIds.IndexOf(';') != -1) ? companyIds : (companyIds != "") ? companyIds + ';' : string.Empty;
				row["Requesting Company"] = companyIds;

				string talentIds = string.Empty;
				talentIds = string.Join(";", (from talent in variation.ArtistList
												select talent.Id.ToString()).Distinct().ToArray());
				talentIds = (talentIds.IndexOf(';') != -1) ? talentIds : (talentIds != "") ? talentIds + ';' : string.Empty;
				row["Talent Id"] = talentIds;

				row["Skip International Marketing"] = (variation.IsSkipIntlMarketing.ToString() == "True") ? "Y" : (variation.IsSkipIntlMarketing.ToString() == "") ? string.Empty : "N";
				row["Skip National Marketing"] = (variation.IsSkipNtnlMarketing.ToString() == "True") ? "Y" : (variation.IsSkipNtnlMarketing.ToString() == "") ? string.Empty : "N";
				row["Skip Local Label Marketing"] = (variation.IsSkipLocalLabel.ToString() == "True") ? "Y" : (variation.IsSkipLocalLabel.ToString() == "") ? string.Empty : "N";
				row["Comments"] = variation.Comments;
					
				dataTable.Rows.Add(row);
			}
			#endregion
			string worksheetName = RoutingRuleVariations.RuleName;
				
			using (var excelEngine = new ExcelEngine())
			{
				var excel = excelEngine.Excel;
				var workbook = excel.Workbooks.Create(1);
				workbook.StandardFontSize = 8;

				var workbookProperties = workbook.BuiltInDocumentProperties;
				workbookProperties.Author = "UMG";
				workbookProperties.Company = "UMG";
				workbookProperties.Title = "Routing Variations";
				workbookProperties.Subject = "Routing Variations";

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

				var worksheet = workbook.Worksheets.Create("Routing Variations");
				workbook.Worksheets.Remove(0);
				var startRowIndex = 1;
				var maxColumnCount = 0;
				var columnCount = dataTable.Columns.Count;
				if (columnCount > maxColumnCount) { maxColumnCount = columnCount; }
				worksheet.ImportDataTable(dataTable, true, startRowIndex + 1, 1, -1, -1, true);
				worksheet.Range[startRowIndex + 1, 1, startRowIndex + 1, columnCount].CellStyleName = "TableHeaderStyle";
				startRowIndex = worksheet.UsedRange.LastRow + 4;
				worksheet.UsedRange.AutofitRows();
				worksheet.UsedRange.ColumnWidth = 25;
				var worksheetTitle = worksheet.Range[1, 1, 1, maxColumnCount];
				worksheetTitle.Merge();
				worksheetTitle.Text = worksheetName;
				worksheetTitle.CellStyleName = "WorksheetTitleStyle";
				worksheetTitle.RowHeight = 20;
					
				workbook.SaveAs(string.Format("{0}.{1}", worksheetName, "xls"), ExcelSaveType.SaveAsXLS, HttpContext.ApplicationInstance.Response, ExcelDownloadType.PromptDialog);

                LoggerFactory.LogWriter.MethodExit();

                return new EmptyResult();
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
		public JsonResult SelectMultiArtist(string ArtistName, int jtStartIndex, long ArtistID = 0, int PageSize = 0, long rowIndex = 0, string userName = "", string jtSorting = null)
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
					var artistResult = _routingRepository.SelectMultiArtist(searchOption, criteria.QualificationCriteria);
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
						var artistResult = _routingRepository.SelectMultiArtist(searchOption, criteria.QualificationCriteria);
						var result = artistResult.Details.ToList();
                        LoggerFactory.LogWriter.MethodExit();
                        return Json(new { Result = Constants.JsonOk, Records = result, TotalRecordCount = artistResult.RowsRetreived, RowIndex = artistResult.RowIndex });

					}
				}

			}
			catch (Exception ex)
			{
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                return Json(new { Result = Constants.JsonError, ex.Message });
			}
		}

		[HttpPost]
		public JsonResult SearchForArtist(string existingArtist)
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();

                var result = new List<ArtistInfo>();

                if (existingArtist.Trim() != string.Empty)
				    result = _routingRepository.GetArtists(existingArtist);
				
                LoggerFactory.LogWriter.MethodExit();
                return Json(new { Result = Constants.JsonOk, Records = result, TotalRecordCount = result.Count });
			}
			catch (Exception ex)
			{
                LoggerFactory.LogWriter.Error(Category.UI, ex);
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

		/// <summary>
		/// Get Current Login Name
		/// </summary>
		/// <param name="collection">The FormCollection</param>
		/// <returns>LoginName String</returns>
		private string GetCurrentLoginName()
		{
			try
			{
                LoggerFactory.LogWriter.MethodStart();
                return SessionWrapper.GcsCurrentPermissions.IsMimicUser == false ? SessionWrapper.CurrentUserInfo.UserLoginName : SessionWrapper.CurrentUserInfo.MimicedRccAdminLoginName;
            }
            catch (Exception ex)
            {
                LoggerFactory.LogWriter.Error(Category.UI, ex);
                throw;
			}
		}
        #endregion

        #endregion Public Methods

        #endregion User Defined Methods
    }
}
