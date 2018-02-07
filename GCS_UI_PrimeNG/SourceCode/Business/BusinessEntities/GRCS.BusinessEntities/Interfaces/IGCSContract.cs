using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IContract
    {
        #region "ManageArtistContract"

        /// <summary>
        /// SearchContractbyArtist this instance.
        /// </summary>
        [OperationContract]
        ArtistContractSearchResult SearchContractbyArtist(ArtistSearchCriteria artistSearchCriteria, bool isPaging, UserInfo userInfo);

        #endregion

        #region "Manage Resource Contract"

        /// <summary>
        /// SearchContractbyResource this instance.
        /// </summary>
        [OperationContract]
        ResourceContractSearchResult SearchContractbyResource(ResourceSearchCriteria resourceSearchCriteria, bool isPaging, UserInfo userInfo);

        /// <summary>
        /// GetResourceContractByContractIdList this instance.
        /// </summary>
        [OperationContract]
        List<ResourceContract> GetResourceContractByContractIdList(List<DeviationResourceContract> contractIdList, UserInfo userInfo);

        [OperationContract]
        List<LeanContractInfo> GetContractsByResource(long resourceId, string userLoginName);

        #endregion

        #region "GetArtistByContract"

        /// <summary>
        /// getArtistbyContract this instance.
        /// </summary>
        [OperationContract]
        List<ArtistContract> GetArtistByContract(List<long> contractids, string userLoginName);

        #endregion

        #region"GetContractsByArtist"
        /// <summary>
        /// Get contract Details for the given artist Id
        /// </summary>
        /// <param name="artistId">The artistID</param>
        /// <returns>Collection of contract details</returns>
        [OperationContract(Name = "GetContractsByArtist")]
        List<LeanContractInfo> GetContractsByArtist(long artistId, string userLoginName);
        #endregion

        #region"GetLeanContract"
        /// <summary>
        /// Get contract Details for the given contract Id
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetLeanContract")]
        LeanContractInfo GetLeanContract(long contractId, string userLoginName);
        #endregion

    }
}
