﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMGI.GRCS.UI.Proxies.ProjectService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProjectService.IProject")]
    public interface IProject {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/Search", ReplyAction="http://tempuri.org/IProject/SearchResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectSearchResult Search(UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectSearchCriteria searchCriteria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/GetAssociatedRelease", ReplyAction="http://tempuri.org/IProject/GetAssociatedReleaseResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.ReleaseSearchResult GetAssociatedRelease(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/GetAssociatedResource", ReplyAction="http://tempuri.org/IProject/GetAssociatedResourceResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ResourceEntities.ResourceSearchResult GetAssociatedResource(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/GetAssociatedReleaseResource", ReplyAction="http://tempuri.org/IProject/GetAssociatedReleaseResourceResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ResourceEntities.ReleaseResourcesInfo GetAssociatedReleaseResource(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/IsAlreadyLinkedContract", ReplyAction="http://tempuri.org/IProject/IsAlreadyLinkedContractResponse")]
        bool IsAlreadyLinkedContract(long projectid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProject/GetprojectInfo", ReplyAction="http://tempuri.org/IProject/GetprojectInfoResponse")]
        UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectInfo GetprojectInfo(long projectId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProjectChannel : UMGI.GRCS.UI.Proxies.ProjectService.IProject, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProjectClient : System.ServiceModel.ClientBase<UMGI.GRCS.UI.Proxies.ProjectService.IProject>, UMGI.GRCS.UI.Proxies.ProjectService.IProject {
        
        public ProjectClient() {
        }
        
        public ProjectClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProjectClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProjectClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProjectClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectSearchResult Search(UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectSearchCriteria searchCriteria) {
            return base.Channel.Search(searchCriteria);
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.ReleaseSearchResult GetAssociatedRelease(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo) {
            return base.Channel.GetAssociatedRelease(linkProjectInfo);
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ResourceEntities.ResourceSearchResult GetAssociatedResource(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo) {
            return base.Channel.GetAssociatedResource(linkProjectInfo);
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ResourceEntities.ReleaseResourcesInfo GetAssociatedReleaseResource(UMGI.GRCS.BusinessEntities.Entities.ReleaseEntities.LinkedInfo linkProjectInfo) {
            return base.Channel.GetAssociatedReleaseResource(linkProjectInfo);
        }
        
        public bool IsAlreadyLinkedContract(long projectid) {
            return base.Channel.IsAlreadyLinkedContract(projectid);
        }
        
        public UMGI.GRCS.BusinessEntities.Entities.ProjectEntities.ProjectInfo GetprojectInfo(long projectId) {
            return base.Channel.GetprojectInfo(projectId);
        }
    }
}
