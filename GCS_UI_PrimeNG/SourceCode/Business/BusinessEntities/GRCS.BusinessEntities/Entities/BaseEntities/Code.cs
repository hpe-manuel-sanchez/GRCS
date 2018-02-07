/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Code.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Vijay Venkatesh Prasad . N
 * Created on   : 19-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description      Defines the Code Entities

****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{

    /// <summary>
    /// Code
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class Code <T>
    {
        /// <summary>
        /// Gets or sets the configuration code.
        /// </summary>
        /// 
        /// <value>The configuration code.</value>
        [DataMember]
        public T ConfigurationCode { get; set; }

        /// <summary>
        /// Gets or sets the configuration description.
        /// </summary>
        /// <value>The configuration description.</value>
        [DataMember]
        public T ConfigurationDescription { get; set; }
    }
}
