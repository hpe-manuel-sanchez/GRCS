/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ProjectDetail.cs 
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
 *Description :  Defines the entities for Project Details
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ArtistEntities;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    /// <summary>
    /// ProjectDetail which is inherited from BaseClass ClassInfo 
    /// </summary>
    [DataContract]
    public class ProjectDetail : EntityInformation
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        [DataMember]
        public long ProjectId { get; set; }


        /// <summary>
        /// Gets or sets the project description.
        /// </summary>
        /// <value>The project description.</value>
        [DataMember]
        public string ProjectDescription { get; set; }


        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>The company.</value>
        [DataMember]
        public string Company { get; set; }


        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        /// <value>The company id.</value>
        [DataMember]
        public long CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        [DataMember]
        public string Label { get; set; }


        /// <summary>
        /// Gets or sets the division.
        /// </summary>
        /// <value>The division.</value>
        [DataMember]
        public string Division { get; set; }


        /// <summary>
        /// Gets or sets the info.
        /// </summary>
        /// <value>The info.</value>
        [DataMember]
        public ArtistSearch Info { get; set; }


        /// <summary>
        /// Gets or sets the release count.
        /// </summary>
        /// <value>The release count.</value>
        [DataMember]
        public long ReleaseCount { get; set; }


        /// <summary>
        /// Gets or sets the resource count.
        /// </summary>
        /// <value>The resource count.</value>
        [DataMember]
        public long ResourceCount { get; set; }
    }
}