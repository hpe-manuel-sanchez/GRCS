/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IWorkQueue.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 03-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Pavan Kumar K     20-10-2012      RerouteResource parameters modified
 *                                   and created as seperate class
 * Karthik P         19-02-2013      Added interfaces for UC18-20                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      

****************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AdminEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkQueueEntities;
using UMGI.GRCS.BusinessEntities.Requests;


namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    /// <summary>
    /// WorkQueue operations
    /// </summary>
    [ServiceContract]
    public interface IWorkQueue
    {
        [OperationContract]
        void RemoveWorkQueueItems(List<TaskInfo> workQueueItems);

        [OperationContract]
        WorkQueueResult GetWorkQueueItems(WorkQueueFilters filterFields);

     
        [OperationContract]
        WorkQueueResult RightsReview(List<long> workQueueItemIds);

        [OperationContract]
        List<RightsCountry> GetCountries(RightsCountry countryDataInfo);

         [OperationContract]
        List<WorkQueueReleaseDate> SearchWorkQueueComparisionParameter(
         WorkQueueReleaseComparisionCriteria workQueueReleaseComparisionCriteria);

          [OperationContract]
         List<long> CheckRemovedTaskItems(List<TaskInfo> workQueueItems);

        #region "Admin WorkQueue"

        [OperationContract]
        WorkQueueReleaseDate SaveWorkQueueComparisionParameterData(WorkQueueReleaseDate workQueueReleaseDate);

        [OperationContract]
        void DeleteWorkQueueComparisionParameterData(List<long> workQueueReleaseId,string userLogName);
        #endregion

        #region Custom WorkQueue
        [OperationContract]
        CustomWorkQueueSetting LoadCustomWorkQueueSettings(string userName);

        [OperationContract]
        void SaveUserCustomWQConfig(CustomWorkQueueSetting userCustomSetting);

        [OperationContract]
        string RepertoireReviewRights(List<TaskInfo> workQueueItems);

        #endregion

        #region Rights Review
        [OperationContract]
        ReleaseRightsResult GetReleaseRightsWQ
            (ReleaseFilterParameters filter);

        [OperationContract]
        ResourceRightsResult GetResourceRightsWQ
            (ResourceFilterParameters filter);

        [OperationContract]
        ResourceDigitalRightsResult LoadResourceDigitalRights
            (ResourceFilterParameters filter);

        [OperationContract]
        ResourceDigitalRightsResult GetResourceDigitalRightsPredefined
            (PreDefinedParametersWQ filter);

        [OperationContract]
        ResourceSecondaryRightsResult LoadResourceSecondaryRights(ResourceFilterParameters filter);

        [OperationContract]
        ResourceSecondaryRightsResult GetResourceSecondaryRights(PreDefinedParametersWQ filter);

        [OperationContract]
        ResourcePreClearanceResult GetResourcesPreclearancePredefined(PreDefinedParametersWQ filter);

        [OperationContract]
        ResourcePreClearanceResult LoadResourcePreClearanceInfo(ResourceFilterParameters filter);

        [OperationContract]
        ReleaseDigitalRightsResult LoadReleaseDigitalRights(ReleaseFilterParameters filter);

        [OperationContract]
        ReleaseDigitalRightsResult LoadReleaseDigitalRightsPredefined(PreDefinedParametersWQ filter);

        [OperationContract]
        ResourceRightsResult LoadResourceRightsWQPredefined(PreDefinedParametersWQ filter);


        #endregion

        /// <summary>
        /// Gets the work queue master data.
        /// </summary>       
        [OperationContract]
        WorkQueueMasterData GetWorkQueueMasterData();

        [OperationContract]
        WorkQueueResult RerouteResource(RerouteResources rerouteResources);
    }
}
