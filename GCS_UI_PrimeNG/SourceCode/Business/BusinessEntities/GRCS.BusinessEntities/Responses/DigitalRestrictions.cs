/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DigitalRestrictions.cs 
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
 *Description : Defines the entities for Digital Restrictions
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    /// <summary>
    /// DigitalRestrictions
    /// </summary>
    public class DigitalRestrictions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalRestrictions"/> class.
        /// </summary>
        public DigitalRestrictions()
        {
            CommercialModelTypes = new List<StringIdentifier>();
            ConsentPeriodTypes = new List<StringIdentifier>();
            ContentTypes = new List<StringIdentifier>();
            RestrictionTypes = new List<StringIdentifier>();
            UseTypes = new List<StringIdentifier>();
            RestrictionReasonTypes = new List<StringIdentifier>();
            DigitalRestrictionTemplates = new List<TemplateDetails>();
        }

        /// <summary>
        /// Gets or sets the content types.
        /// </summary>
        /// <value>The content types.</value>
        [DataMember]
        public List<StringIdentifier> ContentTypes { get; set; }

        /// <summary>
        /// Gets or sets the use types.
        /// </summary>
        /// <value>The use types.</value>
        [DataMember]
        public List<StringIdentifier> UseTypes { get; set; }

        /// <summary>
        /// Gets or sets the restriction types.
        /// </summary>
        /// <value>The restriction types.</value>
        [DataMember]
        public List<StringIdentifier> RestrictionTypes { get; set; }

        /// <summary>
        /// Gets or sets the commercial model types.
        /// </summary>
        /// <value>The commercial model types.</value>
        [DataMember]
        public List<StringIdentifier> CommercialModelTypes { get; set; }

        /// <summary>
        /// Gets or sets the consent period types.
        /// </summary>
        /// <value>The consent period types.</value>
        [DataMember]
        public List<StringIdentifier> ConsentPeriodTypes { get; set; }

        /// <summary>
        /// Gets or sets the digital restriction templates.
        /// </summary>
        /// <value>The digital restriction templates.</value>
        [DataMember]
        public List<TemplateDetails> DigitalRestrictionTemplates { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public List<StringIdentifier> RestrictionReasonTypes { get; set; }



    }
}