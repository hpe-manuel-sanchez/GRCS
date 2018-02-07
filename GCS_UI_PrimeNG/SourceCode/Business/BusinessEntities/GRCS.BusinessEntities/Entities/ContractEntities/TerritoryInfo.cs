/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TerritoryInfo.cs 
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
 *Description :  Defines the entities for Territory Info
                  
****************************************************************************/
using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ContractEntities
{
    /// <summary>
    /// TerritoryInfo which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    [Serializable]
    public class TerritoryInfo : EntityInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryInfo"/> class.
        /// </summary>
        public TerritoryInfo()
        {
            CountryDetails = new CountryInfo();
        }

        /// <summary>
        /// TerritoryId
        /// </summary>
        [DataMember]
        public int TerritoryId { get; set; }

        /// <summary>
        /// TerritoryName
        /// </summary>
        [DataMember]
        public string TerritoryName { get; set; }

        /// <summary>
        /// CountryDetails
        /// </summary>
        [DataMember]
        public CountryInfo CountryDetails { get; set; }

        //ParentTerriroryId
    }
}