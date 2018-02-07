/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : PreClearanceMasterData.cs 
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
* Description :  Defines the entities for PreClearance MasterData Information
                  
****************************************************************************/
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for PreClearance
    /// Master Data Information
    /// </summary>
    [DataContract]
    public class PreClearanceMasterData
    {
        /// <summary>
        /// Gets or sets the clearance master data.
        /// </summary>
        /// <value>The clearance master data.</value>
        [DataMember]
        public Dictionary<int,string> ClearanceMasterData
        {
            get;
            set; 
        }
    }
}
