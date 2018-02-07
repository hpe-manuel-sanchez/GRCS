/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GcsAuthentication.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Sudharani Dasari
 * Created on   : 02-11-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
                             
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the GcsAuthentication entities.                                      
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    [DataContract]
    [Serializable]
    public class GcsAuthentication
    {
        /// <summary>
        /// GRS permission
        /// </summary>
        [DataMember]
        public GcsPermission[] Permissions { get; set; }

        /// <summary>
        /// Target Application
        /// </summary>
        [DataMember]
        public AnaTargetApplication TargetApplication { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// check Authorized
        /// </summary>
        [DataMember]
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// User EmailId
        /// </summary>
        [DataMember]
        public string EmailId { get; set; }

        /// <summary>
        /// GCS Workgroup
        /// </summary>
        [DataMember]
        public List<KeyValuePair<long, string>> Workgroups { get; set; }

        /// <summary>
        /// GCS Roles
        /// </summary>
        [DataMember]
        public List<KeyValuePair<Byte, string>> Roles { get; set; }

		/// <summary>
		/// Is User Mimiced
		/// </summary>
		[DataMember]
		public bool IsMimicUser { get; set; }

		/// <summary>
		/// Mimiced user name
		/// </summary>
		[DataMember]
		public string MimicUserName { get; set; }

		/// <summary>
		/// user Id
		/// </summary>
        [DataMember]
        public long UserId { get; set; }

		/// <summary>
		/// GCS Group Roles
		/// Will hold the list of groups to which user belongs to. This will be used to define inbox tabs
		/// </summary>
		[DataMember]
		public List<KeyValuePair<Byte, string>> RoleGroups { get; set; }

		/// <summary>
		/// Default user’s group. Will be used to show the default selected tab for inbox
		/// </summary>
		[DataMember]
		public KeyValuePair<Byte, string> PreferredRoleGroup { get; set; }


		/// <summary>
		/// User Special Rights
		/// </summary>
		public List<GcsUserSpecialRights> UserSpecialPermission { get; set; }

		/// <summary>
		/// user Default Currency
		/// </summary>
		[DataMember]
		public string DefaultCurrency { get; set; }

		/// <summary>
		/// user Requested CompanyId
		/// </summary>
		[DataMember]
		public long DefaultRequestingCompanyId { get; set; }

        /// <summary>
        /// UserPreference Initial Count Check
        /// </summary>
        [DataMember]
        public bool IsUserPreferenceSet { get; set; }
    }
}