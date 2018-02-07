/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ProjectSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 24-08-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
                                  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Project Search
                  
****************************************************************************/


using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    /// <summary>
    /// ProjectSearch which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ProjectSearch : EntityInformation
    {
        /// <summary>
        /// ProjectID
        /// </summary> 
        [DataMember]
        public long ProjectId { get; set; }

        /// <summary>
        /// ProjectDescription
        /// </summary>
        [DataMember]
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Artists Information
        /// </summary>
        [DataMember]
        public ArtistInfo Info { get; set; }

        /// <summary>
        /// DataAdminCompany
        /// </summary>
        [DataMember]
        public string DataAdminCompany { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [DataMember]
        public string Label { get; set; }
    }
}