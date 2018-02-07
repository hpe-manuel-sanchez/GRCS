/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AnaTargetApplication.cs 
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
    /// enumaration for ANATargetApplication
    /// </summary>
    [DataContract]
    public enum AnaTargetApplication
    {
        //parameter to ANA, ANA checks for Roles and Tasks against this application
        [EnumMember]
        None,
        [EnumMember]
        Grcs,
        [EnumMember]
        Grs,
        [EnumMember]
        Gcs,
        [EnumMember]
        R2

    }
}