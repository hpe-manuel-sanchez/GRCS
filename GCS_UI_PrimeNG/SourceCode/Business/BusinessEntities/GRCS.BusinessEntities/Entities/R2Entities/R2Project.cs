/* ************************************************************************
 * Copyrights � 2012 UMGI
 * ************************************************************************
 * File Name    : R2Project.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : gsenthilkumar
 * Created on   : 29-12-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.R2Entities
{
    /// <summary>
    /// This class is intended to used for creating Projects in R2 
    /// </summary>
    [DataContract]
    public class R2Project : EntityInformation
    {
        [DataMember]
        public long? Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long? CompanyId { get; set; }

        [DataMember]
        public long? DivisionId { get; set; }

        [DataMember]
        public long? LabelId { get; set; }

        [DataMember]
        public long? BudgetNumber { get; set; }

        [DataMember]
        public string RoyaltyAdmin { get; set; }

        [DataMember]
        public char? Status { get; set; }

        [DataMember]
        public string ProfitCentreCode { get; set; }

        [DataMember]
        public string FinancialLabelCode { get; set; }

        [DataMember]
        public List<long> ArtistIds { get; set; }
    }
}