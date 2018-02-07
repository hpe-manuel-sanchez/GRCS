/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ParentContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Email Groups
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities
{
    /// <summary>
    /// EmailGroup which is inherited from BaseIdentifier
    /// </summary>
    [DataContract]
    [Serializable]
    public class EmailGroup : BaseIdentifier
    {
        //  Group Id
        //  Group Name
    }

    /// <summary>
    ///  EmailGroupDetails  which is inherited from EmailGroup
    /// </summary>
    [DataContract]
    [Serializable]
    public class EmailGroupDetails : EmailGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailGroupDetails"/> class.
        /// </summary>
        public EmailGroupDetails()
        {
            CountryDetails = new CountryInfo();
            PageDetails = new FilterFields();
        }

        /// <summary>
        /// EmailId
        /// </summary>
        [DataMember]
        public string EmailIds { get; set; }


        /// <summary>
        /// CountryDetails
        /// </summary>
        [DataMember]
        public CountryInfo CountryDetails { get; set; }

        /// <summary>
        /// For Populating in the UI for selected items from parent page
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        public string LastModifiedDate { get; set; }
    }
}