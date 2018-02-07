/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IProjectData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Bijesh             06-12-1012     Added UpdateProject method
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IProjectData
    {
        List<long> SaveProjectArtist(List<ArtistInfo> artistList, long projectId, string userName, DateTime requestedDateTime);

        long SaveProjectContract(long projectId, long contractId, string userName, DateTime requestedDateTime);

       // long SaveProject(ProjectInfo projectInfo);

        long InsertNewProject(long id, long r2AccountId, string projectCode, long adminCompanyId, string title, bool isContractRequired, long labelId, string userName, DateTime requestedDateTime);

        long SaveProjectRelease(long projectId, long releaseId, string userName);

        long SaveProjectResource(long projectId, long resourceId, string userName);

        bool IsAlreadyLinkedContract(long projectid);

        long? GetAssociatedContractId(long projectid);

        long UpdateProject(ProjectInfo projectDetail);
        List<long> SaveProjectResources(List<long> resourceIds, long projectId, string username);
        List<long> SaveProjectReleases(List<long> releaseIds, long projectId, string username);
        bool CheckProjectExist(long projectid);

        //List<Project_Artist> GetExistingProjectArtists(long projectId, string archieve);

        //List<long> ArchieveProjectArtist(List<ArtistInfo> artistList, long projectId, string userName);

        bool CheckProjectReleaseExist(long projectId, long releaseId);

        ProjectInfo GetprojectInfo(long projectId);
        
    }
}