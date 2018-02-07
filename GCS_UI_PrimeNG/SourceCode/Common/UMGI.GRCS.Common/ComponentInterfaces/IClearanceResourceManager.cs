/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ICLearanceResourceManager.cs 
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

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceResourceManager
    {
        ClearanceResourceSearchResult SearchR2Resource(ResourceSearchCriteria SearchCriteria);

        void BG_ResourceArtist(List<ResourceInfo> resources, LeanUserInfo user);

        List<ArtistInfo> GetR2ResourceArtists(long r2ResourceId);

        bool RemoveResourceProject(string archiveFlag, List<long> listclrResourceId);

        List<ContractDetails> GetResourceRights(long r2resourceId);

        List<long> SaveTrackinClearanceProjectResource(List<TrackInfo> trackInfoList, long clearanceProjectID);

        bool SaveClearanceResourceCompletenessCheck(ResourceCompletenessCheck clrResourceCompletenessCheck);

        void ProcessResourceCompletenessCheck();

        ResourceInfo GetResourceInfo(long resourceId);

        bool SaveManageContract(List<KeyValuePair<long, bool>> contractIdList, long routedItemId, string userName);

        void FillResourceSideArtist(List<long> resourceIds);

        #region Downstream Notification

        ClrResource BuildClearanceResource(long clrResourceId);

        List<ClrResource> GetGCSResources(string ResourceId, string GCSProjectID, ResourceSearchOption resourceSearchOption);

        
        void AutoCancelRequestLines();
		#endregion Downstream Notification

    }
}
