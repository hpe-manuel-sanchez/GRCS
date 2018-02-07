/* ************************************************************************ 
 * Copyrights ® 2012 UMGI 
 * ************************************************************************ 
 * File Name    : NotificationMessageType.cs 
 * Project Code : UMG-GRCS(C/115921) 
 * Author       : Senthil Kumar Gunasekaran
 * Created on   : 18-03-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 *                          
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************/


using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    [Flags]
    [Serializable]
    [DefaultValue(None)]
    public enum NotificationMessageType : long
    {
        [EnumMember] None = 0L,
        [EnumMember] Project = 2L,
        [EnumMember] Release = 4L,
        [EnumMember] ReleaseLocal = 8L,
        [EnumMember] Resource = 16L,
        [EnumMember] ResourceLocal = 32L,

        [EnumMember] RepertoireNotification = Project | Release | ReleaseLocal | Resource | ResourceLocal,

        [EnumMember] Talent = 64L,
// ReSharper disable InconsistentNaming
        [EnumMember] P_CCompany = 128L,
// ReSharper restore InconsistentNaming
        [EnumMember] ArtistPcCompanyNotification = Talent | P_CCompany,

        [EnumMember] ReleaseRights = 256L,
        [EnumMember] MediaPortalNotification = ReleaseRights,

        [EnumMember] CprsNotification = 512L,
        [EnumMember] GdrsReleaseTypeOne = 1024L,

        [EnumMember] ReleaseDateNotification =
            GdrsReleaseTypeOne | CprsNotification | PhysicalReleaseSchedule | ReleaseSchedule,

        [EnumMember] Company = 2048L,
        [EnumMember] CompanyDivision = 4096L,
        [EnumMember] Configuration = 8192L,
        [EnumMember] Country = 16384L,
        [EnumMember] Label = 32768L,
        [EnumMember] DivisionLabel = 65536L,
        [EnumMember] Territory = 131072L,
        [EnumMember] TerritoryArea = 262144L,
        [EnumMember] TerritoryCountry = 524288L,
        [EnumMember] Carrier = 1048576L,
        [EnumMember] Currency = 2097152L,
        [EnumMember] ParentTerritory = 4194304L,

        [EnumMember] PhysicalReleaseSchedule = 8388608L,
        [EnumMember]
        ReleaseSchedule = 16777216L,

        [EnumMember] GtaNotification =
            Company | CompanyDivision | Configuration | Country | Label | DivisionLabel | Territory | TerritoryArea |
            TerritoryCountry | Carrier | Currency | ParentTerritory,
    }
}