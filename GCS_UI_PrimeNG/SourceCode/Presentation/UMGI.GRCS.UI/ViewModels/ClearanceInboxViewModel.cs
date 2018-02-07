using Syncfusion.Mvc.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities;

namespace UMGI.GRCS.UI.ViewModels.ClearanceInbox
{
    public class InboxViewModel
    {
        public InboxViewModel()
        {
            this.Projects = new List<InboxProject>();
            this.SearchResult = new InboxSearchResult();
            this.SearchCriteria = new InboxSearchCriteria();
            this.FilterCriteria = new InboxFilterCriteria();
            this.State = new InboxState();
            State.ShowAllProjects = false;
            this.ColumnSettings = new List<ColumnSetting>();
        }

        public List<InboxProject> Projects { get; set; }

        public InboxSearchCriteria SearchCriteria { get; set; }

        public InboxSearchResult SearchResult { get; set; }

        public InboxFilterCriteria FilterCriteria { get; set; }

        public InboxState State { get; set; }

        public long FolderToExpandByDefault { get; set; }

        public class InboxProject
        {
            public int FolderViewId { get; set; }

            public bool? IsSystemFolder { get; set; }

            public string FolderInformation { get; set; }

            public long CurrentFolderId { get; set; }            

            public long OriginalSystemFolderId { get; set; }

            public long FolderId { get; set; }

            public string FolderName { get; set; }

            public long ClearanceProjectId { get; set; }

            public string ProjectTitle { get; set; }

            public string ProjectType { get; set; }

            public long ProjectTypeId { get; set; }

            public string ProjectDetail { get; set; }

            public string ProjectReferenceNumber { get; set; }

            public long ProjectStatusId { get; set; }

            public long? EstimatedSalesUnit { get; set; }

            public DateTime? ReleaseDate { get; set; }

            public DateTime ProjectSubmissionDate { get; set; }

            public DateTime? NotificationRecieved { get; set; }

            public long? RccHandlerId { get; set; }

            public string RccHandlerName { get; set; }

            public string RequestorName { get; set; }

            public string RequestingCompanyName { get; set; }

            public string RequestingCompanyIsoName { get; set; }

            public string ThirdPartyCompanyName { get; set; }

            public string ThirdPartyCompanyIsoName { get; set; }

            public bool IsUnread { get; set; }

            public bool IsThirdParty { get; set; }

            public bool IsAllRequestReviewed { get; set; }

            public long? AssignedToUserId { get; set; }

            public string AssignedToUser { get; set; }

            public long RoleId { get; set; }

            public string RoleName { get; set; }

            public long WorkgroupId { get; set; }

            public string WorkGroupName { get; set; }

            public long TotalRecordCount { get; set; }

            public string CreatedByUser { get; set; }

            public DateTime CreatedDate { get; set; }

            public string ModifiedUser { get; set; }

            public DateTime ModifiedDate { get; set; }

            public string ModifiedUserAssignedTo { get; set; }

            public DateTime? ModifiedDateAssignedTo { get; set; }

            public bool ShowInformation { get; set; }
        }

        public class InboxSearchCriteria
        {
            public InboxSearchCriteria()
            {
                this.SearchType = new List<ListItem>();
            }

            public List<ListItem> SearchType { get; set; }
        }

        public class InboxSearchResult
        {
            public long ReminderCount { get; set; }

            public long NotificationCount { get; set; }

            public string ErrorMsg { get; set; }


        }

        public class InboxFilterCriteria
        {
            public InboxFilterCriteria()
            {
                this.RequestType = new List<ListItem>();
                this.RccAdminRequestType = new List<ListItem>();
                this.RccHandler = new List<ListItem>();
                this.Requestor = new List<ListItem>();
                this.ScopeType = new List<ListItem>();
                this.Workgroup = new List<WorkgroupBase>();
            }

            public List<ListItem> RequestType { get; set; }

            public List<ListItem> RccAdminRequestType { get; set; }

            public List<ListItem> RccHandler { get; set; }

            public List<ListItem> Requestor { get; set; }

            public List<ListItem> ScopeType { get; set; }

            public List<WorkgroupBase> Workgroup { get; set; }
        }

        public class InboxState
        {
            public InboxState()
            {
                SortDescriptor = new SortDescriptor();
                SortDescriptor.ColumnName = "NotificationRecieved";
                SortDescriptor.SortDirection = ListSortDirection.Descending;
            }

            public long? FolderSize { get; set; }

            public long? SelectedFolderId { get; set; }

            public bool ShowAllProjects { get; set; }

            public SortDescriptor SortDescriptor { get; set; }

            public long? SelectedProjectId { get; set; }

            public bool? ProjectReadStatus { get; set; }
        }

        public List<ColumnSetting> ColumnSettings { get; set; }

        public class ColumnSetting
        {
            public string GridId { get; set; }
            public string Column { get; set; }
            public int Width { get; set; }
        }
    }
}