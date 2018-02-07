using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractTemplate.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Contract Template
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.Templates
{
    /// <summary>
    ///  ContractTemplate which is inherited from TemplateDetails
    /// </summary>
    [DataContract]
    public class ContractTemplate : TemplateDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractTemplate"/> class.
        /// </summary>
        public ContractTemplate()
        {
            ContractDetailsData = new ContractDetails();
        }

        /// <summary>
        /// Gets or sets the contract details data.
        /// </summary>
        /// <value>The contract details data.</value>
        [DataMember]
        public ContractDetails ContractDetailsData { get; set; }
    }
}