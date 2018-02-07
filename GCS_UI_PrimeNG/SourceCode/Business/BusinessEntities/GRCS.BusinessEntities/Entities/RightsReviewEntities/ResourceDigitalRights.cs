/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceDigitalRights.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Karthik
 * Created on   : 08-01-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Resource Digital Rights Details                  
****************************************************************************/
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource Digital Rights Details
    /// </summary>
    [DataContract]
    public partial class ResourceDigitalRights : Resource
    {
        public ResourceDigitalRights()
        {
            DigitalRestriction = new ResourceDigitalRestrictions();
        }
        /// <summary>
        /// Gets or sets the digital restriction.
        /// </summary>
        /// <value>The digital restriction.</value>
        [DataMember]
        public ResourceDigitalRestrictions DigitalRestriction
        {
            get;
            set;
        }

       

    }
}
