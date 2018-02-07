/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ISessionWrapper.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 23-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */


using System.Web.Mvc;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    /// <summary>
    /// Interface for Session Wrapping
    /// </summary>
    public interface ISessionWrapper
    {
        GrsAuthentication CurrentPermissions { get; set; }

        GcsAuthentication GcsCurrentPermissions { get; set; }

        //This property is to avoid unnecesary call to ANA, while RCC admin exit as mimiced user
        GcsAuthentication GcsPermissionsBeforeMimic { get; set; }

        IContractModel ContractModelStore { get; set; }

        UserInfo CurrentUserInfo { get; set; }

        IContractTerritorialRightsModel TerritorialModelStore { get; set; }      
        
        List<ReleaseInfo> ReleaseCollection { get; set; }

        ContractInfo ContractInfo { get; set; }

        List<long> LinkedRepertoireCount { get; set; }

        SearchFields SearchFields { get; set; }

        List<SelectListItem> UserRoles { get; set; }

        IManageTerritoryModel<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.TerritorialDisplay> ManageTerritoryModel { get; set; }

        List<WorkGroupUser> WorkGroupUsers { get; set; }

        List<WorkGroupUser> SearchedUsers { get; set; }

        List<WorkGroupUser> SelectedUsers { get; set; }
    }
}
