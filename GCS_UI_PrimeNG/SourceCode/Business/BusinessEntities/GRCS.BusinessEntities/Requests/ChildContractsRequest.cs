/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : IContractService.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Pavan Kumar Kota
 * Created on   : 18-12-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
 *************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 *Description :  Request parameters for GetChildContracts method
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    [DataContract]
    public class ChildContractsRequest : BaseRequest
    {
        [DataMember]
        public long ContractId { get; set; }

    }
}
