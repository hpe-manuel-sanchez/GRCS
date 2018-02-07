/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceRightSet.cs 
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
* Description :  Defines the entities for Resource RightSet Details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource RightSet Details
    /// </summary>
    [DataContract]
    public class ResourceRightSet
    {
        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public ReviewStatusType Status
        {
            get;
            set;
        }
    }
}
