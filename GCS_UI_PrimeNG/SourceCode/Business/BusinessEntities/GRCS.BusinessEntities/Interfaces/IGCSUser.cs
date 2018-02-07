using System.ServiceModel;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.BusinessEntities.Interfaces
{
    public partial interface IUser
    {
        /// <summary>
        /// Authorizes the user by Login and responds back with respective responsibility/ Access to application.
        /// </summary>
        [OperationContract]
        GcsAuthentication GetGcsAuthorizationByLoginNameAndApplication(UserInfo userInfo);

        /// <summary>
        /// Getuserses this instance.
        /// </summary>
        [OperationContract]
        List<WorkGroupUser> GetUsers(WorkGroupUserSearchCriteria userSearchCriteria, AnaTargetApplication targetApplication, string userLoginName);
    }
}
