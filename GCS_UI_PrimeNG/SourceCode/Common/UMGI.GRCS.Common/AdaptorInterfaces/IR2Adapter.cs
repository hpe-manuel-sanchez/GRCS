using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;

namespace UMGI.GRCS.Common.AdaptorInterfaces
{
    public interface IR2Adapter
    {
        #region Company

        R2Companies SearchPcNoticeCompany(NoticeCompanySearch searchOption);

        #endregion

        #region Artists

        R2Artists SearchArtists(ArtistSearchCriteria searchOption);
        R2Artists GetArtist(long artistId);

        #endregion

        #region Resource

        R2Resources GetResource(long resourceId);
        R2Resources GetResources(List<long> resourceIds);
        string GetResourceXml(long resourceId);
        R2Resources SearchResources(ResourceSearchCriteria searchOption);
        R2Resources GetResourcesByProject(long projectId);
        R2Resources GetResourcesByProjectNoOriginalProjectFilter(long projectId);
        R2Resources GetResourcesByRelease(long releaseId);
        R2Resources GetResourcesByProjects(List<long> projectIds);
        R2Resources GetResourcesByReleases(List<long> releaseIds);
        R2Resources GetResourcesByRelease(long releaseId, bool excludeNonTracks);
        R2Resources GetResourcesByReleases(List<long> releaseIds, bool excludeNonTracks);
        R2ResourceAdditionalInfo GetResourceAdditionalDetails(long resourceId);
        bool IsMediaPortalResource(long resourceId);
        ResourceCompletenessCheckResult GetResourceCompletenessCheck(long resourceCompanyLinkId);
        R2RepertoireResult LinkResourceToProject(long projectId, string projectCode, long resourceId, string isrc);
        R2RepertoireResult SendNewRelease(ClearanceRelease release);

        #endregion

        #region Project

        R2Projects GetProject(long projectId);
        R2Projects GetProjects(List<long> projectIds);
        R2Projects SearchProjects(ProjectSearchCriteria projectSearchCriteria);
        string GetProjectXml(long projectId);
        R2ProjectAdditionalInfo GetProjectAdditionalDetails(long projectId);
        Dictionary<long, string> CreateProject(R2Project project);
        Dictionary<long, List<string>> GetResourcesOfProject(long projectId);
        bool IsReleaseContentFinal(long projectId);
        List<ArtistInfo> GetArtistsOfProject(long projectId);
        #endregion

        #region Release

        List<TrackInfo> GetComponentTracksOfRelease(long releaseId);
        R2Releases GetRelease(long releasId);
        R2Releases GetReleases(List<long> releasIds);
        R2Releases SearchReleases(ReleaseSearchCriteria releaseSearchCriteria);
        R2Releases GetReleasesByProject(long projectId);
        R2Releases GetReleasesByProjectNoOriginalProjectIdFilter(long projectId);
        R2Releases GetReleasesByProjects(List<long> projectIds);
        R2Releases GetReleasesByResource(long resourceId);
        R2Releases GetReleasesByResources(List<long> resourceIds);
        R2ReleaseAdditionalInfo GetReleaseAdditionalDetails(long releaseId);
        R2ReleaseAdditionalInfo GetReleaseAdditionalDetails(long releaseId, bool includeAllReleasesInAllPackages);

        #endregion
    }
}