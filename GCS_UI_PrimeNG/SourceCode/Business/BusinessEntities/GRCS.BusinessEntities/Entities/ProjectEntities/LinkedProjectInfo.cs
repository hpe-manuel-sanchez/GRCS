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

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    [DataContract]
    public class LinkedProjectInfo 
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
        /// Gets or sets the project code.
        /// </summary>
        /// <value>The project code.</value>
        [DataMember]
        public string ProjectCode { get; set; }

        /// <summary>
        /// Gets or sets the admin company id.
        /// </summary>
        /// <value>The admin company id.</value>
        [DataMember]
        public long AdminCompanyId { get; set; }


        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
        [DataMember]
        public string ArtistName { get; set; }


        [DataMember]
        public int TotalRows { get; set; }

    }
}
