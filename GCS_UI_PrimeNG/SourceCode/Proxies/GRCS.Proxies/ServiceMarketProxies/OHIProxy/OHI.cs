﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("http://ohiservice.umusic.net/types/v4", ClrNamespace = "ohiservice.umusic.net.types.v4")]

namespace ohiservice.umusic.net.types.v4
{


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetHistObjRV4", Namespace="http://ohiservice.umusic.net/types/v4")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.GetHistObjDiv))]
    public partial class GetHistObjRV4 : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string Login_NameField;
        
        private long OHI_Hist_Obj_IDField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login_Name
        {
            get
            {
                return this.Login_NameField;
            }
            set
            {
                this.Login_NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long OHI_Hist_Obj_ID
        {
            get
            {
                return this.OHI_Hist_Obj_IDField;
            }
            set
            {
                this.OHI_Hist_Obj_IDField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetHistObjDiv", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class GetHistObjDiv : ohiservice.umusic.net.types.v4.GetHistObjRV4
    {
        
        private long OHI_Hist_Obj_ID_PreField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long OHI_Hist_Obj_ID_Pre
        {
            get
            {
                return this.OHI_Hist_Obj_ID_PreField;
            }
            set
            {
                this.OHI_Hist_Obj_ID_PreField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RetHistDifResult", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class RetHistDifResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private ohiservice.umusic.net.types.v4.RetXMLChange[] Dif_XMLField;
        
        private string Pre_XMLField;
        
        private string Succ_XMLField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ohiservice.umusic.net.types.v4.RetXMLChange[] Dif_XML
        {
            get
            {
                return this.Dif_XMLField;
            }
            set
            {
                this.Dif_XMLField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Pre_XML
        {
            get
            {
                return this.Pre_XMLField;
            }
            set
            {
                this.Pre_XMLField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Succ_XML
        {
            get
            {
                return this.Succ_XMLField;
            }
            set
            {
                this.Succ_XMLField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RetXMLChange", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class RetXMLChange : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string CurrentValueField;
        
        private string PreviousValueField;
        
        private string StatusField;
        
        private string XPathField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CurrentValue
        {
            get
            {
                return this.CurrentValueField;
            }
            set
            {
                this.CurrentValueField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PreviousValue
        {
            get
            {
                return this.PreviousValueField;
            }
            set
            {
                this.PreviousValueField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XPath
        {
            get
            {
                return this.XPathField;
            }
            set
            {
                this.XPathField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetObjectTypeV4", Namespace="http://ohiservice.umusic.net/types/v4")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.GetObjectV4))]
    public partial class GetObjectTypeV4 : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string BLC_ID_CREATORField;
        
        private string HIST_OBJ_TYPE_NAMEField;
        
        private string HIST_OBJ_TYPE_VERSIONField;
        
        private string LOGIN_NAMEField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLC_ID_CREATOR
        {
            get
            {
                return this.BLC_ID_CREATORField;
            }
            set
            {
                this.BLC_ID_CREATORField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HIST_OBJ_TYPE_NAME
        {
            get
            {
                return this.HIST_OBJ_TYPE_NAMEField;
            }
            set
            {
                this.HIST_OBJ_TYPE_NAMEField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HIST_OBJ_TYPE_VERSION
        {
            get
            {
                return this.HIST_OBJ_TYPE_VERSIONField;
            }
            set
            {
                this.HIST_OBJ_TYPE_VERSIONField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LOGIN_NAME
        {
            get
            {
                return this.LOGIN_NAMEField;
            }
            set
            {
                this.LOGIN_NAMEField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetObjectV4", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class GetObjectV4 : ohiservice.umusic.net.types.v4.GetObjectTypeV4
    {
        
        private string BLC_Deploy_EnvField;
        
        private string Object_ID_SourceField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLC_Deploy_Env
        {
            get
            {
                return this.BLC_Deploy_EnvField;
            }
            set
            {
                this.BLC_Deploy_EnvField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Object_ID_Source
        {
            get
            {
                return this.Object_ID_SourceField;
            }
            set
            {
                this.Object_ID_SourceField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HistObjTypeFull", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class HistObjTypeFull : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool AnA_Secure_IndField;
        
        private bool BEV_Publish_IndField;
        
        private string BLC_ID_CreatorField;
        
        private System.DateTime Expire_TimeField;
        
        private ohiservice.umusic.net.types.v4.History_Object_Event_Attributes[] HistObjEventAttrListField;
        
        private string Hist_Obj_Type_NameField;
        
        private string Hist_Obj_Type_VersionField;
        
        private long OHI_Hist_Obj_Type_IdField;
        
        private bool PubSub_MT_registered_IndField;
        
        private string PubSub_Publish_OptionField;
        
        private string XSDField;
        
        private string XSLField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool AnA_Secure_Ind
        {
            get
            {
                return this.AnA_Secure_IndField;
            }
            set
            {
                this.AnA_Secure_IndField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BEV_Publish_Ind
        {
            get
            {
                return this.BEV_Publish_IndField;
            }
            set
            {
                this.BEV_Publish_IndField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLC_ID_Creator
        {
            get
            {
                return this.BLC_ID_CreatorField;
            }
            set
            {
                this.BLC_ID_CreatorField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Expire_Time
        {
            get
            {
                return this.Expire_TimeField;
            }
            set
            {
                this.Expire_TimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ohiservice.umusic.net.types.v4.History_Object_Event_Attributes[] HistObjEventAttrList
        {
            get
            {
                return this.HistObjEventAttrListField;
            }
            set
            {
                this.HistObjEventAttrListField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hist_Obj_Type_Name
        {
            get
            {
                return this.Hist_Obj_Type_NameField;
            }
            set
            {
                this.Hist_Obj_Type_NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hist_Obj_Type_Version
        {
            get
            {
                return this.Hist_Obj_Type_VersionField;
            }
            set
            {
                this.Hist_Obj_Type_VersionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long OHI_Hist_Obj_Type_Id
        {
            get
            {
                return this.OHI_Hist_Obj_Type_IdField;
            }
            set
            {
                this.OHI_Hist_Obj_Type_IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool PubSub_MT_registered_Ind
        {
            get
            {
                return this.PubSub_MT_registered_IndField;
            }
            set
            {
                this.PubSub_MT_registered_IndField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PubSub_Publish_Option
        {
            get
            {
                return this.PubSub_Publish_OptionField;
            }
            set
            {
                this.PubSub_Publish_OptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XSD
        {
            get
            {
                return this.XSDField;
            }
            set
            {
                this.XSDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XSL
        {
            get
            {
                return this.XSLField;
            }
            set
            {
                this.XSLField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="History_Object_Event_Attributes", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class History_Object_Event_Attributes : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long ohi_bevpub_attr_idField;
        
        private string ohi_bevpub_attr_keyField;
        
        private string ohi_bevpub_attr_xpathField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ohi_bevpub_attr_id
        {
            get
            {
                return this.ohi_bevpub_attr_idField;
            }
            set
            {
                this.ohi_bevpub_attr_idField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ohi_bevpub_attr_key
        {
            get
            {
                return this.ohi_bevpub_attr_keyField;
            }
            set
            {
                this.ohi_bevpub_attr_keyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ohi_bevpub_attr_xpath
        {
            get
            {
                return this.ohi_bevpub_attr_xpathField;
            }
            set
            {
                this.ohi_bevpub_attr_xpathField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HistObj", Namespace="http://ohiservice.umusic.net/types/v4")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.OHIHistObj))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.HistObjDetail))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.HistObjectV4))]
    public partial class HistObj : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string BLC_Deploy_EnvField;
        
        private string BLC_ID_CreatorField;
        
        private System.DateTime Change_TimeField;
        
        private bool Deleted_IndField;
        
        private string Hist_Obj_Type_NameField;
        
        private string Login_NameField;
        
        private string Object_ID_SourceField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLC_Deploy_Env
        {
            get
            {
                return this.BLC_Deploy_EnvField;
            }
            set
            {
                this.BLC_Deploy_EnvField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BLC_ID_Creator
        {
            get
            {
                return this.BLC_ID_CreatorField;
            }
            set
            {
                this.BLC_ID_CreatorField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Change_Time
        {
            get
            {
                return this.Change_TimeField;
            }
            set
            {
                this.Change_TimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted_Ind
        {
            get
            {
                return this.Deleted_IndField;
            }
            set
            {
                this.Deleted_IndField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hist_Obj_Type_Name
        {
            get
            {
                return this.Hist_Obj_Type_NameField;
            }
            set
            {
                this.Hist_Obj_Type_NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Login_Name
        {
            get
            {
                return this.Login_NameField;
            }
            set
            {
                this.Login_NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Object_ID_Source
        {
            get
            {
                return this.Object_ID_SourceField;
            }
            set
            {
                this.Object_ID_SourceField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OHIHistObj", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class OHIHistObj : ohiservice.umusic.net.types.v4.HistObj
    {
        
        private long OHI_Hist_Obj_IDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long OHI_Hist_Obj_ID
        {
            get
            {
                return this.OHI_Hist_Obj_IDField;
            }
            set
            {
                this.OHI_Hist_Obj_IDField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HistObjDetail", Namespace="http://ohiservice.umusic.net/types/v4")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ohiservice.umusic.net.types.v4.HistObjectV4))]
    public partial class HistObjDetail : ohiservice.umusic.net.types.v4.HistObj
    {
        
        private string XMLField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XML
        {
            get
            {
                return this.XMLField;
            }
            set
            {
                this.XMLField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HistObjectV4", Namespace="http://ohiservice.umusic.net/types/v4")]
    public partial class HistObjectV4 : ohiservice.umusic.net.types.v4.HistObjDetail
    {
        
        private bool Archive_IndField;
        
        private System.DateTime Expire_TimeField;
        
        private string Hist_Obj_Type_VersionField;
        
        private string XSDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Archive_Ind
        {
            get
            {
                return this.Archive_IndField;
            }
            set
            {
                this.Archive_IndField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Expire_Time
        {
            get
            {
                return this.Expire_TimeField;
            }
            set
            {
                this.Expire_TimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hist_Obj_Type_Version
        {
            get
            {
                return this.Hist_Obj_Type_VersionField;
            }
            set
            {
                this.Hist_Obj_Type_VersionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XSD
        {
            get
            {
                return this.XSDField;
            }
            set
            {
                this.XSDField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IOHI")]
public interface IOHI
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/GetSecuredHistoryObjectDifferenceXML", ReplyAction="http://tempuri.org/IOHI/GetSecuredHistoryObjectDifferenceXMLResponse")]
    ohiservice.umusic.net.types.v4.RetHistDifResult GetSecuredHistoryObjectDifferenceXML(ohiservice.umusic.net.types.v4.GetHistObjDiv ohStruct);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/GetSecuredHistoryObjectTypeXML", ReplyAction="http://tempuri.org/IOHI/GetSecuredHistoryObjectTypeXMLResponse")]
    ohiservice.umusic.net.types.v4.HistObjTypeFull GetSecuredHistoryObjectTypeXML(ohiservice.umusic.net.types.v4.GetObjectTypeV4 ObjectHistoryTypeStruct);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/GetSecuredHistoryObjectXML", ReplyAction="http://tempuri.org/IOHI/GetSecuredHistoryObjectXMLResponse")]
    ohiservice.umusic.net.types.v4.HistObjectV4 GetSecuredHistoryObjectXML(ohiservice.umusic.net.types.v4.GetHistObjRV4 ohStruct);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/GetSecuredHistoryObjectListXML", ReplyAction="http://tempuri.org/IOHI/GetSecuredHistoryObjectListXMLResponse")]
    ohiservice.umusic.net.types.v4.OHIHistObj[] GetSecuredHistoryObjectListXML(ohiservice.umusic.net.types.v4.GetObjectV4 ohStruct);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/UpdateHistoryObjectV4XML", ReplyAction="http://tempuri.org/IOHI/UpdateHistoryObjectV4XMLResponse")]
    bool UpdateHistoryObjectV4XML(ohiservice.umusic.net.types.v4.HistObjectV4 ohStruct);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOHI/GetLatestSecuredHistoryObjectXML", ReplyAction="http://tempuri.org/IOHI/GetLatestSecuredHistoryObjectXMLResponse")]
    ohiservice.umusic.net.types.v4.HistObjectV4 GetLatestSecuredHistoryObjectXML(ohiservice.umusic.net.types.v4.GetObjectV4 ohStruct);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IOHIChannel : IOHI, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class OHIClient : System.ServiceModel.ClientBase<IOHI>, IOHI
{
    
    public OHIClient()
    {
    }
    
    public OHIClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public OHIClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public OHIClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public OHIClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public ohiservice.umusic.net.types.v4.RetHistDifResult GetSecuredHistoryObjectDifferenceXML(ohiservice.umusic.net.types.v4.GetHistObjDiv ohStruct)
    {
        return base.Channel.GetSecuredHistoryObjectDifferenceXML(ohStruct);
    }
    
    public ohiservice.umusic.net.types.v4.HistObjTypeFull GetSecuredHistoryObjectTypeXML(ohiservice.umusic.net.types.v4.GetObjectTypeV4 ObjectHistoryTypeStruct)
    {
        return base.Channel.GetSecuredHistoryObjectTypeXML(ObjectHistoryTypeStruct);
    }
    
    public ohiservice.umusic.net.types.v4.HistObjectV4 GetSecuredHistoryObjectXML(ohiservice.umusic.net.types.v4.GetHistObjRV4 ohStruct)
    {
        return base.Channel.GetSecuredHistoryObjectXML(ohStruct);
    }
    
    public ohiservice.umusic.net.types.v4.OHIHistObj[] GetSecuredHistoryObjectListXML(ohiservice.umusic.net.types.v4.GetObjectV4 ohStruct)
    {
        return base.Channel.GetSecuredHistoryObjectListXML(ohStruct);
    }
    
    public bool UpdateHistoryObjectV4XML(ohiservice.umusic.net.types.v4.HistObjectV4 ohStruct)
    {
        return base.Channel.UpdateHistoryObjectV4XML(ohStruct);
    }
    
    public ohiservice.umusic.net.types.v4.HistObjectV4 GetLatestSecuredHistoryObjectXML(ohiservice.umusic.net.types.v4.GetObjectV4 ohStruct)
    {
        return base.Channel.GetLatestSecuredHistoryObjectXML(ohStruct);
    }
}
