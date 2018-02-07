/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ICLearanceReleaseData.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Lookups;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IClearanceReleaseData
    {
        List<ClearanceRelease> GetReleaseLabelList();
        List<ClearanceRelease> GetReleaseConfigGroupList();
        List<ClearanceRelease> GetReleaseConfigList(string ConfigGroupId);
        // List<ClearanceRelease> GetReleaseConfigList();

        string SaveReleaseNew(List<ClearanceRelease> clearanceRelease, LeanUserInfo userInfo);

        ClearanceReleaseSearchResult GetExistingReleases(string UPC, string ReleaseTitle, string ArtistName, int ArtistID);
        ClearanceReleaseSearchResult GetExistingReleasesSaveMode(string projectCode);
        string SaveExistingReleases(ClearanceReleaseSearchResult releaseSearchResult, LeanUserInfo userInfo);
        List<DropDeviatedICLALevel> GeDeviatedIclaLevel();
        List<DropPriceLevel> GetPriceLevel();
        List<string[]> GetUPCNumber(List<long> classiceReleaseId, List<long> nonClassiceReleaseId, string userId);
        bool UpdateUpcNumer(long projectReleaseId, string upcNumer,bool isManual, string userId);
        bool RemoveUPCNumber(long projectReleaseId, string userId);
        List<PackageInfo> GetReleasesPackageDetailsGCS(long ReleaseId);
        ClearanceRelease GetReleasesIdGCS(long ReleaseId);
        List<long> GetExistingR2ReleaseIdForPackage(List<long> r2ReleaseId, long? ParentReleaseId);
        List<long?> GetExistingR2ReleaseIdFromGcs(List<long?> r2ReleaseId);
        List<long> UpdatePackage(List<PackageInfo> packageInfo);
        long GetPackageIdGCS(long PackageId);
        ClearanceRelease GetReleasesTracksandArtists(ClearanceRelease clrRelease, long ReleaseId);
        List<PackageInfo> GetPackageDetailsforProjectRelease(long releaseId);
        List<long>GetReleaseId(long clrProjectId, EmailType emailType);
        
        List<long> SaveReleaseArtistClearance(long releaseId, List<ArtistInfo> artistList);

        #region Downstream Notification
        //ClearanceReleaseDetails GetClearanceReleaseDetails(long ClearanceReleaseId);
        ClearanceReleaseDetails GetClearanceReleaseDetails(long ClearanceProjectId, long releaseId);

        List<long> GetClearanceReleaseId(long R2ReleaseId, long GCSProjectId);
        List<long> GetClearanceReleaseId(string UPC, long GCSProjectId);
        List<long> GetClearanceRelease(long GCSReleaseId, long GCSProjectId);

        List<long> GetReleaseFromClrProject(long clrProjectId);

        Dictionary<long, long> GetReleaseIdClrProjectFromClrRelease(long clrReleaseId);


        #endregion
    }
}
 