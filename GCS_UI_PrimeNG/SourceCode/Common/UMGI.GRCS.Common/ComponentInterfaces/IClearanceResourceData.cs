/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ICLearanceResourceData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Dhruv Arora
 * Created on   : 10-14-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceResourceData
    {
        bool RemoveResourceProject(string archiveFlag, List<long> listclrResourceId);

        List<long> SaveTrackinClearanceProjectResource(List<TrackInfo> trackInfoList, long clearanceProjectID);

        bool SaveClearanceResourceCompletenessCheck(ResourceCompletenessCheck clrResourceCompletenessCheck);    

        List<ResourceCompletenessCheck> GetClearanceResourceOnStatus();

        bool UpdateResosourceCompletenessCheck(long routed_Item_Id, bool isCompleted, byte completenessCheckStatus, string reason);

        ResourceInfo GetResourceInfor(long resourceId);

        bool SaveManageContract(List<KeyValuePair<long, bool>> contractIdList, long routedItemId, string userName);

        List<ClearanceEmail> GetEmailAddressOfR2Contact(long workgroupId, long routedItemId);

        List<long> SaveResourceArtistClearance(long resourceId, List<ArtistInfo> newArtistList);

        DateTime? GetResourceStreetDate(long resourceId);
        List<ClearanceRoutedProject> GetRoutedProjectByRequestLinesWaiting();

        void FillResourceSideArtist(List<long> resourceIds);

        #region Downstream Notification

        ClearanceResourceDetails GetClearanceResourceDetails(long ClearanceResourceId);

        List<long> GetClearanceResourceId(long R2ResourceId, long GCSProjectId);

        List<long> GetClearanceResourceId(string ISRC, long GCSProjectId);

        #endregion Downstream Notification
    }
}
