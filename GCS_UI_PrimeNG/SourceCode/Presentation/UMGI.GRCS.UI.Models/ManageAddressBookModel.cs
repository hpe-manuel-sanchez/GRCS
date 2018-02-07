/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ManageAddressBookModel.cs
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
using System;
using System.Runtime.Serialization;


namespace UMGI.GRCS.UI.Models
{
    /// <summary>
    /// Model for Manage Address Book page
    /// </summary>
    [Serializable]
    public class ManageAddressBookModel : IManageAddressBookModel, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageAddressBookModel"/> class.
        /// </summary>
        public ManageAddressBookModel()
        {
            EmailGroupDetails = new EmailGroupDetails();            
        }

        public EmailGroupDetails EmailGroupDetails { get; set; }
        public IEnumerable<SelectListItem> ShowItemsPerPage { get; set; }


        protected ManageAddressBookModel(SerializationInfo info, StreamingContext context)
        {           
            this.EmailGroupDetails = (EmailGroupDetails)info.GetValue("EmailGroupDetails", typeof(EmailGroupDetails));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EmailGroupDetails", this.EmailGroupDetails);
        }
    }
    }




