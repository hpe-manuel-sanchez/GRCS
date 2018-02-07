/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DigitalRestrictionTemplate.cs 
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
 *Description :  Defines the entities for Digital Restriction Templates 
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.Templates
{
    /// <summary>
    ///  DigitalRestrictionTemplate which is inherited from TemplateDetails
    /// </summary>
    [DataContract]
    public class DigitalRestrictionTemplate : TemplateDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalRestrictionTemplate"/> class.
        /// </summary>
        public DigitalRestrictionTemplate()
        {
            DigitalRestrictions = new List<ContractDigitalRestrictions>();
        }

        /// <summary>
        /// Gets or sets the digital restrictions.
        /// </summary>
        /// <value>The digital restrictions.</value>
        [DataMember(IsRequired = true)]
        public List<ContractDigitalRestrictions> DigitalRestrictions { get; set; }
    }
}