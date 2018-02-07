/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AnaSourceApplication.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil kumar
 * Created on   : 05-10-2012
 * Modified on  : 22-01-2013
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
    /// Enumeration for ANAConfigurationType;
    /// This is for ANA Resolver to get parameters from its source application
    /// This will come in the XML as part of the DAG. 
    /// </summary>
    [DataContract]
    public enum AnaSourceApplication
    {
        [EnumMember] Gta,
        [EnumMember] Grcs
    }
}