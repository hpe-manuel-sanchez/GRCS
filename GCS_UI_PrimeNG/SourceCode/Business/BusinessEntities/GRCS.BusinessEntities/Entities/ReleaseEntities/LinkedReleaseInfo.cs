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

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    [DataContract]
   public class LinkedReleaseInfo
    {

        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string Upc { get; set; }

        /// <summary>
        /// Gets or sets the release title.
        /// </summary>
        /// <value>The release title.</value>
        [DataMember]
        public string ReleaseTitle { get; set; }


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
        /// Gets or sets the type of the release.
        /// </summary>
        /// <value>The type of the release.</value>
        [DataMember]
        public long? ReleaseType { get; set; }

        /// <summary>
        /// Gets or sets the component count.
        /// </summary>
        /// <value>The component count.</value>
        [DataMember]
        public long? ComponentCount { get; set; }
        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the admin company id.
        /// </summary>
        /// <value>The admin company id.</value>
        [DataMember]
        public long ReleaseId { get; set; }

        [DataMember]
        public int TotalRows { get; set; }



    }
}
