/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : SearchResourceReleaseCriteria.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : Rikhu Prasad
 * Created on   : 17/12/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for SearchResourceReleaseCriteria
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    /// <summary>
    /// Resource Release Search
    /// </summary>
    [DataContract]
    [Serializable]
    public class SearchResourceReleaseCriteria : EntityInformation
    {
        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is exact search.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is exact search; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsExactSearch { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiple contracts.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is multiple contracts; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsMultipleContracts { get; set; }

        /// <summary>
        /// Gets or sets the ISRC.
        /// </summary>
        /// <value>The ISRC.</value>
        [DataMember]
        public string Isrc { get; set; }

        /// <summary>
        /// Gets or sets the name of the linked contract.
        /// </summary>
        /// <value>The name of the linked contract.</value>
        [DataMember]
        public string LinkedContractName { get; set; }

        /// <summary>
        /// Gets or sets the release title.
        /// </summary>
        /// <value>The release title.</value>
        [DataMember]
        public string ReleaseTitle { get; set; }

        /// <summary>
        /// Gets or sets the resource title.
        /// </summary>
        /// <value>The resource title.</value>
        [DataMember]
        public string ResourceTitle { get; set; }

        /// <summary>
        /// Gets or sets the resource track title.
        /// </summary>
        /// <value>The resource track title.</value>
        [DataMember]
        public string ResourceTrackTitle { get; set; }

        /// <summary>
        /// Gets or sets the resource type id.
        /// </summary>
        /// <value>The resource type id.</value>
        [DataMember]
        public byte? ResourceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The UPC.</value>
        [DataMember]
        public string Upc { get; set; }

        #region "For UI Purpose"
        [DataMember]
        public string LinkedContractId { get; set; }
        #endregion 

    }
}
