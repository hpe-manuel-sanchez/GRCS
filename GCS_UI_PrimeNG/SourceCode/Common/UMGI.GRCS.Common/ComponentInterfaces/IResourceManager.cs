using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public partial interface IResourceManager
    {
        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        ResourceSearchResult Search(ResourceSearchCriteria searchCriteria);

        /// <summary>
        ///
        /// </summary>
        /// <param name="resourceInfo"></param>
        /// <returns></returns>
        long SaveResource(ResourceInfo resourceInfo);

        /// <summary>
        /// Saves the resource.
        /// </summary>
        /// <param name="resourceInfo">The resource info.</param>
        /// <returns></returns>
        long SaveResource(ResourceInfo resourceInfo, out long newResourceId);

        Dictionary<long, long> SaveResources(List<ResourceInfo> resourceInfo);

        /// <summary>
        /// Saves the resource artist.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="artistIdList">The artist id list.</param>
        /// <returns></returns>
        List<long> SaveResourceArtist(long resourceId, List<ArtistInfo> artistIdList);

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="resourceIds">The resource ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        ResourceSearchResult GetResources(List<long> resourceIds, string userName);

        /// <summary>
        /// Gets the resource details.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <returns></returns>
        R2ResourceAdditionalInfo GetResourceDetails(long resourceId);

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        ResourceSearchResult GetResource(long resourceId, string userName);

        long CheckResourceExist(long resourceId);

        List<RepertoireRightsSearchResult> GetResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetResourcesTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetResourcesTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsResourcesReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        DigitalRestrictionRights GetDigitalRestrictionsReleases(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetResourcesReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Roll Up calculation for resource
        /// </summary>
        /// <param name="resourceIdList">The resource ids</param>
        void RollUp(List<long> resourceIdList);

        /// <summary>
        /// Release Level Hirarachy For Resource
        /// </summary>
        /// <param name="resourceId">The Resource Id</param>
        /// <returns>List of resource hirarchy</returns>
        List<ReleaseHierarchy> LoadReleaseLevelHierarchyForResource(long resourceId);

        List<ResourceRights> GetResourceRightXml(string id, string searchOption);

        TrackRights GetTrackRightXml(string upc, string isrc);

        /// <summary>
        /// Auto search the resource title.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        List<StringIdentifier> AutoSearchResourceTitle(string searchTerm);

        /// <summary>
        /// Autoes the search resource artist.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="isExact">if set to <c>true</c> [is exact].</param>
        /// <returns></returns>
        List<StringIdentifier> AutoSearchResourceArtist(string searchTerm, bool isExact);

        /// <summary>
        /// Added by vivek on dated: 15-Nov-2012
        /// </summary>
        ResourceSearchResult Search(ResourceSearchCriteria searchCriteria, bool applyQualifyingCriteria);

        List<ResourceInfo> GetResourceInfo(List<long> resourceId);
    }
}