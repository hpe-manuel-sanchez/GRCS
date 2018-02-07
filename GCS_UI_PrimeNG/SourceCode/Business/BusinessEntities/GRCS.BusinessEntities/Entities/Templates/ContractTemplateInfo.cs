using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractTemplateInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 21-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Contract Templates Info
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.Templates
{
    /// <summary>
    ///  ContractTemplateInfo which is inherited from TemplateDetails
    /// </summary>
    [DataContract]
    public class ContractTemplateInfo : TemplateDetails
    {
        public ContractTemplateInfo()
        {
            ContractInfo = new ContractDetails();
        }

        /// <summary>
        /// Gets or sets the contract info.
        /// </summary>
        /// <value>The contract info.</value>
        [DataMember]
        public ContractDetails ContractInfo { get; set; }
    }
}