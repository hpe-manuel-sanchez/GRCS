/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : AnaConfigurationType.cs 
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
using System.ComponentModel;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// Enumeration for ANAConfigurationType;
    /// REpresents the two types of the configuration pattern in ANA
    /// </summary>
    [DataContract]
    public enum AnaConfigurationType
    {
        [EnumMember] Role,
        [EnumMember] Task
    }

    public enum AnaRoles
    {
        [Description("Label User")]
        LabelUser,
        [Description("Label User - Enhanced")]
        LabelUserEnhanced,
        [Description("Label User (UMGNA)")]
        LabelUserUMGNA,
        [Description("Label User (UMGI)")]
        LabelUserUMGI,
        [Description("Release Approver")]
        ReleaseApprover,
        [Description("Global Royalties")]
        GlobalRoyalties,
        [Description("Licensed Out/Role")]
        LicensedOut
    }

    public enum AnaTask
    {
        [Description("Create Project")]
        CreateProject,
        [Description("Search Project")]
        SearchProject
    }

}