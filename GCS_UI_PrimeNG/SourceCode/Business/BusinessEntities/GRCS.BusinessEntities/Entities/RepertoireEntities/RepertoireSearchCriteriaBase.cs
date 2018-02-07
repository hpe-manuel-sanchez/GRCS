/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RepertoireSearchCriteriaBase.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : vijayakumar R
 * Created on   : 03/10/2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks  
 * KayalVizhi.V      11-10-2012      Added Entities to Resource File  
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for RepertoireSearchCriteriaBase 

****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
   [DataContract]
    public class RepertoireSearchCriteriaBase : EntityInformation
    {

       public RepertoireSearchCriteriaBase()
       {
           Criteria = new FilterFields();
       }

       /// <summary>
       /// Gets or sets a value indicating whether [exclude local rows].
       /// </summary>
       /// <value><c>true</c> if [exclude local rows]; otherwise, <c>false</c>.</value>
       [DataMember]
       public bool ExcludeLocalRows { get; set; }
       /// <summary>
       /// Gets or sets the artist id.
       /// </summary>
       /// <value>The artist id.</value>
       [DataMember]
       public long ArtistId { get; set; }

       /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <value>The name of the artist.</value>
       [DataMember]
       public string ArtistName { get; set; }

       /// <summary>
       /// Gets or sets the clearance admin company id.
       /// </summary>
       /// <value>The clearance admin company id.</value>
       [DataMember]
       public long ClearanceAdminCompanyId { get; set; }

       /// <summary>
       /// Gets or sets the criteria.
       /// </summary>
       /// <value>The criteria.</value>
       [DataMember]
       public FilterFields Criteria { get; set; }

    }
}
