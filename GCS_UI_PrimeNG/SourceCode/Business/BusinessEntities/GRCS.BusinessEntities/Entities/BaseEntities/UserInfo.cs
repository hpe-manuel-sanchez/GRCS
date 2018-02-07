/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : UserInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the UserInfo entities                                    
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.AnaEntities;
using System;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// UserInfo which is inherited from the BaseClass ClassInfo
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserInfo : EntityInformation
    {
        /// <summary>
        /// user login name
        /// </summary>
        [DataMember]
        public string UserLoginName { get; set; }

        /// <summary>
        /// Application Name
        /// </summary>
        [DataMember]
        public AnaTargetApplication UserApplicationName { get; set; }

        /// <summary>
        /// User Role Permission - Role,Task and DAG(country,company)
        /// </summary>
        [DataMember]
        public GrsPermission[] Permissions { get; set; }

        /// <summary>
        /// GCS User Role Permission - Role,Task and DAG
        /// </summary>
        [DataMember]
        public GcsPermission[] GcsPermissions { get; set; }

        /// <summary>
        /// user Email Id
        /// </summary>
        [DataMember]
        public string EmailId { get; set; }

        /// <summary>
        /// Preferred RoleId (out of several roles that a user can have, only one role could be - "preferred role" - at any time).
        /// </summary>
        [DataMember]
        public long PreferredRoleId { get; set; }

        /// <summary>
        /// List of User Roles
        /// </summary>
        [DataMember]
        public List<byte> Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsMimicUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MimicedRccAdminLoginName { get; set; }


    }
}