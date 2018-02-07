/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireFilter.cs 
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
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

/// <summary>
/// Contains entities for Resource Parameters Details
/// </summary>
[DataContract]
    public class RepertoireFilter : EntityInformation
    {
        /// <summary>
        /// Gets or sets the UPC.
        /// </summary>
        /// <value>The UPC.</value>
        [DataMember]
        public string UPC
        {
            get;
            set;
        }

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
        /// Gets or sets the release title.
        /// </summary>
        /// <value>The release title.</value>
        [DataMember]
        public string ReleaseTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the linked contract.
        /// </summary>
        /// <value>The linked contract.</value>
        [DataMember]
        public string LinkedContract
        {
            get;
            set;
        }       

        [DataMember]
        public string RightSetIds { get; set; }

        [DataMember]
        public bool IsRightsReviewWQ { get; set; }

        [DataMember]
        public bool IsLinkNavigation { get; set; }

        [DataMember]
        public string LinkUPC { get; set; }

        [DataMember]
        public string LinkISRC { get; set; }
        
    }

