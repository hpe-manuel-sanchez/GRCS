/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IWorkQueueData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 03-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rengaraj           22-11-2012     Modified for code refactoring
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      

****************************************************************************/

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.BusinessEntities.Requests;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IWorkQueueData
    {
        void AddToPriorityWorkQueue(TaskInfo trackInfo, long contractId, string itemType, string reasonType);
        //WorkQueueResult RemoveWorkQueueItems(List<TaskInfo> workQueueItemIds, WorkQueueFilters filterFields);
        WorkQueueResult GetWorkQueueItems(List<long> userCompanyIds, WorkQueueFilters filterFields);
        List<long?> GetContractsInWorkQueue(long itemId, string itemType, string reviewReason);
        void DeletePriorityWorkQueue(List<TaskInfo> tasks);
        List<long> CheckRemovedTaskItems(List<TaskInfo> tasks);
        //List<WorkQueue> BuildWorkQueueDisplay();

        WorkQueueResult RightsReview(List<long> workQueueItemIds);

        List<RightsCountry> GetCountries(RightsCountry countryInfo);

        List<WorkQueueReleaseDate> SearchWorkQueueComparisionParameter(
            WorkQueueReleaseComparisionCriteria workQueueReleaseComparisionCriteria);

        bool IsAlreadyInWorkQueue(long itemId, string itemType, string status);
        bool IsAlreadyInWorkQueue(long itemId, string itemType);

        bool IsAlreadyInWorkQueue(long itemId, string itemType, string reviewReason,long? contractId);
        bool IsAlreadyInWorkQueue(long itemId, string itemType, string reviewReason, bool archiveCheck, long? contractId);

        long? GetContractId(long itemId, string itemType, string reviewReason);
        
        #region "Admin WorkQueue"

        WorkQueueReleaseDate SaveWorkQueueComparisionParameterData(WorkQueueReleaseDate workQueueReleaseDate);

        void DeleteWorkQueueComparisionParameterData(List<long> workQueueReleaseId, string userLogName, DateTime requestDateTime);
        #endregion

        #region Custom WorkQueue
        CustomWorkQueueSetting GetCustomWorkQueueSettings(string userName);
        Dictionary<int, string> GetDefaultCustomWQConfig();
        void UpdateUserCustomWQConfig(CustomWorkQueueSetting userCustomSetting);
        void InsertUserCustomWQConfig(CustomWorkQueueSetting userCustomSetting);
        void DeleteUserCustomWQConfig(string userName);
        bool DoesUserCustomSettingExists(string userName);

        string RepertoireReviewRights(List<TaskInfo> workQueueInfo);
        #endregion Custom WorkQueue

        void DeleteNotificationPriorityWorkQueue(List<TaskInfo> tasks);      

        List<StringIdentifier> LoadMatchType();

        /// <summary>
        /// Determines whether [has lead release date] [the specified country id].
        /// </summary>
        /// <param name="countryId">The country id.</param>
        /// <param name="existingReleaseDateLeadId">The existing release date lead id.</param>
        /// <returns>
        /// 	<c>true</c> if [has lead release date] [the specified country id]; otherwise, <c>false</c>.
        /// </returns>
        bool HasLeadReleaseDate(long? countryId, out long existingReleaseDateLeadId);

        /// <summary>
        /// Saves the lookup release date lead details.
        /// </summary>
        /// <param name="workQueueReleaseDate">The work queue release date.</param>
        /// <returns></returns>
        WorkQueueReleaseDate SaveLookupReleaseDateLeadDetails(WorkQueueReleaseDate workQueueReleaseDate);

        /// <summary>
        /// Updates the lookup release date lead details.
        /// </summary>
        /// <param name="workQueueReleaseDate">The work queue release date.</param>
        /// <returns></returns>
        WorkQueueReleaseDate UpdateLookupReleaseDateLeadDetails(WorkQueueReleaseDate workQueueReleaseDate);
        /// <summary>
        /// RerouteResource
        /// </summary>
        /// <param name="rerouteResources"></param>
        /// <returns></returns>
        WorkQueueResult RerouteResource(RerouteResources rerouteResources);
    }

}
