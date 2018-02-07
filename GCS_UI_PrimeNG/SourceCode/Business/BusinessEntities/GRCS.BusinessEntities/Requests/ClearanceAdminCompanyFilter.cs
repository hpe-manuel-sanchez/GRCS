/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Pavan Kumar K
 * Created on   : 12-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Filter for Clearance Admin Company 
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    [DataContract]
    public class ClearanceAdminCompanyFilter : EntityInformation
    {
        /// <summary>
        /// Gets or sets the Company Name to filter.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company ids.
        /// </summary>
        /// <value>The clearance admin company ids.</value>
        [DataMember]
        public List<long> ClearanceAdminCompanyIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has page details.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has page details; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasPageDetails { get; set; }

        /// <summary>
        /// Gets or sets the page details.
        /// </summary>
        /// <value>The page details.</value>
        [DataMember]
        public Page PageDetails { get; set; }

        public ClearanceAdminCompanyFilter()
        {
            ClearanceAdminCompanyIds = new List<long>();
        }
    }
}
