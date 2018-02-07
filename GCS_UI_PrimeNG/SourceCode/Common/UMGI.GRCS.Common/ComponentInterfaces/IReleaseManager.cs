using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    /// <summary>
    /// Represents the operations for Release related information
    /// </summary>
    public partial interface IReleaseManager
    {
        ReleaseSearchResult Search(ReleaseSearchCriteria searchCriteria);

        ResourceSearchResult GetAssociatedResource(LinkedInfo linkReleaseInfo);

        DateTime? GetEarliestReleaseDate(string upc, string isoCode, ReleaseType releaseType);

        List<long> SavePackage(List<PackageInfo> packageInfo);

        List<long> SavePackage(List<PackageInfo> packageInfo, long parentReleaseId);

        List<long> SaveTrack(List<TrackInfo> trackInfo);

        List<long> SaveTrack(List<TrackInfo> trackInfo, long releaseId);

        long GetReleaseId(long releaseId);

        long SaveRelease(ReleaseInfo releaseInfo);

        List<long> SaveReleaseArtist(long releaseId, List<ArtistInfo> artistList);

        R2ReleaseAdditionalInfo GetReleaseDetails(long releaseId);

        ReleaseSearchResult GetReleases(List<long> releaseIds, string userName);

        ResourceSearchResult GetAssociatedResources(List<LinkedInfo> lstLinkReleaseInfo);

        List<ResourceInfo> GetResources(List<ReleaseInfo> releaseInfo);

        /// <summary> Operation for getting release information for a territorial country
        /// </summary>
        /// <param name="territorialCountryId">
        ///     represents the country id for
        ///     <see cref="GetTerritorialReleases"/> method</param>
        /// <returns>List of releases for the country</returns>
        List<ReleaseInfo> GetTerritorialReleases(long territorialCountryId);

        List<string> ValidateUpc(List<string> upcFromFile);

        List<RepertoireRightsSearchResult> GetReleasesSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Roll Up calculation for release
        /// </summary>
        /// <param name="releaseIdList">The releaseId List</param>
        void RollUp(List<long> releaseIdList);

        /// <summary>
        /// Loads the release level hierarchy.
        /// </summary>
        /// <param name="releaseId">The release id.</param>
        /// <returns></returns>
        List<ReleaseHierarchy> LoadReleaseLevelHierarchy(long releaseId);

        List<ReleaseRights> GetReleaseRightXml(string id, string searchOption);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        ConfigurationInfo GetConfiguration(string groupCode);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        ConfigurationInfo GetConfigurationGroup();

        /// <summary>
        /// UC020 Release popup
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        List<StringIdentifier> AutoSearchReleaseTitle(string searchTerm);

        /// <summary>
        /// UC020 Release Popup Get AutoSearch Release Artist
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="isExact"></param>
        /// <returns></returns>
        List<StringIdentifier> AutoSearchReleaseArtist(string searchTerm, bool isExact);

        ReleaseSearchResult GetRelease(long releaseId, string userName);

        ReleaseSearchResult GCSReleaseSearch(ReleaseSearchCriteria searchCriteria, bool applyQualifyingCriteria);

        List<long> GCS_SaveTrack(List<TrackInfo> trackInfo);
    }
}