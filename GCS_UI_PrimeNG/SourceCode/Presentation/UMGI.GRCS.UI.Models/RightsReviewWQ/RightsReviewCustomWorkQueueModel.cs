/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsReviewCustomWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Vijay Venkatesh Prasad.N
 * Created on     : 03-04-2013 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 * 
 * ***************************************************************************/

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    /// <summary>
    /// Model for Custom Rights Review WorkQueue Page
    /// </summary>
   public class RightsReviewCustomWorkQueueModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RightsReviewCustomWorkQueueModel"/> class.
        /// </summary>
       public RightsReviewCustomWorkQueueModel()
       {
           ReleaseRightsWorkQueueModel=new ReleaseRightsWorkQueueModel();
           ResourceRightsWorkQueueModel=new ResourceRightsWorkQueueModel();
       }

       /// <summary>
       /// Gets or sets the release rights work queue model.
       /// </summary>
       /// <value>The release rights work queue model.</value>
       public ReleaseRightsWorkQueueModel ReleaseRightsWorkQueueModel { get; set; }


       /// <summary>
       /// Gets or sets the resource rights work queue model.
       /// </summary>
       /// <value>The resource rights work queue model.</value>
       public ResourceRightsWorkQueueModel  ResourceRightsWorkQueueModel { get; set; }
    }
}
