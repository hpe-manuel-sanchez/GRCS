/* ************************************************************************ 
* Copyrights ® 2012 UMGI 
* ************************************************************************ 
* File Name    : NotificationMessage.cs 
* Project Code : UMG-GRCS(C/115921) 
* Author       : Senthil Kumar G
* Created on   : 05-11-2012
* ************************************************************************ 
* Modification History 
* ************************************************************************ 
* Modified by       Modified on     Remarks 

*************************************************************************** 
* Reviewed by       Modified on     Remarks 

****************************************************************************

                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    /// <summary>
    /// All the notification messages from the service market, GDRS and CPRS messages shall be handled with this class
    /// before processing. Objective is to track all messages with a uniqueId and time Stamp
    /// </summary>
    [DataContract]
    public class NotificationMessage : EntityInformation
    {
        /// <summary>
        /// Identifier for Notification Type
        /// </summary>
        [DataMember]
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        [DataMember]
        public DateTime ReceivedTimeStamp { get; set; }


        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        /// <value>The unique id.</value>
        [DataMember]
        public long UniqueId { get; set; }


        /// <summary>
        /// Gets or sets the external unique id.
        /// </summary>
        /// <value>The external unique id.</value>
        [DataMember]
        public long ExternalUniqueId { get; set; }


        /// <summary>
        /// Gets or sets the notification XML.
        /// </summary>
        /// <value>The notification XML.</value>
        [DataMember]
        public string NotificationXml { get; set; }
    }
}