/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CountryInfo.cs 
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
* Description :  Defines the entities for CountryInfol Class
                  
****************************************************************************/


using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// Contry Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class CountryInfo : BaseSearch
    {
        /// <summary>
        /// CountryID
        /// </summary>
        [DataMember]
        public long CountryId { get; set; }

        /// <summary>
        /// Country Details
        /// </summary>
        [DataMember]
        public string CountryName { get; set; }
    }
}