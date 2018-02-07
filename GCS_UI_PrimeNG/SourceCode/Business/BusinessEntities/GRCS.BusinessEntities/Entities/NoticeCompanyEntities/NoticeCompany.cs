/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NoticeCompany.cs
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
 *Description :  Defines the entities for Notice Company
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.NoticeCompanyEntities
{
    /// <summary>
    /// NoticeCompany which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class NoticeCompany : EntityInformation
    {
        /// <summary>
        /// CompanyId
        /// </summary>
        [DataMember]
        public long CompanyId { get; set; }

        /// <summary>
        /// PCCompanyName
        /// </summary>
        [DataMember]
        public String CompanyName { get; set; }

        /// <summary>
        /// CountryId
        /// </summary>
        [DataMember]
        public String CountryId { get; set; }

        /// <summary>
        /// PCCountryName
        /// </summary>
        [DataMember]
        public String CountryName { get; set; }

        /// <summary>
        /// AdditionalNotes
        /// </summary>
        [DataMember]
        public String AdditionalNotes { get; set; }

       
    }
}