using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IContractManager
    {
      
        #region GetArtistbyContract
        /// <summary>
        /// Get contract Details for the given artist Id
        /// </summary>
        /// <param name="contractids">List of Contact Id's</param>
        /// <returns>Collection of ArtistContract details</returns>
        /// 
        List<ArtistContract> GetArtistByContract(List<long> contractids);

        #endregion

        #region ManageArtistContract

        ArtistContractSearchResult SearchContractbyArtist(ArtistSearchCriteria artistSearchCriteria, bool isPaging);

        #endregion

        #region ManageResourceContract

        ResourceContractSearchResult SearchContractbyResource(ResourceSearchCriteria resourceSearchCriteria, bool isPaging);

        List<ResourceContract> GetResourceContractByContractIdList(List<DeviationResourceContract> contractIdList);

        List<LeanContractInfo> GetContractsByResource(long resourceId);
        #endregion

        #region"GetContractsByArtist"
        /// <summary>
        /// Get contract Details for the given artist Id
        /// </summary>
        /// <param name="artistId">The artistID</param>
        /// <returns>Collection of contract details</returns>
        List<LeanContractInfo> GetContractsByArtist(long artistId);
        #endregion

        #region"GetLeanContract"
        /// <summary>
        /// Get contract Details for the given contract Id
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        LeanContractInfo GetLeanContract(long contractId);
        #endregion

        #region "Routing"
        List<LeanContractInfo> GetContractsByResourceId(long resourceId);
        List<ContractRightExplotaition> GetContractsAndRightSetsByResourceId(long resourceId, List<long> contractIds);
        List<LeanContractInfo> GetContractsOfPrimaryArtistByResourceId(long resourceId);
        List<LeanContractInfo> GetResourceContractDetails(long resourceId, long contractId);
        List<LeanContractInfo> GetArtistContractDetails(long resourceId, long contractId);
        List<LeanContractInfo> GetContractAndRightDetails(long resourceId, long contractId);
        #endregion

        #region Clearance Project search contract
        List<ContractDetails> ClearanceContractSearch(ContractDetails contractSearch, UserInfo userInformation);
        List<ContractDetails> GetContractDetailOnArtistid(List<long> artistId, List<long> contractId, long resourceId, UserInfo userInformation);

        #endregion

        #region GetResourceRights

        List<ContractDetails> GetResourceRights(long r2resourceId);

        #endregion

    }
}
