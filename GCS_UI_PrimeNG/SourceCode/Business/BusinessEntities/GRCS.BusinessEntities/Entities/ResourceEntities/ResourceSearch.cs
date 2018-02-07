/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
 * KayalVizhi.V      1-09-2012       Added Resource File                               
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Resource Search
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    /// <summary>
    ///  ResourceSearch which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ResourceSearch : EntityInformation
    {
        /// <summary>
        /// Resource project id.
        /// </summary>
        /// <value>The project id.</value>
        [DataMember]
        public long ProjectId { get; set; }

        /// <summary>
        /// Resource ISRC Value.
        /// </summary>
        /// <value>The isrc.</value>
        [DataMember]
        public string Isrc { get; set; }

        /// <summary>
        /// Artists Information
        /// </summary>
        /// <value>The info.</value>
        [DataMember]
        public ArtistInfo Info { get; set; }

        /// <summary>
        /// Resource Type
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public string ResourceType { get; set; }


        /// <summary>
        /// Resource version title
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Resource title
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Resource Genre
        /// </summary>
        /// <value>The resource genre.</value>
        [DataMember]
        public string ResourceGenre { get; set; }

        /// <summary>
        /// Page Number
        /// </summary>
        /// <value>The page number.</value>
        [DataMember]
        public int PageNumber { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        /// <value>The size of the page.</value>
        [DataMember]
        public int PageSize { get; set; }
    }
}