
using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    public partial class ResourceSecondaryRights
    {
        public  ResourceSecondaryRights()
        {
            Exploitations=new List<ContractExploitationRestrictions>();
        }

        public SecondaryRights ConstructSaveInfo()
        {
            Enum.TryParse
                (ReviewStatusLnr, true, out Rights.ReviewStatus.Status);
            Rights.SecondaryExploitation = Exploitations;
            Rights.RightSetId=RightSetId ;
            Rights.ModifiedDateTime = ModifiedDateTimeLnr;

            var _dict = new Dictionary<string, bool>
                            {
                                {"Y", true},
                                {"N", false},
                                {"No Rights", true}
                            };

            Rights.SecondaryExploitation[0].IsRestriction = _dict[IsMACRestrictedLnr];
            Rights.SecondaryExploitation[1].IsRestriction = _dict[IsSyncronizationRestrictedLnr];
            Rights.SecondaryExploitation[2].IsRestriction = _dict[IsMidPriceRestrictedLnr];
            Rights.SecondaryExploitation[3].IsRestriction = _dict[IsBudgetRestrictedLnr];
            Rights.SecondaryExploitation[4].IsRestriction = _dict[IsPremiumRestrictedLnr];
            Rights.SecondaryExploitation[5].IsRestriction = _dict[IsSampleRestrictedLnr];
            Rights.SecondaryExploitation[6].IsRestriction = _dict[IsRoyaltyBreakRestrictedLnr];
            Rights.SecondaryExploitation[7].IsRestriction = _dict[IsClubOrderRestrictedLnr];
            Rights.SecondaryExploitation[8].IsRestriction = _dict[IsRemixestrictedLnr];
            Rights.SecondaryExploitation[9].IsRestriction = _dict[IsGreatestHitsRestrictedLnr];
            return Rights;
        }

        public ResourceSecondaryRights ConstructViewInfo()
        {
            var _dict = new Dictionary<bool?, string>
                            {
                                {true, "Y"},
                                {false, "N"}
                            };

            IsMACRestrictedLnr = _dict[Rights.SecondaryExploitation[0].IsRestriction];
            IsMACRestrictedLnr = SetNoRightsData(IsMACRestrictedLnr, Rights.SecondaryExploitation[0].RestrictionTypeId);
            IsSyncronizationRestrictedLnr = _dict[Rights.SecondaryExploitation[1].IsRestriction];
            IsSyncronizationRestrictedLnr = SetNoRightsData(IsSyncronizationRestrictedLnr, Rights.SecondaryExploitation[1].RestrictionTypeId);
            IsMidPriceRestrictedLnr = _dict[Rights.SecondaryExploitation[2].IsRestriction];
            IsMidPriceRestrictedLnr = SetNoRightsData(IsMidPriceRestrictedLnr, Rights.SecondaryExploitation[2].RestrictionTypeId);
            IsBudgetRestrictedLnr = _dict[Rights.SecondaryExploitation[3].IsRestriction];
            IsBudgetRestrictedLnr = SetNoRightsData(IsBudgetRestrictedLnr, Rights.SecondaryExploitation[3].RestrictionTypeId);
            IsPremiumRestrictedLnr = _dict[Rights.SecondaryExploitation[4].IsRestriction];
            IsPremiumRestrictedLnr = SetNoRightsData(IsPremiumRestrictedLnr, Rights.SecondaryExploitation[4].RestrictionTypeId);
            IsSampleRestrictedLnr = _dict[Rights.SecondaryExploitation[5].IsRestriction];
            IsSampleRestrictedLnr = SetNoRightsData(IsSampleRestrictedLnr, Rights.SecondaryExploitation[5].RestrictionTypeId);
            IsRoyaltyBreakRestrictedLnr = _dict[Rights.SecondaryExploitation[6].IsRestriction];
            IsRoyaltyBreakRestrictedLnr = SetNoRightsData(IsRoyaltyBreakRestrictedLnr, Rights.SecondaryExploitation[6].RestrictionTypeId);
            IsClubOrderRestrictedLnr = _dict[Rights.SecondaryExploitation[7].IsRestriction];
            IsClubOrderRestrictedLnr = SetNoRightsData(IsClubOrderRestrictedLnr, Rights.SecondaryExploitation[7].RestrictionTypeId);
            IsRemixestrictedLnr = _dict[Rights.SecondaryExploitation[8].IsRestriction];
            IsRemixestrictedLnr = SetNoRightsData(IsRemixestrictedLnr, Rights.SecondaryExploitation[8].RestrictionTypeId);
            IsGreatestHitsRestrictedLnr = _dict[Rights.SecondaryExploitation[9].IsRestriction];
            IsGreatestHitsRestrictedLnr = SetNoRightsData(IsGreatestHitsRestrictedLnr, Rights.SecondaryExploitation[9].RestrictionTypeId);
           // IsKioskRestrictedLnr = _dict[Rights.SecondaryExploitation[10].IsRestriction];
           // IsKioskRestrictedLnr = SetNoRightsData(IsKioskRestrictedLnr, Rights.SecondaryExploitation[10].RestrictionTypeId);
            Exploitations = Rights.SecondaryExploitation;
            SampleExistsLnr = SampleExists ? "Y" : "";
            SideArtistLnr = SideArtist ? "Y" : "";
            RightSetId = Rights.RightSetId;
            ReviewStatusLnr = Rights.ReviewStatus.Status.ToString();
            RightsTypeLnr = RightsType.ToString();
            TerritorialRightsLnr = Rights.TerritorialRights;
            ModifiedDateTimeLnr = Rights.ModifiedDateTime;
            ContractIdLnr = Rights.ContractId.HasValue ? Rights.ContractId.Value.ToString() : string.Empty;
            IsLostRights = Rights.LostRights.ToString();
            return this;
        }

        /// <summary>
        /// Gets or sets the lost rights LNR.
        /// </summary>
        /// <value>The lost rights LNR.</value>
        public string IsLostRights { get; set; }

        /// <summary>
        /// Sets the no rights data.
        /// </summary>
        /// <param name="exploitationType">Type of the exploitation.</param>
        /// <param name="restrictionOption">The restriction option.</param>
        /// <returns></returns>
        public string SetNoRightsData(string exploitationType,int? restrictionOption)
        {
            if (exploitationType == "Y" && restrictionOption == 1)
            {
                return "No Rights";
            }
            return exploitationType;
        }

        public string SampleExistsLnr { get; set; }

        public string SideArtistLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string ReviewStatusLnr { get; set; }
        public string ReviewReasonLnr { get; set; }
        public string RightsTypeLnr { get; set; }


        public string TerritorialRightsLnr { get; set; }

        public string IsMACRestrictedLnr { get; set; }

        public string IsSyncronizationRestrictedLnr { get; set; }

        public string IsMidPriceRestrictedLnr { get; set; }

        public string IsBudgetRestrictedLnr { get; set; }

        public string IsPremiumRestrictedLnr { get; set; }

        public string IsSampleRestrictedLnr { get; set; }

        public string IsRoyaltyBreakRestrictedLnr { get; set; }

        public string IsClubOrderRestrictedLnr { get; set; }

        public string IsRemixestrictedLnr { get; set; }

        public string IsGreatestHitsRestrictedLnr { get; set; }

        public string IsKioskRestrictedLnr { get; set; }

        public string Error { get; set; }

        public List<ContractExploitationRestrictions> Exploitations { get; set; }

        public string RowId { get; set; }

        public string JsonStringDatas { get; set; }
        public string ContractIdLnr { get; set; }

        public long RightSetId { get; set; }
        /// <summary>
        /// Gets or sets Error
        /// </summary>
        /// <value>The Error.</value>
        public string ModifiedDateTimeLnr { get; set; }

    }
}
