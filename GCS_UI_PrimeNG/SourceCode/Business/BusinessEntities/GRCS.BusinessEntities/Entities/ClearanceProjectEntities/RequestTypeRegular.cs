/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RequestTypeRegular.cs 
 * Project Code : UMG-GRCS 
 * Author       : sarika tyagi
 * Created on   : 10-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceProjectEntities
{
    [DataContract]
    [Serializable]
    public class RequestTypeRegular
    {

        public RequestTypeRegular()
        {
            newlyAddedSalesChannelsAfterSubmit = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// Physical
        /// </summary>
        [DataMember]
        //[Display(Name = "Physical", ResourceType = typeof(Entity))]
        public bool Physical { get; set; }

        /// <summary>
        /// Digital
        /// </summary>
        [DataMember]
        //[Display(Name = "Digital", ResourceType = typeof(Entity))]
        public bool Digital { get; set; }

        /// <summary>
        /// RegularRetail
        /// </summary>
        [DataMember]
        public bool RegularRetail { get; set; }

        /// <summary>
        /// Club
        /// </summary>
        [DataMember]
        public bool Club { get; set; }

        /// <summary>
        /// NonTraditional
        /// </summary>
        [DataMember]
        public bool NonTraditional { get; set; }

        /// <summary>
        /// Promotional
        /// </summary>
        [DataMember]
        public bool Promotional { get; set; }

        /// <summary>
        /// TVRadioBreakICLA
        /// </summary>
        [DataMember]
        public bool TVRadioBreakICLA { get; set; }

        /// <summary>
        /// PriceReduction
        /// </summary>
        [DataMember]
        public bool PriceReduction { get; set; }

        /// <summary>
        /// DurationFrom
        /// </summary>
        [DataMember]
        public string DurationFrom { get; set; }

        /// <summary>
        /// DurationTo
        /// </summary>
        [DataMember]
        public string DurationTo { get; set; }

        /// <summary>
        /// SalesChannelPhysical
        /// </summary>
        [DataMember]
        public bool SalesChannelPhysical { get; set; }

        /// <summary>
        /// SalesChannelAlaCarteDownload
        /// </summary>
        [DataMember]
        public bool SalesChannelAlaCarteDownload { get; set; }

        /// <summary>
        /// SalesChannelSubscriptionDownload
        /// </summary>
        [DataMember]
        public bool SalesChannelSubscriptionDownload { get; set; }

        /// <summary>
        /// SalesChannelMobileRealTones
        /// </summary>
        [DataMember]
        public bool SalesChannelMobileRealTones { get; set; }

        /// <summary>
        /// SalesChannelMobileRingBackTones
        /// </summary>
        [DataMember]
        public bool SalesChannelMobileRingBackTones { get; set; }

        /// <summary>
        /// Streaming
        /// </summary>
        [DataMember]
        public bool Streaming { get; set; }

        /// <summary>
        /// PhysicalSalesSplitSalesToDate
        /// </summary>
        [DataMember]
        public int? PhysicalSalesSplitSalesToDate { get; set; }


        /// <summary>
        /// PhysicalSalesSplitSalesWith
        /// </summary>
        [DataMember]
        public int? PhysicalSalesSplitSalesWith { get; set; }

        /// <summary>
        /// PhysicalSalesSplitSalesWithout
        /// </summary>
        [DataMember]
        public int? PhysicalSalesSplitSalesWithout { get; set; }

        /// <summary>
        /// DigitalSalesSplitSalesToDate
        /// </summary>
        [DataMember]
        public int? DigitalSalesSplitSalesToDate { get; set; }

        /// <summary>
        /// DigitalSalesSplitSalesWith
        /// </summary>
        [DataMember]
        public int? DigitalSalesSplitSalesWith { get; set; }

        /// <summary>
        /// DigitalSalesSplitSalesWithout
        /// </summary>
        [DataMember]
        public int? DigitalSalesSplitSalesWithout { get; set; }

        /// <summary>
        /// TotalSalesSplitSalesToDate
        /// </summary>
        [DataMember]
        public int? TotalSalesSplitSalesToDate { get; set; }

        /// <summary>
        /// TotalSalesSplitSalesWith
        /// </summary>
        [DataMember]
        public int? TotalSalesSplitSalesWith { get; set; }

        /// <summary>
        /// TotalSalesSplitSalesWithout
        /// </summary>
        [DataMember]
        public int? TotalSalesSplitSalesWithout { get; set; }

        /// <summary>
        /// PhysicalRevenueToDate
        /// </summary>
        [DataMember]
        public decimal? PhysicalRevenueToDate { get; set; }

        /// <summary>
        /// PhysicalRevenueWith
        /// </summary>
        [DataMember]
        public decimal? PhysicalRevenueWith { get; set; }

        /// <summary>
        /// PhysicalRevenueWithout
        /// </summary>
        [DataMember]
        public decimal? PhysicalRevenueWithout { get; set; }

        /// <summary>
        /// DigitalRevenueToDate
        /// </summary>
        [DataMember]
        public decimal? DigitalRevenueToDate { get; set; }

        /// <summary>
        /// DigitalRevenueWith
        /// </summary>
        [DataMember]
        public decimal? DigitalRevenueWith { get; set; }

        /// <summary>
        /// DigitalRevenueWithout
        /// </summary>
        [DataMember]
        public decimal? DigitalRevenueWithout { get; set; }

        /// <summary>
        /// TotalRevenueToDate
        /// </summary>
        [DataMember]
        public decimal? TotalRevenueToDate { get; set; }

        /// <summary>
        /// TotalRevenueWith
        /// </summary>
        [DataMember]
        public decimal? TotalRevenueWith { get; set; }

        /// <summary>
        /// TotalRevenueWithout
        /// </summary>
        [DataMember]
        public decimal? TotalRevenueWithout { get; set; }

        /// <summary>
        /// CurrentPriceLevel_ID
        /// </summary>
        [DataMember]
        public int CurrentPriceLevel_ID { get; set; }

        /// <summary>
        /// CurrentPriceLevel_Name
        /// </summary>
        [DataMember]
        public string CurrentPriceLevel_Name { get; set; }

        /// <summary>
        /// RequestedPriceLevel_ID
        /// </summary>
        [DataMember]
        public int RequestedPriceLevel_ID { get; set; }

        /// <summary>
        /// RequestedPriceLevel
        /// </summary>
        [DataMember]
        public string RequestedPriceLevel { get; set; }

        /// <summary>
        /// AdditionalMailOrder
        /// </summary>
        [DataMember]
        public bool AdditionalMailOrder { get; set; }

        /// <summary>
        /// IntroductoryUse
        /// </summary>
        [DataMember]
        public bool IntroductoryUse { get; set; }

        /// <summary>
        /// DistributionTo
        /// </summary>
        [DataMember]
        public string DistributionTo { get; set; }

        /// <summary>
        /// ClientName
        /// </summary>
        [DataMember]
        public string ClientName { get; set; }

        /// <summary>
        /// ClientWebsite
        /// </summary>
        [DataMember]
        public string ClientWebsite { get; set; }

        /// <summary>
        /// MediaPromoSpendComment
        /// </summary>
        [DataMember]
        public string MediaPromoSpendComment { get; set; }

        /// <summary>
        /// ManufacturedByUMG
        /// </summary>
        [DataMember]
        public string ManufacturedByUMG { get; set; }

        /// <summary>
        /// Partwork
        /// </summary>
        [DataMember]
        public bool Partwork { get; set; }

        /// <summary>
        /// Kiosk
        /// </summary>
        [DataMember]
        public bool Kiosk { get; set; }

        /// <summary>
        /// MailOrder
        /// </summary>
        [DataMember]
        public bool MailOrder { get; set; }

        /// <summary>
        /// Internet
        /// </summary>
        [DataMember]
        public bool Internet { get; set; }

        /// <summary>
        /// DirectResponse
        /// </summary>
        [DataMember]
        public bool DirectResponse { get; set; }

        /// <summary>
        /// Educational
        /// </summary>
        [DataMember]
        public bool Educational { get; set; }

        /// <summary>
        /// Premium
        /// </summary>
        [DataMember]
        public bool Premium { get; set; }

        /// <summary>
        /// GiveAwayFreeCharge
        /// </summary>
        [DataMember]
        public bool GiveAwayFreeCharge { get; set; }

        /// <summary>
        /// Other
        /// </summary>
        [DataMember]
        public bool Other { get; set; }

        /// <summary>
        /// PremiumComments
        /// </summary>
        [DataMember]
        public string PremiumComments { get; set; }

        /// <summary>
        /// GiveAwayComments
        /// </summary>
        [DataMember]
        public string GiveAwayComments { get; set; }


        /// <summary>
        /// OtherComments
        /// </summary>
        [DataMember]
        public string OtherComments { get; set; }

        /// <summary>
        /// TV
        /// </summary>
        [DataMember]
        public TV TV { get; set; }

        /// <summary>
        /// Radio
        /// </summary>
        [DataMember]
        public Radio Radio { get; set; }

        /// <summary>
        /// OtherICLA
        /// </summary>
        [DataMember]
        public OthersICLA OthersICLA { get; set; }

        /// <summary>
        /// Territories
        /// </summary>
        [DataMember]
        public List<TerritorialDisplay> Territories { get; set; }
       

        // newly added sales channel, by default it is 15
        [DataMember]
        public List<int> newlyAddedSalesChannelsAfterSubmit { get; set; }

        [DataMember]
        public string IncludedTerritories { get; set; }

        [DataMember]
        public string ExcludedTerritories { get; set; }

    }
}
