/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractRestrictions.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Rubini Raja
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description : Defines the entities for Contract Restrictions
                  
****************************************************************************/

using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Responses;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    /// <summary>
    ///  ContractRestrictions which is inherited from RightsAndRestrictions
    /// </summary>
    [DataContract]
    public class ContractRestrictions : RightsAndRestrictions
    {
        /// <summary>
        /// Gets or sets the contract id.
        /// </summary>
        /// <value>The contract id.</value>
        [DataMember]
        public long ContractId { get; set; }

        /// <summary>
        /// Gets or sets the clearance admin id.
        /// </summary>
        /// <value>The clearance admin id.</value>
        [DataMember]
        public long ClearanceAdminId { get; set; }
    }
}