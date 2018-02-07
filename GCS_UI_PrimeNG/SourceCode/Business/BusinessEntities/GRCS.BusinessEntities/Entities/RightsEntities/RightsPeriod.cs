/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RightsPeriod.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Rights Period
                  
****************************************************************************/


using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsEntities
{
    /// <summary>
    /// RightsPeriod
    /// </summary>
    [DataContract]
    public class RightsPeriod
    {
        /// <summary>
        /// Gets or sets the rights period id.
        /// </summary>
        /// <value>The rights period id.</value>
        [DataMember]
        public int RightsPeriodId { get; set; }

        /// <summary>
        /// Gets or sets the type of the rights period.
        /// </summary>
        /// <value>The type of the rights period.</value>
        [DataMember]
        public string RightsPeriodType { get; set; }
    }
}