/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NotificationRecipient.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities which are used for Notification Recipients                                    
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Notification Recipient
    /// </summary>
    [DataContract]
    public class NotificationRecipient
    {
        /// <summary>
        /// Property holding the NotificationRecipient "Email"
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "Group"
        /// </summary>
        [DataMember]
        public string Group { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "Name"
        /// </summary>
        [DataMember]
        public int Name { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "EmployeeCode"
        /// </summary>
        [DataMember]
        public int EmployeeCode { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "Title"
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Property holding the NotificationRecipient "Location"
        /// </summary>
        [DataMember]
        public string Location { get; set; }
    }
}