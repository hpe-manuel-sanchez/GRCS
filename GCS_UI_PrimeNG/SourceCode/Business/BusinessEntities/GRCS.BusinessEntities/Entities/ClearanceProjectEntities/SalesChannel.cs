using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    public class SalesChannel
    {
        [DataMember]
        public long ReleaseId { get; set; }

        [DataMember]
        public long Sales_Channel_Id { get; set; }

        [DataMember]
        public long Clr_Project_Id { get; set; }

        [DataMember]
        public string Release_Dt { get; set; }

        [DataMember]
        public byte Price_Level_Type { get; set; }

        [DataMember]
        public bool Icla_Level_Deviated { get; set; }

        [DataMember]
        public byte Icla_Level_Type { get; set; }

        [DataMember]
        public byte Icla_Level_Deviated_Type { get; set; }

        [DataMember]
        public string Icla_Level_Deviated_Comment { get; set; }

        [DataMember]
        public string Created_User { get; set; }

        [DataMember]
        public DateTime Created_Dttm { get; set; }

        [DataMember]
        public string Modified_User { get; set; }

        [DataMember]
        public DateTime Modified_Dttm { get; set; }

        [DataMember]
        public string Archive_Flag { get; set; }


        [DataMember]
        public Decimal? ExactPPD { get; set; }

        [DataMember]
        public Decimal? EstimatedRetail { get; set; }

        [DataMember]
        public Decimal? InvoicePrice { get; set; }


        [DataMember]
        public Decimal? ResourceFee { get; set; }

        [DataMember]
        public Decimal? DeemedPPD { get; set; }

        [DataMember]
        public Decimal? ICLAAccountingBase { get; set; }

        [DataMember]
        public Decimal? SellingPriceLesVAT { get; set; }

        [DataMember]
        public Boolean Is_ICLA { get; set; }

        [DataMember]
        public Boolean Is_SuggestedFee { get; set; }

        [DataMember]
        public string NewRelease_Comments { get; set; }
    }
}
