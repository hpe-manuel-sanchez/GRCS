/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NoticeCompanySearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Territorial Display
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities
{
    /// <summary>
    /// NoticeCompanySearch which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class NoticeCompanySearch : EntityInformation
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        [DataMember]
        public string CompanyName { get; set; }


        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        /// <value>The country id.</value>
        [DataMember]
        public long CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        [DataMember]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        [DataMember]
        public FilterFields Criteria { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}