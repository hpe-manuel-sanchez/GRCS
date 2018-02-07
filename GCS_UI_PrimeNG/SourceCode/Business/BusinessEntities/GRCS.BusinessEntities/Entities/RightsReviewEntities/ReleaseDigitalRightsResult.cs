/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseDigitalRightsResult.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Release Digital Rights Result
                 Details                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Digital Rights
    /// Result Details
    /// </summary>
    [DataContract]
    public class ReleaseDigitalRightsResult
    {
        public ReleaseDigitalRightsResult()
        {
            Rights = new List<ReleaseDigitalRights>();
        }
        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>The rights.</value>
        [DataMember]
        public List<ReleaseDigitalRights> Rights
        {
            get;
            set;
        }

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
    }
}
