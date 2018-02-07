/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : MappingFilterCriteria.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 20-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the MappingFilterCriteria CDL-Contract Entities
 
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    [DataContract]
    [Serializable]
    public class MappingFilterCriteria : EntityInformation
    {
        public MappingFilterCriteria()
        {
            MappingInfo = new MappingInfo();
            FilterFields = new FilterFields();
        }

        /// <summary>
        /// Gets or sets the mapping info.
        /// </summary>
        /// <value>The mapping info.</value>
        [DataMember]
        public MappingInfo MappingInfo { get; set; }

        /// <summary>
        /// Gets or sets the filter fields.
        /// </summary>
        /// <value>The filter fields.</value>
        [DataMember]
        public FilterFields FilterFields { get; set; }
    }
}
