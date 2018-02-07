/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearanceResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Team
 * Created on   : 07-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for PreClearanceResult Details Linear
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Partial Class for Preclearance Result For Grid Binding
    /// </summary>
    public partial class PreClearanceResult
    {

        /// <summary>
        /// Constructs the save info.
        /// </summary>
        /// <returns></returns>
        public PreClearanceInfo ConstructSaveInfo()
        {
            var _dict = new Dictionary<string, bool?>
                            {
                                {"N", false},
                                {"Y", true},
                                {"", (bool?)null}
                            };

            if (TopPriceCompilationLnr != null)
                PreClearanceInfo.TopPriceCompilation = _dict[TopPriceCompilationLnr];
            if (MidPriceCompilationLnr != null)
                PreClearanceInfo.MidPriceCompilation = _dict[MidPriceCompilationLnr];
            if (BudgetCompilationLnr != null)
                PreClearanceInfo.BudgetCompilation = _dict[BudgetCompilationLnr];
            if (DirectMarketingLnr != null)
                PreClearanceInfo.DirectMarketing = _dict[DirectMarketingLnr];
            if (PremiumLnr != null)
                PreClearanceInfo.Premium = _dict[PremiumLnr];
            if (MasterSyncroniseUseLnr != null)
                PreClearanceInfo.MasterSyncroniseUse = _dict[MasterSyncroniseUseLnr];
            PreClearanceInfo.TerritoryExclusions = TerritoryExclusionsLnr;
            PreClearanceInfo.RightInfo.RightSetId = RightsIdLnr;
            ReviewStatusType reviewStatusType;
            Enum.TryParse
                   (ReviewStatusLnr, true, out reviewStatusType);
            PreClearanceInfo.ReviewStatusTypes.Status = reviewStatusType;
            SampleExists = SampleExistsLnr == "Y";
            SideArtist = SideArtistLnr == "Y";
            PreClearanceInfo.RightInfo.ModifiedDateTime = ModifiedDateTimeLnr;
            if (!string.IsNullOrEmpty(ExcludedCountries) && ExcludedCountries.Split(',').Length > 0 && ExcludedCountries != "null")
            {
                ExcludedCountries.Split(',').ToList().ForEach(id => PreClearanceInfo.ExcludedCountries.Add(Convert.ToInt64(id)));
            }
            return PreClearanceInfo;
        }

        /// <summary>
        /// Constructs the view info.
        /// </summary>
        /// <returns></returns>
        public PreClearanceResult ConstructViewInfo()
        {
            var _dict = new Dictionary<string, string>
                            {
                                {"True", "Y"},
                                {"False", "N"}
                            };
            TopPriceCompilationLnr = PreClearanceInfo.TopPriceCompilation.HasValue ? _dict[PreClearanceInfo.TopPriceCompilation.ToString()] : "";
            MidPriceCompilationLnr = PreClearanceInfo.MidPriceCompilation.HasValue ? _dict[PreClearanceInfo.MidPriceCompilation.ToString()] : "";
            BudgetCompilationLnr = PreClearanceInfo.BudgetCompilation.HasValue ? _dict[PreClearanceInfo.BudgetCompilation.ToString()] : "";
            DirectMarketingLnr = PreClearanceInfo.DirectMarketing.HasValue ? _dict[PreClearanceInfo.DirectMarketing.ToString()] : "";
            PremiumLnr = PreClearanceInfo.Premium.HasValue ? _dict[PreClearanceInfo.Premium.ToString()] : "";
            MasterSyncroniseUseLnr = PreClearanceInfo.MasterSyncroniseUse.HasValue ? _dict[PreClearanceInfo.MasterSyncroniseUse.ToString()] : "";
            SampleExistsLnr = SampleExists ? "Y" : "";
            SideArtistLnr = SideArtist ? "Y" : "";
            TerritoryExclusionsLnr = PreClearanceInfo.TerritoryExclusions;
            if (TerritoryExclusionsLnr == "null" || TerritoryExclusionsLnr == null)
                TerritoryExclusionsLnr = "";
            ReviewStatusLnr = PreClearanceInfo.ReviewStatusTypes.Status.ToString();
            RightsIdLnr = PreClearanceInfo.RightInfo.RightSetId;
            RightsTypeLnr = RightsType.ToString();
            ModifiedDateTimeLnr = PreClearanceInfo.RightInfo.ModifiedDateTime;
            ContractIdLnr = PreClearanceInfo.RightInfo.ContractId.HasValue ? PreClearanceInfo.RightInfo.ContractId.Value.ToString() : string.Empty;
            RepertoireIdLnr = RepertoireId.ToString();
            IsLostRights = PreClearanceInfo.RightInfo.LostRights.HasValue ? _dict[PreClearanceInfo.RightInfo.LostRights.ToString()] : "";

            return this;
        }


        /// <summary>
        /// Gets or sets the lost rights LNR.
        /// </summary>
        /// <value>The lost rights LNR.</value>
        public string IsLostRights { get; set; }

        /// <summary>
        /// Gets or sets the top price compilation LNR.
        /// </summary>
        /// <value>The top price compilation LNR.</value>
        public string TopPriceCompilationLnr { get; set; }

        /// <summary>
        /// Gets or sets the contract id LNR.
        /// </summary>
        /// <value>The contract id LNR.</value>
        public string ContractIdLnr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [mid price compilation].
        /// </summary>
        /// <value><c>true</c> if [mid price compilation]; otherwise, <c>false</c>.</value>
        public string MidPriceCompilationLnr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [budget compilation].
        /// </summary>
        /// <value><c>true</c> if [budget compilation]; otherwise, <c>false</c>.</value>
        public string BudgetCompilationLnr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [direct marketing].
        /// </summary>
        /// <value><c>true</c> if [direct marketing]; otherwise, <c>false</c>.</value>
        public string DirectMarketingLnr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PreClearanceInfo"/> is premium.
        /// </summary>
        /// <value><c>true</c> if premium; otherwise, <c>false</c>.</value>
        public string PremiumLnr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [master syncronise use].
        /// </summary>
        /// <value><c>true</c> if [master syncronise use]; otherwise, <c>false</c>.</value>
        public string MasterSyncroniseUseLnr { get; set; }

        /// <summary>
        /// Gets or sets the territory exclusions.
        /// </summary>
        /// <value>The territory exclusions.</value>
        public string TerritoryExclusionsLnr { get; set; }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        public string ReviewStatusLnr { get; set; }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        public long RightsIdLnr { get; set; }

        /// <summary>
        /// Gets or sets the Error.
        /// </summary>
        /// <value>Error value.</value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets a value ReviewReason.
        /// </summary>
        /// <value>ReviewReason.</value>
        public string ReviewReasonLnr { get; set; }

        /// <summary>
        /// Gets or sets a value RightsType.
        /// </summary>
        /// <value>RightsType.</value>
        public string RightsTypeLnr { get; set; }

        /// <summary>
        /// Gets or sets the side artist LNR.
        /// </summary>
        /// <value>The side artist LNR.</value>
        public string SideArtistLnr { get; set; }

        /// <summary>
        /// Gets or sets the sample exists LNR.
        /// </summary>
        /// <value>The sample exists LNR.</value>
        public string SampleExistsLnr { get; set; }

        /// <summary>
        /// Gets or sets the modified date time LNR.
        /// </summary>
        /// <value>The modified date time LNR.</value>
        public string ModifiedDateTimeLnr { get; set; }

        /// <summary>
        /// Gets or sets the repertoire id LNR.
        /// </summary>
        /// <value>The repertoire id LNR.</value>
        public string RepertoireIdLnr { get; set; }

    }

}
