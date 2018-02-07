/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseRightsResult.cs 
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
* Description :  Defines the entities for Release Rights Result details
                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Rights Results Details
    /// </summary>
    [DataContract]
    public class ReleaseRightsResult : EntityInformation
    {

        public ReleaseRightsResult()
        {
            ReleaseRights = new List<ReleaseRight>();
        }
        /// <summary>
        /// Gets or sets the release rights.
        /// </summary>
        /// <value>The release rights.</value>
        [DataMember]
        public List<ReleaseRight> ReleaseRights
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
