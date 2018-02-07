/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : ResourceSearchCriteria.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
 * KayalVizhi.V      11-10-2012      Added Entities to Resource File
***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************
 *Description :  Defines the entities for ResourceSearchCriteria Details

****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    public class ResourceSearchCriteria : RepertoireSearchCriteriaBase
    {
        /// <summary>
        /// Gets or sets the release upc.
        /// </summary>
        /// <value>The release upc.</value>
        [DataMember]
        public string ReleaseUpc { get; set; }

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
        /// Gets or sets a value indicating whether this instance is artist exact search.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is artist exact search; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsArtistExactSearch { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the IncludeMobile option.
        /// </summary>
        [DataMember]
        public bool IsIncludeMobileLock { get; set; }


        /// <summary>
        /// Gets or sets the type of the mobile artist search.
        /// </summary>
        /// <value>The type of the mobile artist search.</value>
        [DataMember]
        public MobileArtistSearchType MobileArtistSearchType{ get; set; }

        /// <summary>
        /// Gets or sets the clearance admin company name.
        /// </summary>
        /// <value>The clearance admin company name.</value>
        [DataMember]
        public string ClearanceAdminCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the ResourceTypeId.
        /// </summary>
        /// <value>The ResourceTypeId.</value>
        [DataMember]
        public int ResourceTypeId { get; set; }

        /// <summary> 
        /// Gets or sets the Upc. 
        /// </summary> 
        /// <value>Upc</value> 
        [DataMember]
        public string Upc { get; set; }

        /// <summary> 
        /// Gets or sets the R2 Project ID. 
        /// </summary> 
        /// <value>R2ProjectID</value> 
        [DataMember]
        public string R2ProjectID { get; set; }
    }
}