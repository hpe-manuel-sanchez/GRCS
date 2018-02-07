/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearance.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rengaraj
 * Created on   : 11-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rikhu Prasad      27 Dec 2012     Inherited Entity Information
 * Rikhu Prasad      27 Dec 2012     Added TypeName
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the PreClearance properties
 
****************************************************************************/
using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    [Serializable]
    public class PreClearance : EntityInformation
    {
        /// <summary>
        /// Gets or sets the is preclearance.
        /// </summary>
        /// <value>The is preclearance.</value>
        [DataMember]
        public byte? IsPreclearance { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        [DataMember]
        public string TypeName { get; set; }
    }
}
