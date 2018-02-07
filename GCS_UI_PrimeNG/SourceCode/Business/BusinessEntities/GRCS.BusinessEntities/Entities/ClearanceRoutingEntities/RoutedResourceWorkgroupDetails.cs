/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RoutedResourceWorkgroupDetails.cs
 * Project      : UMG GRCS
 * Author       : Arunagiri G
 * Created on   : 09-01-2014 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare properties to get all the workgroup and variatiion details for the routed resource

****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    public class RoutedResourceWorkgroupDetails
    {
        //Input Properties
        public long ClrProjectID { get; set; }
        public long ResourceId { get; set; }
        public long ContractID { get; set; }
        public long RightSetID { get; set; }
        public long ClearanceAdminCompanyId { get; set; }
        public long RequestingCountryId { get; set; }
        public long ResourceRequestTypeId { get; set; }
        public string ProjectRequestTypeId { get; set; }
        public bool IsNationalRouting { get; set; }
        public bool IsArtistContract { get; set; }

        //Output Properties
        public int? LocalLabelWorkgroupId { get; set; }
        public int? MarketingWorkgroupId { get; set; }
        public int? LegalWorkgroupId { get; set; }
        public bool? CanSkipNational { get; set; }
        public bool? CanSkipLocalLabel { get; set; }
        public bool? CanSkipInternational { get; set; }
        public long? ProjectType { get; set; }
        public long? RoutingRuleId { get; set; }
        public long? RoutingVariationId { get; set; }
        public string RuleName { get; set; }
        public bool? IsSendEmail { get; set; }

    }
}
