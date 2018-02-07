using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace UMGI.GRCS.UI.Rights.Models
{
    public class ManagerBase
    {
      
       public ManagerBase()   
       {
         
       }

       public T GetService<T>() where T : class
       {
           try
           {
               var binding = ConfigurationManager.AppSettings["binding"];
               bool IsSecurityEnabled = bool.Parse(ConfigurationManager.AppSettings["IsSecured"]);
               var timeOut = int.Parse(ConfigurationManager.AppSettings["ServiceTimeOut"]);
               return (T)GetClientChannel<T>(ConfigurationManager.AppSettings["ContractService"], binding, IsSecurityEnabled,timeOut);               
           }
           catch (Exception)
           {
               throw;
           }
       }

       private IClientChannel GetClientChannel<T>(string serviceUri, string binding, bool IsSecurityEnabled, int timeOut)
       {
           return (IClientChannel)CreateChannelFactory<T>(serviceUri, binding, IsSecurityEnabled,timeOut).CreateChannel();
       }

       private ChannelFactory<T> CreateChannelFactory<T>(string url,string bindingType, bool IsSecurityEnabled, int timeOut)
       {
           BasicHttpBinding binding;
           //Basic Http
           if (bindingType == "BasicHttp")
           {
               if (IsSecurityEnabled)
                   binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
               else
                   binding = new BasicHttpBinding();

               binding.MaxReceivedMessageSize = 2147483647;
               binding.MaxBufferSize = 2147483647;
               binding.OpenTimeout = new TimeSpan(0, timeOut, 0);
               binding.CloseTimeout = new TimeSpan(0, timeOut, 0);
               binding.ReceiveTimeout = new TimeSpan(0, timeOut, 0);
               binding.SendTimeout = new TimeSpan(0, timeOut, 0);

               return new ChannelFactory<T>(binding, new EndpointAddress(url));
           }
           else if (bindingType == "NetTcp")//Net tcp
           {

               var netTcpBinding = new NetTcpBinding
               {
                   ReceiveTimeout = new TimeSpan(0, timeOut, 0),
                   CloseTimeout = new TimeSpan(0, timeOut, 0),
                   MaxReceivedMessageSize = int.MaxValue                   
               };

               //netTcpBinding.MaxBufferSize = bindingConfig.MaxBufferSize;
               //netTcpBinding.MaxReceivedMessageSize = bindingConfig.MaxRecievedMessageSize;
               //netTcpBinding.MaxBufferPoolSize = bindingConfig.MaxBufferPoolSize;
               //netTcpBinding.PortSharingEnabled = bindingConfig.PortSharing;
               //netTcpBinding.ReaderQuotas = GetReaderQuotas();
               //netTcpBinding.MaxConnections = bindingConfig.MaxConnection;
               //netTcpBinding.ListenBacklog = bindingConfig.ListenBacklog;
                              
               if (IsSecurityEnabled)
               {
                   NetTcpSecurity security = new NetTcpSecurity();
                   security.Mode = SecurityMode.Transport;
                   security.Transport = new TcpTransportSecurity() { ClientCredentialType = TcpClientCredentialType.Windows };
                   netTcpBinding.Security = security;
               }

               return new ChannelFactory<T>(netTcpBinding, new EndpointAddress(url));
           
           }

           return new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress(url));

           
       }  

    }
}