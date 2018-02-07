/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : IRepertoireData.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Vijayakumar.R
 * Created on   : 19-Oct-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
 * Pavan Kumar       30/10/2012     Code Refactored
 * Karthik .P        31/12/2012      Methods Added for UC15
***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IRepertoireData
    {
        void SaveContractRepertoireDetails(long contractId, List<ReleaseInfo> releaseList, List<ResourceInfo> resourceList);

        int UpdateContractRepertoireDetails(long contractId, long repertoireid, string status);

        void SaveProjectRepertoireDetails(long projectId, string userName);

        ProjectInfo GetProjectRepertoireDetails();

        bool GetGcsReleaseInfo(long releaseId, string userName);

        RepertoireInfo GetRepertoire();

        void UpdateRollUpStatus(long id, RollUpStatus status);

        bool SaveGCSResourceLinkInfo(List<ResourceInfo> resourceList, long contractId, GCSInfo releaseLinkInfo);
    }
}