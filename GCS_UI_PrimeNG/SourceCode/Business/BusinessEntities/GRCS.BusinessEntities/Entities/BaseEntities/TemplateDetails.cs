/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TemplateDetails.cs 
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
* Description :  Defines the entities for Template Details
 
****************************************************************************/

using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Template Details
    /// </summary>
    [DataContract]
    [Serializable]
    public class TemplateDetails:EntityInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateDetails"/> class.
        /// </summary>
        public TemplateDetails()
        {
            OwnerCompany = new StringIdentifier(1, string.Empty);
        }

        /// <summary>
        /// property holding the template name
        /// </summary>
        /// <value>The name of the template.</value>
        [DataMember]
        public string TemplateName { get; set; }

        /// <summary>
        /// property holding the template id
        /// </summary>
        /// <value>The template id.</value>
        [DataMember]
        public long TemplateId { get; set; }

        /// <summary>
        /// Id of the clearance admin company
        /// of the saved template
        /// </summary>
        /// <value>The clearance admin.</value>
        [DataMember]
        public string ClearanceAdmin { get; set; }

        /// <summary>
        /// Gets or sets the owner company.
        /// </summary>
        /// <value>The owner company.</value>
        [DataMember]
        public StringIdentifier OwnerCompany { get; set; }

        /// <summary>
        /// Gets or sets the type of the template.
        /// </summary>
        /// <value>The type of the template.</value>
        [DataMember]
        public string TemplateType { get; set; }

        /// <summary>
        /// Last Modified Time will be helpful while updating a record to check concurrency issue
        /// </summary>
        [DataMember]
        public DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Gets or sets the last modified date. 
        /// This property is used to hold the serialized LastModifiedTime for Update scenario. This one need not be datemember property as it's not defined to travel across WCF services.
        /// </summary>
        /// <value>The last modified date.</value>
        public string LastModifiedDate { get; set; }

        [DataMember]
        public long ClearenceCompanyId { get;set; }

    }
}