/* *************************************************************************** 
 * Copyrights ® 2013 Universal Music Group 
 * *************************************************************************** 
 * FileName     : EmailType.cs
 * Project      : UMG GRCS
 * Author       : Arunagiri G
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
 * Description     Request status types enum

****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public enum RequestStatus
    {
        [EnumMember]
        Waiting = 1,

        [EnumMember]
        Approved = 2,

        [EnumMember]
        ConditionallyApproved = 3,

        [EnumMember]
        Rejected = 4,

        [EnumMember]
        Dispatched = 5,

        [EnumMember]
        RoutedToRccAdmin = 6,

        [EnumMember]
        Initiated = 7,

        [EnumMember]
        IgnoredForRerouting = 8,

        [EnumMember]
        ArtistConsent = 9,

        [EnumMember]
        ReInstated = 10,

        [EnumMember]
        SystemCancel = 11,

        [EnumMember]
        Cancel = 12,

        [EnumMember]
        RoutingStopped = 13,

        [EnumMember]
        Deleted = 14,

        [EnumMember]
        SystemReInstated = 15,

        [EnumMember]
        UndoArtistConsent = 16,

        [EnumMember]
        MoveToResearchFolder = 76,

        [EnumMember]
        MoveToInternalReviewFolder = 77,

        [EnumMember]
        MoveToSideArtistSample = 78,

        [EnumMember]
        UndoMoveToResearchFolder = 79,

        [EnumMember]
        UndoMoveToInternalReviewFolder = 80,

        [EnumMember]
        UndoMoveToSideArtistSample = 81,
        
        [EnumMember]
        AssignedTo = 82
    }
}
