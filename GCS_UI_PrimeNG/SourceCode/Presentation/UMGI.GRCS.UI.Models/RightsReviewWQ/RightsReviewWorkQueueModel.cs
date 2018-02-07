/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : RightsReviewWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Karthik
 * Created on     : 12-02-2013 
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
using UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models.RightsReviewWQ
{
    public class RightsReviewWorkQueueModel : IRightsReviewWorkQueueModel
    {
        public ReleaseRightsResult ReleaseRightsResult { get; set; }
        public ReviewRightsMasterInfo MasterData { get; set; }
    }
}
