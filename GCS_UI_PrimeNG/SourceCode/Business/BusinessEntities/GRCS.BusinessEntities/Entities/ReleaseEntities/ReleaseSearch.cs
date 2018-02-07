/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by         Modified on     Remarks  
 * Sudarsan Nagarajan  27-08-2012      Add the Entities 
 * KayalVizhi.V           1-09-2012       Added Resource File              
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Release Search
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities
{
    /// <summary>
    /// ReleaseSearch which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ReleaseSearch : EntityInformation
    {
        /// <summary>
        /// Release ProjectID.
        /// </summary> 
        [DataMember]
        public long ProjectId { get; set; }

        /// <summary>
        /// Release UPC.
        /// </summary> 
        [DataMember]
        public string Upc { get; set; }

        /// <summary>
        /// Release Project Description.
        /// </summary> 
        [DataMember]
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Release Title.
        /// </summary> 
        [DataMember]
        public string ReleaseTitle { get; set; }

        /// <summary>
        /// Release Version Title.
        /// </summary> 
        [DataMember]
        public string VersionTitle { get; set; }

        /// <summary>
        /// Artist Information.
        /// </summary> 
        [DataMember]
        public ArtistInfo ArtistName { get; set; }

        /// <summary>
        /// Release Catalogue Number
        /// </summary> 
        [DataMember]
        public string CatalogueNumber { get; set; }

        /// <summary>
        /// Configurationgroup
        /// </summary> 
        [DataMember]
        public string ConfigurationGroup { get; set; }


        /// <summary>
        /// Configuration
        /// </summary> 
        [DataMember]
        public string Configuration { get; set; }
    }
}