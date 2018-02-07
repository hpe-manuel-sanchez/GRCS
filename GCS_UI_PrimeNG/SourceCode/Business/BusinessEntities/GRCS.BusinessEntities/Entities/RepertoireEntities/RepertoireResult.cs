/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : RepertoireResult.cs
 * Project Code : UMG-GRCS(C/115921)   
 * Author       : vijayakumar R
 * Created on   : 11/11/2012 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by       Modified on     Remarks 
 * 
****************************************************************************** 
 * Reviewed by       Modified on     Remarks 
 *
******************************************************************************
 * Description : Defines the entities for Release & Resource List.
                  
****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ResourceEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RepertoireEntities
{
    [DataContract]
    public class RepertoireResult : IEnumerable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RepertoireResult"/> class.
        /// </summary>
        public RepertoireResult()
        {
            ReleaseList = new List<ReleaseInfo>();
            ResourceList = new List<ResourceInfo>();
        }

        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the release list.
        /// </summary>
        /// <value>The release list.</value>
        [DataMember]
        public List<ReleaseInfo> ReleaseList { get; set; }

        /// <summary>
        /// Gets or sets the resource list.
        /// </summary>
        /// <value>The resource list.</value>
        [DataMember]
        public List<ResourceInfo> ResourceList { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
