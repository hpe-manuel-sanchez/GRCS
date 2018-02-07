/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IManageAddressBookModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : 
 * Created on     : 
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
using System.Web.Mvc;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IManageAddressBookModel
    {
        EmailGroupDetails EmailGroupDetails { get; set; }
        IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }
    }
}
