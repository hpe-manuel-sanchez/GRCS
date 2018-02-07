/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : Resource.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 07-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Details
                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Details
    /// </summary>
    [DataContract]
    public class Resource : Repertoire
    {
        /// <summary>
        /// Gets or sets the ISRC id.
        /// </summary>
        /// <value>The ISRC id.</value>
        [DataMember]
        public string ISRCId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UPC id.
        /// </summary>
        /// <value>The UPC id.</value>
        [DataMember]
        public string UPCId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the rights.
        /// </summary>
        /// <value>The type of the rights.</value>
        [DataMember]
        public RightTypes RightsType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review reason.
        /// </summary>
        /// <value>The review reason.</value>
        [DataMember]
        public int ReviewReason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review reason.
        /// </summary>
        /// <value>The review reason.</value>
        [DataMember]
        public List<int> ReviewReasons
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets a value indicating 
        /// whether [sample exists].
        /// </summary>
        /// <value><c>true</c> 
        /// if [sample exists]; otherwise,
        /// <c>false</c>.</value>
        [DataMember]
        public bool SampleExists
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the P year.
        /// </summary>
        /// <value>The P year.</value>
        [DataMember]
        public string PYear
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating
        /// whether [side artist].
        /// </summary>
        /// <value><c>true</c> if [side artist];
        /// otherwise, <c>false</c>.</value>
        [DataMember]
        public bool SideArtist
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the resource.
        /// </summary>
        /// <value>The type of the resource.</value>
        [DataMember]
        public ResourcesType ResourceType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the excluded countries.
        /// </summary>
        /// <value>The excluded countries.</value>
        [DataMember]
        public string ExcludedCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the included countries.
        /// </summary>
        /// <value>The included countries.</value>
        [DataMember]
        public string IncludedCountries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the r2 resource id.
        /// </summary>
        /// <value>The r2 resource id.</value>
        [DataMember]
        public long? R2ResourceId
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the sample description.
        /// </summary>
        /// <value>The sample description.</value>
        [DataMember]
        public string SampleDescription
        {
            get;
            set;
        }

    }
}
