/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Request.cs 
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
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities
{
    [DataContract]
    public class Request : PagingBase
    {
        [DataMember]
        public long ID{ get; set; }

        [DataMember]
        public long ProjectID{ get; set; }

        [DataMember]
        public string ResourceTitle{ get; set; }

        [DataMember]
        public string ArtistName{ get; set; }

        [DataMember]
        public string ISRC{ get; set; }

        [DataMember]
        public string ClearanceOwner{ get; set; }

        [DataMember]
        public long WorkgroupID{ get; set; }

        [DataMember]
        public string WorkgroupName { get; set; }

        [DataMember]
        public string ProjectReferenceNumber { get; set; }
      
    }
}
