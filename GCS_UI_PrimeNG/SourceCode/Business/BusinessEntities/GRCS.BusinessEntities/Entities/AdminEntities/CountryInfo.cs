/* ************************************************************************ 
* Copyrights ® 2012 UMGI 
* ************************************************************************ 
* File Name    : CountryInfo.cs 
* Project Code : UMG-GRCS(C/115921) 
* Author       : Rengaraj G
* Created on   : 29-10-2012
* ************************************************************************ 
* Modification History 
* ************************************************************************ 
* Modified by       Modified on     Remarks 

*************************************************************************** 
* Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for CountryInfo
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
   
    /// <summary>
    /// Contry Info
    /// </summary>
    [DataContract]
    [Serializable]
    public class CountryInfo : BaseSearch
    {
        /// <summary>
        /// CountryID List from ANA
        /// </summary>
        [DataMember]
        public List<long> Ids { get; set; }

        /// <summary>
        /// Country Details
        /// </summary>
        [DataMember]
        public string PatternName { get; set; }
    }
}
