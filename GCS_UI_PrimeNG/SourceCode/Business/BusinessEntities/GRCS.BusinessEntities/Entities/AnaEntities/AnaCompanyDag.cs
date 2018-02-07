/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : CompanyAuthentication.cs 
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


using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// 
    /// </summary>
    public class AnaCompanyDag 
    {
        [DataMember]
        public long CompanyId { get; set; }


        [DataMember]
        public long? DivisionId { get; set; }


        [DataMember]
        public long? LabelId { get; set; }
    }
}