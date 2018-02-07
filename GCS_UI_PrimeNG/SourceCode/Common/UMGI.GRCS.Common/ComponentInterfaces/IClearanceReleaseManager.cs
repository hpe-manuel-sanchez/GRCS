/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IClearanceReleaseManager.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceReleaseManager
    {
        List<ClearanceRelease> GetReleaseLabelList();
        List<ClearanceRelease> GetReleaseConfigGroupList();
        List<ClearanceRelease> GetReleaseConfigList(string ConfigGroupId);

        string SaveReleaseNew(List<ClearanceRelease> clearanceRelease, LeanUserInfo userInfo);
        ClearanceReleaseSearchResult GetExistingReleases(string UPC, string ReleaseTitle, string ArtistName, int ArtistID);
        ClearanceReleaseSearchResult GetExistingReleasesSaveMode(string projectCode);
        string SaveExistingReleases(ClearanceReleaseSearchResult releaseSearchResult, LeanUserInfo userInfo);

        List<DropDeviatedICLALevel> GeDeviatedIclaLevel();
        List<DropPriceLevel> GetPriceLevel();

        //Background Method
        void BG_ReleaseDetails(List<ReleaseInfo> releases, LeanUserInfo userInfo,long clearanceProjectID);

        //added by vivek on dated: 16-Nov-2012
        ReleaseSearchResult GCSReleaseSearch(ReleaseSearchCriteria searchCriteria);

        List<TrackInfo> R2GetReleaseAdditionalDetails(long releaseId);
        string GetUPCNumber(List<long> classiceReleaseId, List<long> nonClassiceReleaseId, string userId);
        bool RemoveUPCNumber(long projectReleaseId, string userId);
        string GetUpc_Check_Digit(string upcNumber);

        ClearanceRelease GetReleasePackageDetails(long releaseId);
        ClearanceRelease GetReleaseIdGCS(long ReleaseId);
        List<long> GetExistingR2ReleaseIdForPackage(List<long> r2ReleaseId, long? ParentReleaseId);
        List<long?> GetExistingR2ReleaseIdFromGcs(List<long?> r2ReleaseId);
        void PackageReleaseSave(List<PackageInfo> packageInfo, LeanUserInfo userinfo, long clearanceProjectID);
        List<long> UpdatePackage(List<PackageInfo> packageInfo, LeanUserInfo userinfo);
        void PackageReleaseSaveReleaseNew(List<PackageInfo> packageInfo, LeanUserInfo userinfo, long clearanceProjectID);
        ClearanceRelease GetReleaseTracksandArtists(ClearanceRelease clrRelease, long ReleaseId);
        List<PackageInfo> GetPackageDetailsforProjectRelease(long releaseId);

        #region Downstream Notification
        ClrRelease BuildClearanceRelease(long clrReleaseId, long releaseId);
        List<ClrRelease> GetGCSReleases(string ReleaseId, string GCSProjectID, ReleaseSearchOption releaseSearchOption);
        List<ClrRelease> GenerateClearanceRelease(long clrProjectId);
        ClrRelease BuildClearanceReleaseFromClrRelease(long clrReleaseId);
        #endregion

        string getLabelNmForExistingRelease(int LabelId);
        List<long> GetReleaseId(long clrProjectId, EmailType emailType);
        bool IsReleaseContentFinal(long projectId);
        Dictionary<long, List<string>> GetResourcesOfProject(long projectId);
        string UpdateManualUpc(long releaseId, string upcNumber, string userId);
        ReleaseSearchResult GetReleaseDetailsBasedonR2ReleaseId(List<long> R2ReleaseId, LeanUserInfo userInfo);

    }
}
