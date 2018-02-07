/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearanceCountry.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rengaraj
 * Created on   : 11-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the PreClearanceCountry properties
 
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    public class PreClearanceCountry : EntityInformation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the IncludeType.
        /// </summary>
        /// <value>The IncludeType.</value>
        [DataMember]
        public string IncludeType { get; set; }

       
    }
}
