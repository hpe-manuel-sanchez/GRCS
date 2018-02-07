/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ArtistSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * Rengaraj          03-Aug-2012     modified code for coding standard format                                    
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the entities for Artist Search.                                      
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    public class ArtistSearch : EntityInformation
    {
        /// <summary>
        /// Artists First Name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Artists Last Name / Group Name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// Artists Information
        /// </summary>
        [DataMember]
        public ArtistInfo Info { get; set; }

        /// <summary>
        /// check search name used in all name fields
        /// </summary>
        [DataMember]
        public bool IsSearchNameInAllNameFields { get; set; }

        /// <summary>
        /// Page Number
        /// </summary>
        [DataMember]
        public int PageNumber { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
    }
}