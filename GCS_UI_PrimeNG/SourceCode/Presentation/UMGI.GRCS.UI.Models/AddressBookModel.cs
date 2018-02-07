/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : AddressBookModel.cs
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
using System.Linq;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities;
using UMGI.GRCS.UI.Interfaces;
using System.Globalization;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Model for Address Book and Manage Address Book pages
    /// </summary>
    public class AddressBookModel : IAddressBookModel
    {
        public EmailGroupDetails MailDetails { get; set; }
        public string MailReceipients { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractModel"/> class.
        /// </summary>
        public AddressBookModel()
        {
            MailDetails = new EmailGroupDetails();            
        }
        public IEnumerable<SelectListItem> AddressBookPerPage { get; set; }
    }
}
