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
using System.Web;
using UMGI.GRCS.Entities.ANAEntities;
using UMGI.GRCS.UI.Rights.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class HttpContextSessionWrapper:ISessionWrapper
    {
        private T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public GrsAuthentication CurrentUserInfo
        {
            get { return GetFromSession<GrsAuthentication>("CurrentUserInfo"); }
            set { SetInSession("CurrentUserInfo", value); }
        }

        public List<SelectListItem> UserRoles
        {
            get { return GetFromSession<List<SelectListItem>>("UserRoles"); }
            set { SetInSession("UserRoles", value); }
        }

        public IContractModel ContractModelStore
        {
            get { return GetFromSession<IContractModel>("ContractModel"); }
            set { SetInSession("ContractModel", value); }
        }
    }
}
