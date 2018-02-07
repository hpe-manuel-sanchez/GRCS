using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IResourceData
    {
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
        /// <returns>returns the SaveResource long values</returns>
        long SaveResource(ResourceInfo resourceInfo, out long newResourceId);

        Dictionary<long, long> SaveResources(List<ResourceInfo> resourceInfo);

        /// <summary>
        /// Saves the resource artist.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="artistList">The artist list.</param>
        /// <returns>returns the SaveResourceArtist long value</returns>
        List<long> SaveResourceArtist(long resourceId, List<ArtistInfo> artistList);

        /// <summary>
        /// Checks the resource exist.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>returns the CheckResourceExist long value </returns>
        long CheckResourceExist(long resourceId);

        long GetLookupTypeIdbyName(string description, string type);

        long GetLookupTypeIdbyShortName(string description, string type);

        string GetLookupName(byte? value, string type);

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
        /// Gets the resources basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>returns the RepertoireRightsSearchResult</returns>
        List<RepertoireRightsSearchResult> GetResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources tracks basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>returns the RepertoireRightsSearchResult</returns>
        List<RepertoireRightsSearchResult> GetResourcesTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources tracks release search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>returns the RepertoireRightsSearchResult</returns>
        List<RepertoireRightsSearchResult> GetResourcesTracksSearchRelease(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the Digital Restriction resources tracks basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsResourcesReleaseParameters
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsResourcesRelease(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsTracksBasicSearch
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsTracksSearchReleaseParameters
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsTracksSearchRelease(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsResourcesAndTracksBasicSearch
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracks(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsResourcesAndTracksSearchReleaseParameters
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracksSearchRelease(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// GetDigitalRestrictionsReleases
        /// </summary>
        /// <param name="isSensitive"></param>
        /// <param name="searchFilter"></param>
        /// <returns>returns the DigitalRestrictionRights</returns>
        DigitalRestrictionRights GetDigitalRestrictionsReleases(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources release search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns>returns the RepertoireRightsSearchResult</returns>
        List<RepertoireRightsSearchResult> GetResourcesReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Roll Up calculation for resource
        /// </summary>
        /// <param name="resourceIdList">The resourceId List</param>
        void RollUp(List<long> resourceIdList);

        /// <summary>
        /// Release Level Hierarchy For Resource
        /// </summary>
        /// <param name="resourceId">The Resource Id</param>
        /// <returns>List of resource hierarchy</returns>
        List<ReleaseHierarchy> LoadReleaseLevelHierarchyForResource(long resourceId);

        /// <summary>
        /// Get Resource Information.
        /// </summary>
        /// <param name="resourceId">resourceId.</param>
        /// <returns></returns>
        List<ResourceInfo> GetResourceInfo(List<long> resourceId);
    }
}