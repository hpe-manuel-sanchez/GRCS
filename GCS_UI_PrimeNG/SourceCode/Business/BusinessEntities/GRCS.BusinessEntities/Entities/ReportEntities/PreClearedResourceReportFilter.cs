/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReportFilters
 * Project Code :   
 * Author       : Shivangi Jain
 * Created on   :  7/5/2013
 * Description  :  To add filters in active roster reports
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 *                                   Initial Version
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description :  Defines the entities for Active roster Report Filter Fields
                  
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
    public class PreClearedResourceReportFilter:Page 
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="Report Filter Fields"/> class.
        /// </summary>
        public PreClearedResourceReportFilter()
        {
            IsAscendingOrder = true;
        }
        /// <summary>
        /// Gets or sets the ClearanceCompanyId
        /// </summary>
        [DataMember]
        public string ClearanceCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the ArtistId
        /// </summary>
        [DataMember]
        public string ArtistId { get; set; }


        /// <summary>
        /// Gets or sets the resource genre
        /// </summary>
        [DataMember]
        public string genre { get; set; }

        /// <summary>
        ///  Gets or sets the PreClearanceType
        /// </summary>
        [DataMember]
        public string PreClearanceType { get; set; }

        /// <summary>
        /// Get or set SensitiveCompanyId
        /// </summary>
        [DataMember]
        public string SensitiveCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        ///  Gets or sets the YearFrom
        /// </summary>
        [DataMember]
        public int YearFrom { get; set; }

        /// <summary>
        ///  Gets or sets the YearTo
        /// </summary>
        [DataMember]
        public int YearTo { get; set; }

        /// <summary>
        ///  Gets or sets the CountryId
        /// </summary>
        [DataMember]
        public string CountryId { get; set; }

        [DataMember]
        public string ResourceType { get; set; }

        [DataMember]
        public string ExportType { get; set; }

        [DataMember]
        public string ClearanceCompanyName { get; set; }

    }
}
