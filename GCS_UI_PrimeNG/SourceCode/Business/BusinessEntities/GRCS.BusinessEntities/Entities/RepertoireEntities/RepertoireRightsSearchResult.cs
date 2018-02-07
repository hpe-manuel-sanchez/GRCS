/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RepertoireRightsSearchResult.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Rikhu Prasad
 * Created on   : 17/12/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for RepertoireRightsSearchResult
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Repertoire Rights Search Result
    /// </summary>
    [DataContract]
    [Serializable]
    public class RepertoireRightsSearchResult:EntityInformation
    {
        public RepertoireRightsSearchResult()
        {
            DigitalRestriction = new ReportoireDigitalRestriction();
        }
        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The active for marketing.</value>
        [DataMember]
        public string IsActiveForMarketing { get; set; }
        
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        [DataMember]
        public string Artist { get; set; }
       
        /// <summary>
        /// Gets or sets the clearance data admin company.
        /// </summary>
        /// <value>The clearance data admin company.</value>
        [DataMember]
        public string ClearanceDataAdminCompany { get; set; }
        
        /// <summary>
        /// Gets or sets the digital exploitation rights.
        /// </summary>
        /// <value>The digital exploitation rights.</value>
        [DataMember]
        public string IsDigitalExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets the digital unbundling allowed.
        /// </summary>
        /// <value>The digital unbundling allowed.</value>
        [DataMember]
        public string IsDigitalUnbundlingAllowed { get; set; }
        
        /// <summary>
        /// Gets or sets the ISRC.
        /// </summary>
        /// <value>The ISRC.</value>
        [DataMember]
        public string Isrc { get; set; }

        [DataMember]
        public string Kiosk { get; set; }

        /// <summary>
        /// Gets or sets the linked contract.
        /// </summary>
        /// <value>The linked contract.</value>
        [DataMember]
        public string LinkedContract { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is lost rights indicator.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is lost rights indicator; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public string IsLostRightsIndicator { get; set; }

        /// <summary>
        /// Gets or sets the lost rights date.
        /// </summary>
        /// <value>The lost rights date.</value>
        [DataMember]
        public string LostRightsDate { get; set; }

        /// <summary>
        /// Gets or sets the lost rights reason.
        /// </summary>
        /// <value>The lost rights reason.</value>
        [DataMember]
        public string LostRightsReason { get; set; }

        /// <summary>
        /// Gets or sets the master sync use.
        /// </summary>
        /// <value>The master sync use.</value>
        [DataMember]
        public string MasterSyncUse { get; set; }
           
        /// <summary>
        /// Gets or sets the mobile exploitation rights.
        /// </summary>
        /// <value>The mobile exploitation rights.</value>
        [DataMember]
        public string IsMobileExploitationRights { get; set; }
        
        /// <summary>
        /// Gets or sets the new for review.
        /// </summary>
        /// <value>The new for review.</value>
        [DataMember]
        public string NewForReview { get; set; }

        /// <summary>
        /// Gets or sets the physical exploitation rights.
        /// </summary>
        /// <value>The physical exploitation rights.</value>
        [DataMember]
        public string IsPhysicalExploitationRights { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PPB revenue claim.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is PPB revenue claim; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public string IsPpbRevenueClaim { get; set; }

        /// <summary>
        /// Gets or sets the pre clearance territory exclusions.
        /// </summary>
        /// <value>The pre clearance territory exclusions.</value>
        [DataMember]
        public string PreClearanceTerritoryExclusions { get; set; }
        
        /// <summary>
        /// Gets or sets the P year.
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public string PYear { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        [DataMember]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public string ReviewStatus { get; set; }

        /// <summary>
        /// Gets or sets the rights id.
        /// </summary>
        /// <value>The rights id.</value>
        [DataMember]
        public int RightsSetId { get; set; }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The rights period.</value>
        [DataMember]
        public string RightsPeriod { get; set; }

        /// <summary>
        /// Gets or sets the type of the rights.
        /// </summary>
        /// <value>The type of the rights.</value>
        [DataMember]
        public string RightsType { get; set; }

        /// <summary>
        /// Gets or sets the sample credit.
        /// </summary>
        /// <value>The sample credit.</value>
        [DataMember]
        public string SampleCredit { get; set; }
        
        /// <summary>
        /// Gets or sets the territorial rights.
        /// </summary>
        /// <value>The territorial rights.</value>
        [DataMember]
        public string TerritorialRights { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the top price compilation.
        /// </summary>
        /// <value>The top price compilation.</value>
        //[DataMember]
        //public string TopPriceCompilation { get; set; }

        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The UPC.</value>
        [DataMember]
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is artist.
        /// </summary>
        /// <value><c>true</c> if this instance is artist; otherwise, <c>false</c>.</value>
        [DataMember]
        public string IsArtist { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sample.
        /// </summary>
        /// <value><c>true</c> if this instance is sample; otherwise, <c>false</c>.</value>
        [DataMember]
        public string IsSample { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is sample.
        /// </summary>
        /// <value><c>true</c> if this instance is sample; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsContainSearch { get; set; }

        /// <summary>
        /// Gets or sets the type of the rights.
        /// </summary>
        /// <value>The type of the rights.</value>
        [DataMember]
        public string ResourceType { get; set; }


        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public string ReleaseType { get; set; }

        /// <summary>
        /// Gets or sets the Resource id.
        /// </summary>
        /// <value>The Resource id.</value>
        [DataMember]
        public long? ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the Release id.
        /// </summary>
        /// <value>The Release id.</value>
        [DataMember]
        public long? ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the Contract id.
        /// </summary>
        /// <value>The Contract id.</value>
        [DataMember]
        public long? ContractId { get; set; }
        /// <summary>
        /// Gets or sets the Is Mac.
        /// </summary>
        /// <value>The Is Mac.</value>
        [DataMember]
        public string IsMac { get; set; }

        /// <summary>
        /// Gets or sets the R2 Release id.
        /// </summary>
        /// <value>The R2 Release id.</value>
        [DataMember]
        public long? R2ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the R2 Resource id.
        /// </summary>
        /// <value>The R2 Resource id.</value>
        [DataMember]
        public long? R2ResourceId { get; set; }

        #region "PreClearance"

        /// <summary>
        /// IsTopPriceCompilationCleared
        /// </summary>
        [DataMember]
        public string IsTopPriceCompilationCleared;

        /// <summary>
        /// IsMidPriceCompilationCleared
        /// </summary>
        [DataMember]
        public string IsMidPriceCompilationCleared;

        /// <summary>
        /// IsBudgetCompilationCleared
        /// </summary>
        [DataMember]
        public string IsBudgetCompilationCleared;

        /// <summary>
        /// IsDirectMarketingCleared
        /// </summary>
        [DataMember]
        public string IsDirectMarketingCleared;

        /// <summary>
        /// IsPremiumCleared
        /// </summary>
        [DataMember]
        public string IsPremiumCleared;

        /// <summary>
        /// IsMasterUseCleared
        /// </summary>
        [DataMember]
        public string IsMasterUseCleared;

        /// <summary>
        /// IsSynchronisationUseCleared
        /// </summary>
        [DataMember]
        public string IsSynchronisationUseCleared;

        #endregion 

        #region "For UI Purpose"
        /// <summary>
        /// Gets or sets the pre clearance. 
        /// Used for Model-View binding in UI
        /// </summary>
        /// <value>The pre clearance.</value>
        [DataMember]
        public IEnumerable<PreClearance> PreClearance { get; set; }
        
        /// <summary>
        /// Exploitations
        /// </summary>
        [DataMember]
        public IEnumerable<Exploitations> Exploitations { get; set; }

        [DataMember]
        public string LinkedContractInfo { get; set; }

        [DataMember]
        public int TotalRowsCount { get; set; }

        [DataMember]
        public string SampleExistValue { get; set; }

        [DataMember]
        public string IsSplitContract { get; set; }
        
        [DataMember]
        public string LookupDataSourceText { get; set; }
        
        [DataMember]
        public string LkupDataSourceVal { get; set; }
        #endregion 


        #region "Secondary ExploitationBasicSearch"
       
        /// <summary>
        /// Gets or sets the multi artist compilation.
        /// </summary>
        /// <value>The multi artist compilation.</value>
        [DataMember]
        public string MultiArtistCompilation { get; set; }

        /// <summary>
        /// Gets or sets the synchronisation.
        /// </summary>
        /// <value>The synchronisation.</value>
        [DataMember]
        public string Synchronisation { get; set; }

        /// <summary>
        /// Gets or sets the mid price.
        /// </summary>
        /// <value>The mid price.</value>
        [DataMember]
        public string MidPrice { get; set; }

        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>The budget.</value>
        [DataMember]
        public string Budget { get; set; }
        
        /// <summary>
        /// Gets or sets the premiums.
        /// </summary>
        /// <value>The premiums.</value>
        [DataMember]
        public string Premiums { get; set; }

        /// <summary>
        /// Gets or sets the Sampleuse.
        /// </summary>
        /// <value>The Sampleuse.</value>
        [DataMember]
        public string Sampleuse { get; set; }

        /// <summary>
        /// Gets or sets the advert royalty breaks.
        /// </summary>
        /// <value>The advert royalty breaks.</value>
        [DataMember]
        public string AdvertRoyaltyBreaks { get; set; }

        /// <summary>
        /// Gets or sets the club mail order.
        /// </summary>
        /// <value>The club mail order.</value>
        [DataMember]
        public string ClubMailOrder { get; set; }
        
        /// <summary>
        /// Gets or sets the remix edits.
        /// </summary>
        /// <value>The remix edits.</value>
        [DataMember]
        public string RemixEdits { get; set; }

        /// <summary>
        /// Gets or sets the greatest hits.
        /// </summary>
        /// <value>The greatest hits.</value>
        [DataMember]
        public string GreatestHits { get; set; }
        
        /// <summary>
        /// Gets or sets the digital restriction.
        /// </summary>
        /// <value>The digital restriction.</value>
        [DataMember]
        public ReportoireDigitalRestriction DigitalRestriction { get; set; }

        #endregion
    }

}
