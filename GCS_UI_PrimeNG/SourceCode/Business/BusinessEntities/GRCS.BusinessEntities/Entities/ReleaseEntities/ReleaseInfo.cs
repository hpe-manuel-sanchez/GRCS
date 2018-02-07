/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : ReleaseDetail.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by         Modified on     Remarks

***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************
 *Description :  Defines the entities for Release Info

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ProjectEntities;
using UMGI.GRCS.BusinessEntities.Entities.R2Entities;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    [DataContract]
    [Serializable]
    public class ReleaseInfo : RepertoireSearchResultsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseInfo"/> class.
        /// </summary>
        public ReleaseInfo()
        {
            ReleaseType = new ReleaseType();
            LinkedContractDetails = new List<LinkedContractDetails>();
            PackageInfo = new List<PackageInfo>();
            TrackInfo = new List<TrackInfo>();
            ReleaseResources = new List<R2ReleaseResources>();
        }

        /// <summary>
        /// Gets or sets the upc.
        /// </summary>
        /// <value>The upc.</value>
        [DataMember]
        public string PackageIndicator { get; set; }

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
        /// Gets or sets the configuration id.
        /// </summary>
        /// <value>The configuration id.</value>
        [DataMember]
        public string Configuration { get; set; }

        /// <summary>
        /// Gets or sets the configuration id.
        /// </summary>
        /// <value>The configuration id.</value>
        [DataMember]
        public string ConfigurationDisplay { get; set; }

        /// <summary>
        /// Gets or sets the catalogue no .
        /// </summary>
        /// <value>The catalogue no .</value>
        [DataMember]
        public string CatalogueNo { get; set; }

        /// <summary>
        /// Gets or sets the P year.
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public int PYear { get; set; }

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
        /// Gets or sets the P licensing extension.
        /// </summary>
        /// <value>The P licensing extension.</value>
        [DataMember]
        public string PLicensingExtension { get; set; }

        /// <summary>
        /// Gets or sets the data admin company.
        /// </summary>
        /// <value>The data admin company.</value>
        [DataMember]
        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Gets or sets the data admin company id.
        /// </summary>
        /// <value>The data admin company id.</value>
        [DataMember]
        public long DataAdminCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the r2 status.
        /// </summary>
        /// <value>The r2 status.</value>
        [DataMember]
        public string R2Status { get; set; }

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
        /// Gets or sets the release id.
        /// </summary>
        /// <value>The release id.</value>
        [DataMember]
        public long ReleaseId { get; set; }

        /// <summary>
        /// Gets or sets the owned project id.
        /// </summary>
        /// <value>The owned project id.</value>
        [DataMember]
        public long OwnedProjectId { get; set; }

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
        /// Gets or sets the grid.
        /// </summary>
        /// <value>The grid.</value>
        [DataMember]
        public string Grid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mac.
        /// </summary>
        /// <value><c>true</c> if this instance is mac; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool? IsMac { get; set; }

        /// <summary>
        /// Gets or sets the soundtrack indicator.
        /// </summary>
        /// <value>The soundtrack indicator.</value>
        [DataMember]
        public string SoundtrackIndicator { get; set; }

        /// <summary>
        /// Gets or sets the type of the music class.
        /// </summary>
        /// <value>The type of the music class.</value>
        [DataMember]
        public string MusicClassType { get; set; }

        /// <summary>
        /// Gets or sets the component count.
        /// </summary>
        /// <value>The component count.</value>
        [DataMember]
        public long ComponentCount { get; set; }

        /// <summary>
        /// Gets or sets the track count.
        /// </summary>
        /// <value>The track count.</value>
        [DataMember]
        public long? TrackCount { get; set; }

        /// <summary>
        /// Gets or sets the eariler release date.
        /// </summary>
        /// <value>The eariler release date.</value>
        [DataMember]
        public DateTime? EarilerReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the scope.
        /// </summary>
        /// <value>The type of the scope.</value>
        [DataMember]
        public string ScopeType { get; set; }

        /// <summary>
        /// Gets or sets the type of the r2 status.
        /// </summary>
        /// <value>The type of the r2 status.</value>
        [DataMember]
        public string R2StatusType { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        [DataMember]
        public string Sequence { get; set; }

        /// <summary>
        /// Gets or sets the type of the assigned.
        /// </summary>
        /// <value>The type of the assigned.</value>
        [DataMember]
        public string AssignedType { get; set; }

        /// <summary>
        /// Gets or sets the linked contract details.
        /// </summary>
        /// <value>The linked contract details.</value>
        [DataMember]
        public List<LinkedContractDetails> LinkedContractDetails { get; set; }

        /// <summary>
        /// Gets or sets the type of the release.
        /// </summary>
        /// <value>The type of the release.</value>
        [DataMember]
        public ReleaseType ReleaseType { get; set; }

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
        public List<R2ReleaseResources> ReleaseResources { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the mobile artist.
        /// </summary>
        /// <value>The mobile artist.</value>
        [DataMember]
        public string MobileArtist { get; set; }

        /// <summary>
        /// Gets or sets the link contract details.
        /// </summary>
        /// <value>The link contract details.</value>
        [DataMember]
        public string LinkContractDetails { get; set; }

        /// <summary>
        /// is GCS Release
        /// </summary>
        [DataMember]
        public bool? IsGcsRelease { get; set; }

        /// <summary>
        /// Is Upc type as manual
        /// </summary>
        [DataMember]
        public bool? IsUpcManual { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        [DataMember]
        public string Issuer { get; set; }

        [DataMember]
        public long R2ReleaseId { get; set; }
    }
}