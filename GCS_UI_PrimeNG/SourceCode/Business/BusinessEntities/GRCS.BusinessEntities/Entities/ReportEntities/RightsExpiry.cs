/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsExpiry.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Report Team
 * Created on   : 12-Feb-2013
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
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Runtime.Serialization;


namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    /// <summary>
    /// Active Roster Report Search Result
    /// </summary>
    [DataContract]
    public class RightsExpiry : EntityInformation
    {
        /// <summary>
        /// Gets or sets the Contract Id .
        /// </summary>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
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
        /// Gets or sets the Contract Local Reference Number .
        /// </summary>
        [DataMember]
        public String LocalReferenceNumber { get; set; }
        
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
        ///  Gets or sets the WorkFlowStatus.
        /// </summary>
        [DataMember]
        public string WorkFlowStatus { get; set; }
        
        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        [DataMember]
        public string LinkedRepertoire { get; set; }

        /// <summary>
        /// Gets or sets the flag for IsLostRight .
        /// </summary>
        [DataMember]
        public string IsLostRight { get; set; }
        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }
    }
      
}
