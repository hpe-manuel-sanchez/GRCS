using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IClearanceReleaseRepository
    {
       // IClearanceReleaseModel IClearanceReleaseModel { get; set; }
        ReleaseSearchResult R2ReleaseSearch(ReleaseSearchCriteria releaseSearchCriteria, LeanUserInfo userInfo);
        List<TrackInfo> R2GetReleaseAdditionalDetails(long releaseId, LeanUserInfo userInfo);
        string GetUPCNumber(List<long> classiceReleaseId, List<long> nonClassiceReleaseId, string userId);
        bool RemoveUPCNumber(long projectReleaseId, string userId);
        string GetUpc_Check_Digit(string upcNumber, LeanUserInfo userInfo);
        IClearanceReleaseModel GetPackageDetails(long releaseId, LeanUserInfo userInfo);
        ClearanceReleaseSearchResult GetExistingReleases(string UPC, string ReleaseTitle, string ArtistName, int ArtistID, LeanUserInfo userInfo);
        IClearanceReleaseModel GetReleaseDetails(long releaseId, LeanUserInfo userInfo);
        List<long?> GetExistingR2ReleaseIdFromGcs(List<long?> r2ReleaseId);
        IClearanceReleaseModel UpdatePackage(List<PackageInfo> packageInfo, LeanUserInfo userInfo);
        List<PackageInfo> GetPackageDetailsforProjectRelease(long releaseId, LeanUserInfo userInfo);
        string getLabelNmForExistingRelease(int LabelId,LeanUserInfo userInfo);
        //IClearanceProjectModel GetReleaseLabelList(UserInfo userInfo);
        List<ClearanceRelease> GetReleaseLabelList(LeanUserInfo userInfo);
        IClearanceProjectModel GetReleaseConfigList(string ConfigGroupId, LeanUserInfo userInfo);
        IClearanceProjectModel GetReleaseConfigGroupList(LeanUserInfo userInfo);        
        ClearanceReleaseSearchResult GetExistingReleasesSaveMode(string projectCode, LeanUserInfo userInfo);
        string SaveExistingReleases(ClearanceReleaseSearchResult releaseSearchResult, LeanUserInfo userInfo);
        string SaveReleaseNew(List<ClearanceRelease> clearanceRelease, LeanUserInfo userInfo);
        string UpdateManualUpc(long releaseId, string upcNumber, LeanUserInfo userInfo);
        List<long> GetExistingR2ReleaseIdForPackage(List<long> r2ReleaseId, long? ParentReleaseId);
        ReleaseSearchResult GetReleaseDetailsBasedonR2ReleaseId(List<long> R2ReleaseId, LeanUserInfo userInfo);
    }
}
