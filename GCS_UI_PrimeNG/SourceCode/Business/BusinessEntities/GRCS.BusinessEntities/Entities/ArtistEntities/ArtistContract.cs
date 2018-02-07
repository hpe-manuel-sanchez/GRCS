using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.Resx.Resource.EntityResource;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    [Serializable]
    public class ArtistContract : EntityInformation
    {
        /// <summary>
        /// Artist info - TalentId
        /// </summary>
        [DataMember]
        [Display(Name = "TalentID", ResourceType = typeof(Entity))]
        public long ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        [Display(Name = "Artist", ResourceType = typeof(Entity))]
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company id.
        /// </summary>
        /// <value>The clearance admin company id.</value>
        [DataMember]
        public long ClearanceAdminCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company name.
        /// </summary>
        /// <value>The clearance admin company name.</value>
        [DataMember]
        public string ClearanceAdminCompanyName { get; set; }


        private long? _contractId=0;
        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long? ContractId
        {
            get { return _contractId; }
            set { if (value == null) value = 0;
                _contractId = value;
            }
        }

        /// <summary>
        /// Gets or sets the Additional Information.
        /// </summary>
        /// <value>Additional Information.</value>
        [DataMember]
        public string AdditionalInformation { get; set; }
        
        /// <summary>
        /// Gets or sets the IsLinked.
        /// </summary>
        /// <value>IsLinked.</value>
        [DataMember]
        public bool IsLinked { get; set; }

        /// <summary>
        /// Gets or sets the ISAC.
        /// </summary>
        /// <value>ISAC.</value>
        [DataMember]
        public string ISAC { get; set; }
         
    }


}
