﻿/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReportFilters
 * Project Code :   
 * Author       : Shivangi Jain
 * Created on   :  7/5/2013
 * Description  :  To add filters in ResourceRightsDiscrepancies reports
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 *                                   Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for ResourceRightsDiscrepancies Filter Fields
                  
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
    public class ResourceRightsDiscrepanciesReportFilter : Page
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Report Filter Fields"/> class.
        /// </summary>
        public ResourceRightsDiscrepanciesReportFilter()
        {
            IsAscendingOrder = true;
        }
        [DataMember]
        public string ClearanceCompanyId { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string ExportType { get; set; }
        [DataMember]
        public string SensitiveCompanyId { get; set; }

        [DataMember]
        public string ClearanceCompanyName { get; set; }
    }
}
