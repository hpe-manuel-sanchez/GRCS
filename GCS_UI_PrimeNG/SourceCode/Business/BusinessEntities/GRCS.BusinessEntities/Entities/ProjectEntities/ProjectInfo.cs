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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProjectInfo : RepertoireSearchResultsBase
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        [DataMember]
        public long ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the data admin company.
        /// </summary>
        /// <value>The data admin company.</value>
        [DataMember]
        public string DataAdminCompany { get; set; }


        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        [DataMember]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the label id.
        /// </summary>
        /// <value>The label id.</value>
        [DataMember]
        public long LabelId { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>The project code.</value>
        [DataMember]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contract required.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is contract required; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsContractRequired { get; set; }


        /// <summary>
        /// Gets or sets the package info.
        /// </summary>
        /// <value>The package info.</value>
        [DataMember]
        public List<PackageInfo> PackageInfo { get; set; }

        /// <summary>
        /// Gets or sets the track info.
        /// </summary>
        /// <value>The track info.</value>
        [DataMember]
        public List<TrackInfo> TrackInfo { get; set; }

        [DataMember]
        public long? artistId { get; set; }

        [DataMember]
        public long divisionId { get; set; }

        [DataMember]
        public string divisionDisplay { get; set; }

        [DataMember]
        public string ArtistNameInfo { get; set; }

        [DataMember]
        public string ArtistIdInfo { get; set; }
    }
}
