/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : StringIdentifier.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the String Identifier entities
 
****************************************************************************/

using System.Runtime.Serialization;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// String builder - representation for DB Lookups
    /// </summary>
    [DataContract]
    [Serializable]
    public class StringIdentifier
    {
        public StringIdentifier()
        {
        }

        public StringIdentifier(long id, string value)
        {
            Id = id;
            Description = value;
        }

        //private string _descriptionTrim;
        /// <summary>
        /// Gets or sets the Lookup Id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }


        /// <summary>
        /// Gets or sets the look up Type id.
        /// </summary>
        /// <value>The look up type id.</value>
       
        public long LookUpTypeId { get; set; }


        /// <summary>;
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>;
        /// Gets or sets the description for UI purpose-Syncfusion.
        /// </summary>
        /// <value>The description.</value>
        public string DescriptionTrim { get; set; }

        /// <summary>
        /// Code to Identify the Look up Type. Eg: WRKFW for Workflow.
        /// LOOKUP_TYPE.TYPE
        /// </summary>
        /// <value>The type of the identifier.</value>
       
        public string IdentifierType { get; set; }

        /// <summary>
        /// Gets or sets the value. Ex. 1,2,3
        /// </summary>
        /// <value>The value.</value>
        [DataMember]
        public byte Value { get; set; }
    }
}