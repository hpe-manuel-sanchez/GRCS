/* *************************************************************************** 
 * Copyrights ® 2010 Universal Music Group 
 * *************************************************************************** 
 * FileName     : ReleaseResourcesInfo.cs
 * Project Code : UMG-GRCS(C/115921)   
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
 * Description : Defines the entities for Release & Resource List.
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.ResourceEntities
{
   [DataContract]
   public class ReleaseResourcesInfo : EntityInformation
    {
        /// <summary>
        /// Gets or sets the release search list.
        /// </summary>
        /// <value>The release search list.</value>
       [DataMember]
       public ReleaseSearchResult ReleaseSearchList { get; set; }

       /// <summary>
       /// Gets or sets the resource search list.
       /// </summary>
       /// <value>The resource search list.</value>
       [DataMember]
       public ResourceSearchResult ResourceSearchList { get; set; }
    }
}
