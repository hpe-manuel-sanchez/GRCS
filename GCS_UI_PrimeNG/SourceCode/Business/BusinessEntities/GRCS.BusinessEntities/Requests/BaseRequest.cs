/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : BaseRequest.cs 
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
 *Description :  BaseRequest for all requests
                  
****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Requests
{
    [DataContract]
    public abstract class BaseRequest
    {
        [DataMember]
        public string UserName { get; set; }
        
        [DataMember]
        public string ClientIp { get; set; }
    }
}
