/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsAndRestrictions.cs 
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
 *Description : Defines the entities for RightsAndRestrictions
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    /// <summary>
    /// RightsAndRestrictions
    /// </summary>
    [DataContract]
    [Serializable]
    public class RightsAndRestrictions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RightsAndRestrictions"/> class.
        /// </summary>
        public RightsAndRestrictions()
        {
            AcquisitableRights = new List<ContractRightsAcquired>();
            DigitalRestrictions = new List<ContractDigitalRestrictions>();
            DigitalRestrictionTemplates = new List<TemplateDetails>();
            AcquiredOption = new List<StringIdentifier>();
            AcquiredStatus = new List<StringIdentifier>();
        }

        /// <summary>
        /// Gets or sets the acquisitable rights.
        /// </summary>
        /// <value>The acquisitable rights.</value>
        [DataMember(IsRequired = true)]
        public List<ContractRightsAcquired> AcquisitableRights { get; set; }


        /// <summary>
        /// Gets or sets the digital restrictions.
        /// </summary>
        /// <value>The digital restrictions.</value>
        [DataMember(IsRequired = false)]
        public List<ContractDigitalRestrictions> DigitalRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the digital restriction templates.
        /// </summary>
        /// <value>The digital restriction templates.</value>
        [DataMember(IsRequired = false)]
        public List<TemplateDetails> DigitalRestrictionTemplates { get; set; }

        /// <summary>
        /// Gets or sets the acquired option.
        /// </summary>
        /// <value>The acquired option.</value>
        [DataMember]
        public List<StringIdentifier> AcquiredOption { get; set; }

        /// <summary>
        /// Gets or sets the acquired status.
        /// </summary>
        /// <value>The acquired status.</value>
        [DataMember]
        public List<StringIdentifier> AcquiredStatus { get; set; }
    }
}