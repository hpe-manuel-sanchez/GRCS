/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GCSInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik P
 * Created on   : 10/08/2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
 ************************************************************************ 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for GCSInfo
                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    /// <summary>
    ///  Defines the entities for GCSInfo
    /// </summary>
    [DataContract]
    public class GCSInfo
    {
       
        /// <summary>
        /// Gets or sets a value indicating whether this instance is GCS linking.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is GCS linking; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsGCSLinking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is RCC admin.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is RCC admin; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsRCCAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auto link.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is auto link; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsAutoLink { get; set; }
    }
}
