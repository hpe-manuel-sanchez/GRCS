/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for ResourceInfo Details
                  
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    [DataContract]
    [Serializable]
    public class ResourceInfo : RepertoireSearchResultsBase
    {

        public ResourceInfo()
        {
            LinkedContractDetails = new List<LinkedContractDetails>();
        }

        [DataMember]
        public long ReleaseId { get; set; }
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
        /// Gets or sets the P company.
        /// </summary>
        /// <value>The P company.</value>
        [DataMember]
        public long PCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the P company.
        /// </summary>
        /// <value>The P company.</value>
        [DataMember]
        public string PCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the P year.
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public int PYear { get; set; }

        /// <summary>
        /// Gets or sets the P licensing extension.
        /// </summary>
        /// <value>The P licensing extension.</value>
        [DataMember]
        public string PLicensingExtension { get; set; }

        /// <summary>
        /// Gets or sets the type of the rights.
        /// </summary>
        /// <value>The type of the rights.</value>
        [DataMember]
        public string RightsType { get; set; }

        /// <summary>
        /// Gets or sets the rights type code.
        /// </summary>
        /// <value>The rights type code.</value>
        [DataMember]
        public string RightsTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the linked contract details.
        /// </summary>
        /// <value>The linked contract details.</value>
        [DataMember]
        public List<LinkedContractDetails> LinkedContractDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is already linked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is already linked; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsAlreadyLinked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is media portal.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is media portal; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsMediaPortal { get; set; }


        /// <summary>
        /// Gets or sets the label id.
        /// </summary>
        /// <value>The label id.</value>
        [DataMember]
        public long LabelId { get; set; }


        /// <summary>
        /// Gets or sets the division id.
        /// </summary>
        /// <value>The division id.</value>
        [DataMember]
        public long DivisionId { get; set; }

        /// <summary>
        /// Gets or sets the data admin company id.
        /// </summary>
        /// <value>The data admin company id.</value>
        [DataMember]
        public long DataAdminCompanyId { get; set; }


        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>The resource id.</value>
        [DataMember]
        public long ResourceId { get; set; }


        /// <summary>
        /// Gets or sets the owned project id.
        /// </summary>
        /// <value>The owned project id.</value>
        [DataMember]
        public long OwnedProjectId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has sample.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has sample; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasSample { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has side artist.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has side artist; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasSideArtist { get; set; }

        /// <summary>
        /// Gets or sets the genre id.
        /// </summary>
        /// <value>The genre id.</value>
        [DataMember]
        public string GenreId { get; set; }

        /// <summary>
        /// Gets or sets the sample credit.
        /// </summary>
        /// <value>The sample credit.</value>
        [DataMember]
        public string SampleCredit { get; set; }

         /// <summary>
        /// Gets or sets the R2ResourceCompanyLinkId.
        /// </summary>
        /// <value>The R2 Resource Company Link Id.</value>
        [DataMember]
        public long R2ResourceCompanyLinkId { get; set; }
        
                
        /// <summary>
        /// Gets or sets the source upc.
        /// </summary>
        /// <value>The source upc.</value>
        [DataMember]
        public string SourceUpc { get; set; }

        /// <summary>
        /// Gets or sets the type of the live studio.
        /// </summary>
        /// <value>The type of the live studio.</value>
        [DataMember]
        public string LiveStudioType { get; set; }

        /// <summary>
        /// Gets or sets the type of the audio video.
        /// </summary>
        /// <value>The type of the audio video.</value>
        [DataMember]
        public string AudioVideoType { get; set; }

        /// <summary>
        /// Gets or sets the type of the music class.
        /// </summary>
        /// <value>The type of the music class.</value>
        [DataMember]
        public string MusicClassType { get; set; }



        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public bool IsMobileResource { get; set; }

        /// <summary>
        /// Gets or sets the music time.
        /// </summary>
        /// <value>The music time.</value>
        [DataMember]
        public string MusicTime { get; set; }

        /// <summary>
        /// Gets or sets the eariler release date.
        /// </summary>
        /// <value>The eariler release date.</value>
        [DataMember]
        public DateTime? EarilerReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the reviewstatus.
        /// </summary>
        /// <value>The reviewstatus.</value>
        [DataMember]
        public byte? Reviewstatus { get; set; }

        /// <summary>
        /// Gets or sets the link contract details.
        /// </summary>
        /// <value>The link contract details.</value>
        [DataMember]
        public string LinkContractDetails { get; set; }

        /// <summary>
        /// R2 Resource Id 
        /// </summary>
        [DataMember]
        public long R2_ResourceId { get; set; }

        // added by Dhruv  for resolving customer defect to show Clearance Admin Comapany rather than ID
        //Dated in 12/06/2012
        [DataMember]
        public string DataAdminCompanyName { get; set; }

        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public DateTime? StreetDate { get; set; }
    }
}
