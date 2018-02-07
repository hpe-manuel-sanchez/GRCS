using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ResourcesRight
    {
        /// <summary>
        /// Constructs the information for save.
        /// </summary>
        /// <returns></returns>
        public ResourceAcquiredRights ConstructSaveInfo()
        {
            var _dict = new Dictionary<string, bool>
                            {
                                {"N", false},
                                {"Y", true}
                            };

            var _dictException = new Dictionary<string, bool>
                            {
                                {"Applied",true},
                                {"NotApplied",false}
                            };
            Rights.ActiveForMrkt = ActiveForMrktLnr != null ? _dict[ActiveForMrktLnr] : (bool?)null;
            //RightsPeriodLnr = RightsPeriodLnr.Replace(" ", "").Replace(" ","");
            Rights.LostRights = LostRightsLnr != null ? _dict[LostRightsLnr] : (bool?)null;
            //Rights.LostRightsDate = LostRightsDateLnr != null ? Convert.ToDateTime(LostRightsDateLnr) : (DateTime?)null;            
            Rights.LostRightsDate = !string.IsNullOrEmpty(LostRightsDateLinear) ? Convert.ToDateTime(LostRightsDateLinear) : (DateTime?)null;
            //LostRightsReasonLnr = LostRightsReasonLnr.Replace(" ", "").Replace(" ", "");
            SampleExists = SampleExistsLnr == "Y";
            SideArtist = SideArtistLnr == "Y";
            Rights.Exception = RightsExceptionLnr != null ? _dictException[RightsExceptionLnr.Replace(" ", "").Replace(" ", "")] : (bool?)null;
            Rights.PhysicallyExploited = PhysicalExpLnr != null ? _dict[PhysicalExpLnr] : (bool?)null;
            Rights.DigitallyExploited = DigitalExpLnr != null ? _dict[DigitalExpLnr] : (bool?)null;
            Rights.DigitallyUnbundled = DigitalUnbundledLnr != null ? _dict[DigitalUnbundledLnr] : (bool?)null;
            Rights.MobileExploited = MobileExpLnr != null ? _dict[MobileExpLnr] : (bool?)null;
            Rights.PPBClaim = PpbClaimLnr != null ? _dict[PpbClaimLnr] : (bool?)null;
            Rights.Status.Status = (ReviewStatusType)Enum.Parse(typeof(ReviewStatusType), ReviewStatusLnr);
            //  Enum.TryParse(ReviewStatusLnr, true, out Rights.Status.Status);
            RightTypes rightsType;
            Enum.TryParse(RightsTypeLnr, true, out rightsType);
            Rights.RightSetId = RightsId;
            Rights.ModifiedDateTime = ModifiedDateTimeLnr;
            if(ExcludedCountryLnr !=null)
            Rights.ExcludedCountry = saveInfo(ExcludedCountryLnr);
            if (IncludedCountryLnr != null)
            Rights.IncludedCountry = saveInfo(IncludedCountryLnr);
            if (ExcludedTerritoryLnr != null)
            Rights.ExcludedTerritory = saveInfo(ExcludedTerritoryLnr);
            if (IncludedTerritoryLnr != null)
            Rights.IncludedTerritory = saveInfo(IncludedTerritoryLnr);
            Rights.TerritorialRights = TerritoryRightsLnr;
            
            return Rights;
        }

        private List<long> saveInfo(string jsonData)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<long>));
            var JsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            var jsonArray = (List<long>)jsonSerializer.ReadObject(JsonStream);
            return jsonArray;
        }

        /// <summary>
        /// Constructs the view information for grid.
        /// </summary>
        /// <returns></returns>
        public ResourcesRight ConstructViewInfo()
        {
            var _dict = new Dictionary<string, string>
                            {
                                {"True", "Y"},
                                {"False", "N"}
                            };
            var _dictException = new Dictionary<string, string>
                            {
                                {"True", "Applied"},
                                {"False", "Not Applied"}
                            };
            ActiveForMrktLnr = Rights.ActiveForMrkt != null ? _dict[Rights.ActiveForMrkt.ToString()] : "";
            LostRightsLnr = Rights.LostRights != null ? _dict[Rights.LostRights.ToString()] : "";
            SampleExistsLnr = SampleExists ? "Y" : "";
            SideArtistLnr = SideArtist ? "Y" : "";
            LostRightsDateLnr = Rights.LostRightsDate.HasValue ? Rights.LostRightsDate.Value : (DateTime?)null;
            RightsExceptionLnr = Rights.Exception != null ? _dictException[Rights.Exception.ToString()] : "";
            PhysicalExpLnr = Rights.PhysicallyExploited != null ? _dict[Rights.PhysicallyExploited.ToString()] : "";
            DigitalExpLnr = Rights.DigitallyExploited != null ? _dict[Rights.DigitallyExploited.ToString()] : "";
            DigitalUnbundledLnr = Rights.DigitallyUnbundled != null ? _dict[Rights.DigitallyUnbundled.ToString()] : "";
            MobileExpLnr = Rights.MobileExploited != null ? _dict[Rights.MobileExploited.ToString()] : "";
            PpbClaimLnr = Rights.PPBClaim != null ? _dict[Rights.PPBClaim.ToString()] : "";
            ReviewStatusLnr = Rights.Status.Status.ToString();
            RightsId = Rights.RightSetId;
            //RightsTypeLnr =  RightsType.ToString();
            TerritoryRightsLnr = Rights.TerritorialRights;
            if (TerritoryRightsLnr == "null")
                TerritoryRightsLnr = "";
            SampleExistsLnr = SampleExists ? "Y" : "";
            SideArtistLnr = SideArtist ? "Y" : "";
            ModifiedDateTimeLnr = Rights.ModifiedDateTime;
            ISRCLnr = ISRCId;
            UPCLnr = UPCId;
            LostRightsDateLinear = Rights.LostRightsDate.HasValue?Rights.LostRightsDate.Value.ToString():"";            
            ContractIdLnr = Rights.ContractId.HasValue ? Rights.ContractId.Value.ToString() : string.Empty;
            
            return this;
        }

        /// <summary>
        /// Gets or sets the territory exclusions.
        /// </summary>
        /// <value>The territory exclusions.</value>
        public string TerritoryRightsLnr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rightstype Linear.
        /// </summary>
        /// <value>The rights type LNT.</value>
        public string RightsTypeLnr { get; set; }


        /// <summary>
        /// Gets or sets the sample Status.
        /// </summary>
        /// <value>The is sample LNR.</value>
        public string SampleExistsLnr { get; set; }

        /// <summary>
        /// Gets or sets the side artist status.
        /// </summary>
        /// <value>The is side artist string.</value>
        public string SideArtistLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string ActiveForMrktLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public long RightsId { get; set; }


        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string RightsPeriodLnr { get; set; }


        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string LostRightsLnr { get; set; }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public DateTime? LostRightsDateLnr { get; set; }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string LostRightsReasonLnr { get; set; }

        /// <summary>
        /// Gets or sets the rights period.
        /// </summary>
        /// <value>The notes.</value>
        public string ReviewReasonLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string RightsExceptionLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string PhysicalExpLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string DigitalExpLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string DigitalUnbundledLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string MobileExpLnr { get; set; }

        /// <summary>
        /// Gets or sets ppbclaim
        /// </summary>
        /// <value>The notes.</value>
        public string PpbClaimLnr { get; set; }

        /// <summary>
        /// Gets or sets the active for marketing.
        /// </summary>
        /// <value>The notes.</value>
        public string ReviewStatusLnr { get; set; }

        /// <summary>
        /// Gets or sets Error
        /// </summary>
        /// <value>The Error.</value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets Error
        /// </summary>
        /// <value>The Error.</value>
        public string ModifiedDateTimeLnr { get; set; }


        /// <summary>
        /// Gets or sets the UPC Linear.
        /// </summary>
        /// <value>The UPC LNR.</value>
        public string UPCLnr { get; set; }



        /// <summary>
        /// Gets or sets the ISRC Linear.
        /// </summary>
        /// <value>The ISRC LNR.</value>
        public string ISRCLnr { get; set; }

        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        public string ContractIdLnr { get; set; }

        /// <summary>
        /// Gets or sets the IncludedCountry.
        /// </summary>
        /// <value>The status.</value>     
        public string IncludedCountryLnr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IncludedTerritory.
        /// </summary>
        /// <value>The status.</value>        
        public string IncludedTerritoryLnr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ExcludedCountry.
        /// </summary>
        /// <value>The status.</value>        
        public string ExcludedCountryLnr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ExcludedTerritory.
        /// </summary>
        /// <value>The status.</value>        
        public string ExcludedTerritoryLnr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lost rights date linear.
        /// </summary>
        /// <value>The lost rights date linear.</value>
        public string LostRightsDateLinear
        {
            get;
            set;
        }
    }
}
