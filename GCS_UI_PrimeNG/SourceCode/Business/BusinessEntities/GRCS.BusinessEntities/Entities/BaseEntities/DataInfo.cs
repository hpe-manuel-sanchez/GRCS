/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DataInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 04-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the DataInfo properties that depict id and name of an entity.                
 
****************************************************************************/

using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    [DataContract]
    [Serializable]
    public class DataInfo:EntityInformation
    {
        /// <summary>
        /// Gets or sets the id. For instance, for CompanyInfo, pass company id for this property.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.  For instance, for CompanyInfo, pass company name for this property.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }
    }
}
