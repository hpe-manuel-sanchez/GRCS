/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceSecondaryRightsResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 13-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Secondary Rights 
                 Result Details                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Secondary
    /// Rights Result Details
    /// </summary>
    [DataContract]
    public class ResourceSecondaryRightsResult
    {
        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>The total rows.</value>
        [DataMember]
        public long TotalRows
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rights result.
        /// </summary>
        /// <value>The rights result.</value>
        [DataMember]
        public List<ResourceSecondaryRights> RightsResult
        {
            get;
            set;
        }
    }
}
