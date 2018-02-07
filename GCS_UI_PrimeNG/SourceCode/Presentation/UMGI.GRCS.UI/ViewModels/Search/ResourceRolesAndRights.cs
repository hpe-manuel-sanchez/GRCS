using System.Collections.Generic;

namespace UMGI.GRCS.UI.ViewModels.Search
{
    public class ViewResourceRolesAndRights
    {
        public ViewResourceRolesAndRights()
        {
            ContractRolesAndRights = new List<ViewContractDetails>();
        }

        public List<ViewContractDetails> ContractRolesAndRights { get; set; }
    }

    public class ViewContractDetails
    {
        public ViewContractDetails()
        {
            ContractPreClearanceInformation = new List<ViewContractPreClearanceInformation>();
            ContractExploitationRestrictionsList = new List<ViewContractExploitationRestrictions>();
        }
        /// <summary>
        /// Gets or sets the pc notice country company id.
        /// </summary>
        /// <value>The pc notice country company id.</value>
        public string PcNoticeCountryCompany { get; set; }
        /// <summary>
        /// Gets or sets the clearing notes.
        /// </summary>
        /// <value>The clearing notes.</value>
        public string ClearingNotes { get; set; }
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsPhysicalRights.</value>
        public string IsPhysicalRights { get; set; }
        /// <summary>
        /// Gets/ Sets the look up value, LostRightsReasonId
        /// </summary>
        /// <value>The lost rights reason id.</value>
        public string IsLossRightsIndicator { get; set; }
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsDigitalRights.</value>
        public string IsDigitalRights { get; set; }
        /// <summary>
        /// Default value is null. Gets or Sets the value whether Contract is active for marketing or not
        /// </summary>
        /// <value>The is active for marketing.</value>
        public string IsActiveForMarketing { get; set; }
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>IsDigitalUnbundle.</value>
        public string IsDigitalUnbundle { get; set; }
        /// <summary>
        /// Gets or sets the is sensitive artist. Null is default
        /// </summary>
        /// <value>The is sensitive artist.</value>
        public string IsSensitiveArtist { get; set; }
        /// <summary>
        /// Gets or sets the territorial rights definition.
        /// </summary>
        /// <value>The territorial rights definition.</value>
        public string TerritorialRightsDefinition { get; set; }
        /// <summary>
        /// Gets or sets the Contract PreClearanceInformation List
        /// </summary>
        /// <value>The Contract PreClearanceInformation List.</value>
        public List<ViewContractPreClearanceInformation> ContractPreClearanceInformation { get; set; }
        /// <summary>
        /// Gets or sets the contract exploitation restrictions list.
        /// </summary>
        /// <value>The contract exploitation restrictions list.</value>
        public List<ViewContractExploitationRestrictions> ContractExploitationRestrictionsList { get; set; }
    }

    public class ViewContractPreClearanceInformation
    {
        /// <summary>
        /// PreclearanceTypeDesc
        /// </summary>
        public string PreclearanceTypeDesc { get; set; }
        /// <summary>
        /// IsPreclearance
        /// </summary>
        public string IsPreclearance { get; set; }
        /// <summary>
        /// PreClearedTerritoryExclusion
        /// </summary>
        public string PreClearedTerritoryExclusion { get; set; }
    }

    public class ViewContractExploitationRestrictions
    {
        /// <summary>
        /// Look up value description of Exploitation Type (Value => 1, Description => Multi Artist Compilation)
        /// </summary>
        public string ExploitaionTypeName { get; set; }
        /// <summary>
        /// Rights of the resource
        ///> If Restricted = Yes and Restriction = "No rights"on GRS side then display ‘No’ for the Rights column in GCS v
        ///> If Restricted = Yes and Restriction = Consent Required, on GRS side then display ‘Yes’ for the Rights column in GCS
        ///> If Restricted = Yes and Restriction = Consult, on GRS side then display ‘Yes’ for the Rights column in GCS
        ///> If Restricted = Yes and Restriction = Refer to Legal, on GRS side then display ‘Yes’ for the Rights column in GCS
        ///> If Restricted = NO, then display ‘Yes’ for the Rights column in GCS
        ///*For rest all combinations display Rights = Yes *
        /// </summary>
        public string Rights { get; set; }
    }
}