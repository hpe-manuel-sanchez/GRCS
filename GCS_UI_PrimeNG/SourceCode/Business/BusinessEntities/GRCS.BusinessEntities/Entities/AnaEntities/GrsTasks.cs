/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : GrsTasks.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 16-07-2012
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                          
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/


using System;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.AnaEntities
{
    /// <summary>
    /// enumaration for GRSTasks
    /// These tasks are configured in ANA for GRCS application
    /// </summary>
    [DataContract]
    [Flags]
    [Serializable]
    public enum GrsTasks : long
    {
        [EnumMember] None = 0L,
        [EnumMember] CreateNCRContract = 1L,
        [EnumMember] CreateCRContract = 2L,
        [EnumMember] EditNCRContract = 4L,
        [EnumMember] EditNCRAppContract = 8L,
        [EnumMember] EditCACContract = 16L,
        [EnumMember] EditCRContract = 32L,
        [EnumMember] DeleteNCRcontract = 64L,
        [EnumMember] DeleteCRContract = 128L,
        [EnumMember] ApproveNCRcontract = 256L,
        [EnumMember] ViewUnapprovedcontract = 512L,
        [EnumMember] ContractMaintinanceWQ = 1024L,
        [EnumMember] ContractLinkingWQ = 2048L,
        [EnumMember] LinkContract = 4096L,
        [EnumMember] UnlinkContract = 8192L,
        [EnumMember] UnlinkCRContract = 16384L,
        [EnumMember] LinkNCRContractToContract = 32768L,
        [EnumMember] UnLinkNCRContractToContract = 65536L,
        [EnumMember] LinkSplitContractToContract = 131072L,
        [EnumMember] UnlinkSplitContractToContract = 262144L,
        [EnumMember] RightsReviewWQ = 524288L,
        [EnumMember] EditRightsDataHeader = 1048576L,
        [EnumMember] EditRightsDataDigital = 2097152L,
        [EnumMember] EditRightsDataSecondary = 4194304L,
        [EnumMember] EditRightsDataPrecleared = 8388608L,
        [EnumMember] UpdateReviewStatus = 16777216L,
        [EnumMember] ViewSensitiveData = 33554432L,
        [EnumMember] ViewRepertoireRights = 67108864L,
        [EnumMember] AddressBookMaintinance = 134217728L,
        [EnumMember] LocalAdminPermission = 268435456L,
        [EnumMember] SystemAdministratorPermisson = 536870912L,
        [EnumMember] ContractTemplateMaintinance = 1073741824L,
        [EnumMember] DigitalRestrictionTemplate = 2147483648L,
        [EnumMember] ViewContractSenstiveData = 4294967296L,
        [EnumMember] SearchContract = 8589934592L,
        [EnumMember] ViewSensitiveRightsData = 17179869184L
    }
}