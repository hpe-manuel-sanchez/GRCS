/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DigitalRestrictionList.cs 
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
* Description :  Defines the entities for Digital Restriction List Details                  
****************************************************************************/
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Digital Restriction List Details
    /// </summary>
    [DataContract]
    public class DigitalRestrictionList : RightSetInfo
    {
        /// <summary>
        /// Gets or sets the release digital.
        /// </summary>
        /// <value>The release digital.</value>
        [DataMember]
        public DigitalRestrictions ReleaseDigital
        {
            get;
            set;
        }
    }
}
