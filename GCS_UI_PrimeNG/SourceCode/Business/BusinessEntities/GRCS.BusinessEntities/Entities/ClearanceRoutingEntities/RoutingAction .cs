/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RoutingAction.cs
 * Project      : UMG GRCS
 * Author       : 
 * Created on   :  
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
****************************************************************************
 * Description     Declare Routing action types enum

****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RoutingAction
    {
        [EnumMember] Approve,
        [EnumMember] ConditionallyApprove,
        [EnumMember] Reject,
        [EnumMember] Dispatch,
        [EnumMember] RouteToRccAdmin,
        [EnumMember] Route,
        [EnumMember] Cancel,
        [EnumMember] ReApply,
        [EnumMember] ReInstate,
        [EnumMember] Include,
        [EnumMember] Exclude,
        [EnumMember] ArtistConsent,
        [EnumMember] SystemCancel,
        [EnumMember] Delete,
        [EnumMember] Reminders,
        [EnumMember] SystemReInstate,
        [EnumMember] UndoArtistConsent,
        [EnumMember] AutomaticCancel,
        [EnumMember] MoveToResearchFolder = 76,   
        [EnumMember] MoveToInternalReviewFolder = 77,   
        [EnumMember] MoveToSideArtistSample = 78,   
        [EnumMember] UndoMoveToResearchFolder = 79,        
        [EnumMember] UndoMoveToInternalReviewFolder = 80,        
        [EnumMember] UndoMoveToSideArtistSample = 81,
        [EnumMember] AssignedTo = 82
    }
}