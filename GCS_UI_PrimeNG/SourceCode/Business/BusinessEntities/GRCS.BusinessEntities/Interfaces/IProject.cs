using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IProject
    {
        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        [OperationContract]
        ProjectSearchResult Search(ProjectSearchCriteria searchCriteria);

        /// <summary>
        /// Gets the associated release.
        /// </summary>
        /// <param name="linkProjectInfo">The link project info.</param>
        /// <returns></returns>
        [OperationContract]
        ReleaseSearchResult GetAssociatedRelease(LinkedInfo linkProjectInfo);

        /// <summary>
        /// Gets the associated resource.
        /// </summary>
        /// <param name="linkProjectInfo">The link project info.</param>
        /// <returns></returns>
        [OperationContract]
        ResourceSearchResult GetAssociatedResource(LinkedInfo linkProjectInfo);

        /// <summary>
        /// Gets the associated release resource.
        /// </summary>
        /// <param name="linkProjectInfo">The link project info.</param>
        /// <returns></returns>
        [OperationContract]
        ReleaseResourcesInfo GetAssociatedReleaseResource(LinkedInfo linkProjectInfo);

        [OperationContract]
        bool IsAlreadyLinkedContract(long projectid);

        [OperationContract]
        ProjectInfo GetprojectInfo(long projectId);
    }
}