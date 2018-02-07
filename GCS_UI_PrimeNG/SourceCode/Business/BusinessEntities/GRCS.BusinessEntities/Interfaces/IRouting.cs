using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IRouting
    {
        //[OperationContract(Name = "GetClearanceProject")]
        // KeyValuePair<long, string> GetClearanceRoutedProject();
        //[OperationContract(Name = "AddRoutingDataByRoutedResource")]
        //bool AddRoutingDataByRoutedResource(RoutedResource routedResource, UserInfo userInfo);
        [OperationContract(Name = "ExecuteAction")]
        bool ExecuteAction(ClearanceRoutedProject routedProject);
        [OperationContract(Name = "GetRoutingSalesChannelType")]
        List<Int16> GetRoutingSalesChannelType(RequestTypeRegular requestTypeRegular, bool isMasterProject);
        [OperationContract(Name = "GetWorkGroupRequestTypeBySalesChannelType")]
        List<Int16> GetWorkGroupRequestTypeBySalesChannelType(List<Int16> salesChannel);
        [OperationContract(Name = "GetWorkGroupRequestTypeBySalesChannel")]
        List<KeyValuePair<short, short>> GetWorkGroupRequestTypeBySalesChannel(List<Int16> salesChannel);
        [OperationContract]
        List<TerritorialDisplay> GetSafeAreaTerritory();
        [OperationContract]
        bool AddSafeTerritories(List<TerritorialDisplay> territories, UserInfo userInfo);
		[OperationContract]
		List<RoutingRule> GetRoutingRules();
		[OperationContract]
		RoutingRuleVariation LoadRuleVariations(long ruleId);
		
		[OperationContract]
        string ChangeRuleStatus(long Id, StatusType statusType, ObjectType objectType, string userLoginName);
		[OperationContract]
		long SaveRuleAndVariation(RoutingRuleSaveInfo ruleInfo);
		[OperationContract]
		List<TerritorialDisplay> LoadTerritories(long variationID, TerritoryType territoryType);
		[OperationContract]
		List<KeyValuePair<byte, string>> GetRoutingVariationRequestTypes();
		[OperationContract]
		List<ArtistInfo> GetArtists(string artistIds);
    }
}
