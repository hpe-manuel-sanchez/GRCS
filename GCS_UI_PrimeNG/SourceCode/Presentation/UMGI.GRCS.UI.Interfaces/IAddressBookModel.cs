/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IAddressBookModel.cs
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
namespace UMGI.GRCS.UI.Interfaces
{
    public interface IAddressBookModel
    {       
         IEnumerable<SelectListItem> AddressBookPerPage { get; set; }
         string MailReceipients { get; set; }
    }
}
