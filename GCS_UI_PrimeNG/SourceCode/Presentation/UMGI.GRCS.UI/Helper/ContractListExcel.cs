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

using System;
using System.Globalization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System.Text;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Use to create Excel for Search Contract Results Grid
    /// </summary>
    public static class ContractListExcel
    {
        /// <summary>
        /// Create OpenXML String for Contract Search results
        /// </summary>
        /// <param name="contractInfo"></param>
        /// <returns></returns>
        public static string CreateExcel(ContractSearchResult contractInfo)
        {
            StringBuilder excelString = new StringBuilder();            
            excelString.Append("<Row>");
            excelString.Append(ExcelHelper.GetCell("String", "Is Parent Or Child"));
            excelString.Append(ExcelHelper.GetCell("String", "Artist Name"));
            excelString.Append(ExcelHelper.GetCell("String", "Contracting Party"));
            excelString.Append(ExcelHelper.GetCell("String", "WorkFlow Status"));
            excelString.Append(ExcelHelper.GetCell("String", "Contract Status"));
            excelString.Append(ExcelHelper.GetCell("String", "Contract Description"));
            excelString.Append(ExcelHelper.GetCell("String", "Admin Company"));
            excelString.Append(ExcelHelper.GetCell("String", "Signing Company"));
            excelString.Append(ExcelHelper.GetCell("String", "Rights Type"));
            excelString.Append(ExcelHelper.GetCell("String", "Contract Commencement Date"));
            excelString.Append(ExcelHelper.GetCell("String", "Local ContractRef Number"));
            excelString.Append(ExcelHelper.GetCell("String", "Contract Id"));
            excelString.Append("</Row>");  
            foreach (var item in contractInfo.ContractSearchInfo)
            {
                excelString.Append("<Row>");
                excelString.Append(ExcelHelper.GetCell("String", item.HasChildContract ? "P" : (item.HasParentContract ? "C" : ""))); 
                excelString.Append(ExcelHelper.GetCell("String", item.ArtistName == null ? "" : item.ArtistName.ToString(CultureInfo.InvariantCulture).Trim()));
                excelString.Append(ExcelHelper.GetCell("String", item.ContractingParty==null ? "" : item.ContractingParty.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.WorkflowStatus == null ? "" : item.WorkflowStatus.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.ContractStatus==null ? "" : item.ContractStatus.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.ContractDescription == null ? "" : item.ContractDescription.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.ClearanceCompanyCountry == null ? "" : item.ClearanceCompanyCountry.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.UmgSigningCompany == null ? "" : item.UmgSigningCompany.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.RightsTypeName == null ? "" : item.RightsTypeName.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.ContractCommencementDate == null ? "" : GetDateFromNullableDateTime(item.ContractCommencementDate)));
                excelString.Append(ExcelHelper.GetCell("String", item.LocalContractRefNumber == null ? "" : item.LocalContractRefNumber.ToString(CultureInfo.InvariantCulture)));
                excelString.Append(ExcelHelper.GetCell("String", item.ContractId.ToString(CultureInfo.InvariantCulture)));
               
                excelString.Append("</Row>");                
            }            
            return excelString.ToString();
        }

        private static string GetDateFromNullableDateTime(DateTime? dateTime)
        {
            return dateTime != null ? ((DateTime)dateTime).Date.ToString("dd MMM yyyy") : String.Empty;
        }
    }
}
