/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DagFilterType.cs 
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
using System;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// Enumeration for PermissionFilter
    /// DAG details are configured as these Entities
    /// </summary>
    [DataContract]
    [Serializable]
    public enum DagFilterType
    {
        [EnumMember]
        None,
        [EnumMember]
        Country,
        [EnumMember]
        Company,
        [EnumMember]
        Workflow,
        [EnumMember]
        Contract,
        [EnumMember]
        CompanyAccount

    }
}