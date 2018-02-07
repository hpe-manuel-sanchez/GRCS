using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Requests;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IWorkQueueManager
    {
        WorkQueueResult GetWorkQueueItems(WorkQueueFilters filterFields);

        List<long?> GetContractsInWorkQueue(long itemId, string itemType, string reviewReason);

        List<RightsCountry> GetCountries(RightsCountry countryDataInfo);

        List<long> CheckRemovedTaskItems(List<TaskInfo> tasks);

        string IsPriorityWorkQueueItem(WorkQueueCriteria reviewCriteria, RepertoireType repertoireType);

        void AddToPriorityWorkQueue(TaskInfo trackInfo, long contractId, string itemType, string reasonType);

        void DeletePriorityWorkQueue(List<TaskInfo> tasks);

        void DeletePriorityWorkQueue(List<TaskInfo> tasks, bool ChangeLink);

        List<WorkQueueReleaseDate> SearchWorkQueueComparisionParameter(WorkQueueReleaseComparisionCriteria workQueueReleaseComparisionCriteria);

        WorkQueueResult ReviewRights(List<long> workQueueItemIds);

        bool IsAlreadyInWorkQueue(long itemId, string itemType);

        bool IsAlreadyInWorkQueue(long itemId, string itemType, string reviewReason);

        bool IsAlreadyInWorkQueue(long itemId, string itemType, string reviewReason, bool archiveCheck, long? contractId);

        long? GetContractId(long itemId, string itemType, string reviewReason);

        #region Admin WorkQueue

        WorkQueueReleaseDate SaveWorkQueueComparisionParameterData(WorkQueueReleaseDate workQueueReleaseDate);

        void DeleteWorkQueueComparisionParameterData(List<long> workQueueReleaseId, string userLogName);

        #endregion Admin WorkQueue

        #region Custom WorkQueue

        CustomWorkQueueSetting LoadCustomWorkQueueSettings(string userName);

        void SaveUserCustomWQConfig(CustomWorkQueueSetting userCustomSetting, bool removeSetting);

        string RepertoireReviewRights(List<TaskInfo> workQueueItems);

        #endregion Custom WorkQueue

        #region Rights Review

        ReleaseRightsResult GetReleaseRightsWQ(ReleaseFilterParameters filter);

        ResourceRightsResult GetResourceRightsWQ(ResourceFilterParameters filter);

        ResourceDigitalRightsResult LoadResourceDigitalRights(ResourceFilterParameters filter);

        ResourceDigitalRightsResult GetResourceDigitalRightsPredefined(PreDefinedParametersWQ filter);

        ResourceSecondaryRightsResult LoadResourceSecondaryRights(ResourceFilterParameters filer);

        ResourceSecondaryRightsResult GetResourceSecondaryRightsPredefined(PreDefinedParametersWQ filter);

        ResourcePreClearanceResult GetResourcesPreclearancePredefined(PreDefinedParametersWQ filter);

        ResourcePreClearanceResult LoadResourcePreClearanceInfo(ResourceFilterParameters filter);

        ReleaseDigitalRightsResult LoadReleaseDigitalRights(ReleaseFilterParameters filter);

        ReleaseDigitalRightsResult LoadReleaseDigitalRightsPredefined(PreDefinedParametersWQ filter);

        ResourceRightsResult LoadResourceRightsWQPredefined(PreDefinedParametersWQ filter);

        #endregion Rights Review

        void DeleteNotificationPriorityWorkQueue(List<TaskInfo> tasks);

        /// <summary>
        /// Gets the work queue master data.
        /// </summary>
        /// <returns></returns>
        WorkQueueMasterData GetWorkQueueMasterData();

        List<StringIdentifier> GetMatchType();

        WorkQueueResult RerouteResource(RerouteResources rerouteResources);
    }
}