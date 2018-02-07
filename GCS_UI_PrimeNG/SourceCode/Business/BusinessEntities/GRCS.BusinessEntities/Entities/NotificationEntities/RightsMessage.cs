using UMGI.GRCS.BusinessEntities.Entities.RightsEntities;


namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    public class RightsMessage
    {
        public long LabelId
        {
            get;
            set;
        }

        public RightsType Type
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

    }
}
