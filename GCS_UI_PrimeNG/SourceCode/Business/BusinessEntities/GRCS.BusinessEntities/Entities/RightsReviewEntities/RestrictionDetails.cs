/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : RestrictionDetails.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-02-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Restriction Details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Restriction Details
    /// </summary>
    [DataContract]
    public class RestrictionDetails
    {
        /// <summary>
        /// Gets or sets the restrictions.
        /// </summary>
        /// <value>The restrictions.</value>
        [DataMember]
        public string Restrictions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the consent period.
        /// </summary>
        /// <value>The consent period.</value>
        [DataMember]
        public string ConsentPeriod
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the hold back period.
        /// </summary>
        /// <value>The hold back period.</value>
        [DataMember]
        public string HoldBackPeriod
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [DataMember]
        public string Notes
        {
            get;
            set;
        }

    }
}
