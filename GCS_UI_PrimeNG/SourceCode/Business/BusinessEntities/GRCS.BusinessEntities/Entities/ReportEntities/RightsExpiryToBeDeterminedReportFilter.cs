/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReportFilters
 * Project Code :   
 * Author       : Shivangi Jain
 * Created on   :  7/5/2013
 * Description  :  To add filters in Rights Expiry to be Deteremined reports
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 *                                   Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for Rights Expiry to be Deteremined Report Filter Fields
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;


namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    /// <summary>
    /// FilterFields
    /// </summary>
    [DataContract]
    [Serializable]
    public class RightsExpiryToBeDeterminedReportFilter:Page 
    {
        public RightsExpiryToBeDeterminedReportFilter()
        {
            IsAscendingOrder = true;
        }
        [DataMember]
        public string ClearanceCompanyId { get; set; }

        [DataMember]
        public string ClearanceCompanyName { get; set; }

        [DataMember]
        public string ExportType { get; set; }

        /// <summary>
        /// Get or set SensitiveCompanyId
        /// </summary>
        [DataMember]
        public string SensitiveCompanyId { get; set; }

    }
}
