using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.MediaPortalEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    /// <summary>
    ///
    /// </summary>
    public partial interface IRightsData
    {

        #region "Maintain Rights Expiry Report Parameter"

        List<RightsRoleGroup> GetRightsRoleGroups(RightsRoleGroup roleGroup);

        List<RightsCountry> GetTerritoryRightsCountry(RightsCountry countryDataInfo);

        List<RightsCountry> GetTerritoryRightsCountryFilter(RightsCountry countryDataInfo);

        List<RightsCountry> GetCompanies(RightsCountry companyInfo);

        List<RightsCountry> GetCompaniesFilter(RightsCountry companyInfo);

        List<RightsCountry> GetCountriesFilter(RightsCountry countryInfo);

        #endregion "Maintain Rights Expiry Report Parameter"

        #region "Maintain Rights Data Review Conditions"

        void DeleteRightsDataReview(List<long?> countryId, string userName, DateTime requestDateTime);

        List<RightsReviewRule> SearchRightsDataReviewResult(RightsDataReviewCriteria rightsDataReviewCriteria);

        #endregion "Maintain Rights Data Review Conditions"

        #region "Maintain Rights Repertoire"

        RightsDefaultForRepertoire SaveRightsDefaultRepertoireData(
            RightsDefaultForRepertoire rightsDefaultForRepertoire, string userLogName, DateTime requestDateTime);

        void DeleteRightsDefaultRepertoireData(List<long> rightsDefaultForRepertoireId, string userName, DateTime requestDateTime);

        List<RightsDefaultForRepertoire> SearchRightsDefaultForRepertorieResults(
            RightsDefaultRepertoireCriteria rightsDefaultRepertoireCriteria);

        #endregion "Maintain Rights Repertoire"

        #region Saving Rights data as part of Saving Contract

        //long SaveRightSet(ContractDataEntities contractEntities, ContractDetails contractDetails, UserInfo userInfo);
        long SaveRightSet(ContractDetails contractDetails);

        void SaveRightSetEdited(long rightSetId, bool activeMarketingManually, DateTime requestDateTime, string userName);

        void SaveContractRights(long generatedContractId, long rightSetId, string userName, DateTime requestDateTime);

        void SaveRightAcquired(ContractDetails contractDetails, long rightSetId, string userName);

        List<long> SaveDigitalRestrictions(List<ContractDigitalRestrictions> lstContractDigitalRestrictions, long rightSetId, string userName, DateTime requestDateTime);

        void SaveExploitationRestrictions(IEnumerable<ContractExploitationRestrictions> lstContractExploitationRestrictions, long rightSetId, string userName, DateTime requestDateTime);

        void SaveTerritoriesByUser(string userName, List<TerritorialDisplay> territories, long rightSetId, DateTime requestDateTime);

        #endregion Saving Rights data as part of Saving Contract

        # region "Contract"

        long GetRightsDataForContract(long contractId);

        long SaveReleaseResourceRights(
            long rightSetId,
            MPReleaseRights mediaPortalRights,
            string userName,
            bool isMediaPortal,
            string repertoireType,
            byte? reviewStatus);

        long SaveResourceRightDataset(long rightSetId, ResourceInfo resourceInfo);

        List<long> SaveResourceRightDatasetNew(ResourceInfo resourceInfo, long contractId);

        #endregion "UC014"

        #region Unlink Repertoire

        long GetContractResourceRightId(long contractId, long resourceId);

        long GetContractReleaseRightId(long contractId, long releaseId);

        void UnlinkExploitation(long rightSetId, string userName);

        void UnlinkRightAcquired(long rightSetId, string userName);

        void UnlinkRestriction(long rightSetId, string userName);

        void UnlinkRightCountry(long rightSetId, string userName);

        void UnlinkRightTerrority(long rightSetId, string userName);

        void UnlinkRightPreclearance(long rightSetId, string userName);

        void UnlinkRightPreclearanceCountry(long rightSetId, string userName);

        void UnlinkRightPreclearanceTerrority(long rightSetId, string userName);

        void UnlinkResourceContractRightSet(long rightSetId, string userName);

        void UnlinkRepertoireRightSet(long rightSetId, string userName);

        void UnlinkReleaseContractRightSet(long rightSetId, string userName);

        #endregion Unlink Repertoire

        #region "Territory"

        List<TerritorialDisplay> GetTerritories();

        /// <summary>
        /// Loads the territory data.
        /// </summary>
        /// <param name="rightSetTemplateId">The right set template id.</param>
        /// <param name="isTemplate">is template or </param>
        /// <returns></returns>
        List<TerritorialDisplay> LoadTerritoryData(long rightSetTemplateId, bool isTemplate);

        /// <summary>
        /// Gets the territory details from Right_Set
        /// </summary>
        /// <param name="rightSetId">The right set id.</param>
        /// <returns></returns>
        List<TerritorialDisplay> GetTerritoryDetails(long rightSetId);

        List<TerritorialDisplay> SaveTerritories(string userName, DateTime requestDateTime, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories, long rightSetId);

        List<TerritorialDisplay> SaveTerritoriesByContract(string userName, DateTime requestDateTime, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories);

        #endregion "Territory"

        /// <summary>
        /// Operation for getting rights release information
        ///     for a release
        /// </summary>
        /// <param name="releaseInfo">Set of releases
        ///     to fetch the rights info from
        ///      <see cref="GetReleaseRightInformation"/></param>
        /// <returns>List of rights info for the releases</returns>
        List<RightSetDetail> GetReleaseRightInformation(List<ReleaseInfo> releaseInfo);


        List<string> ValidateIsrcFromFile(List<string> isrcFromFile);

        List<RepertoireRightsSearchResult> GetPreClearedBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetSecondaryExploitationBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetPreClearedReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        List<RepertoireRightsSearchResult> GetSecondaryExploitationReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        string GetRightsDataReviewConditons(long resourceId, long companyId, long contractId);

        void InsertDefaultTrackRights(long trackId, long companyId, string countryCode, string userName, long resourceId);

        long SaveReleaseRightsDataSet(long rightSetId, ReleaseInfo releaseInfo);

        void InsertMacReleaseRights(long releaseId, long companyId, long countryId, string userName);

        #region Downstream

        List<ExploitationDataSet> GetExploitationDataSets(List<long> rightSetIdList);

        List<RestrictionDataSet> GetRestrictionDataSet(long rightSetId);

        List<RightAcquiredDataSet> GetAvailableRightsDataSets(List<long> rightSetId);

        List<RightAcquiredDataSet> GetDeal360DataSets(List<long> rightSetIdList);

        RightDataSet GetRightDataSet(long rightSetId);

        List<string> GetCountryCodes(long rightSetId);

        ContractDataSet GetContractDataSet(long contractId);

        string GetXmlfromInterfaceLog(long itemId, byte itemType);

        ProjectDataSet GetProjectDataSet(long projectId);

        ArtistDataSet GetArtistDataSet(long artistId);

        long UpdateInterfaceLog(string xmlString, long itemId, byte itemType);

        bool GetLostRightIndicator(long rightSetId);

        ResourceDataSet GetResourceDataSet(long resourceId);

        ReleaseDataSet GetReleaseDataSet(long releaseId);

        TrackDataSet GetTrackDataSet(long trackId);

        #region PKID

        List<long> GetPkIdForArtist(long r2TalentNameId);

        List<long> GetPkIdForTrack(string upc, string isrc);

        List<long> GetPkIdForResource(string id, string searchOption);

        List<long> GetPkIdForRelease(string id, string searchOption);

        List<long> GetPkIdForProject(string r2ProjectId);

        #endregion

        #endregion

        #region Lookups

        /// <summary>
        /// Loads the rights periods.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadRightsPeriods();

        /// <summary>
        /// Loads the lost rights reasons.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadLostRightsReasons();

        /// <summary>
        /// Gets the rights default types.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadRightsDefaultTypes();

        /// <summary>
        /// Loads the type of the content.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadContentType();

        /// <summary>
        /// Loads the use type model.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadUseType();

        /// <summary>
        /// Loads the type of the commercial model.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadCommercialType();

        /// <summary>
        /// Loads the consent type model.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadConsentType();

        /// <summary>
        /// Loads the restrictions.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadRestrictions();

        /// <summary>
        /// Loads the acquired status.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadAcquiredStatus();

        /// <summary>
        /// Loads the rights and restrictions.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadRightsAndRestrictions();

        /// <summary>
        /// Loads the secondary exploitation.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadSecondaryExploitation();

        /// <summary>
        /// Loads the type of the resource.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadResourceType();

        /// <summary>
        /// Loads the review reason.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadReviewReason();

        /// <summary>
        /// Loads the review status.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadReviewStatus();

        /// <summary>
        /// Loads the release pre defined parameters.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadReleasePreDefinedParameters();

        /// <summary>
        /// Loads the resource pre defined parameters.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadResourcePreDefinedParameters();

        /// <summary>
        /// Loads the clearance master data.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadClearanceMasterData();

        /// <summary>
        /// Loads the Preclerance types
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadPreClearances();

        /// <summary>
        /// Loads the review condition.
        /// </summary>
        /// <returns></returns>
        List<StringIdentifier> LoadReviewConditions();

        #endregion Lookups

        #region Rights Review

        ReleaseRightsResult GetReleaseRightsWQ(ReleaseFilterParameters filter);

        ResourceRightsResult GetResourceRightsWQ(ResourceFilterParameters filter);

        List<ReviewRightSaveResult> SaveReviewedReleaseRights(ReleaseRightsSaveInfo releaseRights);

        List<ReviewRightSaveResult> SaveReviewedResourceRights(ResourceRightsSaveInfo resourceRights);

        List<ReviewRightSaveResult> SaveResourceSecondaryRights(SecondaryRightsSaveInfo updateRights);

        List<ReviewRightSaveResult> SaveResourcePreClearanceRights(PreClearanceSaveInfo preClearanceSaveInfo);

        ResourceSecondaryRightsResult LoadResourceSecondaryRights(ResourceFilterParameters filter);

        ResourcePreClearanceResult LoadResourcePreClearanceInfo(ResourceFilterParameters filter);

        ReleaseDigitalRightsResult LoadReleaseDigitalRights(ReleaseFilterParameters filter);

        List<ReviewRightSaveResult> SaveReleaseDigitalRights(ReleaseDigitalSaveInfo rights);

        List<ReviewRightSaveResult> SaveResourceDigitalRights(ResourceDigitalSaveInfo resourceDigitalSaveInfo);

        ResourceDigitalRightsResult LoadResourceDigitalRights(ResourceFilterParameters filter);

        List<StringIdentifier> LoadRestrictionReason();

        #endregion

        void DeleteRightSet(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteContractRights(long contractId, long rightSetId, string userName, DateTime requestDateTime);

        void DeleteRightsAcquired(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteTerritories(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteDigitalRestrictions(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteExploitations(long rightSetId, string userName, DateTime requestDateTime);

        void DeleteRightCountries(long rightSetId, string userName, DateTime requestDateTime);

        bool HasRightsReview(long? companyId, long? countryId, out long existingReviewId);

        RightsReviewRule UpdateLookupRightsReview(RightsReviewRule rightsReviewRule);

        RightsReviewRule SaveLookupRightsReview(RightsReviewRule rightsReviewRule);

        bool TrackIsExist(long trackId);
    }
}