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
* Description :  Defines the entities for Company
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.AdminEntities
{
    
    /// <summary>
    /// CompanyInfo which is inherited from BaseIdentifier Class
    /// </summary>
    [DataContract]
    [Serializable]
    public class CompanyInfo : BaseIdentifier
    {
        /// <summary>
        /// Company Ids from ANA
        /// </summary>
        [DataMember]
        public List<long> Ids { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        [DataMember]
        public string PatternName { get; set; }
    }
}
