using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IProjectManager
    {
        ProjectSearchResult Search(ProjectSearchCriteria searchCriteria);

        ReleaseSearchResult GetAssociatedRelease(LinkedInfo linkProjectInfo);

        ResourceSearchResult GetAssociatedResource(LinkedInfo linkProjectInfo);

        ReleaseResourcesInfo GetAssociatedReleaseResource(LinkedInfo linkProjectInfo);

        List<long> SaveProjectArtist(List<ArtistInfo> artistList, long projectId, string userName, DateTime requestedDateTime);

        long SaveProjectContract(long projectId, long contractId, string userName, DateTime requestedDateTime);

        //long SaveProject(ProjectInfo projectInfo);

        ProjectInfo GetprojectInfo(long projectId);

        long SaveProject(long id, long r2AccountId, string projectCode, long adminCompanyId, string title, bool isContractRequired, long labelId, string userName, DateTime requestDateTime);

        long SaveProjectRelease(long projectId, long releaseId, string userName);

        long SaveProjectResource(long projectId, long resourceId, string userName);

        ProjectSearchResult GetProjects(List<long> projectIds, string userName);

        ProjectSearchResult GetProject(long projectId, string userName);

        bool IsAlreadyLinkedContract(long projectid);

        long? GetAssociatedContractId(long projectid);

        bool CheckProjectExist(long projectid);

        bool CheckProjectReleaseExist(long projectId, long releaseId);

        ProjectRights GetProjectRightXml(string r2ProjectId);

        List<long> SaveProjectResources(List<long> resourceList, long projectId, string userName);

        List<long> SaveProjectReleases(List<long> releaseList, long projectId, string userName);

        Dictionary<long, string> CreateProject(R2Project project, long clrProjectId, UserInfo userInfo);
    }
}