/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceResourceDetails.cs
 * Project Code : UMG-GRCS 
 * Author       : Jelio Halleys J
 * Created on   : 24-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks

*************************************************************************** 
 * Description  : Contains the Basic Clearance Resource Details
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ClearanceResourceEntities
{
    [DataContract]
    public class ClearanceResourceDetails
    {
        [DataMember]
        public long GCSProjectID { get; set; }
        [DataMember]
        public long R2ReleaseID { get; set; }
        [DataMember]
        public long? R2ResourceID { get; set; }

        [DataMember]
        public List<string> RequestedCountries { get; set; }


        [DataMember]
        public decimal? SuggestedFee { get; set; }
        [DataMember]
        public string ExcerptTime { get; set; }
        [DataMember]
        public bool? SensitiveExploitationFlag { get; set; }

        [DataMember]
        public bool? GloballyClearedFlag { get; set; }

        [DataMember]
        public List<ClearanceResourceStatus> Status { get; set; }

        [DataMember]
        public string Advertising { get; set; }
        [DataMember]
        public string Film { get; set; }
        [DataMember]
        public string Trailer { get; set; }
        [DataMember]
        public string Others { get; set; }
        [DataMember]
        public string RegularRetail { get; set; }
        [DataMember]
        public string Club { get; set; }
        [DataMember]
        public string NonTradition { get; set; }
        [DataMember]
        public string Promotional { get; set; }
        [DataMember]
        public string TVRadioBreakICLA { get; set; }
        [DataMember]
        public string PriceReduction { get; set; }
        [DataMember]
        public long? ResourceID { get; set; }
    }
}
