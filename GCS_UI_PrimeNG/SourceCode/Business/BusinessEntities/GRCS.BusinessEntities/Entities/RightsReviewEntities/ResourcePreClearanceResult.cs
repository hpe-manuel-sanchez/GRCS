/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourcePreClearanceResult.cs 
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
* Description :  Defines the entities for Resource PreClearance Result
                 Details                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource PreClearance
    /// Result Details
    /// </summary>
    [DataContract]
    public class ResourcePreClearanceResult
    {

        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>The rights.</value>
         [DataMember]
         public List<PreClearanceResult> Rights
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
