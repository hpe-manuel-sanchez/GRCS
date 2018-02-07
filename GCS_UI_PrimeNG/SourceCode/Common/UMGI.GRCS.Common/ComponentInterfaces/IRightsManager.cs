using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.BusinessEntities.Entities.Templates;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IRightsManager
    {
        #region Maintain Rights Expiry Report Parameter

        List<RightsCountry> GetRightCountries(RightsCountry countryInfo, bool isCountry);

        List<RightsCountry> GetRightCountriesFilter(RightsCountry countryInfo, bool isCountry);

        List<RightsRoleGroup> GetRightsRoleGroups(RightsRoleGroup roleGroup);

        List<RightsCountry> GetTerritoryRightsCountry(RightsCountry countryDataInfo);

        List<RightsCountry> GetTerritoryRightsCountryFilter(RightsCountry countryDataInfo);

        List<RightsCountry> GetCompanies(RightsCountry companyInfo);

        #endregion Maintain Rights Expiry Report Parameter

        #region Maintain Rights Data Review Conditions

        List<StringIdentifier> GetRightsDataReviewRules();

        RightsReviewRule SaveRightsDataReview(RightsReviewRule rightsReviewRule);

        void DeleteRightsDataReview(List<long?> countryId, string userName);

        List<RightsReviewRule> SearchRightsDataReviewResult(RightsDataReviewCriteria rightsDataReviewCriteria);

        #endregion Maintain Rights Data Review Conditions

        #region Maintain Rights Repertoire

        RightsDefaultForRepertoire SaveRightsDefaultRepertoireData(RightsDefaultForRepertoire rightsDefaultForRepertoire, string userLogName);

        void DeleteRightsDefaultRepertoireData(List<long> rightsDefaultForRepertoireId, string userName);

        List<RightsDefaultForRepertoire> SearchRightsDefaultForRepertorieResults(RightsDefaultRepertoireCriteria rightsDefaultRepertoireCriteria);

        #endregion Maintain Rights Repertoire

        #region Contract Rights

        long GetRightsDataForContract(long contractId);

        long SaveResourceRightsDataSet(long rightSetId, ResourceInfo resourcesInfo);
        List<long> SaveResourceRightsDataSetNew(ResourceInfo resourcesInfo, long contractId);

        #endregion Contract Rights

        #region UnlinkResource

        long GetContractResourceRightId(long contractId, long resourceId);

        void UnlinkResourceContractRight(long rightSetId, string userName);

        #endregion UnlinkResource

        #region UnlinkRelease

        long GetContractReleaseRightId(long contractId, long releaseId);

        void UnlinkReleaseContractRight(long rightSetId, string userName);

        #endregion UnlinkRelease

        #region Territory

        List<TerritorialDisplay> GetTerritories();

        /// <summary>
        /// Loads the territory data.
        /// </summary>
        /// <param name="rightSetTemplateId">The right set template id.</param>
        /// <param name="isTemplate">is template or </param>
        /// <returns></returns>
        List<TerritorialDisplay> LoadTerritoryData(long rightSetTemplateId, bool isTemplate);

        /// <summary>
        /// Gets the territory data from Right_Set
        /// </summary>
        /// <param name="rightSetId">The right set id.</param>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritoryData(long rightSetId);

        List<TerritorialDisplay> SaveTerritories(string userName, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories, long rightSetId);

        List<TerritorialDisplay> SaveTerritoriesByContract(string userName, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories);

        #endregion Territory

        #region Downstream

        ContractRights BuildContractRight(long contractId);

        ContractRights BuildContractRightXml(long contractId, bool notify);

        ProjectRights BuildProjectRight(long projectId);

        ProjectRights BuildProjectRightXml(long projectId, bool notify);

        ProjectRights GetProjectRightXml(string r2ProjectId);

        ArtistRights BuildArtistRight(long artistId);

        ArtistRights BuildArtistRightXml(long artistId, bool notify);

        ArtistRights GetArtistRightXml(string r2TalentNameId);

        ResourceRights BuildResourceRight(long resourceId);

        ResourceRights BuildResourceRightXml(long resourceId, bool notify);

        List<ResourceRights> GetResourceRightXml(string id, string searchOption);

        ReleaseRights BuildReleaseRight(long releaseId);

        ReleaseRights BuildReleaseRightXml(long releaseId, bool notify);

        List<ReleaseRights> GetReleaseRightXml(string id, string searchOption);

        TrackRights BuildTrackRight(long trackId);

        TrackRights BuildTrackRightXml(long trackId, bool notify);

        TrackRights GetTrackRightXml(string upc, string isrc);

        #endregion Downstream

        List<string> ValidateIsrcFromFile(List<string> isrcFromFile);

        List<RepertoireRightsSearchResult> GetPreClearedBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetSecondaryExploitationBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetPreClearedReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetSecondaryExploitationReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);



        void SaveDefaultTrackRights(List<TrackInfo> trackIds, long companyId, string countryCode, string userName);

        void ProcessDefaultRights(long trackId, long companyId, string countryCode, string userName, long resourceId);

        long SaveReleaseRightsDataSet(long rightSetId, ReleaseInfo releaseInfo);

        string GetRightsDataReviewConditon(long resourceId, long companyId, long contractId);

        long SaveReleaseRightsSet(
            long rightSetId, MPReleaseRights mediaPortalRights,
            string userName, bool isMediaPortal, string repertoireType, byte? reviewStatus);

        void SaveDefaultMacReleaseRights(long releaseId, long companyId, long countryId, string userName);

        /// <summary>
        /// Method to get calculate rollup
        /// for release
        /// </summary>
        /// <param name="releaseId">ReleaseId</param>
        /// <returns>result</returns>
        long RollUpRelease(long releaseId);

        /// <summary>
        /// Method to get calculate rollup
        /// for resource
        /// </summary>
        /// <param name="resourceId">resourceId</param>
        /// <returns>result</returns>
        long RollUpReleaseForResource(long resourceId);

        #region Contract Rights

        long SaveRightSet(ContractDetails contractDetails);

        void SaveRightSetEdited(long rightSetId, bool activeMarketingManually, DateTime requestDateTime, string userName);

        void SaveContractRights(long contractId, long rightSetId, string userName, DateTime requestDateTime);

        void SaveTerritoriesByUser(string userName, List<TerritorialDisplay> list, long rightSetId, DateTime requestDateTime);

        void SaveRightAcquired(ContractDetails contractDetails, long rightSetId, string userName);

        List<long> SaveDigitalRestrictions(List<ContractDigitalRestrictions> list, long rightSetId, string userName, DateTime requestDateTime);

        void SaveExploitationRestrictions(List<ContractExploitationRestrictions> list, long rightSetId, string userName, DateTime requestDateTime);

        #endregion Contract Rights

        #region Lookups

        /// <summary>
        /// Gets the rights periods.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetRightsPeriods();

        /// <summary>
        /// Gets the lost rights reasons.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetLostRightsReasons();

        /// <summary>
        /// Gets the rights default types.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetRightsDefaultTypes();

        /// <summary>
        /// Gets the Review reasons
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetReviewReasons();

        /// <summary>
        /// Gets the review status
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> GetRightsReviewStatuses();

        List<StringIdentifier> GetSecondaryExploitations();

        List<StringIdentifier> GetPreClearances();

        List<StringIdentifier> GetResourceType();

        List<StringIdentifier> GetRightTypes();

        DigitalRestrictions LoadDefaultDigitalRestriction();

        RightsAndRestrictions LoadDefaultRightsAndRestriction();

        SecondaryExploitationDefaults LoadDefaultSecondaryExploitation();

        PreClearanceMasterData LoadDefaultPreClearanceMasterData();

        ReviewRightsMasterInfo LoadDefaultReviewRightsMasterInfo();

        #endregion Lookups

        void DeleteRightSet(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteContractRights(long contractId, long rightSetId, string userName, DateTime requestDateTime);

        void DeleteRightsAcquired(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteTerritories(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteDigitalRestrictions(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteExploitations(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteCountries(long rightSetId, string userName, DateTime requestDateTime);

        #region Rights Review

        ReleaseRightsResult GetReleaseRightsWQ(ReleaseFilterParameters filter);

        ResourceRightsResult GetResourceRightsWQ(ResourceFilterParameters filter);

        List<long> SaveReviewedReleaseRights(ReleaseRightsSaveInfo releaseRights);

        List<long> SaveReviewedResourceRights(ResourceRightsSaveInfo resourceRights);

        List<long> SaveResourceSecondaryRights(SecondaryRightsSaveInfo updateRights);

        List<long> SaveResourcePreClearanceRights(PreClearanceSaveInfo preClearanceSaveInfo);

        List<long> SaveResourceDigitalRights(ResourceDigitalSaveInfo resourceDigitalSaveInfo);

        ResourceDigitalRightsResult LoadResourceDigitalRights(ResourceFilterParameters filter);

        ResourceDigitalRightsResult GetResourceDigitalRightsPredefined(PreDefinedParametersWQ filter);

        ReviewRightsMasterInfo GetRightsAcquiredMasterData();

        SecondaryRightsMasterData LoadSecondaryRightsMasterData();

        PreClearanceMasterData LoadPreClearanceMasterData();

        ResourceSecondaryRightsResult LoadResourceSecondaryRights(ResourceFilterParameters filter);

        ResourceSecondaryRightsResult GetResourceSecondaryRightsPredefined(PreDefinedParametersWQ filter);

        ResourcePreClearanceResult GetResourcesPreclearancePredefined(PreDefinedParametersWQ filter);

        DigitalRightsMasterData LoadDigitalRightsMasterData();

        ResourcePreClearanceResult LoadResourcePreClearanceInfo(ResourceFilterParameters filter);

        ReleaseDigitalRightsResult LoadReleaseDigitalRights(ReleaseFilterParameters filter);

        ReleaseDigitalRightsResult LoadReleaseDigitalRightsPredefined(PreDefinedParametersWQ filter);

        List<long> SaveReleaseDigitalRights(ReleaseDigitalSaveInfo rights);

        ResourceRightsResult LoadResourceRightsWQPredefined(PreDefinedParametersWQ filter);

        #endregion Rights Review

        #region Routing

        /// <summary>
        /// Gets resource rights for the given resourceId
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="contractId"></param>
        /// <param name="isMaster"></param>
        /// <returns></returns>
        LeanContractInfo GetResourceRightsByContract(long resourceId, long contractId, bool isMaster);

        /// <summary>
        /// Get Territorial Rights for right Id
        /// </summary>
        /// <param name="rightId"></param>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritorialRightsForRouting(long rightId);

        #endregion Routing
    }
}