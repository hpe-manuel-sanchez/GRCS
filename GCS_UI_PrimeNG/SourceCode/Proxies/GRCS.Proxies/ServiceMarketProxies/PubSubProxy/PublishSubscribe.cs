﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace="http://itga.umusic.com/servicemarket/publishermanager/v1", ConfigurationName="PublishSubscribeSoap")]
public interface PublishSubscribeSoap
{
    
    // CODEGEN: Generating message contract since element name xDoc from namespace http://itga.umusic.com/servicemarket/publishermanager/v1 is not marked nillable
    [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://itga.umusic.com/servicemarket/publishermanager/v1/Publisher/Publish")]
    void Publish(Publish request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class Publish
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="Publish", Namespace="http://itga.umusic.com/servicemarket/publishermanager/v1", Order=0)]
    public PublishBody Body;
    
    public Publish()
    {
    }
    
    public Publish(PublishBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute(Namespace="http://itga.umusic.com/servicemarket/publishermanager/v1")]
public partial class PublishBody
{
    
    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
    public System.Xml.XmlElement xDoc;
    
    public PublishBody()
    {
    }
    
    public PublishBody(System.Xml.XmlElement xDoc)
    {
        this.xDoc = xDoc;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface PublishSubscribeSoapChannel : PublishSubscribeSoap, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class PublishSubscribeSoapClient : System.ServiceModel.ClientBase<PublishSubscribeSoap>, PublishSubscribeSoap
{
    
    public PublishSubscribeSoapClient()
    {
    }
    
    public PublishSubscribeSoapClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public PublishSubscribeSoapClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public PublishSubscribeSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public PublishSubscribeSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    void PublishSubscribeSoap.Publish(Publish request)
    {
        base.Channel.Publish(request);
    }
    
    public void Publish(System.Xml.XmlElement xDoc)
    {
        Publish inValue = new Publish();
        inValue.Body = new PublishBody();
        inValue.Body.xDoc = xDoc;
        ((PublishSubscribeSoap)(this)).Publish(inValue);
    }
}
