/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ProjectSearchCriteria.cs
 * Project Code :   
 * Author       : vijayakumar R
 * Created on   : 03/10/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for ProjectSearchCriteria.
                  
******************************************************************************/
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ProjectEntities
{
    [DataContract]
    public class ProjectSearchCriteria : RepertoireSearchCriteriaBase
    {
        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>The project code.</value>
        [DataMember]
        public string ProjectCode { get; set; }

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
        /// Gets or sets the label id.
        /// </summary>
        /// <value>The label id.</value>
        [DataMember]
        public long LabelId { get; set; }


        /// <summary>
        /// Gets or sets the data admin company id.
        /// </summary>
        /// <value>The data admin company id.</value>
          [DataMember]
        public long DataAdminCompanyId { get; set; }

          [DataMember]
          public long divisionId { get; set; }

          [DataMember]
          public string ApplicationType { get; set; }
    }
}
