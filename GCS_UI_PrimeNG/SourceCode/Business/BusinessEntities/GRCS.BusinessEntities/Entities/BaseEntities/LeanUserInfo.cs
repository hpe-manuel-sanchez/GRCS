/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : LeanUserInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : GRCS Team
 * Created on   : 26-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  LeanUserInfo is a lightweight - UserInfo - object, holding 
                 just enough properties to accomplish the data management tasks.
                 This makes it suitable to pass it between the application layers.
                 UserInfo on the other hand is a large data object and has 
                 performance implications upon its serialization and deserialization.
                 This makes it unsuitable to pass it between the application layers.
                 LeanUserInfo contains just the bare minimum subset of UserInfo properties.
****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Lean UserInfo 
    /// </summary>
    [DataContract]
    [Serializable]
    public class LeanUserInfo
    {
        /// <summary>
        /// User Id
        /// </summary>
        [DataMember]
        public long UserId { get; set; }

        /// <summary>
        /// User Login Name
        /// </summary>
        [DataMember]
        public string UserLoginName { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// User Email Id
        /// </summary>
        [DataMember]
        public string EmailId { get; set; }

        [DataMember]
        public string MimicedRccAdminLoginName { get; set; }
    }
}