using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    public class RoutingVariations
    {
		[DataMember]
		public ProjectType ProjectType { get; set; }

		[DataMember]
		public RoutingVariatonRequestType RvRequestType { get; set; }

        [DataMember]
        public bool? IsSensitiveArtist { get; set; }

        [DataMember]
        public bool? IsMultiArtist { get; set; }

        [DataMember]
        public bool? IsCompilation { get; set; }

        [DataMember]
        public bool? IsSkipIntlMarketing { get; set; }

        [DataMember]
        public bool? IsSkipNtnlMarketing { get; set; }

        [DataMember]
        public bool? IsSkipLocalLabel { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsActiveArtist { get; set; }


        [DataMember]
        public bool? IsParent { get; set; }

        [DataMember]
        public long RoutingVariationId { get; set; }

		[DataMember]
		public List<BaseCompany> Companies { get; set; }

		[DataMember]
		public List<ArtistInfo> ArtistList { get; set; }

		[DataMember]
		public string IncludedReleaseTerritories { get; set; }

		[DataMember]
		public string ExcludedReleaseTerritories { get; set; }

		[DataMember]
		public string IncludedOwningTerritories { get; set; }

		[DataMember]
		public string ExcludedOwningTerritories { get; set; }

		[DataMember]
		public string IncludedRequestingTerritories { get; set; }

		[DataMember]
		public string ExcludedRequestingTerritories { get; set; }
		//Added for client side manipulations
		public string OwningCompany { get; set; }

		public string RequestingCompany { get; set; }

		public string variationTalent { get; set; }
    }

    
    
    
    
    
  
    





}
