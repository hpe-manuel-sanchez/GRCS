/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsExpiryToBeDetermined.cs 
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
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ReportEntities
{
    /// <summary>
    /// Class for RightsExpiryToBeDetermined search result
    /// </summary>
    [DataContract]
    public class RightsExpiryToBeDetermined
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
        /// Gets or sets the Contract CLEARANCE CO .
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
        ///  Gets or sets the RightsExpiryRule .
        /// </summary>
        [DataMember]
        public String RightsExpiryRule { get; set; }
        
        /// <summary>
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        [DataMember]
        public String TerritorialRight { get; set; }

        /// <summary>
        ///  Gets or sets the WorkFlowStatus.
        /// </summary>
        [DataMember]
        public String WorkFlowStatus { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        [DataMember]
        public String LinkedRepertoire { get; set; }
     
        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }
    }
}
