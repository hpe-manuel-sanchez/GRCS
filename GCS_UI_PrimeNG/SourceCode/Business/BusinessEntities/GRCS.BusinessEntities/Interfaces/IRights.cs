
using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IRights
    {
        #region "Maintain Rights Expiry Report Parameter"

        [OperationContract]
        List<RightsCountry> GetRightCountries(RightsCountry countryInfo, bool isCountry);

        [OperationContract]
        List<RightsCountry> GetRightCountriesFilter(RightsCountry countryInfo, bool isCountry);

        [OperationContract]
        List<RightsRoleGroup> GetRightsRoleGroups(RightsRoleGroup roleGroup);

        [OperationContract]
        List<RightsCountry> GetTerritoryRightsCountry(RightsCountry countryDataInfo);

        [OperationContract]
        List<RightsCountry> GetTerritoryRightsCountryFilter(RightsCountry countryDataInfo);

        [OperationContract]
        List<RightsCountry> GetCountries(RightsCountry countryDataInfo);

        [OperationContract]
        List<RightsCountry> GetCompanies(RightsCountry companyDataInfo);

        #endregion "Maintain Rights Expiry Report Parameter"

        #region "Maintain Rights Data Review Conditions"

        /// <summary>
        /// Gets the rights data review rules/ conditions.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetRightsDataReviewRules();

        [OperationContract]
        RightsReviewRule SaveRightsDataReview(RightsReviewRule rightsReviewRule);

        /// <summary>
        /// Deletes the rights data review.
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <param name="userName">Name of the user.</param>
        [OperationContract]
        void DeleteRightsDataReview(List<long?> countryId, string userName);

        [OperationContract]
        List<RightsReviewRule> SearchRightsDataReviewResult(RightsDataReviewCriteria rightsDataReviewCriteria);

        #endregion "Maintain Rights Data Review Conditions"

        #region "Maintain Rights Repertoire"

        [OperationContract]
        RightsDefaultForRepertoire SaveRightsDefaultRepertoireData(RightsDefaultForRepertoire rightsDefaultForRepertoire, string userLogName);

        /// <summary>
        /// Deletes the rights default repertoire data.
        /// </summary>
        /// <param name="rightsDefaultForRepertoireId">The rights default for repertoire id.</param>
        /// <param name="userName">Name of the user.</param>

        [OperationContract]
        void DeleteRightsDefaultRepertoireData(List<long> rightsDefaultForRepertoireId, string userName);

        [OperationContract]
        List<RightsDefaultForRepertoire> SearchRightsDefaultForRepertorieResults(
            RightsDefaultRepertoireCriteria rightsDefaultRepertoireCriteria);

        #endregion "Maintain Rights Repertoire"

        #region "Territory"

        [OperationContract]
        List<TerritorialDisplay> LoadTerritoryData(long rightSetId, bool isTemplate);

        [OperationContract]
        List<TerritorialDisplay> SaveTerritories(string userName, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories, long rightSetId);

        [OperationContract]
        List<TerritorialDisplay> SaveTerritoriesByContract(string userName, RightsDefaultForRepertoire rightsDefaultForRepertoire, List<TerritorialDisplay> territories);

        #endregion "Territory"

        /// <summary>
        /// Gets the Rights Associated Status
        /// </summary>
        [OperationContract]
        bool Notify(long rightsId, NotificationType type);

        [OperationContract]
        List<string> ValidateIsrcFromFile(List<string> isrcFromFile);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetPreClearedBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetTracksBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetTracksSearchReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetSecondaryExploitationBasicSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetPreClearedReleaseParameters(bool isSensitive, SearchRepertoireCriteria searchFilter);

        [OperationContract]
        List<RepertoireRightsSearchResult> GetSecondaryExploitationReleaseParameterSearch(bool isSensitive, SearchRepertoireCriteria searchFilter);

        #region lookups

        /// <summary>
        /// GetSecondaryExploitations
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetSecondaryExploitations();

        /// <summary>
        /// Gets the reasons for review.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetReasonsForReview();

        /// <summary>
        /// Gets the pre clearances.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetPreClearances();

        /// <summary>
        /// Gets the lost rights reasons.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetLostRightsReasons();

        /// <summary>
        /// Gets the rights review status.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetRightsReviewStatuses();

        /// <summary>
        /// Gets the rights period.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetRightPeriods();

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<StringIdentifier> GetResourceType();

        #endregion lookups

        #region Rights Review

        [OperationContract]
        List<long> SaveReviewedReleaseRights(ReleaseRightsSaveInfo releaseRights);

        [OperationContract]
        List<long> SaveReviewedResourceRights(ResourceRightsSaveInfo resourceRights);

        [OperationContract]
        List<long> SaveResourceSecondaryRights(SecondaryRightsSaveInfo updateRights);

        [OperationContract]
        List<long> SaveResourcePreClearanceRights(PreClearanceSaveInfo preClearanceSaveInfo);

        [OperationContract]
        List<long> SaveResourceDigitalRights(ResourceDigitalSaveInfo resourceDigitalSaveInfo);

        [OperationContract]
        ReviewRightsMasterInfo GetRightsAcquiredMasterData();

        [OperationContract]
        SecondaryRightsMasterData LoadSecondaryRightsMasterData();

        [OperationContract]
        PreClearanceMasterData LoadPreClearanceMasterData();

        [OperationContract]
        DigitalRightsMasterData LoadDigitalRightsMasterData();

        [OperationContract]
        List<long> SaveReleaseDigitalRights(ReleaseDigitalSaveInfo rights);

        #endregion Rights Review
    }
}