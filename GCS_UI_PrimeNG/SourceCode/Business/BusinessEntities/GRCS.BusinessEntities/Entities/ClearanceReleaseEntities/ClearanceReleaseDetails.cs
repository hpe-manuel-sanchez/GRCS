/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceReleaseDetails.cs
 * Project Code : UMG-GRCS 
 * Author       : Jelio Halleys J
 * Created on   : 22-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks

*************************************************************************** 
 * Description  : Contains the Basic Clearance Release Details
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceReleaseEntities
{
    [DataContract]
    public class ClearanceReleaseDetails
    {
        [DataMember]
        public long ReleaseId { get; set; }

        [DataMember]
        public long ReleaseType { get; set; }

        [DataMember]
        public bool IsExisting { get; set; }

        [DataMember]
        public long ProjectId { get; set; }

        [DataMember]
        public string UPC { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string VersionTitle { get; set; }
        
        [DataMember]
        public List<long> R2TalenNameId { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public byte? MusicTypeId { get; set; }

        [DataMember]
        public string MusicType { get; set; }

        [DataMember]
        public string SoundTrack { get; set; }

        [DataMember]
        public int? NumberOfTracks { get; set; }

        [DataMember]
        public long PackageId { get; set; }

        [DataMember]
        public string Package { get; set; }
        
        [DataMember]
        public int? NumberOfComponents { get; set; }
        
        [DataMember]
        public string Configuration { get; set; }
        
        [DataMember]
        public string ConfigurationGroup { get; set; }

        [DataMember]
        public long? R2ReleaseID { get; set; }

        [DataMember]
        public RegularRetail regularRetail { get; set; }
        [DataMember]
        public Club club { get; set; }
        [DataMember]
        public NonTradition nonTradition { get; set; }
        [DataMember]
        public Promotional promotional { get; set; }
        [DataMember]
        public TVRadioBreakICLA tvRadioBreakICLA { get; set; }
        [DataMember]
        public PriceReduction priceReduction { get; set; }
    }

    #region Non Traditional Project
    [DataContract]
    public class RegularRetail
    {
        [DataMember]
        public string PriceLevel { get; set; }
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
    }

    [DataContract]
    public class Club
    {
        [DataMember]
        public string PriceLevel { get; set; }
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
    }

    [DataContract]
    public class NonTradition
    {
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
        [DataMember]
        public bool? ICLAFlag { get; set; }
        [DataMember]
        public bool? SuggestedFeeFlag { get; set; }
        [DataMember]
        public decimal? InvoicePrice { get; set; }
        [DataMember]
        public decimal? SellingPriceLessVAT { get; set; }
        [DataMember]
        public decimal? ICLAAccountingBase { get; set; }
        [DataMember]
        public decimal? ResourceFee { get; set; }
        [DataMember]
        public decimal? DeemedPPD { get; set; }
    }

    [DataContract]
    public class Promotional
    {
        [DataMember]
        public string PriceLevel { get; set; }
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
    }

    [DataContract]
    public class TVRadioBreakICLA
    {
        [DataMember]
        public string PriceLevel { get; set; }
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
        [DataMember]
        public decimal? ExactPPD { get; set; }
        [DataMember]
        public decimal? EstimatedRetail { get; set; }
    }

    [DataContract]
    public class PriceReduction
    {
        [DataMember]
        public string PriceLevel { get; set; }
        [DataMember]
        public string ICLALevel { get; set; }
        [DataMember]
        public bool? DeviatedICLAlevelFlag { get; set; }
        [DataMember]
        public string DeviatedICLAlevel { get; set; }
    }

    #endregion

}
