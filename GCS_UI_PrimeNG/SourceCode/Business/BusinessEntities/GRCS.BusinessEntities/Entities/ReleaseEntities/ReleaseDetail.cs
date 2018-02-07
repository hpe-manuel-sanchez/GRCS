/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseDetail.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by         Modified on     Remarks  
 * Sudarsan Nagarajan  27-08-2012      Add the Entities
 * Vijayakumar R       28-08-2012      Add Some missed Entities 
 * KayalVizhi.V         1-09-2012      Added Resource File              
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Release Details
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.CprsEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    /// <summary>
    /// ReleaseDetail which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ReleaseDetail : CprsReleaseDetail
    {


        /// <summary>
        /// Owning Project ID
        /// </summary>
        /// <value>The project id.</value>
        [DataMember]
        public long ProjectId { get; set; }


        /// <summary>
        /// Release Project Description
        /// </summary>
        /// <value>The project description.</value>
        [DataMember]
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Release Label
        /// </summary>
        /// <value>The label.</value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Release Division
        /// </summary>
        /// <value>The division.</value>
        [DataMember]
        public string Division { get; set; }

        /// <summary>
        /// Release Version Title
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Release Configuration ID
        /// </summary>
        /// <value>The configuration id.</value>
        [DataMember]
        public string ConfigurationId { get; set; }

        /// <summary>
        /// Release Catalogue Number
        /// </summary>
        /// <value>The catalogue number.</value>
        [DataMember]
        public string CatalogueNumber { get; set; }

        /// <summary>
        ///  PCompany id
        /// </summary>
        [DataMember]
        public long PCompanyId { get; set; }

        /// <summary>
        /// PCompany id
        /// </summary>
        /// <value>The P company.</value>
        [DataMember]
        public string PCompany { get; set; }

        /// <summary>
        /// Release PYear
        /// </summary>
        [DataMember]
        public int PYear { get; set; }

        /// <summary>
        /// Release PLicensingExtension
        /// </summary>
        /// <value>The P licensing extension.</value>
        [DataMember]
        public string PLicensingExtension { get; set; }


        /// <summary>
        /// Artists Information
        /// </summary>
        /// <value>The info.</value>
        [DataMember]
        public ArtistSearch Info { get; set; }

        /// <summary>
        /// Release Admin Company ID
        /// </summary>
        /// <value>The admin company id.</value>
        [DataMember]
        public long AdminCompanyId { get; set; }

        /// <summary>
        /// Components
        /// </summary>
        /// <value>The components.</value>
        [DataMember]
        public string Components { get; set; }

        /// <summary>
        /// Components
        /// </summary>
        /// <value>The tracks.</value>
        [DataMember]
        public string Tracks { get; set; }


        ///// <summary>
        ///// UserID
        ///// </summary>
        ///// <value>The user id.</value>
        //[DataMember]
        //public long UserId { get; set; }

        /// <summary>
        /// R2 Status
        /// </summary>
        /// <value>The status.</value>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// CoreRowFlag
        /// </summary>
        /// <value><c>true</c> if [core row flag]; otherwise, <c>false</c>.</value>
        [DataMember]
        public Boolean CoreRowFlag { get; set; }

    }
}