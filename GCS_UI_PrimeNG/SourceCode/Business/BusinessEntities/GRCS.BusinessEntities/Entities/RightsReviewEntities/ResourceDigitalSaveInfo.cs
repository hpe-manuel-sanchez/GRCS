/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceDigitalSaveInfo.cs 
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
* Description :  Defines the entities for Resource Digital Rights Save Information
                                   
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Digital 
    /// Rights save info
    /// </summary>
    [DataContract]
    public class ResourceDigitalSaveInfo
    {
        /// <summary> 
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<ResourceDigitalRestrictions> Rights
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
