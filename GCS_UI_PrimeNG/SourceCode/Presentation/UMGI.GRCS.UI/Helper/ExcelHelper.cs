/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ExcelHelper.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Mohamad
 * Created on     : 27-08-2012 
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
using System.Text;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Helper class for Excel Generation
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <returns></returns>
        public static string GetHeader()
        {
            var excelWriter = new StringBuilder();
            excelWriter.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
            return excelWriter.ToString();
        }

        /// <summary>
        /// Gets the excelwork book.
        /// </summary>
        /// <returns></returns>
        public static string GetExcelworkBook()
        {
            var excelWriter = new StringBuilder();
            excelWriter.AppendFormat(@" <ExcelWorkbook xmlns=""urn:schemas-microsoft-com:office:excel"">{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <WindowHeight>7650</WindowHeight>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <WindowWidth>15120</WindowWidth>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <WindowTopX>120</WindowTopX>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <WindowTopY>480</WindowTopY>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <TabRatio>683</TabRatio>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <ActiveSheet>1</ActiveSheet>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <ProtectStructure>False</ProtectStructure>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"  <ProtectWindows>False</ProtectWindows>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" </ExcelWorkbook>{0}", Environment.NewLine);
            
            return excelWriter.ToString();
        }

        /// <summary>
        /// Creates the worksheet.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string CreateWorksheet(string content, string name)
        {
            var excelWriter = new StringBuilder();
            excelWriter.AppendFormat(@" <Worksheet ss:Name=""{0}"">{1}", name, Environment.NewLine);
            excelWriter.AppendFormat(@"  <Table>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@"   {0}", content);
            excelWriter.AppendFormat(@"  </Table>{0}", Environment.NewLine);
            excelWriter.AppendFormat(@" </Worksheet>{0}", Environment.NewLine);
            return excelWriter.ToString();
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string GetCell(string dataType, string data)
        {
            return string.Format(@"<Cell><Data ss:Type=""{0}"">{1}</Data></Cell>", dataType, data);
        }
    }
}