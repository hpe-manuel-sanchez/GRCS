/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ProjectInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
 * KayalVizhi.v       11-10-2012      Added Entity to Resource File
 ************************************************************************ 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for ProjectInfo
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
      [DataContract]
   public class LinkedResourceInfo
    {
        /// <summary>
        /// Gets or sets the isrc.
        /// </summary>
        /// <value>The isrc.</value>
        [DataMember]
        public string Isrc { get; set; }

        /// <summary>
        /// Gets or sets the resource title.
        /// </summary>
        /// <value>The resource title.</value>
        [DataMember]
        public string ResourceTitle { get; set; }

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }
        /// <summary>
        /// Gets or sets the admin company id.
        /// </summary>
        /// <value>The admin company id.</value>
        [DataMember]
        public long AdminCompanyId { get; set; }


        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }


        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>The resource id.</value>
        [DataMember]
        public long ResourceId { get; set; }


        /// <summary> 
        /// Gets or sets the type of the resource. 
        /// </summary> 
        /// <value>The type of the resource.</value> 
        [DataMember]
        public string ResourceType { get; set; }

        [DataMember]
        public int TotalRows { get; set; }



    }
}
