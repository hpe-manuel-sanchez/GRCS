using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
	public interface IClearanceRoutingWorkflowManager
	{
		bool AddRoutingDataByRoutedProject(ClearanceRoutedProject clearanceRoutedProject);
		bool ExecuteRoutingAction(ClearanceRoutedProject routedProject);
		List<LeanContractInfo> GetContractsByResourceId(long resourceId, bool isMaster);
		List<ContractRightExplotaition> GetContractsAndRightSetsByResourceId(long resourceId, List<long> contractIds);
		List<Int16> GetRoutingSalesChannelType(RequestTypeRegular requestTypeRegular, bool isMasterProject);
		List<Int16> GetWorkGroupRequestTypeBySalesChannelType(List<Int16> salesChannel);
		bool HasApprovedRoutingLog(long clrProjectId);
		bool HasApprovedRoutingLogForUMGI(long clrProjectId);
        bool ValidateStreetDate(long resourceId);
        List<ClearanceRoutingItem> ProcessRoutedItem(int numberOfItems);
		List<LeanContractInfo> GetContractsOfPrimaryArtistByResourceId(long resourceId, bool isMaster);
		bool UpdateRoutingQueueStatus(long routingQueueId, RoutingQueueStatus queueStatus, string errorDescription, ClearanceRoutedProject clearanceRoutedProject);
		bool UpdateRoutingQueueStatus(ClearanceRoutingItem routingItem, string errorDescription);

		/// <summary>
		/// To populate the List which are present in RoutingParams.cs
		/// </summary>
		/// <returns>RoutingParams object</returns>
		RoutingParams GetRoutingParameters();
		void ClearanceInitiateRouting(ClearanceProject clearanceProject, LeanUserInfo userInfo);
		List<LeanContractInfo> AddResourceRightToLeanContract(List<LeanContractInfo> leanContractInfoList, long resourceId, bool isMaster);
		List<KeyValuePair<short, short>> GetWorkGroupRequestTypeBySalesChannel(List<Int16> salesChannel);
		List<TerritorialDisplay> GetSafeAreaTerritory();
		List<long> GetResourcesFromClrProject(ClearanceRoutedProject clearanceRoutedProjec);
		bool AddSafeTerritories(List<TerritorialDisplay> territories, LeanUserInfo userInfo);
		RoutingVariationOutput GetRoutingVariationDetails(RoutingVariationInput routingVariationInput);
		List<KeyValuePair<long, bool>> GetCompanyInfoWithGlobalFlag(List<long> companyIds);
		bool SafeTerritoryCheck(long clrProjectId);
		List<RoutingRule> GetRoutingRules();
		RoutingRuleVariation LoadRuleVariations(long ruleId);
		List<TerritorialDisplay> LoadTerritories(long variationID, TerritoryType territoryType);
		string ChangeRuleStatus(long Id, StatusType statusType, ObjectType objectType, string userLoginName);
		long SaveRuleAndVariation(RoutingRuleSaveInfo ruleInfo);
		List<KeyValuePair<byte, string>> GetRoutingVariationRequestTypes();
		List<ArtistInfo> GetArtists(string artistIds);

		/// <summary>
		/// Get the details of managed contracts for routing
		/// </summary>
		/// <param name="resourceId"></param>
		/// <param name="contractId"></param>
		/// <returns></returns>
		List<LeanContractInfo> GetRoutingContractDetails(long resourceId, long contractId);

		List<long> GetPreClearanceCountries(long rightSetId);

		RoutedResourceWorkgroupDetails GetWorkgroupAndVariationDetails(RoutedResourceWorkgroupDetails routedResourceWorkgroupDetails);

		bool InsetIntoBackgroundStatusTable(long projectId, RoutingAction routingActionType, LeanUserInfo user);
		bool InsertErrorInfoIntoBackgroundTable(long projectId, RoutingAction routingActionType, LeanUserInfo user, Exception exception);
		bool IsResourceUMGIWaitingApproval(long routed_Item_Id);
		ClearanceRoutedProject GetRoutedProjectData(long routingQueueId);
		ClearanceRoutingItem ProcessRoutedItemById(long queueId);

	}
}
