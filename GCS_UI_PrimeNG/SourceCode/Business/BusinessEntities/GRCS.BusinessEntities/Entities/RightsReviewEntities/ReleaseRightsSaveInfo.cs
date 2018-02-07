/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseRightsSaveInfo.cs 
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
* Description :  Defines the entities for Release Rights Save Information
                                   
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Rights Save Information
    /// </summary>
    [DataContract]
    public class ReleaseRightsSaveInfo
    {
        public ReleaseRightsSaveInfo()
        {
            ReleaseRights = new List<RepertoireRights>();
            ModifierInfo = new ModifierInfo();
        }
        /// <summary>
        /// Gets or sets the release rights.
        /// </summary>
        /// <value>The release rights.</value>
        [DataMember]
        public List<RepertoireRights> ReleaseRights
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
