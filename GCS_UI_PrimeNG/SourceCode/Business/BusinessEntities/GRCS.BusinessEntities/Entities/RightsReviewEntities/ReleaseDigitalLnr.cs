/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DigitalRestrictionList.cs 
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
* Description :  Defines the entities for Digital Restriction List Details                  
****************************************************************************/
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    public partial class ReleaseDigitalLnr : EntityInformation
    {

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
        /// Gets or sets the UPC id.
        /// </summary>
        /// <value>The UPC id.</value>
        public string UPCId
        {
            get;
            set;
        }
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
        /// Gets or sets the territorial rights.
        /// </summary>
        /// <value>The territorial rights.</value>
        public string TerritorialRights
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
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        public string RightsPeriod
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
        /// <value>The R2ReleaseId</value>
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

        public long RestrictionIdLnr { get; set; }

        public int IsEditableRight { get; set; }

        public string UseTypeLnr { get; set; }

        public string CommercialModelsLnr { get; set; }

        public long RightSetIdLnr { get; set; }

        public string strTotalRows { get; set; }

        public int MergeCount { get; set; }

        public string ReviewStatus { get; set; }

        public string RestrictionLnr { get; set; }

        public string RestrictonReasonLnr { get; set; }

        public string ConsentPeriodLnr { get; set; }

        public string NotesLnr { get; set; }

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

        public string ModifiedDateTimeLnr { get; set; }

        public string LostRightsLnr { get; set; }

        public string RestrictionReasonForOthers { get; set; }
    }
}

