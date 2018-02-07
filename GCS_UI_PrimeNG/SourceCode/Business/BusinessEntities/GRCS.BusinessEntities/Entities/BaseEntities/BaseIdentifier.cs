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
* Description :  Defines the Base Identifier entities.                                      
                  
****************************************************************************/

using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Base Identifier
    /// </summary>
    [DataContract]
    [Serializable]
    public class BaseIdentifier : BaseSearch
    {
        /// <summary>
        /// Base Identifier- ID
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        ///  Base Identifier- Role Group Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}