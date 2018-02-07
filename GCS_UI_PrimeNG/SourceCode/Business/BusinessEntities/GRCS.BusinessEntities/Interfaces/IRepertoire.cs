using System.Collections.Generic;
using System.ServiceModel;

using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IRepertoire
    {
        /// <summary>
        /// Links the contract in back ground.
        /// </summary>
        /// <param name="releaseList">The release list.</param>
        /// <param name="resourceList">The resource list.</param>
        /// <param name="contractId">The contract id.</param>
        [OperationContract]
        void LinkContractInBackGround(List<ReleaseInfo> releaseList, List<ResourceInfo> resourceList, long contractId);

        [OperationContract]
        bool GetGcsReleaseInfo(long releaseId, string UserName);

        [OperationContract]
        void RollUpRelease();
    }
}