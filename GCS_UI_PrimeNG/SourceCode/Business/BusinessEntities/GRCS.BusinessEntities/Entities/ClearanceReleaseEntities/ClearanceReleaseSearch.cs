using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities
{
    [DataContract]
    [Serializable]
    public class ClearanceReleaseSearch
    {

        public ClearanceReleaseSearch()
        {

        }

        [DataMember]
        public string UpcNumber { get; set; }

        [DataMember]
        public string ArtistName { get; set; }

        [DataMember]
        public int ArtistID { get; set; }

        [DataMember]
        public string ReleaseTitle { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }



    }
}
