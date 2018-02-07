/* ************************************************************************ 
 * Copyrights ® 2013 UMGI 
 * ************************************************************************ 
 * File Name    : ClearanceEmailExtract.cs 
 * Project Code : UMG-GRCS
 * Author       : Arunagiri G
 * Created on   : 12-04-2013
 * ************************************************************************ 
 * Modification History 
 * ************************************************************************ 
 * Modified by       Modified on     Remarks 
 * 
*************************************************************************** 
 * Reviewed by       Modified on     Remarks 

****************************************************************************
* Description :  Defines the entities which is used to sened the email                                    
                  
****************************************************************************/

namespace UMGI.GRCS.BusinessEntities.Entities.NotificationEntities
{
    public class ClearanceEmailExtract
    {
        public long? ClrProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string R2ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public string ResourceTitle { get; set; }
        public string Isrc { get; set; }
        public string VersionTitle { get; set; }
        public string Artist { get; set; }
        public string RequestType { get; set; }
        public string OptionalData1 { get; set; }
        public string OptionalData2 { get; set; }
        public string freehandCompanyName { get; set; }
        public string User_Name { get; set; }
        public string AdminCompanyName { get; set; }
        public string RequestCompany { get; set; }
        public string Estimated_Sales_Units { get; set; }
        public string Action_User_Login_Name { get; set; }
        public string Release_Dt { get; set; }
        public string ActionType { get; set; }
        public string ReleaseTitle { get; set; }
        public string UPC { get; set; }
        public string ReleaseVersionTitle { get; set; }
        public string ReleaseArtist { get; set; }
    }
}
