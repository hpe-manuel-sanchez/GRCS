/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CprsReleaseDetail.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar G
 * Created on   : 01-10-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 

*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities for Email Groups
                  
****************************************************************************/
using System.Runtime.Serialization;
using UMGI.GRCS.BusinessEntities.Entities.GdrsEntities;

namespace UMGI.GRCS.BusinessEntities.Entities.CprsEntities
{
    public class CprsReleaseDetail : GdrsReleaseDetail
    {
        /// <summary>
        ///  to Determine a Release having Physical Rights in CPRS
        /// </summary>
        [DataMember]
        public bool HasPhysicalRights { get; set; }


        /// <summary>
        ///  to Determine a Release having Digital Rights in CPRS
        /// </summary>
        [DataMember]
        public bool HasDigitalRights { get; set; }
    }
}
