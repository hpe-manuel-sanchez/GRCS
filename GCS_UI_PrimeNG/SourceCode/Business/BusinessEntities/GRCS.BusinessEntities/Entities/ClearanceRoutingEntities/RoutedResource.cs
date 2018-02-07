using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceRoutingEntities
{
    [DataContract]
    [Serializable]
    public class RoutedResource
    {
        /// <summary>
        /// Perform a deep Copy of RoutedResource.
        /// </summary>
        /// <returns>The copied RoutedResource.</returns>
        public RoutedResource Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (RoutedResource)formatter.Deserialize(stream);
            }
        }

        [DataMember]
        public long RoutedItemId { get; set; }

        [DataMember]
        public long ResourceId { get; set; }

        //[DataMember]
        //public long ProjectId { get; set; }

        [DataMember]
        public long AdminCompanyId { get; set; }

        [DataMember]
        public long ClearanceProjectResourceId { get; set; }

        [DataMember]
        public int ApprovalStatus { get; set; }

        [DataMember]
        public long RightSetId { get; set; }

        [DataMember]
        public long ContractId { get; set; }

        [DataMember]
        public int SalesChannelId { get; set; }

        //[DataMember]
        //public ScopeType RequestType { get; set; }

        [DataMember]
        public List<RoutedRequest> Request { get; set; }

        [DataMember]
        public List<LeanContractInfo> contractInfo { get; set; }

        [DataMember]
        public bool IsArtistContract { get; set; }

        [DataMember]
        public long PrimaryArtistId { get; set; }

        [DataMember]
        public bool IsSensativeExploitation { get; set; }//spell

        [DataMember]
        public bool HasUMGIConditionChanged { get; set; }

        [DataMember]
        public bool IsGlobalCleared { get; set; }

        [DataMember]
        public long AdminCountryId { get; set; }

        //[DataMember]
        //public long WorkgroupId { get; set; }

        [DataMember]
        public int ReasonId { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public List<TerritorialDisplay> ResourceTerritories { get; set; }

        [DataMember]
        public List<long> ClearenceAdminCountryIdForResource { get; set; } //database

        [DataMember]
        public List<long> ClearenceAdminCompanyIdForResource { get; set; }

        [DataMember]
        public long ClrProjectReleaseId { get; set; }

        [DataMember]
        public long PackageId { get; set; }

        [DataMember]
        public string ResourceComments { get; set; }

        public List<RoutedResource> SplitTerritoryResources { get; set; }

        public short WorkgroupRequestTypeId { get; set; }


        private bool isAllContractsInactive = true;

        public bool IsAllContractsInactive
        {
            get
            {
                return isAllContractsInactive;
            }

            set
            {
                isAllContractsInactive = value;
            }
        }

        public RoutingVariationOutput RoutingVariationInfo { get; set; }

        [DataMember]
        public DateTime LastModifiedDateTime { get; set; }

        [DataMember]
        public List<LeanContractInfo> LinkedContract { get; set; }

        [DataMember]
        public bool IsSplitDeal { get; set; }

        public List<RoutedResource> PreviouslyRoutedSplitDeals { get; set; }

        public bool IsInternational { get; set; }

        public int MusicClassType { get; set; }

        public decimal? SuggestedFee { get; set; }
    }
}
