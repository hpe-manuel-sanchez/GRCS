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

using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.UI.Interfaces;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.UI.Helper
{
    /// <summary>
    /// Wrapper Class for Session
    /// </summary>
    public class HttpContextSessionWrapper : ISessionWrapper
    {
        private T GetObjectFromSession<T>(string key) where T : class
        {
            var sessionObject = (T) HttpContext.Current.Session[key];
            if (sessionObject != null)
            {
                return sessionObject;
            }
            else
            {
                return null;
            }
        }

  

        private void SetObjectToSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public GrsAuthentication CurrentPermissions
        {
            get { return GetObjectFromSession<GrsAuthentication>("CurrentPermissions"); }
            set { SetObjectToSession("CurrentPermissions", value); }
        }
                
        public IContractModel ContractModelStore
        {
            get { return GetObjectFromSession<IContractModel>("ContractModel"); }
            set { SetObjectToSession("ContractModel", value); }
        }

        public UserInfo CurrentUserInfo
        {
            get { return GetObjectFromSession<UserInfo>("CurrentUserInfo"); }
            set { SetObjectToSession("CurrentUserInfo", value); }
        }

        public IContractTerritorialRightsModel TerritorialModelStore
        {
            get { return GetObjectFromSession<IContractTerritorialRightsModel>("TerritorialModelStore"); }
            set { SetObjectToSession("TerritorialModelStore", value); }
        }

        public List<ReleaseInfo> ReleaseCollection
        {
            get { return GetObjectFromSession<List<ReleaseInfo>>("ReleaseCollection"); }
            set { SetObjectToSession("ReleaseCollection", value); }
        }

        public ContractInfo ContractInfo
        {
            get { return GetObjectFromSession<ContractInfo>("ContractInfo"); }
            set { SetObjectToSession("ContractInfo", value); }
        }

        public List<long> LinkedRepertoireCount
        {
            get { return GetObjectFromSession<List<long>>("LinkedRepertoireCount"); }
            set { SetObjectToSession("LinkedRepertoireCount", value); }
        }

        public SearchFields SearchFields
        {
            get { return GetObjectFromSession <SearchFields>("SearchFields"); }
            set { SetObjectToSession("SearchFields", value); }
        }

        public GcsAuthentication GcsCurrentPermissions
        {
            get { return GetObjectFromSession<GcsAuthentication>("GcsCurrentPermissions"); }
            set { SetObjectToSession("GcsCurrentPermissions", value); }
        }
        //This property is to avoid unnecesary call to ANA, while RCC admin exit as mimiced user
        public GcsAuthentication GcsPermissionsBeforeMimic
        {
            get { return GetObjectFromSession<GcsAuthentication>("GcsRCCAdminPermissions"); }
            set { SetObjectToSession("GcsRCCAdminPermissions", value); }
        }

        public IManageTerritoryModel<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.TerritorialDisplay> ManageTerritoryModel
        {
            get { return GetObjectFromSession<IManageTerritoryModel<UMGI.GRCS.BusinessEntities.Entities.ContractEntities.TerritorialDisplay>>("ManageTerritoryModel"); }
            set { SetObjectToSession("ManageTerritoryModel", value); }
        }

        public List<SelectListItem> UserRoles { get; set; }

        public List<WorkGroupUser> WorkGroupUsers
        {
            get { return GetObjectFromSession<List<WorkGroupUser>>("WorkGroupUsers"); }
            set { SetObjectToSession("WorkGroupUsers", value); }
        }

        public List<WorkGroupUser> SearchedUsers
        {
            get { return GetObjectFromSession<List<WorkGroupUser>>("SearchedUsers"); }
            set { SetObjectToSession("SearchedUsers", value); }
        }

        public List<WorkGroupUser> SelectedUsers
        {
            get { return GetObjectFromSession<List<WorkGroupUser>>("SelectedUsers"); }
            set { SetObjectToSession("SelectedUsers", value); }
        }
    }
}
