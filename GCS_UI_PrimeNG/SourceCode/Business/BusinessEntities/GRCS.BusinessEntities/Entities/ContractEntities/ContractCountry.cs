
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractCountry.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 11-07-2012
 * ************************************************************************ 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using UMGI.GRCS.Entities.BaseEntities;

namespace UMGI.GRCS.Entities.ContractEntities
{
    [DataContract]
    public class ContractCountry :   ClassInfo 
    {
        [DataMember]
        public long TerritoryId { get; set; }
        [DataMember]
        public long CountryId { get; set; }
        [DataMember]
        public string CountryName { get; set; }
       
    }
}
