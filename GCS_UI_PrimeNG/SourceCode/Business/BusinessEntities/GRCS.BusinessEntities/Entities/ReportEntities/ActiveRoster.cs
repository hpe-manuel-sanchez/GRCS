/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ActiveRoster.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Richa
 * Created on   : 22-Jan-2013
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
{/// <summary>
 /// Active Roster Report Search Result
 /// </summary>
    [DataContract]
   public class ActiveRoster : EntityInformation
    {   
        /// <summary>
        /// Gets or sets the Contract Id .
        /// </summary>
        /// <value>The Contract Id.</value>
        [DataMember]
        public long? ContractId { get; set; }

        /// <summary>
        /// Gets or sets the Contract Local Reference Number .
        /// </summary>
        /// <value>The Contract Local Reference Number.</value>
        [DataMember]
        public String LocalReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the Artist .
        /// </summary>
        /// <value>The Artist.</value>
        [DataMember]
        public String Artist { get; set; }

        /// <summary>
        /// Gets or sets the Contract Contracting Parties .
        /// </summary>
        /// <value>The Contract Contracting Parties.</value>
        [DataMember]
        public String ContractingParties { get; set; }

        /// <summary>
        /// Gets or sets the Contract CLEARANCE CO .
        /// </summary>
        /// <value>The Contract CLEARANCE CO.</value>
        [DataMember]
        public String CLEARANCECO { get; set; }

        /// <summary>
        /// Gets or sets the Contract Commencement Date .
        /// </summary>
        /// <value>The Contract Commencement Date.</value>
        [DataMember]
        public String CommencementDate { get; set; }

        /// <summary>
        /// Gets or sets the Contract Territorial Right .
        /// </summary>
        /// <value>The Contract Territorial Right.</value>
        [DataMember]
        public String TerritorialRight { get; set; }

        /// <summary>
        /// Gets or sets the Contract UMG Signing Company.
        /// </summary>
        /// <value>The Contract UMG Signing Company.</value>
        [DataMember]
        public String UMGSigningCompany { get; set; }

        /// <summary>
        /// Gets or sets the flag for Linked Repertoire .
        /// </summary>
        /// <value>The flag for Linked Repertoire.</value>
        [DataMember]
        public string LinkedRepertoire { get; set; }
        /// <summary>
        /// Gets or sets the flag for  Total Result Count .
        /// </summary>
        /// <value>The flag for Total Result Count.</value>
        [DataMember]
        public int Total { get; set; }
      
    }
}
