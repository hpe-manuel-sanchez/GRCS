/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : TerritorialDisplayWQ.cs 
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
* Description :  Defines the entities for Territorial Display WQ Details                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Territorial Display
    /// WQ Details
    /// </summary>
    [DataContract]
    public class TerritorialDisplayWQ
    {
        /// <summary>
        /// Gets or sets the right set ID.
        /// </summary>
        /// <value>The right set ID.</value>
        [DataMember]
        public long RightSetId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the territorial display.
        /// </summary>
        /// <value>The territorial display.</value>
        [DataMember]
        public List<TerritorialDisplay> TerritorialDisplay
        {
            get;
            set;
        }

    }
}
