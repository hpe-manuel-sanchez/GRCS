/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GlobalAddress.cs 
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
 *Description :  Defines the entities for Global Address
 
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.GlobalAddressEntities
{
    /// <summary>
    /// GlobalAddress which is inherited from ClassInfo
    /// </summary>
    [DataContract]
    public class GlobalAddress : EntityInformation
    {
        /// <summary>
        /// EmailAddress
        /// </summary>
        [DataMember]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        [DataMember]
        public string Location { get; set; }
    }
}