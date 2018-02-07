/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ResourceSecondaryRights.cs 
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
* Description :  Defines the entities for Resource Secondary Rights Details
                  
****************************************************************************/
using System.ComponentModel;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.RightsReviewEntities
{
    /// <summary>
    /// Contains entities for Resource 
    /// Secondary Rights Details
    /// </summary>
    [DataContract]
    public partial class ResourceSecondaryRights : Resource
    {
        /// <summary>
        /// Gets or sets the rights.
        /// </summary>
        /// <value>The rights.</value>
        [DataMember]
        [DisplayName]
        public SecondaryRights Rights
        {
            get;
            set;
        }
    }
}
