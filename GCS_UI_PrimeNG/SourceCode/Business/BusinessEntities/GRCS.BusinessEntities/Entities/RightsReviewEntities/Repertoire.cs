/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Repertoire.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Repertoire Details
                  
****************************************************************************/

using System;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Repertoire Details
    /// </summary>
    [DataContract]
    public class Repertoire : EntityInformation
    {
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        [DataMember]
        public string Artist
        {
            get;
            set;
        }

   

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [DataMember]
        public string Title
        {
            get;
            set;
        }       

        /// <summary>
        /// Gets or sets the version title.
        /// </summary>
        /// <value>The version title.</value>
        [DataMember]
        public string VersionTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        [DataMember]
        public DateTime? ReleaseDate
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the lost rights date.
        /// </summary>
        /// <value>The lost rights date.</value>
        [DataMember]
        public DateTime? LostRightsDate 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the linked contract.
        /// </summary>
        /// <value>The linked contract.</value>
        [DataMember]
        public LinkType LinkedContract
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the linked tooltip.
        /// </summary>
        /// <value>The linked tooltip.</value>
        [DataMember]
        public string LinkedTooltip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public string AdminCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public long AdminCompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public long? ReleaseId
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the admin company.
        /// </summary>
        /// <value>The admin company.</value>
        [DataMember]
        public long? R2ReleaseId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string TotalRows
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value permissions
        /// </summary>
        /// <value><c>true</c> if authenticated;
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public char ReviewStatusPermitted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value permissions
        /// </summary>
        /// <value><c>true</c> if authenticated;
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public char SensitiveInfoPermitted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value permissions
        /// </summary>
        /// <value><c>true</c> if authenticated;
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public char RightsEditPermitted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repertoire id.
        /// </summary>
        /// <value>The repertoire id.</value>
        [DataMember]
        public long RepertoireId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        /// <value>The country id.</value>
        [DataMember]
        public long? CountryId
        {
            get;
            set;
        }
    }
}
