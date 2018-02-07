/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AddressBook.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rengaraj          03-Aug-2012     modified code for coding standard format  
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Address Book.                                      
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Address Book Details
    /// </summary>
    [DataContract]
    public class AddressBook
    {
        /// <summary>
        /// Recepitent List
        /// </summary>
        [DataMember]
        public List<NotificationRecipient> RecipientList { get; set; }
    }
}