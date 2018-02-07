/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : DagEntityType.cs 
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
    /// Enumeration for PermissionFilterEntityType
    /// Represents to the caption of DAG data, These are the alias names of the table feilds
    /// </summary>
    [DataContract]
    public enum DagEntityType
    {
        // If DAG type is country, This will have data otherwise null
        [EnumMember]
        None,
        [EnumMember]
        CountryId,
        [EnumMember]
        CompanyId,
        [EnumMember]
        Value,
        [EnumMember]
        Description,
        [EnumMember]
        DivisionId,
        [EnumMember]
        LabelId,
        [EnumMember]
        CompanyAccountId,


    }
}