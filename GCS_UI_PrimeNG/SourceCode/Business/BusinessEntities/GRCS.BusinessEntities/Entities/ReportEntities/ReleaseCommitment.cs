/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseCommitment.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vinod
 * Created on   : 15-Feb-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description : Defines the entities for Active Roster Report Search Result
                  
****************************************************************************/
using System;
using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    [DataContract]
    public class ReleaseCommitment
    {
        /// <summary>
        /// Class for ReleaseCommitment search result
        /// </summary>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Artist and Concatenated ContractingParties .
        /// </summary>
        [DataMember]
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
        [DataMember]
        public String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract Clearence Admin Company
        /// </summary>
        [DataMember]
        public String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
        [DataMember]
        public String CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Description.
        /// </summary>
        [DataMember]
        public String ContractDescription { get; set; }

        /// <summary>
        /// Gets or sets the End Of Term Date .
        /// </summary>
        [DataMember]
        public String EndOfTermDate { get; set; }

        /// <summary>
        ///  Gets or sets the Rights Period .
        /// </summary>
        [DataMember]
        public string RightsPeriod { get; set; }

        /// <summary>
        ///  Gets or sets the RightsExpiryRule .
        /// </summary>
        [DataMember]
        public string RightsExpiryRule { get; set; }

        /// <summary>
        ///  Gets or sets the LostRightsDate.
        /// </summary>
        [DataMember]
        public String LostRightsDate { get; set; }

        /// <summary>
        /// Gets or sets the TerritorialRight .
        /// </summary>
        [DataMember]
        public String TerritorialRight { get; set; }

        /// <summary>
        ///  Gets or sets the ReleaseCommitmentAndRightsReversion.
        /// </summary>
        [DataMember]
        public string ReleaseCommitmentAndRightsReversion { get; set; }
        
        /// <summary>
        ///  Gets or sets the Number Of Linked Release.
        /// </summary>
        [DataMember]
        public int? NoOfLinkedReleases { get; set; }

        /// <summary>
        ///  Gets or sets the Number Of Linked Resource.
        /// </summary>
        [DataMember]
        public int? NoOfLinkedResources { get; set; }

        /// <summary>
        /// The workflow status.
        /// </summary>
        [DataMember]
        public List<StringIdentifier> WorkflowStatus { get; set; }
        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }
    }
}
