﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSMediaPortalProxy
{
    // 
    // This source code was auto-generated by wsdl, Version=4.0.30319.1.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ReleaseRightsServiceSoap",
        Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Territory))]
    public partial class ReleaseRightsService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback GetRightsByReleaseOperationCompleted;

        private System.Threading.SendOrPostCallback GetRightsDeltaByDateOperationCompleted;

        private System.Threading.SendOrPostCallback GetLatestRightsDeltaOperationCompleted;

        /// <remarks/>
        public ReleaseRightsService()
        {
            this.Url = "http://www.umgmusicportal.com/MusicPortalWS_QA/ReleaseRightsService.asmx";
        }

        /// <remarks/>
        public event GetRightsByReleaseCompletedEventHandler GetRightsByReleaseCompleted;

        /// <remarks/>
        public event GetRightsDeltaByDateCompletedEventHandler GetRightsDeltaByDateCompleted;

        /// <remarks/>
        public event GetLatestRightsDeltaCompletedEventHandler GetLatestRightsDeltaCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
            "http://umgmusicportal.com/MusicPortalWS/ReleaseRights/GetRightsByRelease",
            RequestNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            ResponseNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            Use = System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ReleaseRights")]
        public ReleaseRights GetRightsByRelease(WSCredentials credentials, string accountNumber, int r2CompanyId,
                                                int r2DivisionId, int r2LabelId, string upc)
        {
            object[] results = this.Invoke("GetRightsByRelease", new object[]
                                                                     {
                                                                         credentials,
                                                                         accountNumber,
                                                                         r2CompanyId,
                                                                         r2DivisionId,
                                                                         r2LabelId,
                                                                         upc
                                                                     });
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetRightsByRelease(WSCredentials credentials, string accountNumber,
                                                           int r2CompanyId, int r2DivisionId, int r2LabelId, string upc,
                                                           System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetRightsByRelease", new object[]
                                                              {
                                                                  credentials,
                                                                  accountNumber,
                                                                  r2CompanyId,
                                                                  r2DivisionId,
                                                                  r2LabelId,
                                                                  upc
                                                              }, callback, asyncState);
        }

        /// <remarks/>
        public ReleaseRights EndGetRightsByRelease(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public void GetRightsByReleaseAsync(WSCredentials credentials, string accountNumber, int r2CompanyId,
                                            int r2DivisionId, int r2LabelId, string upc)
        {
            this.GetRightsByReleaseAsync(credentials, accountNumber, r2CompanyId, r2DivisionId, r2LabelId, upc, null);
        }

        /// <remarks/>
        public void GetRightsByReleaseAsync(WSCredentials credentials, string accountNumber, int r2CompanyId,
                                            int r2DivisionId, int r2LabelId, string upc, object userState)
        {
            if ((this.GetRightsByReleaseOperationCompleted == null))
            {
                this.GetRightsByReleaseOperationCompleted =
                    new System.Threading.SendOrPostCallback(this.OnGetRightsByReleaseOperationCompleted);
            }
            this.InvokeAsync("GetRightsByRelease", new object[]
                                                       {
                                                           credentials,
                                                           accountNumber,
                                                           r2CompanyId,
                                                           r2DivisionId,
                                                           r2LabelId,
                                                           upc
                                                       }, this.GetRightsByReleaseOperationCompleted, userState);
        }

        private void OnGetRightsByReleaseOperationCompleted(object arg)
        {
            if ((this.GetRightsByReleaseCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs =
                    ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRightsByReleaseCompleted(this,
                                                 new GetRightsByReleaseCompletedEventArgs(invokeArgs.Results,
                                                                                          invokeArgs.Error,
                                                                                          invokeArgs.Cancelled,
                                                                                          invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
            "http://umgmusicportal.com/MusicPortalWS/ReleaseRights/GetRightsDeltaByDate",
            RequestNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            ResponseNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            Use = System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ReleaseRights")]
        public ReleaseRights GetRightsDeltaByDate(WSCredentials credentials, System.DateTime fromDate,
                                                  [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] System.Nullable<System.DateTime> toDate)
        {
            object[] results = this.Invoke("GetRightsDeltaByDate", new object[]
                                                                       {
                                                                           credentials,
                                                                           fromDate,
                                                                           toDate
                                                                       });
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetRightsDeltaByDate(WSCredentials credentials, System.DateTime fromDate,
                                                             System.Nullable<System.DateTime> toDate,
                                                             System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetRightsDeltaByDate", new object[]
                                                                {
                                                                    credentials,
                                                                    fromDate,
                                                                    toDate
                                                                }, callback, asyncState);
        }

        /// <remarks/>
        public ReleaseRights EndGetRightsDeltaByDate(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public void GetRightsDeltaByDateAsync(WSCredentials credentials, System.DateTime fromDate,
                                              System.Nullable<System.DateTime> toDate)
        {
            this.GetRightsDeltaByDateAsync(credentials, fromDate, toDate, null);
        }

        /// <remarks/>
        public void GetRightsDeltaByDateAsync(WSCredentials credentials, System.DateTime fromDate,
                                              System.Nullable<System.DateTime> toDate, object userState)
        {
            if ((this.GetRightsDeltaByDateOperationCompleted == null))
            {
                this.GetRightsDeltaByDateOperationCompleted =
                    new System.Threading.SendOrPostCallback(this.OnGetRightsDeltaByDateOperationCompleted);
            }
            this.InvokeAsync("GetRightsDeltaByDate", new object[]
                                                         {
                                                             credentials,
                                                             fromDate,
                                                             toDate
                                                         }, this.GetRightsDeltaByDateOperationCompleted, userState);
        }

        private void OnGetRightsDeltaByDateOperationCompleted(object arg)
        {
            if ((this.GetRightsDeltaByDateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs =
                    ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRightsDeltaByDateCompleted(this,
                                                   new GetRightsDeltaByDateCompletedEventArgs(invokeArgs.Results,
                                                                                              invokeArgs.Error,
                                                                                              invokeArgs.Cancelled,
                                                                                              invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute(
            "http://umgmusicportal.com/MusicPortalWS/ReleaseRights/GetLatestRightsDelta",
            RequestNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            ResponseNamespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights",
            Use = System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ReleaseRights")]
        public ReleaseRights GetLatestRightsDelta(WSCredentials credentials)
        {
            object[] results = this.Invoke("GetLatestRightsDelta", new object[]
                                                                       {
                                                                           credentials
                                                                       });
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetLatestRightsDelta(WSCredentials credentials, System.AsyncCallback callback,
                                                             object asyncState)
        {
            return this.BeginInvoke("GetLatestRightsDelta", new object[]
                                                                {
                                                                    credentials
                                                                }, callback, asyncState);
        }

        /// <remarks/>
        public ReleaseRights EndGetLatestRightsDelta(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ReleaseRights)(results[0]));
        }

        /// <remarks/>
        public void GetLatestRightsDeltaAsync(WSCredentials credentials)
        {
            this.GetLatestRightsDeltaAsync(credentials, null);
        }

        /// <remarks/>
        public void GetLatestRightsDeltaAsync(WSCredentials credentials, object userState)
        {
            if ((this.GetLatestRightsDeltaOperationCompleted == null))
            {
                this.GetLatestRightsDeltaOperationCompleted =
                    new System.Threading.SendOrPostCallback(this.OnGetLatestRightsDeltaOperationCompleted);
            }
            this.InvokeAsync("GetLatestRightsDelta", new object[]
                                                         {
                                                             credentials
                                                         }, this.GetLatestRightsDeltaOperationCompleted, userState);
        }

        private void OnGetLatestRightsDeltaOperationCompleted(object arg)
        {
            if ((this.GetLatestRightsDeltaCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs =
                    ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLatestRightsDeltaCompleted(this,
                                                   new GetLatestRightsDeltaCompletedEventArgs(invokeArgs.Results,
                                                                                              invokeArgs.Error,
                                                                                              invokeArgs.Cancelled,
                                                                                              invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class WSCredentials
    {

        private string usernameField;

        private string passwordField;

        /// <remarks/>
        public string Username
        {
            get { return this.usernameField; }
            set { this.usernameField = value; }
        }

        /// <remarks/>
        public string Password
        {
            get { return this.passwordField; }
            set { this.passwordField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class YouTubeVevoRight
    {

        private bool adSupportedStreamingField;

        private YouTubeVevoRightEnum uGCYouTubeField;

        private YouTubeVevoRightEnum vevoPolicyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool AdSupportedStreaming
        {
            get { return this.adSupportedStreamingField; }
            set { this.adSupportedStreamingField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public YouTubeVevoRightEnum UGCYouTube
        {
            get { return this.uGCYouTubeField; }
            set { this.uGCYouTubeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public YouTubeVevoRightEnum VevoPolicy
        {
            get { return this.vevoPolicyField; }
            set { this.vevoPolicyField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public enum YouTubeVevoRightEnum
    {

        /// <remarks/>
        Share,

        /// <remarks/>
        Block,

        /// <remarks/>
        Research,

        /// <remarks/>
        NA,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class TerritoryRight
    {

        private int rightIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RightId
        {
            get { return this.rightIdField; }
            set { this.rightIdField = value; }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResourceTerritory))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ReleaseTerritory))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public abstract partial class Territory
    {

        private string territoryISOCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TerritoryISOCode
        {
            get { return this.territoryISOCodeField; }
            set { this.territoryISOCodeField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class ResourceTerritory : Territory
    {

        private YouTubeVevoRight youTubeVevoRightsField;

        /// <remarks/>
        public YouTubeVevoRight YouTubeVevoRights
        {
            get { return this.youTubeVevoRightsField; }
            set { this.youTubeVevoRightsField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class ReleaseTerritory : Territory
    {

        private TerritoryRight[] territoryRightsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TerritoryRights")]
        public TerritoryRight[] TerritoryRights
        {
            get { return this.territoryRightsField; }
            set { this.territoryRightsField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class Resource
    {

        private ReleaseTerritory[] releaseTerritoriesField;

        private ResourceTerritory[] resourceTerritoriesField;

        private string iSRCField;

        private Type typeField;

        private string rightsExpiryDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReleaseTerritories")]
        public ReleaseTerritory[] ReleaseTerritories
        {
            get { return this.releaseTerritoriesField; }
            set { this.releaseTerritoriesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResourceTerritories")]
        public ResourceTerritory[] ResourceTerritories
        {
            get { return this.resourceTerritoriesField; }
            set { this.resourceTerritoriesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ISRC
        {
            get { return this.iSRCField; }
            set { this.iSRCField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public Type Type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RightsExpiryDate
        {
            get { return this.rightsExpiryDateField; }
            set { this.rightsExpiryDateField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public enum Type
    {

        /// <remarks/>
        Audio,

        /// <remarks/>
        Video,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class Release
    {

        private Resource[] resourcesField;

        private string uPCField;

        private string exclusiveInfoField;

        private bool deliverToYouTubeVevoField;

        private string accountNumberField;

        private int companyIdField;

        private int divisionIdField;

        private int labelIdField;

        private string gRidField;

        private int releaseStatusIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Resources")]
        public Resource[] Resources
        {
            get { return this.resourcesField; }
            set { this.resourcesField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UPC
        {
            get { return this.uPCField; }
            set { this.uPCField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ExclusiveInfo
        {
            get { return this.exclusiveInfoField; }
            set { this.exclusiveInfoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool DeliverToYouTubeVevo
        {
            get { return this.deliverToYouTubeVevoField; }
            set { this.deliverToYouTubeVevoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AccountNumber
        {
            get { return this.accountNumberField; }
            set { this.accountNumberField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int CompanyId
        {
            get { return this.companyIdField; }
            set { this.companyIdField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int DivisionId
        {
            get { return this.divisionIdField; }
            set { this.divisionIdField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int LabelId
        {
            get { return this.labelIdField; }
            set { this.labelIdField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GRid
        {
            get { return this.gRidField; }
            set { this.gRidField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ReleaseStatusId
        {
            get { return this.releaseStatusIdField; }
            set { this.releaseStatusIdField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class Right
    {

        private int idField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int Id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://umgmusicportal.com/MusicPortalWS/ReleaseRights")]
    public partial class ReleaseRights
    {

        private Right[] rightsField;

        private Release[] releasesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Rights")]
        public Right[] Rights
        {
            get { return this.rightsField; }
            set { this.rightsField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Releases")]
        public Release[] Releases
        {
            get { return this.releasesField; }
            set { this.releasesField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void GetRightsByReleaseCompletedEventHandler(object sender, GetRightsByReleaseCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRightsByReleaseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetRightsByReleaseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled,
                                                      object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ReleaseRights Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ReleaseRights)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void GetRightsDeltaByDateCompletedEventHandler(
        object sender, GetRightsDeltaByDateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRightsDeltaByDateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetRightsDeltaByDateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled,
                                                        object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ReleaseRights Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ReleaseRights)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void GetLatestRightsDeltaCompletedEventHandler(
        object sender, GetLatestRightsDeltaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLatestRightsDeltaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetLatestRightsDeltaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled,
                                                        object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ReleaseRights Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ReleaseRights)(this.results[0]));
            }
        }
    }
}