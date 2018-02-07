/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceDetail.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
      KayalVizhi.V           3-09-2012         Added Resource File         
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Release Details
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
    /// <summary>
    /// ResourceDetail which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ResourceDetail : EntityInformation
    {
        /// <summary>
        /// projectID
        /// </summary>
        /// <value>The project id.</value>
        [DataMember]
        public string ProjectId { get; set; }

        /// <summary>
        /// ResourceID.
        /// </summary>
        /// <value>The resource id.</value>
        [DataMember]
        public string ResourceId { get; set; }

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
        public ArtistSearch Info { get; set; }

        /// <summary>
        /// Rights Type
        /// </summary>
        [DataMember]
        public string RightsType { get; set; }

        /// <summary>
        /// Resource Type
        /// </summary>
        /// <value>The type of the asset.</value>
        [DataMember]
        public string AssetType { get; set; }


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
        /// Genre
        /// </summary>
        /// <value>The genre.</value>
        [DataMember]
        public string Genre { get; set; }

        /// <summary>
        /// PCompany id
        /// </summary>
        /// <value>The P company id.</value>
        [DataMember]
        public string PCompanyId { get; set; }

        /// <summary>
        /// PCompany id
        /// </summary>
        /// <value>The P company.</value>
        [DataMember]
        public String PCompany { get; set; }

        /// <summary>
        /// Pyear
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public string PYear { get; set; }

        /// <summary>
        /// Pyear
        /// </summary>
        /// <value>The P notice extension.</value>
        [DataMember]
        public string PNoticeExtension { get; set; }

        /// <summary>
        /// label
        /// </summary>
        /// <value>The label.</value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Division
        /// </summary>
        /// <value>The division.</value>
        [DataMember]
        public string Division { get; set; }

        /// <summary>
        /// SampleExists
        /// </summary>
        /// <value>The sample exists.</value>
        [DataMember]
        public string SampleExists { get; set; }

        /// <summary>
        /// SampleCredit
        /// </summary>
        /// <value>The sample credit.</value>
        [DataMember]
        public string SampleCredit { get; set; }


        /// <summary>
        ///  Company id
        /// </summary>
        [DataMember]
        public long CompanyId { get; set; }

        /// <summary>
        ///  IsCoreRow
        /// </summary>
        [DataMember]
        public string IsCoreRow { get; set; }

        // added by Dhruv for saving resource-- to be updated after we get this enitity from rights

        /// <summary>
        ///  AccountId
        /// </summary>
        [DataMember]
        //[Display(Name = "AccountId", ResourceType = typeof(Entity))]
        public long AccountId { get; set; }
        
        /// <summary>
        /// R2_ResourceId
        /// </summary>
        [DataMember]
        public long R2_ResourceId { get; set; }

        /// <summary>
        /// Resource RecordingType Entities
        /// </summary>
        [DataMember]
        public int RecordingType { get; set; }
        /// <summary>
        /// Resource ResourceType Entities
        /// </summary>
        [DataMember]
        public int ResourceType { get; set; }

        /// <summary>
        /// Resource MusicType Entities
        /// </summary>
        [DataMember]
        public int MusicType { get; set; }
    }
}