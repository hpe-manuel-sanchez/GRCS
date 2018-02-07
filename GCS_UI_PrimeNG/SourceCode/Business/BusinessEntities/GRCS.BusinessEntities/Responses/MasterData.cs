/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MasterData.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description : Defines the entities for Master Data
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    /// <summary>
    /// MasterData
    /// </summary>
    [DataContract]
    public class MasterData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterData"/> class.
        /// </summary>
        public MasterData()
        {
            ContractDescriptions = new List<StringIdentifier>();
            OnActiveRoster = new List<StringIdentifier>();
            RightsPeriods = new List<StringIdentifier>();
            LostRightsReasons = new List<StringIdentifier>();
            RightTypes = new List<StringIdentifier>();
            //ClearanceAdminCompanies = new List<ClearanceAdminCompany>();
            ContractStatus = new List<StringIdentifier>();
            WorkflowStatus = new List<StringIdentifier>();
            //UmgSigningCompanies = new List<UmgSigningCompany>();
            PCompanies = new List<NoticeCompany>();
        }

        /// <summary>
        /// Gets or sets the contract descriptions.
        /// </summary>
        /// <value>The contract descriptions.</value>
        [DataMember]
        public List<StringIdentifier> ContractDescriptions { get; set; }

        /// <summary>
        /// Gets or sets the on active roster.
        /// </summary>
        /// <value>The on active roster.</value>
        [DataMember]
        public List<StringIdentifier> OnActiveRoster { get; set; }

        /// <summary>
        /// Gets or sets the rights periods.
        /// </summary>
        /// <value>The rights periods.</value>
        [DataMember]
        public List<StringIdentifier> RightsPeriods { get; set; }

        /// <summary>
        /// Gets or sets the lost rights reasons.
        /// </summary>
        /// <value>The lost rights reasons.</value>
        [DataMember]
        public List<StringIdentifier> LostRightsReasons { get; set; }

        /// <summary>
        /// Gets or sets the right types.
        /// </summary>
        /// <value>The right types.</value>
        [DataMember]
        public List<StringIdentifier> RightTypes { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin companies.
        /// </summary>
        /// <value>The clearance admin companies.</value>
        [DataMember]
        public List<ClearanceAdminCompany> ClearanceAdminCompanies { get; set; }

        /// <summary>
        /// Gets or sets the contract status.
        /// </summary>
        /// <value>The contract status.</value>
        [DataMember]
        public List<StringIdentifier> ContractStatus { get; set; }

        /// <summary>
        /// Gets or sets the workflow status.
        /// </summary>
        /// <value>The workflow status.</value>
        [DataMember]
        public List<StringIdentifier> WorkflowStatus { get; set; }

        /// <summary>
        /// Gets or sets the umg signing companies.
        /// </summary>
        /// <value>The umg signing companies.</value>
        [DataMember]
        public List<UmgSigningCompany> UmgSigningCompanies { get; set; }

        /// <summary>
        /// Gets or sets the P companies.
        /// </summary>
        /// <value>The P companies.</value>
        [DataMember]
        public List<NoticeCompany> PCompanies { get; set; }
    }
}