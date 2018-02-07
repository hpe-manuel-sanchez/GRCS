/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireBase.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       :Mohammad
 * Created on   : 10/3/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Siva              23/10/2012      Changed datatypes of ArtistId and CompanyId
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for RepertoireBase class
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// RepertoireBase 
    /// </summary>
    [DataContract]
    public class RepertoireBase : EntityInformation
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// ArtistId
        /// </summary>
        [DataMember]
        public long? ArtistId { get; set; }

        /// <summary>
        /// ArtistName
        /// </summary>
        [DataMember]
        public string ArtistName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// CompanyId
        /// </summary>
        [DataMember]
        public long? CompanyId { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }
    }
}
