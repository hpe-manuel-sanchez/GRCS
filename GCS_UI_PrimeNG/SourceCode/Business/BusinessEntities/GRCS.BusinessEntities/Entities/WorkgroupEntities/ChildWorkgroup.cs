/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ChildWorkgroup.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : 
 * Created on   : 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class ChildWorkgroup : ParentWorkgroup
    {
        [DataMember]
        public List<DeviationResourceContract> ResourceContracts { get; set; }

        [DataMember]
        public List<KeyValuePair<long, long>> RequestTypes { get; set; }

        [DataMember]
        public List<KeyValuePair<long, string>> RequestTypesLookup { get; set; }

        [DataMember]
        public List<DeviationArtistContract> ArtistContracts { get; set; }

        [DataMember]
        public List<DeviationRequestType> DeviationRequestTypes { get; set; }

        [DataMember]
        public string UserLoginName { get; set; }
    }
}
