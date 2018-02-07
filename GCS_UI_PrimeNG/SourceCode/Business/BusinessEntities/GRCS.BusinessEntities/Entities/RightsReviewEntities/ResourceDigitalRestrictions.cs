/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceDigitalRestrictions.cs 
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
* Description :  Defines the entities for Resource Digital Restrictions 
                 details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    ///  Contains entities for Resource Digital
    ///  Restrictions Details
    /// </summary>
    [DataContract]
    public class ResourceDigitalRestrictions
    {
        public ResourceDigitalRestrictions()
        {
            DigitalRestrictions = new DigitalRestrictionsInfo();
            ReviewStatus = new ReviewStatus();
        }
        /// <summary>
        /// Gets or sets the digital restrictions.
        /// </summary>
        /// <value>The digital restrictions.</value>
        [DataMember]
        public DigitalRestrictionsInfo DigitalRestrictions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the review status.
        /// </summary>
        /// <value>The review status.</value>
        [DataMember]
        public ReviewStatus ReviewStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the is acquired.
        /// </summary>
        /// <value>The is acquired.</value>
        [DataMember]
        public bool? IsAcquired
        {
            get;
            set;
        }  
    }
}
