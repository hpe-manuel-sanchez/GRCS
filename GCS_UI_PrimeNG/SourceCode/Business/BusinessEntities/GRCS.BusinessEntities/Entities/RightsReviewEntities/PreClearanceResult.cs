/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearanceResult.cs 
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
* Description :  Defines the entities for PreClearance Result Details
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for PreClearance Result Details
    /// </summary>
    [DataContract]
    public partial class PreClearanceResult : Resource
    {
        /// <summary>
        /// Gets or sets the pre clearance info.
        /// </summary>
        /// <value>The pre clearance info.</value>
        [DataMember]
        public PreClearanceInfo PreClearanceInfo
        {
            get;
            set;
        }
    }
}
