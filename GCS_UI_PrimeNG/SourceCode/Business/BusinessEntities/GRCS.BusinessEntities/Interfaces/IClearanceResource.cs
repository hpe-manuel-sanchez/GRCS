/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : IClearanceResource.cs
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
using UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    [ServiceContract]
    public interface IClearanceResource
    {
        /// <summary>
        /// Search Resource
        /// </summary>
        /// <param name="resourceSearch"></param>
        /// <returns></returns>
        [OperationContract]
        ClearanceResourceSearchResult SearchR2Resource(ResourceSearchCriteria resourceSearch, LeanUserInfo user);

        /// <summary>
        /// Search Resource
        /// </summary>
        /// <param name="resourceSearch"></param>
        /// <returns></returns>
        [OperationContract]
        void BG_ResourceArtist(List<ResourceInfo> resources, LeanUserInfo user);

        [OperationContract]
        bool RemoveResourceProject(string archiveFlag, List<long> listclrResourceId, LeanUserInfo user);

        [OperationContract]
        List<ContractDetails> GetResourceRights(long r2resourceId, LeanUserInfo user);

    }


}
