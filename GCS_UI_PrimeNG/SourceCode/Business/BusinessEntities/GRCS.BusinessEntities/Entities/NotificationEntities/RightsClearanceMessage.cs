/* ************************************************************************ 
 * Copyrights ? 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsMessage.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Jelio Halleys J
 * Created on   : 12-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *
 * Description   : Used for Passing Rights Messages to MQ
 * 
*****************************************************************************/



namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    public class RightsClearanceMessage
    {
        public long LabelId
        {
            get;
            set;
        }

        public NotificationType Type
        {
            get;
            set;
        }

        public ContractRights ContractRights
        {
            get;
            set;
        }
        public ProjectRights ProjectRights
        {
            get;
            set;
        }
        public ReleaseRights ReleaseRights
        {
            get;
            set;
        }
        public ResourceRights ResourceRights
        {
            get;
            set;
        }
        public TrackRights TrackRights
        {
            get;
            set;
        }
        public ArtistRights ArtistRights
        {
            get;
            set;
        }

        public ClrProject ClearanceProject
        {
            get;
            set;
        }

        public ClrRelease ClearanceRelease
        {
            get;
            set;
        }

        public ClrResource ClearanceResource
        {
            get;
            set;
        }


    }
}
