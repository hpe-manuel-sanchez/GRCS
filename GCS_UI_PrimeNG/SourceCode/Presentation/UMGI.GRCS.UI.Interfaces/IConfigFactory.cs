/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : IConfigFactory.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 23-08-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 */

using System.Collections.Generic;
using System.Web.Mvc;

namespace UMGI.GRCS.UI.Interfaces
{
    public interface IConfigFactory
    {
        string ArtistService { get; set; }
        string Binding { get; set; }
        string ContractService { get; set; }
        bool IsSecurityEnabled { get; set; }
        int PageSize { get; set; }
        Dictionary<int, string> PageSizeValues { get; set; }
        string ProjectService { get; set; }
        string ReleaseService { get; set; }
        string ResourceService { get; set; }
        string RightsService { get; set; }
        int TimeOut { get; set; }
        string AppVersion { get; set; }
        string AppEnvironment { get; set; }
        string AppBuildDate { get; set; }
        string UserService { get; set; }
        string WorkgroupService { get; set; }        
        string GrcsUtilityService { get; set; }
        string WorkQueueService { get; set; }
        string GlobalService { get; set; }
        string RepertoireService { get; set; }
        string ClearanceInboxService { get; set; }
        string ClearanceProjectService { get; set; }
        string ClearanceResourceService { get; set; }
        string ClearanceReleaseService { get; set; }
        string RoutingService { get; set; }
        string PCompanyService { get; set; }
        string ReportService { get; set; }
        bool IsBindingInConfig { get; set; }
        IEnumerable<SelectListItem> GetPageSizeList();
        bool IsLocalDateTimeEnabled { get; set; }
    }
}
