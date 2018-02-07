/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ContractSearch.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Pavan Kumar K
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Defines the entities for Contract Search 
                  
****************************************************************************/

using System.Collections.Generic;
using UMGI.GRCS.BusinessEntities.Entities.BaseEntities;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    /// <summary>
    /// ContractSearch which is inherited from ContractInfo
    /// </summary>

    public class ContractSearch : EntityInformation
    {
        public ContractInfo ContractFilter { get; set; }
        public List<long> ContractIdsInSplitDeal { get; set; }
        public List<long> UserClearanceAdminCompanyIds { get; set; }

    }
}