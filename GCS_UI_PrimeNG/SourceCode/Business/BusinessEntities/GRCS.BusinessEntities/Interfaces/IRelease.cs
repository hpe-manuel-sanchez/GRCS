using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IRelease
    {
        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [OperationContract]
        ReleaseSearchResult Search(ReleaseSearchCriteria searchCriteria);

        /// <summary>
        /// Gets the associated resource.
        /// </summary>
        /// <param name="linkReleaseInfo">The link release info.</param>
        /// <returns></returns>
        [OperationContract]
        ResourceSearchResult GetAssociatedResource(LinkedInfo linkReleaseInfo);

        /// <summary>
        /// Gets the associated resources.
        /// </summary>
        /// <param name="lstLinkReleaseInfo">The LST link release info.</param>
        /// <returns></returns>
        [OperationContract]
        ResourceSearchResult GetAssociatedResources(List<LinkedInfo> lstLinkReleaseInfo);

        /// <summary>
        /// Gets the releases.
        /// </summary>
        /// <param name="releaseIds">The release ids.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [OperationContract]
        ReleaseSearchResult GetReleases(List<long> releaseIds, string userName);

        [OperationContract]
        List<ResourceInfo> GetResources(List<ReleaseInfo> releaseInfo);

        [OperationContract]
        List<string> ValidateUpc(List<string> upcFromFile);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetReleasesSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ConfigurationInfo GetConfiguration(string groupCode);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ConfigurationInfo GetConfigurationGroup();

        /// <summary>
        /// UC020 Release Popup Get AutoSearch Release Title
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> AutoSearchReleaseTitle(string searchTerm);

        /// <summary>
        /// UC020 Release Popup Get AutoSearch Release Artist
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="isExact"></param>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> AutoSearchReleaseArtist(string searchTerm, bool isExact);
    }
}