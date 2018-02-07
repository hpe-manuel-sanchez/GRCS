/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Identifier.cs 
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
 * Description      Defines the Identifier Entities

****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Identifier
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    [DataContract]
    public class Identifier<T1,T2,T3> : EntityInformation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public T1 Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public T2 Name { get; set; }

        /// <summary>
        /// Gets or sets the code
        /// </summary>
        /// <value>The Code</value>
        [DataMember]
        public T3 Code { get; set; }
        
    }
}
