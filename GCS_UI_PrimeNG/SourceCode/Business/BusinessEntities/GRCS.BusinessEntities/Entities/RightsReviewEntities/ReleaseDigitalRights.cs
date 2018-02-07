/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ReleaseDigitalRights.cs 
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
* Description :  Defines the entities for Release Digital Rights Details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Release Digital Rights Details
    /// </summary>
    [DataContract]
    public class ReleaseDigitalRights : Release
    {
        public  ReleaseDigitalRights()
        {
            DigitalRights = new DigitalRestrictionsInfo();
        }

        /// <summary>
        /// Gets or sets the release digital.
        /// </summary>
        /// <value>The release digital.</value>
        [DataMember]
        public DigitalRestrictionsInfo DigitalRights
        {
            get;
            set;
        }
    }
}
