/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceRightsResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Rights Result details
                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource
    /// Rights Result Details
    /// </summary>
    [DataContract]
    public  class ResourceRightsResult
    {
        /// <summary>
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<ResourcesRight> ResourceRights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>The total rows.</value>
        [DataMember]
        public int TotalRows
        {
            get;
            set;
        }
    }
}
