/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AnaApplicationUsers.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 01-07-2013
 
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
    [DataContract]
    public class AnaApplicationUsers
    {
        [DataMember] public string HashCode;

        [DataMember] public List<ApplicationUser> Users;
    }
}