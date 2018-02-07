using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    /// <summary>
    /// Partial class implemented by GCS Team
    /// </summary>
    public partial interface IRightsData
    {

        #region "routing"
        /// <summary>
        /// Gets resource rights for the given resourceId
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="contractId"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        LeanContractInfo GetResourceRightsByContract(long resourceId, long contractId, bool isMaster);

        /// <summary>
        /// Get Territorial Rights for right Id
        /// </summary>
        /// <param name="rightId"></param>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritorialRightsForRouting(long rightId);

        #endregion
    }
}
