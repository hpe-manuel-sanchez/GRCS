using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.UI.Helper;
using UIConstant = UMGI.GRCS.UI.Utilities.Constants;
using UMGI.GRCS.UI.ViewModels.ClearanceInbox;
using UMGI.GRCS.UI.Models;

namespace UMGI.GRCS.UI.Extensions
{
    /// <summary>
    /// Class for custom extensions
    /// </summary>
    public static class CustomExtensions
    {
        private static int _rowCounter = -1;
        private static int _innerRowCounter = -1;
        // private static string arrowImage;

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool @ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = @ascending ? "OrderBy" : "OrderByDescending";
            var types = new[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);


        }

        #region "Territorial Rights"
        public static MvcHtmlString SiteMenuAsUnorderedList(this HtmlHelper helper, List<TerritorialDisplay> countryLinks, bool search)
        {
            if (countryLinks == null || countryLinks.Count == 0)
                return MvcHtmlString.Empty;
            var topLevelParentId = TreeViewHelper.GetCountryParentId(countryLinks);
            return MvcHtmlString.Create(BuildMenuItems(countryLinks, topLevelParentId, search, 0));
        }


        public static string BuildMenuItems(List<TerritorialDisplay> countryLinks, long parentId, bool search, int level)
        {
            var parentTag = new StringBuilder();
            parentTag.Append("<ul id=\"TreeView\" class=\"TreeView\">");
            var childCountryInfo = TreeViewHelper.GetChildCountryInfo(countryLinks, parentId);

            foreach (var countryInfo in childCountryInfo)
            {
                var itemTag = new StringBuilder();
                var inputName = new StringBuilder();

                var labelTag = new StringBuilder("<label>");
                labelTag.Append(countryInfo.Name);
                labelTag.Append("</label>");

                inputName.AppendFormat("{0}|{1}", HttpUtility.HtmlEncode(countryInfo.Name.Trim()), countryInfo.Id.ToString(CultureInfo.InvariantCulture) + countryInfo.ParentId.ToString(CultureInfo.InvariantCulture));

                var radioTag0 = new StringBuilder();
                var radioTag1 = new StringBuilder();
                var radioTag2 = new StringBuilder();

                var expandCollapse = new StringBuilder();
                expandCollapse.Append(((search || countryInfo.Id == 0 || countryInfo.Id == 2) ? "Expanded" : "Collapsed"));

                var isNa = !countryInfo.IsIncluded && !countryInfo.IsExcluded;
                const string radiochecked = "checked=checked";

                radioTag0.AppendFormat("<label for=\"Radio0\"><input alt=\"Radio0\" type='radio' name = \'" + inputName + "\' value=\'" + countryInfo.Id + "|" + countryInfo.ParentId + "\' {0}  />", isNa ? radiochecked : string.Empty);
                radioTag1.AppendFormat("<label for=\"Radio1\"><input alt=\"Radio1\" type='radio' name = \'" + inputName + "\' value=\'" + countryInfo.Id + "|" + countryInfo.ParentId + "\' {0} />", countryInfo.IsIncluded ? radiochecked : string.Empty);
                radioTag2.AppendFormat("<label for=\"Radio2\"><input alt=\"Radio2\" type='radio' name = \'" + inputName + "\' value=\'" + countryInfo.Id + "|" + countryInfo.ParentId + "\' {0} />", countryInfo.IsExcluded ? radiochecked : string.Empty);

                if (countryInfo.IsTerritory)
                    _rowCounter++;

                if (TreeViewHelper.CountryHasChildren(countryLinks, countryInfo.Id) || countryInfo.IsTerritory)
                {
                    var divTableTag = new StringBuilder();
                    divTableTag.AppendFormat("<div><div class=\"Territoryimageaction\"></div><table  style=\'margin-top:-1px;'  class=\"territoryCountry\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"FirstColumn\"><div><div class=\"divTree {0}\" style=\"float:left;width:20px;height:20px;margin-left:{1}px;\"></div>{2}<div></td>"
                                     , expandCollapse, level * 10, labelTag);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td>", radioTag0);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td>", radioTag1);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td>", radioTag2);
                    divTableTag.AppendFormat("<td class=\"FinalColumn\"><label id=\"TerritorialCount{0}\">{1}</label></td></tr></table></div>", countryInfo.Id, countryInfo.TerritoryCount);

                    itemTag.AppendFormat("<li class={0}>{1}{2}</li>", expandCollapse, divTableTag, (BuildMenuItems(countryLinks, countryInfo.Id, search, level + 1)));
                }
                else
                {
                    _innerRowCounter++;

                    var innerTableRowColor = "#000000";
                    innerTableRowColor = (_innerRowCounter % 2) == 0 ? "#F5F5F5" : "#FFFFFF";

                    var divTableTag = new StringBuilder();
                    divTableTag.AppendFormat("<div class=\"Territoryimageaction\"></div><table style=\'margin-top:-1px; background-color:{0}\' class=\"territoryCountry\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"FirstColumn\"><div><div style=\"float:left;width:20px;height:20px;margin-left:{1}px;\"></div>{2}<div></td>",
                        innerTableRowColor, level * 10, labelTag);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td><td class=\"SecondColumn\">{1}</td><td class=\"SecondColumn\">{2}</td><td class=\"FinalColumn\">  </td></tr></table>", radioTag0, radioTag1, radioTag2);

                    itemTag.AppendFormat("<li class={0}>{1}</li>", expandCollapse, divTableTag);
                }
                parentTag.Append(itemTag);
            }
            parentTag.Append("</ul>");
            return parentTag.ToString();
        }

        #endregion

        #region Safe Territories

        public static MvcHtmlString BindMenu(this HtmlHelper helper, List<TerritorialDisplay> countryLinks, bool search)
        {
            if (countryLinks == null || countryLinks.Count == 0)
                return MvcHtmlString.Empty;
            var topLevelParentId = TreeViewHelper.GetCountryParentId(countryLinks);
            return MvcHtmlString.Create(BuildMenus(countryLinks, topLevelParentId, search, 0));
        }

        public static string BuildMenus(List<TerritorialDisplay> countryLinks, long parentId, bool search, int level)
        {
            var parentTag = new StringBuilder();
            parentTag.Append("<ul id=\"TreeView\" class=\"TreeView\">");
            var childCountryInfo = TreeViewHelper.GetChildCountryInfo(countryLinks, parentId);

            foreach (var countryInfo in childCountryInfo)
            {
                var itemTag = new StringBuilder();
                var inputName = new StringBuilder();

                var labelTag = new StringBuilder("<label>");
                labelTag.Append(countryInfo.Name);
                labelTag.Append("</label>");

                inputName.AppendFormat("{0}|{1}", HttpUtility.HtmlEncode(countryInfo.Name.Trim()), countryInfo.Id.ToString(CultureInfo.InvariantCulture) + countryInfo.ParentId.ToString(CultureInfo.InvariantCulture));

                var radioTag0 = new StringBuilder();
                var radioTag1 = new StringBuilder();

                var expandCollapse = new StringBuilder();
                expandCollapse.Append(((search || countryInfo.Id == 0 || countryInfo.Id == 2) ? "Expanded" : "Collapsed"));

                var isNa = !countryInfo.IsIncluded && !countryInfo.IsExcluded;
                const string radiochecked = "checked=checked";

                radioTag0.AppendFormat("<label for=\"Radio0\"><input alt=\"Radio0\" type='radio' name = \'" + inputName + "\' value=\'" + countryInfo.Id + "|" + countryInfo.ParentId + "\' {0}  />", isNa ? radiochecked : string.Empty);
                radioTag1.AppendFormat("<label for=\"Radio1\"><input alt=\"Radio1\" type='radio' name = \'" + inputName + "\' value=\'" + countryInfo.Id + "|" + countryInfo.ParentId + "\' {0} />", countryInfo.IsSafeTerritory ? radiochecked : string.Empty);

                if (countryInfo.IsTerritory)
                    _rowCounter++;

                if (TreeViewHelper.CountryHasChildren(countryLinks, countryInfo.Id) || countryInfo.IsTerritory)
                {
                    var divTableTag = new StringBuilder();
                    divTableTag.AppendFormat("<div><div class=\"Territoryimageaction\"></div><table  style=\'margin-top:-1px;'  class=\"territoryCountry\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"FirstColumn\"><div><div class=\"divTree {0}\" style=\"float:left;width:20px;height:20px;margin-left:{1}px;\"></div>{2}<div></td>"
                                     , expandCollapse, level * 10, labelTag);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td>", radioTag0);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\">{0}</td>", radioTag1);
                    divTableTag.AppendFormat("</tr></table></div>");

                    itemTag.AppendFormat("<li class={0}>{1}{2}</li>", expandCollapse, divTableTag, (BuildMenus(countryLinks, countryInfo.Id, search, level + 1)));
                }
                else
                {
                    _innerRowCounter++;

                    var innerTableRowColor = "#000000";
                    innerTableRowColor = (_innerRowCounter % 2) == 0 ? "#F5F5F5" : "#FFFFFF";

                    var divTableTag = new StringBuilder();
                    divTableTag.AppendFormat("<div class=\"Territoryimageaction\"></div><table style=\'margin-top:-1px; background-color:{0}\' class=\"territoryCountry\" cellpadding=\"0\" cellspacing=\"0\"><tr><td class=\"FirstColumn\"><div><div style=\"float:left;width:20px;height:20px;margin-left:{1}px;\"></div>{2}<div></td>",
                        innerTableRowColor, level * 10, labelTag);
                    divTableTag.AppendFormat("<td class=\"SecondColumn\"></td><td class=\"SecondColumn\"></td></tr></table>");

                    itemTag.AppendFormat("<li class={0}>{1}</li>", expandCollapse, divTableTag);
                }
                parentTag.Append(itemTag);
            }
            parentTag.Append("</ul>");
            return parentTag.ToString();
        }

        #endregion


        public static class Inbox
        {
            public static int GetColumnWidth(List<InboxViewModel.ColumnSetting> dataList, string columnName, string id, int defaultWidth)
            {
                InboxViewModel.ColumnSetting columnSetting = null;
                if (dataList != null)
                {
                    columnSetting = dataList.Where(i => i.Column == columnName && i.GridId == id).SingleOrDefault();
                }
                return columnSetting == null ? defaultWidth : columnSetting.Width;
            }

            public static int GetFolderId(object text)
            {
                try
                {
                    return Convert.ToInt32((text.ToString().Split('-')[0]).Split(':')[1]);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public static string SubstringVal(string inputVal, int length)
        {
            if (inputVal == null || string.IsNullOrEmpty(inputVal))
            {
                return string.Empty;
            }

            if (inputVal.Length > length)
            {
                inputVal = inputVal.Substring(0, length);
                inputVal += "...";
            }

            return inputVal;
        }
    }

    public static class VersionExtension
    {
        public static MvcHtmlString GetFileLastVersion(string filename)
        {
            var physicalPath = HttpContext.Current.Server.MapPath(filename);
            string file = filename;

            if (File.Exists(physicalPath))
            {
                var version = new FileInfo(physicalPath).LastWriteTime.ToString("yyyyMMddHHmmss");
                file = String.Format("{0}?v={1}", filename, version);
            }

            return MvcHtmlString.Create(file);
        }
        
    }

}