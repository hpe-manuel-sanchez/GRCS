/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceParameters.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Parameters details
                  
****************************************************************************/
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Parameters Details
    /// </summary>
    [DataContract]
    public class ResourceFilterParameters : RepertoireParameter
    {
        public ResourceFilterParameters()
        {
            RepertoireFilter = new RepertoireFilter();
        }
        /// <summary>
        /// Gets or sets the ISRC.
        /// </summary>
        /// <value>The ISRC.</value>
        [DataMember]
        public string ISRC
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the resource title.
        /// </summary>
        /// <value>The resource title.</value>
        [DataMember]
        public string ResourceTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is resource.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is resource; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsResource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Track.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Track; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsTrack
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public int ReviewStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sample exists.
        /// </summary>
        /// <value>The sample exists.</value>
        [DataMember]
        public bool SampleExists
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the side artist.
        /// </summary>
        /// <value>The side artist.</value>
        [DataMember]
        public bool SideArtist
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the RepertoireFilter.
        /// </summary>
        /// <value>The RepertoireFilter.</value>
        [DataMember]
        public RepertoireFilter RepertoireFilter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Referer.
        /// </summary>
        /// <value>The Referer like type Resource/Release</value>
        [DataMember]
        public string Referer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Image.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Image; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsImage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Video.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Video; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsVideo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Audio.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is Audio; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IsAudio
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is custom work queue.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is custom work queue; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool? IsCustomWorkQueue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is Right Set id List.
        /// </summary>        
        [DataMember]
        public List<long> RightSetList
        {
            get;
            set;
        }

    }
}
