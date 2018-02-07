/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsExpiryReportParameter.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vaishnavi Lakshmanan
 * Created on   : 05-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for RightsExpiry
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{

    /// <summary>
    /// RightsExpiryReportParameter
    /// </summary>
    [DataContract]
    public class RightsExpiryReportParameter : BaseSearch 
    {
        public RightsExpiryReportParameter()
        {
            RoleGroup = new List<RightsRoleGroup>();
            RoleGroupFilter = new RightsRoleGroup();
            RightsCountries = new RightsCountry();
        }

        /// <summary>
        /// Gets or sets the right expiry id.
        /// </summary>
        /// <value>The right expiry id.</value>
        [DataMember]
        public long RightExpiryId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public int? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public int? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The notification.</value>
        [DataMember]
        public string Notification { get; set; }

        /// <summary>
        /// Gets or sets the role group.
        /// </summary>
        /// <value>The role group.</value>
        [DataMember]
        public IEnumerable<RightsRoleGroup> RoleGroup { get; set; }

        /// <summary>
        /// Gets or sets single Role Group
        /// </summary>
        [DataMember]
        public RightsRoleGroup RoleGroupFilter { get; set; }


        /// <summary>
        /// Gets or sets the rights countries.
        /// </summary>
        /// <value>The rights countries.</value>
        [DataMember]
        public RightsCountry RightsCountries { get; set; }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>The countryName.</value>
        [DataMember]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the Email Group List.
        /// </summary>
        /// <value> Role Group List</value>
        [DataMember]
        public string RoleGroupNames { get; set; }
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
