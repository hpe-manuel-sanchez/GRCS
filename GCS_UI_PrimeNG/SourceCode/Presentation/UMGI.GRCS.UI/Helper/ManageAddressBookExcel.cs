
/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ContractListExcel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Mohammad
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
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Class to Export Manage Address Book List as Excel
    /// </summary>
    public static class ManageAddressBookExcel
    {
        /// <summary>
        /// Create OpenXML String for Contract Search results
        /// </summary>
        /// <param name="emailGroupDetails"></param>
        /// <returns></returns>
        public static string CreateExcel(List<EmailGroupDetails> emailGroupDetails)
        {
            var excelString = new StringBuilder();
            excelString.Append("<Row>");

            excelString.Append(ExcelHelper.GetCell("String", "Role/Group Name"));
            excelString.Append(ExcelHelper.GetCell("String", "Country"));
            excelString.Append(ExcelHelper.GetCell("String", "EmailIds"));
            excelString.Append("</Row>");
            foreach (var item in emailGroupDetails)
            {
                excelString.Append("<Row>");

                excelString.Append(ExcelHelper.GetCell("String", item.Name == null ? "" : item.Name.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.CountryDetails.CountryName == null ? "" : item.CountryDetails.CountryName.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.EmailIds == null ? "" : item.EmailIds.ToString(CultureInfo.InvariantCulture)));
                excelString.Append("</Row>");
            }
            return excelString.ToString();
        }
    }
}