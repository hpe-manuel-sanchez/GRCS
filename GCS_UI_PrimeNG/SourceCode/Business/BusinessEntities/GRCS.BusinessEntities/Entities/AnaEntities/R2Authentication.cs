/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : R2Authentication.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 04-03-2013
 
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for ANA
                  
****************************************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class R2Authentication
    {
        [DataMember] public string TaskName;
        [DataMember] public string HashCode;
        [DataMember] public bool IsAuthorized;
        [DataMember] public AnaConfigurationType PermissionType;
        [DataMember] public List<string> PermissionCandidates;
        [DataMember] public List<long?> AccountIds;
        [DataMember] public List<long?> CountryIds;
        [DataMember] public List<AnaCompany> Companies;
    }
}