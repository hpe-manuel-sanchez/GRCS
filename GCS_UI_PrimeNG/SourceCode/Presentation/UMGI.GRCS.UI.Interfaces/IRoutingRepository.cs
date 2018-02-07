/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IRoutingRepository.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * R.MuthuKumar      02/27/2013      Initial Creation
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
namespace UMGI.GRCS.UI.Interfaces
{
    public interface IRoutingRepository
    {
        List<TerritorialDisplay> GetSafeAreaTerritory();
        bool AddSafeTerritories(List<TerritorialDisplay> territories, UserInfo userInfo);
		List<RoutingRule> GetRoutingRules();
		RoutingRuleVariation LoadRuleVariations(long ruleId);
        string ChangeRuleStatus(long Id, StatusType statusType, ObjectType objectType, string userLoginName);
		long SaveRuleAndVariation(RoutingRuleSaveInfo ruleInfo);
		List<TerritorialDisplay> LoadTerritories(long variationID, TerritoryType territoryType);
		List<KeyValuePair<byte, string>> GetRoutingVariationRequestTypes();
		ArtistSearchResult SelectMultiArtist(ArtistSearchCriteria searchOption, bool artistQualifyingCriteria);
		List<ArtistInfo> GetArtists(string artistIds);
    }
}
