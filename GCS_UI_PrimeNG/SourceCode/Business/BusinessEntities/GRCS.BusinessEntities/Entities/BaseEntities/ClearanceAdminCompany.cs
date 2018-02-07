/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceAdminCompany.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 09-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * Rengaraj          03-Aug-2012     modified code for coding standard format       
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
 * Description :  Defines the Clearance Admin Company entities                                      
                  
****************************************************************************/

using System.Runtime.Serialization;
using System;
using UMGI.GRCS.BusinessEntities.Entities.ContractEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.BaseEntities
{
    /// <summary>
    /// Clearance Admin Company
    /// </summary>
    [DataContract]
    [Serializable]
    public class ClearanceAdminCompany : CompanyInfo
    {
      
    }
}