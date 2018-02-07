using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IRepertoireProcessor
    {
        void IsContractExists(long contractId);

        void ProcessReleaseResource(List<ReleaseInfo> releaseList, List<ResourceInfo> resourceList, long contractId);

        void ProcessReleaseResource(List<ReleaseInfo> releaseList, List<ResourceInfo> resourceList, long contractId, GCSInfo GCSLinkInfo);

        void ProcessReleases(ReleaseInfo releaseInfo, long contractId, long rightSetId);

        void ProcessResources(ResourceInfo resourceInfo, long contractId, long rightSetId, WorkQueueCriteria workqueueCriteria);

        void AutoLinkResourceReleasetoContract(ResourceInfo resourceInfo, ReleaseInfo releaseInfo, long? contractId);

        void DeletePriorityWqForResourceRelease(List<TaskInfo> tasks);

        void DeleteResolvedConflictsInPriorityWorkQueue(List<TaskInfo> tasks);

        void SaveContractRepertoireDetails(long contractId, List<ReleaseInfo> releaseList, List<ResourceInfo> resourceList);

        void SaveProjectRepertoireDetails(long projectId, string userName);

        long GetRightsDataForContract(long contractId);

        bool GetGcsReleaseInfo(long releaseId, string userName);

        void RollUpRelease();

        void LinkPacakageTrackbackground();

        void SavePacakageTrackinbackground(long projectId, string userName, long adminCompanyId);

    }
}