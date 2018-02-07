﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMGI.GRCS.UI.Proxies.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserService.IUser")]
    public interface IUser {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/GetGcsAuthorizationByLoginNameAndApplication", ReplyAction="http://tempuri.org/IUser/GetGcsAuthorizationByLoginNameAndApplicationResponse")]
        UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GcsAuthentication GetGcsAuthorizationByLoginNameAndApplication(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.UserInfo userInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/GetUsers", ReplyAction="http://tempuri.org/IUser/GetUsersResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.Workgroup))]
        System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.WorkGroupUser> GetUsers(UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.WorkGroupUserSearchCriteria userSearchCriteria, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.AnaTargetApplication targetApplication, string userLoginName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/GetAuthorizationByLoginNameAndApplication", ReplyAction="http://tempuri.org/IUser/GetAuthorizationByLoginNameAndApplicationResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.WorkGroupUser))]
        UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GrsAuthentication GetAuthorizationByLoginNameAndApplication(string userLoginName, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.AnaTargetApplication applicationName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserChannel : UMGI.GRCS.UI.Proxies.UserService.IUser, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserClient : System.ServiceModel.ClientBase<UMGI.GRCS.UI.Proxies.UserService.IUser>, UMGI.GRCS.UI.Proxies.UserService.IUser {
        
        public UserClient() {
        }
        
        public UserClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GcsAuthentication GetGcsAuthorizationByLoginNameAndApplication(UMGI.GRCS.BusinessEntities.Entities.BaseEntities.UserInfo userInfo) {
            return base.Channel.GetGcsAuthorizationByLoginNameAndApplication(userInfo);
        }
        
        public System.Collections.Generic.List<UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.WorkGroupUser> GetUsers(UMGI.GRCS.BusinessEntities.Entities.WorkgroupEntities.WorkGroupUserSearchCriteria userSearchCriteria, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.AnaTargetApplication targetApplication, string userLoginName) {
            return base.Channel.GetUsers(userSearchCriteria, targetApplication, userLoginName);
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.AnaEntities.GrsAuthentication GetAuthorizationByLoginNameAndApplication(string userLoginName, UMGI.GRCS.BusinessEntities.Entities.AnaEntities.AnaTargetApplication applicationName) {
            return base.Channel.GetAuthorizationByLoginNameAndApplication(userLoginName, applicationName);
        }
    }
}
