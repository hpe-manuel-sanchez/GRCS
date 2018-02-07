/* ************************************************************************
 * Copyrights ® 2012 UMGI
 * ************************************************************************
 * File Name    : CompanyInfo.cs
 * Project Code : UMG-GRCS(C/115921)
 * Author       : Senthil Kumar G
 * Created on   : 28-11-2012
 * ************************************************************************
 * Modification History
 * ************************************************************************
 * Modified by       Modified on     Remarks
 * Guna              12-Dec-2012     added two more types (R2ReleaseLocal and R2ResourceLocal)
***************************************************************************
 * Reviewed by       Modified on     Remarks

****************************************************************************
* Description :  Defines the Upstream Notification Types

****************************************************************************/

using System.Runtime.Serialization;

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    /// <summary>
    /// Enum for Upstream Notification Types
    /// Notifications from the upstream systems are mapped to any one of these notification types
    /// Same names are used as the username for Created By and Modified By user names
    /// </summary>
    [DataContract]
    public enum NotificationType
    {
        [EnumMember]
        Invalid,

        [EnumMember]
        R2Project,

        [EnumMember]
        R2Release,

        [EnumMember]
        R2ReleaseLocal,

        [EnumMember]
        R2Resource,

        [EnumMember]
        R2ResourceLocal,

        [EnumMember]
        R2PcNoticeCompany,

        [EnumMember]
        Gta,

        [EnumMember]
        R2Artist,

        [EnumMember]
        Cprs,

        [EnumMember]
        Gdrs,

        [EnumMember]
        MediaPortal,

        [EnumMember]
        CprsPhysicalRelease,

        [EnumMember]
        DSchedDigitalRelease,

        [EnumMember]
        MediaPortalMigration,

        //Downstream GRS Notification Types
        [EnumMember]
        Contractrights = 34,
        [EnumMember]
        Projectrights = 38,
        [EnumMember]
        Resourcerights = 36,
        [EnumMember]
        Releaserights = 35,
        [EnumMember]
        Trackrights = 37,
        [EnumMember]
        Artistrights = 107,

        //Downstream GCS Notification Types
        [EnumMember]
        ClearanceProject = 201,
        [EnumMember]
        ClearanceRelease = 202,
        [EnumMember]
        ClearanceResource = 203

    };
}