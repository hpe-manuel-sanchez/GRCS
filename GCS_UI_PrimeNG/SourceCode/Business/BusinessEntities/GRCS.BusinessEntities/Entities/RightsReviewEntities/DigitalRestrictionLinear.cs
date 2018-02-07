using System;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    public partial class ResourceDigitalRights
    {

        /// <summary>
        /// Constructs the save info.
        /// </summary>
        /// <returns></returns>
        public ResourceDigitalRestrictions ConstructSaveInfo()
        {
            if(DigitalRestriction == null)
                DigitalRestriction = new ResourceDigitalRestrictions();
            DigitalRestriction.DigitalRestrictions.ModifiedDateTime = ModifiedDate;
            DigitalRestriction.DigitalRestrictions.RightSetId = RightSetIdLnr;
            Enum.TryParse(ReviewStatusLnr, true, out DigitalRestriction.ReviewStatus.Status);
            DigitalRestriction.DigitalRestrictions.DigitalRestrictions.Add
                (new ContractDigitalRestrictions
                     {
                         DigitalRestrictionId = RestrictionIdLnr,
                        //ConsentPeriodId = ConsentPeriodLnr,
                     });
                
            return DigitalRestriction;
        }

        /// <summary>
        /// Gets or sets the right set id LNR.
        /// </summary>
        /// <value>The right set id LNR.</value>
        public long RightSetIdLnr { get; set; }

        /// <summary>
        /// Gets or sets the restriction id LNR.
        /// </summary>
        /// <value>The restriction id LNR.</value>
        public long RestrictionIdLnr { get; set; }

        /// <summary>
        /// Gets or sets the merge count LNR.
        /// </summary>
        /// <value>The merge count LNR.</value>
        public int MergeCountLnr { get; set; }

        /// <summary>
        /// Gets or sets the review status LNR.
        /// </summary>
        /// <value>The review status LNR.</value>
        public string ReviewStatusLnr { get; set; }

        /// <summary>
        /// Gets or sets the use type LNR.
        /// </summary>
        /// <value>The use type LNR.</value>
        public string UseTypeLnr { get; set; }

        /// <summary>
        /// Gets or sets the commercial models LNR.
        /// </summary>
        /// <value>The commercial models LNR.</value>
        public string CommercialModelsLnr { get; set; }

        /// <summary>
        /// Gets or sets the restriction LNR.
        /// </summary>
        /// <value>The restriction LNR.</value>
        public string RestrictionLnr { get; set; }

        /// <summary>
        /// Gets or sets the restricton reason LNR.
        /// </summary>
        /// <value>The restricton reason LNR.</value>
        public string RestrictonReasonLnr { get; set; }

        /// <summary>
        /// Gets or sets the consent period LNR.
        /// </summary>
        /// <value>The consent period LNR.</value>
        public string ConsentPeriodLnr { get; set; }

        /// <summary>
        /// Gets or sets the notes LNR.
        /// </summary>
        /// <value>The notes LNR.</value>
        public string NotesLnr { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public string Error { get; set; }
        /// <summary>
        /// Gets or sets the rights type LNR.
        /// </summary>
        /// <value>The rights type LNR.</value>
        public string RightsTypeLnr { get; set; }
        /// <summary>
        /// Gets or sets the territorial rights LNR.
        /// </summary>
        /// <value>The territorial rights LNR.</value>
        public string TerritorialRightsLnr { get; set; }

        /// <summary>
        /// Gets or sets the resource type LNR.
        /// </summary>
        /// <value>The resource type LNR.</value>
        public string ResourceTypeLnr { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        public string ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the sample Status.
        /// </summary>
        /// <value>The is sample LNR.</value>
        public string SampleExistsLnr { get; set; }

        /// <summary>
        /// Gets or sets the side artist status.
        /// </summary>
        /// <value>The is side artist string.</value>
        public string SideArtistLnr { get; set; }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string ReviewReasonLnr { get; set; }

        /// <summary>
        /// Gets or sets the contract id LNR.
        /// </summary>
        /// <value>The contract id LNR.</value>
        public string ContractIdLnr { get; set; }
        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string LostRightsLnr { get; set; }

        /// <summary>
        /// Gets or sets the restriction reason for others.
        /// </summary>
        /// <value>The restriction reason for others.</value>
        public string RestrictionReasonForOthers { get; set; }

    }
}
