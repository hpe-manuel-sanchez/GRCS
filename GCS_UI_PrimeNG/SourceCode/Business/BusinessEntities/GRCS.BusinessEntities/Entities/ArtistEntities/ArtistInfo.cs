/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ArtistInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 * Rengaraj          03-Aug-2012     modified code for coding standard format                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Artist information.                                      
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.ArtistEntities
{
    [DataContract]
    [Serializable]
    public class ArtistInfo : EntityInformation
    {
        /// <summary>
        /// Artist info - TalentId
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Artist info - TalentId
        /// </summary>
        /// <value>The name id.</value>
        [DataMember]
        public long NameId { get; set; }

        /// <summary>
        /// Artist info - Artist Name
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the is primary.
        /// </summary>
        /// <value>The is primary.</value>
        [DataMember]
        public string IsPrimary { get; set; }


        /// <summary>
        /// Gets or sets the r2 account id.
        /// </summary>
        /// <value>The r2 account id.</value>
        [DataMember]
        public long R2AccountId { get; set; }

        /// <summary>
        /// Gets or sets the contribution id.
        /// </summary>
        /// <value>The contribution id.</value>
        [DataMember]
        public long ContributionId { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance has composer role.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has composer role; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasComposerRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has artist role.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has artist role; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasArtistRole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has artist role.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has artist role; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool HasWriterRole { get; set; }


        /// <summary>
        /// Gets or sets the sequence no.
        /// </summary>
        /// <value>The sequence no.</value>
        [DataMember]
        public int SequenceNo { get; set; }

        [DataMember]
        public int RoleNo { get; set; }    
    }
}