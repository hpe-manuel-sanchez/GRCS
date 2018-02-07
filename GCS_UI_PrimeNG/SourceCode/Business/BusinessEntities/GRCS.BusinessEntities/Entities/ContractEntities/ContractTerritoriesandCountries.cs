
/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractTerritoriesandCountries.cs 
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
using UMGI.GRCS.Entities.ContractEntities;


namespace UMGI.GRCS.Entities.ContractEntities
{
    [DataContract]
    public class ContractTerritoriesandCountries : ClassInfo 
    {       
        [DataMember]
        public List<TerritorialDisplay> ContractTerritories { get; set; }
        [DataMember]
        public ObservableCollection<ContractCountry> ContractIncludeCountries { get; set; }
        [DataMember]
        public ObservableCollection<ContractCountry> ContractExcludeCountries { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
