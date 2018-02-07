/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseParameters.cs 
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
* Description :  Defines the entities for Release Parameters Details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Parameters Details
    /// </summary>
    [DataContract]
    public class ReleaseFilterParameters : RepertoireParameter
    {
        public ReleaseFilterParameters()
        {
            RepertoireFilter = new RepertoireFilter();
        }
        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The UPC.</value>
        [DataMember]
        public RepertoireFilter RepertoireFilter
        {
            get;
            set;
        }
    }
}
