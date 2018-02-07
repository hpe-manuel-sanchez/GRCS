/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : SecondaryRightsMasterData.cs 
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
    /// <summary>
    /// Contains entities for Secondary Rights
    /// Master Data Information
    /// </summary>
    [DataContract]
    public class SecondaryRightsMasterData
    {
        /// <summary>
        /// Gets or sets the restrictions.
        /// </summary>
        /// <value>The restrictions.</value>
        [DataMember]
        public Dictionary<int, string> Restrictions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the exploitation types.
        /// </summary>
        /// <value>The exploitation types.</value>
        [DataMember]
        public Dictionary<int, string> ExploitationTypes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the consent period.
        /// </summary>
        /// <value>The consent period.</value>
        [DataMember]
        public Dictionary<int, string> ConsentPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the restricted options.
        /// </summary>
        /// <value>The restricted options.</value>
        [DataMember]
        public Dictionary<int, string> RestrictedOptions
        {
            get;
            set;
        }
      
    }
}
