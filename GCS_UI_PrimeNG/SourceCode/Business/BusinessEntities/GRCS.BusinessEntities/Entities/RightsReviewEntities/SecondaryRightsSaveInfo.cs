/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : SecondaryRightsSaveInfo.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 13-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Secondary Rights MasterData
                 Information
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    [DataContract]
    public class SecondaryRightsSaveInfo
    {
        /// <summary>
        /// Gets or sets the resource rights.
        /// </summary>
        /// <value>The resource rights.</value>
        [DataMember]
        public List<SecondaryRights> Rights
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
