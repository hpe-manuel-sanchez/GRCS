using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.Data.Entities.RoutingEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceRoutingWorkflowData
    {
        bool AddRoutingDataByRoutedProject(ClearanceRoutedProject clearanceRoutedProject);

        bool AddRoutingQueue(string xmlRoutedProjectData, ClearanceRoutedProject routedProject);

        /// <summary>
        /// Returns Routing SalesChannel Types for regular type project
        /// </summary>
        /// <param name="requestTypeRegular"></param>
        /// <param name="isMasterProject"></param>
        /// <returns></returns>
        List<Int16> GetRoutingSalesChannelType(RequestTypeRegular requestTypeRegular, bool isMasterProject);

        /// <summary>
        /// Returns Workgroup request types for sales channel types
        /// </summary>
        /// <param name="salesChannel"></param>
        /// <returns></returns>
        List<Int16> GetWorkGroupRequestTypeBySalesChannelType(List<Int16> salesChannel);

        List<ClearanceRoutingItem> ProcessRoutedItem(int numberOfItems);
        bool UpdateRoutingQueueStatus(long routingQueueId, RoutingQueueStatus queueStatus, string errorDescription, ClearanceRoutedProject clearanceRoutedProject);
        bool UpdateRoutingQueueStatus(ClearanceRoutingItem routingItem, string errorDescription);

        bool ExecuteInboxAction(ClearanceRoutedProject clearanceRoutedProject,
                                                RequestStatus requestStatus);

        bool ExecuteProjectAction(ClearanceRoutedProject clearanceRoutedProject,
                                                          RoutingAction routingAction);
        /// <summary>
        /// To get the Third Party Company Ids from database when there is no data in the cache.
        /// </summary>
        /// <returns>RoutingParams object</returns>
        List<long> GetThridPartyCompanyIds();

        /// <summary>
        /// To get the Excluded Country Ids from the database when there is no data in the cache.
        /// </summary>
        /// <returns></returns>
        List<long> GetExcludedCountryIds();

        /// <summary>
        /// To get the Excluded Company Ids from the database when there is no data in the cache.
        /// </summary>
        /// <returns></returns>
        List<long> GetExcludedComapnyIds();
        bool IsResourceUMGIWaitingApproval(long routed_Item_Id);

        ClearanceRoutedProject RoutingFieldFromDataBase(ClearanceRoutedProject routedProjectentity, ClearanceProject clearanceProject, LeanUserInfo userinfo);
        bool UpdateRoutingFlagforResourceRelease(ClearanceProject clearanceProject, ClearanceRoutedProject routedProjectentity);
        List<TrackInfo> GetTrackDetails(long releaseId);

        bool UpdateRequestStatusByReApplyRoutingAction(ClearanceRoutedProject clearanceRoutedProject);
        bool AddInboxNotificationData(Request routedRequest, string userLoginName, long clrProjectId, RoutingNotificationEnum notificationType, long toWorkgroupId);
        List<KeyValuePair<short, short>> GetWorkGroupRequestTypeBySalesChannel(List<Int16> salesChannel);
        List<TerritorialDisplay> GetSafeAreaTerritory();
        List<long> GetResourcesFromClrProject(long clrProjectId);
        bool AddSafeTerritories(List<TerritorialDisplay> territories, LeanUserInfo userInfo);
        RoutingVariationOutput GetRoutingVariationDetails(RoutingVariationInput routingVariationInput);
        bool SafeTerritoryCheck(long clrProjectId);
        List<RoutingRule> GetRoutingRules();
        bool HasApprovedRoutingLog(long clrProjectId);
        bool HasApprovedRoutingLogForUMGI(long clrProjectId);
        RoutingRuleVariation LoadRuleVariations(long ruleId);
        string ActiveRule(long Id, string userLoginName);
        string ActiveRuleVariation(long Id, string userLoginName);
        bool DeactivateRule(long ruleID, string userLoginName);
        bool DeactivateRuleVariation(long variationID, string userLoginName);
        ClearanceRoutingItem ProcessRoutedItemById(long queueId);
        long SaveRule(RoutingRule ruleInfo);
        long SaveVariations(RoutingRuleSaveInfo ruleInfo);
        List<TerritorialDisplay> LoadTerritories(long variationID, TerritoryType territoryType);
        bool UpdateRule(RoutingRule ruleInfo);
        bool UpdateVariations(RoutingRuleSaveInfo ruleInfo);
        List<KeyValuePair<byte, string>> GetRoutingVariationRequestTypes();
        List<ArtistInfo> GetArtists(string artistIds);
        bool RemoveRuleVariation(long variationID, string userLoginName);

        List<long> GetFreehandResources(List<long> resourceIdList);

        List<long> GetPreClearanceCountries(long rightSetId);

        RoutedResourceWorkgroupDetails GetWorkgroupAndVariationDetails(RoutedResourceWorkgroupDetails routedResourceWorkgroupDetails);

        bool InsetIntoBackgroundStatusTable(long projectId, RoutingAction routingActionType, LeanUserInfo user);
        bool InsertErrorInfoIntoBackgroundTable(long projectId, RoutingAction routingActionType, LeanUserInfo user, Exception exception);
        bool PerformRouteActionFromRCCAdmin(ClearanceRoutedProject routedProject);
        List<KeyValuePair<long, bool>> GetResourceLevelChangesforRoutingInput(List<long> resourceIds, long clrProjectId);

        ClearanceRoutedProject GetRoutingProjectData(long routingQueueId);

    }
}
