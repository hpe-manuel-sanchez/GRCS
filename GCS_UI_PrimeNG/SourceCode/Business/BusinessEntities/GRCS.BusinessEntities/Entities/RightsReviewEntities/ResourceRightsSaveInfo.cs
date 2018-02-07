/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceRightsSaveInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 11-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Rights Save Information                                   
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Rights Save Information
    /// </summary>
    [DataContract]
    public class ResourceRightsSaveInfo
    {
        public ResourceRightsSaveInfo()
        {
            ResourceRights = new List<ResourceAcquiredRights>();
            ModifierInfo = new ModifierInfo();
        }
        /// <summary>
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<ResourceAcquiredRights> ResourceRights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the modifier info.
        /// </summary>
        /// <value>The modifier info.</value>
        [DataMember]
        public ModifierInfo ModifierInfo
        {
            get;
            set;
        }       
    }
}
