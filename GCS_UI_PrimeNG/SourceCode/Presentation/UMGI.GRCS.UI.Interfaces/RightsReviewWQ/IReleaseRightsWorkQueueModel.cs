/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IResourceRightsWorkQueueModel.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Srinivas
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

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IReleaseRightsWorkQueueModel
    {
        ReleaseRightsAcquired ReleaseRightsInfo { get; set; }
        ReviewRightsMasterInfo RightsMasterData{get;set;}
    }
}

