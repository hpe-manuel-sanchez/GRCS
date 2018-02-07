using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.UI.Helper;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Extensions
{
    /// <summary>
    /// JQuery UI DatePicker helper.
    /// </summary>
    public static class DatePickerHelper
    {
        /// <summary>
        /// Converts the .net supported date format current culture 
        /// format into JQuery Datepicker format.
        /// </summary>
        /// <param name="html">HtmlHelper object.</param>
        /// <param name="dateFormat"> </param>
        /// <returns>Format string that supported in JQuery Datepicker.</returns>
        public static string GetJqueryDateFormat(this HtmlHelper html, string dateFormat = "")
        {
            try
            {
                if (dateFormat != string.Empty)
                {
                    return ConvertDateFormat(html, dateFormat);
                }
                else
                {
                    return GlobalConstants.DateOnlyFormat;
                }
            }
            catch (Exception)
            {
                return GlobalConstants.DateOnlyFormat;
            }
        }

        public static string GetDotNetDateFormat(this HtmlHelper html, string dateFormat = "")
        {
            try
            {
                if (dateFormat != string.Empty)
                {
                    return dateFormat;
                }
                else
                {
                    return GlobalConstants.DateOnlyFormat;
                }
            }
            catch (Exception)
            {
                return GlobalConstants.DateOnlyFormat;
            }
        }

        /// <summary>
        /// Gets the local short date.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="dateValue">The date value.</param>
        /// <param name="dateFormat">The culture string.</param>
        /// <returns></returns>
        public static string GetLocalShortDate(this HtmlHelper html, DateTime? dateValue, string dateFormat = "")
        {
            try
            {
                if (dateValue.HasValue)
                {
                    if (dateFormat != string.Empty)
                    {
                        return dateValue.Value.ToString(dateFormat);
                    }
                    else
                    {
                        return dateValue.Value.ToString(GlobalConstants.DateOnlyFormat);
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the request culture string.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string GetRequestDateFormatString(this HtmlHelper html)
        {
            try
            {
                var userLanguages = html.ViewContext.HttpContext.Request.UserLanguages;
                if (userLanguages != null && userLanguages.Any())
                {
                    try
                    {
                        var configFactory = DependencyResolver.Current.GetService<IConfigFactory>();
                        if (configFactory.IsLocalDateTimeEnabled)
                        {
                            var cultureInfo = new CultureInfo(userLanguages[0]);
                            return cultureInfo.DateTimeFormat.ShortDatePattern;
                        }
                    }
                    catch (CultureNotFoundException)
                    {
                        return GlobalConstants.DateOnlyFormat;
                    }
                }
                else
                {
                    return GlobalConstants.DateOnlyFormat;
                }

                return GlobalConstants.DateOnlyFormat;
            }
            catch (Exception)
            {
                return GlobalConstants.DateOnlyFormat;
            }
        }

        /// <summary>
        /// Gets the request culture string.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string GetRequestDateFormat(this HtmlHelper html)
        {
            try
            {
                var userLanguages = html.ViewContext.HttpContext.Request.UserLanguages;
                if (userLanguages != null && userLanguages.Any())
                {
                    try
                    {
                        var configFactory = DependencyResolver.Current.GetService<IConfigFactory>();
                        if (configFactory.IsLocalDateTimeEnabled)
                        {
                            var cultureInfo = new CultureInfo(userLanguages[0]);
                            return string.Concat("{0:", cultureInfo.DateTimeFormat.ShortDatePattern, "}");
                        }
                    }
                    catch (CultureNotFoundException)
                    {
                        return string.Concat("{0:", GlobalConstants.DateOnlyFormat, "}");
                    }
                }
                else
                {
                    return string.Concat("{0:", GlobalConstants.DateOnlyFormat, "}");
                }

                return string.Concat("{0:", GlobalConstants.DateOnlyFormat, "}");
            }
            catch (Exception)
            {
                return GlobalConstants.DateOnlyFormat;
            }
        }

        /// <summary>
        /// Gets the general date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static DateTime? GetGeneralDateTime(string dateTime, HttpRequestBase request)
        {
            try
            {
                return DateTime.ParseExact(dateTime, GetRequestDateFormatString(request), null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the request date format string.
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns></returns>
        private static string GetRequestDateFormatString(HttpRequestBase httpRequest)
        {
            try
            {
                var userLanguages = httpRequest.UserLanguages;
                if (userLanguages != null && userLanguages.Any())
                {
                    try
                    {
                        var configFactory = DependencyResolver.Current.GetService<IConfigFactory>();
                        if (configFactory.IsLocalDateTimeEnabled)
                        {
                            var cultureInfo = new CultureInfo(userLanguages[0]);
                            return cultureInfo.DateTimeFormat.ShortDatePattern;
                        }
                    }
                    catch (CultureNotFoundException)
                    {
                        return GlobalConstants.DateOnlyFormat;
                    }
                }
                else
                {
                    return GlobalConstants.DateOnlyFormat;
                }

                return GlobalConstants.DateOnlyFormat;
            }
            catch (Exception)
            {
                return GlobalConstants.DateOnlyFormat;
            }
        }

        /// <summary>
        /// Converts the .net supported date format current culture 
        /// format into JQuery Datepicker format.
        /// </summary>
        /// <param name="html">HtmlHelper object.</param>
        /// <param name="format">Date format supported by .NET.</param>
        /// <returns>Format string that supported in JQuery Datepicker.</returns>
        private static string ConvertDateFormat(this HtmlHelper html, string format)
        {
            /*
             *  Date used in this comment : 5th - Nov - 2009 (Thursday)
             *
             *  .NET    JQueryUI        Output      Comment
             *  --------------------------------------------------------------
             *  d       d               5           day of month(No leading zero)
             *  dd      dd              05          day of month(two digit)
             *  ddd     D               Thu         day short name
             *  dddd    DD              Thursday    day long name
             *  M       m               11          month of year(No leading zero)
             *  MM      mm              11          month of year(two digit)
             *  MMM     M               Nov         month name short
             *  MMMM    MM              November    month name long.
             *  yy      y               09          Year(two digit)
             *  yyyy    yy              2009        Year(four digit)             *
             */

            string currentFormat = format;

            // Convert the date
            currentFormat = currentFormat.Replace(GlobalConstants.Date4S, GlobalConstants.Date2C);
            currentFormat = currentFormat.Replace(GlobalConstants.Date3S, GlobalConstants.Date1C);

            // Convert month
            if (currentFormat.Contains(GlobalConstants.Month4C))
            {
                currentFormat = currentFormat.Replace(GlobalConstants.Month4C, GlobalConstants.Month2C);
            }
            else if (currentFormat.Contains(GlobalConstants.Month3C))
            {
                currentFormat = currentFormat.Replace(GlobalConstants.Month3C, GlobalConstants.Month1C);
            }
            else if (currentFormat.Contains(GlobalConstants.Month2C))
            {
                currentFormat = currentFormat.Replace(GlobalConstants.Month2C, GlobalConstants.Month2S);
            }
            else
            {
                currentFormat = currentFormat.Replace(GlobalConstants.Month1C, GlobalConstants.Month1S);
            }

            // Convert year
            currentFormat = currentFormat.Contains(GlobalConstants.Year4S) ?
            currentFormat.Replace(GlobalConstants.Year4S, GlobalConstants.Year2S) : currentFormat.Replace(GlobalConstants.Year2S, GlobalConstants.Year1S);

            return currentFormat;
        }
    }
}