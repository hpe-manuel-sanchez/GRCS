using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IReleaseData
    {
        List<long> SavePackage(List<PackageInfo> packageInfo);

        List<long> SavePackage(List<PackageInfo> packageInfo, long parentReleaseId);

        List<long> SaveTrack(List<TrackInfo> trackInfo);

        long GetReleaseId(long releaseId);

        long SaveRelease(ReleaseInfo releaseInfo);

        List<long> SaveReleaseArtist(long releaseId, List<ArtistInfo> artistList);

        List<ResourceInfo> GetResources(List<ReleaseInfo> releaseInfo);

        long GetLookupTypeIdbyName(string description, string type);

        string GetLookupName(byte? value, string type);

        /// <summary>
        /// Operation for getting release information
        /// for a territorial country
        /// </summary>
        /// <param name="territorialCountryId">The territorial country id.</param>
        /// <returns>List of releases for the country</returns>
        List<ReleaseInfo> GetTerritorialReleases(long territorialCountryId);

        List<string> ValidateUpc(List<string> upcFromFile);

        List<RepertoireRightsSearchResult> GetReleasesSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        /// <summary>
        /// Roll Up calculation for release
        /// </summary>
        /// <param name="releaseIdList">The releaseid List</param>
        void RollUp(List<long> releaseIdList);

        /// <summary>
        /// Loads the release level hierarchy.
        /// </summary>
        /// <param name="repertoireId">The repertoire id.</param>
        /// <returns>Return Hierarchy list of repertoireId</returns>
        List<ReleaseHierarchy> LoadReleaseLevelHierarchy(long repertoireId);

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
        /// UC020 Release Popup Get AutoSearch Release Title
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
    }
}