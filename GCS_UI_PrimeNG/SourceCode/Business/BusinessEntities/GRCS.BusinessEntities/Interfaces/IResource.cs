using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IResource
    {
        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [OperationContract]
        ResourceSearchResult Search(ResourceSearchCriteria searchCriteria);

        /// <summary>
        /// Gets the resources.
        /// </summary>
        /// <param name="resourceIds">The resource ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        ResourceSearchResult GetResources(List<long> resourceIds, string userName);

        /// <summary>
        /// Gets the resources basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        List<RepertoireRightsSearchResult> GetResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources tracks basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        List<RepertoireRightsSearchResult> GetResourcesTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources tracks search release parameters.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        List<RepertoireRightsSearchResult> GetResourcesTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restriction resources basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionResourcesBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions resources release parameters.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsResourcesReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions tracks basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions tracks search release parameters.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions resources and tracks basic search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions resources and tracks search release parameters.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsResourcesAndTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the digital restrictions releases.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        DigitalRestrictionRights GetDigitalRestrictionsReleases(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the resources release parameter search.
        /// </summary>
        /// <param name="isSensitive">if set to <c>true</c> [is sensitive].</param>
        /// <param name="searchFilter">The search filter.</param>
        /// <returns></returns>
        [OperationContract]
        List<RepertoireRightsSearchResult> GetResourcesReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Auto search the resource title.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> AutoSearchResourceTitle(string searchTerm);

        /// <summary>
        /// Autoes the search resource artist.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="isExact">if set to <c>true</c> [is exact].</param>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> AutoSearchResourceArtist(string searchTerm, bool isExact);
    }
}