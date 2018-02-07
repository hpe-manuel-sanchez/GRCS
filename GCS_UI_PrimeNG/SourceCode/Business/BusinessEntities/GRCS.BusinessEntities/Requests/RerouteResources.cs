/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Pavan Kumar Kota
 * Created on   : 20-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Request object to enclose the details of Reroute resource(s) 
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    /// <summary>
    /// For rerouting resources from workqueue
    /// </summary>
    [DataContract]
    public class RerouteResources : EntityInformation
    {
        /// <summary>
        /// Gets or sets the work queue items.
        /// </summary>
        /// <value>The work queue items.</value>
        [DataMember]
        public List<long> WorkQueueItems { get; set; }

        /// <summary>
        /// Gets or sets to RCC user.
        /// </summary>
        /// <value>To RCC user. Null by default. True for RCC user, False for Clearance Admin Company</value>
        [DataMember]
        public bool? ToRccUser { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company.
        /// </summary>
        /// <value>The clearance admin company.</value>
        [DataMember]
        public long ClearanceAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets to RCC user.
        /// </summary>
        /// <value>To RCC user. Null by default. True for RCC user, False for ContractId</value>
        [DataMember]
        public bool? ContractId { get; set; }

        public RerouteResources()
        {
            WorkQueueItems = new List<long>();           
        }
    }
}
