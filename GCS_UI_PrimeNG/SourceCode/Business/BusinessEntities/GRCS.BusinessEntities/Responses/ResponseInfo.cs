/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResponseInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Siva
 * Created on   : 13-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the ResponseInfo properties such as long 
 *                Id and string Name. Used primarily to populate Companies, 
 *                Divisions, Labels
 
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Responses
{
    [DataContract]
    public class ResponseInfo : EntityInformation
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the CountryId
        /// </summary>
        [DataMember]
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryName
        /// </summary>
        [DataMember]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        /// <value>The country iso code.</value>
        [DataMember]
        public string CountryIsoCode { get; set; }
    }
}
