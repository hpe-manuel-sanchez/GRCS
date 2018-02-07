
/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IClearanceRelease.cs
 * Project Code :   
 * Author       : dhruv arora
 * Created on   : 14 October 2012 
 * Description  :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************/

using System.Collections.Generic;
using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IClearanceRelease
    {
        [OperationContract(Name = "GetReleaseLabelList")]
        List<ClearanceRelease> GetReleaseLabelList(LeanUserInfo userInfo);

        [OperationContract(Name = "GetReleaseConfigList")]
        List<ClearanceRelease> GetReleaseConfigList(string ConfigGroupId, LeanUserInfo userInfo);

        [OperationContract(Name = "GetReleaseConfigGroupList")]
        List<ClearanceRelease> GetReleaseConfigGroupList(LeanUserInfo userInfo);

        [OperationContract(Name = "SaveReleaseNew")]
        string SaveReleaseNew(List<ClearanceRelease> clearanceRelease, LeanUserInfo userInfo);

        [OperationContract]
        ClearanceReleaseSearchResult GetExistingReleases(string UPC, string ReleaseTitle, string ArtistName, int ArtistID, LeanUserInfo userInfo);

        [OperationContract]
        ClearanceReleaseSearchResult GetExistingReleasesSaveMode(string projectCode, LeanUserInfo userInfo);

        [OperationContract]
        string SaveExistingReleases(ClearanceReleaseSearchResult releaseSearchResult, LeanUserInfo userInfo);

        [OperationContract]
        List<DropDeviatedICLALevel> GeDeviatedIclaLevel(LeanUserInfo userInfo);

        [OperationContract]
        List<DropPriceLevel> GetPriceLevel(LeanUserInfo userInfo);

        [OperationContract]
        void BG_ReleaseDetails(List<ReleaseInfo> releases, LeanUserInfo userInfo,long clearanceProjectID);

        [OperationContract]
        ReleaseSearchResult GCSReleaseSearch(ReleaseSearchCriteria searchCriteria, LeanUserInfo userInfo);

        [OperationContract]
        List<TrackInfo> R2GetReleaseAdditionalDetails(long releaseId, LeanUserInfo userInfo);

        [OperationContract(Name = "GetUPCNumber")]
        string GetUPCNumber(List<long> classiceReleaseId, List<long> nonClassiceReleaseId, string userId);

        [OperationContract(Name = "GetUpc_Check_Digit")]
        string GetUpc_Check_Digit(string upcNumber, LeanUserInfo userInfo);

        [OperationContract(Name = "RemoveUPCNumber")]
        bool RemoveUPCNumber(long projectReleaseId, string userId);

        [OperationContract]
        ClearanceRelease GetReleaseAdditionalDetails(long releaseId, LeanUserInfo userInfo);

        [OperationContract]
        ClearanceRelease GetReleaseDetailsGCS(long releaseId, LeanUserInfo userInfo);

        [OperationContract]
        List<long?> GetExistingR2ReleaseIdFromGcs(List<long?> r2ReleaseId);

        [OperationContract]
        List<long> GetExistingR2ReleaseIdForPackage(List<long> r2ReleaseId, long? ParentReleaseId);

        [OperationContract]
        List<long> UpdatePackage(List<PackageInfo> packageInfo, LeanUserInfo userInfo);

        [OperationContract]
        List<PackageInfo> GetPackageDetailsforProjectRelease(long ReleaseId, LeanUserInfo userInfo);
   
        [OperationContract]
        string getLabelNmForExistingRelease(int LabelId, LeanUserInfo userInfo);

        [OperationContract]
        string UpdateManualUpc(long releaseId, string upcNumber, string userId);

        [OperationContract]
        ReleaseSearchResult GetReleaseDetailsBasedonR2ReleaseId(List<long> R2ReleaseId, LeanUserInfo userInfo);

        
    }
}
