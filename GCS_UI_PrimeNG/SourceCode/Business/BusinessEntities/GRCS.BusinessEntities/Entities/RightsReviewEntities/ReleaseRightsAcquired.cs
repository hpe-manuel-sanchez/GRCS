/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Srinivas
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Release Rights Details                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    public partial class ReleaseRightsAcquired : EntityInformation
    {

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public string Artist
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get;
            set;
        }       

        /// <summary>
        /// Gets or sets the type of the release.
        /// </summary>
        /// <value>The type of the release.</value>
        public string ReleaseType
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        public string VersionTitle
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public DateTime? ReleaseDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the configration.
        /// </summary>
        /// <value>The configration.</value>
        public string Configration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the linked contract.
        /// </summary>
        /// <value>The linked contract.</value>
        public LinkType LinkedContract
        {
            get;
            set;
        }

        public bool IsSplitDeal
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the linked tooltip.
        /// </summary>
        /// <value>The linked tooltip.</value>
        public string LinkedTooltip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        public string AdminCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        public string RightsPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [lost rights].
        /// </summary>
        /// <value><c>true</c> if [lost rights];
        /// otherwise, <c>false</c>.</value>
        public string LostRightsText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lost reason.
        /// </summary>
        /// <value>The lost reason.</value>
        public string LostReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [physically exploited].
        /// </summary>
        /// <value><c>true</c> if [physically exploited]; 
        /// otherwise, <c>false</c>.</value>
        public string PhysicallyExploitedText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether this
        /// <see cref="RepertoireRights"/> is exception.
        /// </summary>
        /// <value><c>true</c> if exception; 
        /// otherwise, <c>false</c>.</value>
        public string ExceptionText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value Exception Notes
        /// </summary>
        public string Notes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether this
        /// <see cref="RepertoireRights"/> is error.
        /// </summary>
        /// <value><c>true</c> if exception; 
        /// otherwise, <c>false</c>.</value>
        public string Error
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [digitally exploited].
        /// </summary>
        /// <value><c>true</c> if [digitally exploited];
        /// otherwise, <c>false</c>.</value>
        public string DigitallyExploitedText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the modified date time.
        /// </summary>
        /// <value>The modified date time.</value>
        public DateTime? ModifiedDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right set Id.
        /// </summary>
        /// <value>The right set Id.</value>
        public long RightSetId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the release Id.
        /// </summary>
        /// <value>The release Id.</value>
        public long? ReleaseId
        {
            get;
            set;
        }




        /// <summary>
        /// Gets or sets the R2ReleaseId.
        /// </summary>
        /// <value>The R2ReleaseId.</value>
        public long? R2ReleaseId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the contract Id.
        /// </summary>
        /// <value>The contract Id.</value>
        public long? ContractId
        {
            get;
            set;
        }

        public int IsEditableRight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the territorial rights.
        /// </summary>
        /// <value>The territorial rights.</value>
        public string TerritorialRights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UPC id.
        /// </summary>
        /// <value>The UPC id.</value>
        public string UPCId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights source.
        /// </summary>
        /// <value>The rights source.</value>
        public int? RightsSourceId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights source.
        /// </summary>
        /// <value>The rights source.</value>
        public string RightsSourceText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lost rights date.
        /// </summary>
        /// <value>The lost rights date.</value>
        public DateTime? LostRightsDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value permissions
        /// </summary>
        /// <value><c>true</c> if authenticated;
        /// otherwise, <c>false</c>.</value>
        public char SensitiveInfoPermitted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value permissions
        /// </summary>
        /// <value><c>true</c> if authenticated;
        /// otherwise, <c>false</c>.</value>
        public char RightsEditPermitted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets Total Rows for Syncfusion Grid paging purpose
        /// </summary>
        public string TotalRows
        {
            get;
            set;
        }

        public string IncludedCountryLnr
        {
            get;
            set;
        }

        public string IncludedTerritoryLnr
        {
            get;
            set;
        }

        public string ExcludedCountryLnr
        {
            get;
            set;
        }

        public string ExcludedTerritoryLnr
        {
            get;
            set;
        }

        public string ModifiedDateTimeLnr { get; set; }

        public string RightsExpiryRule { get; set; }

        public string LostRightsDateLinear { get; set; }
    }
}
