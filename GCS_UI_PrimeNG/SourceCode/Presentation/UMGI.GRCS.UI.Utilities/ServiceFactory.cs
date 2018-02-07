/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ServiceFactory.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 10-07-2012 
 * Reference      : 
 * *************************************************************************** 
 * Modification History 
 * *************************************************************************** 
 * Modified by                   Modified on                       Remarks 
 *
 * ***************************************************************************
 * Reviewed by                 Modified on                       Remarks 
 *
*************************************************************************** */

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;


namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Class for accessing WCF service dyanamically
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        private ILogFactory _logFactory { get; set; }

        private IConfigFactory _configFactory { get; set; }

        //private static Dictionary<string, object> _serviceChannels{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFactory"/> class.
        /// </summary>
        public ServiceFactory(ILogFactory loggerFactory, IConfigFactory configFactory)
        {
            _logFactory = loggerFactory;
            _configFactory = configFactory;
            //if (_serviceChannels == null)
            //{
            //    _serviceChannels = new Dictionary<string, object>();
            //}
        }

        /// <summary>
        /// Returns the client channel of the required service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
        public T GetService<T>(string serviceName) where T : class
        {
            try
            {
                _logFactory.LogWriter.Debug(string.Format("Service Name:{0}", serviceName));

                string serviceUri = ""; //Dont changed to string.empty(BugID-1570)

                switch (serviceName)
                {
                    case Constants.ContractService:
                        serviceUri = _configFactory.ContractService;
                        break;
                    case Constants.ArtistService:
                        serviceUri = _configFactory.ArtistService;
                        break;
                    case Constants.UserService:
                        serviceUri = _configFactory.UserService;
                        break;
                    case Constants.ProjectService:
                        serviceUri = _configFactory.ProjectService;
                        break;
                    case Constants.RightsService:
                        serviceUri = _configFactory.RightsService;
                        break;
                    case Constants.ReleaseService:
                        serviceUri = _configFactory.ReleaseService;
                        break;
                    case Constants.ResourceService:
                        serviceUri = _configFactory.ResourceService;
                        break;
                    case Constants.GrcsUtilityService:
                        serviceUri = _configFactory.GrcsUtilityService;
                        break;
                    case Constants.WorkQueueService:
                        serviceUri = _configFactory.WorkQueueService;
                        break;
                    case Constants.GlobalService:
                        serviceUri = _configFactory.GlobalService;
                        break;
                    case Constants.RepertoireService:
                        serviceUri = _configFactory.RepertoireService;
                        break;
                    case Constants.PCompanyService:
                        serviceUri = _configFactory.PCompanyService;
                        break;
                    case Constants.ReportService:
                        serviceUri = _configFactory.ReportService;
                        break;
                    case Constants.RoutingService:
                        serviceUri = _configFactory.RoutingService;
                        break;
                }

                _logFactory.LogWriter.Debug(string.Format("Service Name:{0}", serviceName));
                //if(_serviceChannels.ContainsKey(serviceName))
                // {
                //     return (T)_serviceChannels[serviceName];
                // }

                IClientChannel clientChannel;

                if (_configFactory.IsBindingInConfig)
                {
                    clientChannel = GetClientChannel<T>(serviceUri, _configFactory.Binding);
                }
                else
                {
                    clientChannel = GetClientChannel<T>(serviceUri, _configFactory.Binding,
                                                        _configFactory.IsSecurityEnabled, _configFactory.TimeOut);
                }

                clientChannel.Faulted += new EventHandler(ClientChannel_Faulted);

                //_serviceChannels.Add(serviceName, clientChannel);

                return (T) clientChannel;
            }
            catch (Exception ex)
            {
                _logFactory.LogWriter.Error(Category.UI, ex);
                throw;
            }
        }

        /// <summary>
        /// Handles the Faulted event of the clientChannel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void ClientChannel_Faulted(object sender, EventArgs e)
        {
            ((ICommunicationObject) sender).Abort();

            //var channel = _serviceChannels.First(item => item.Value == sender);
            //if (_serviceChannels.ContainsKey(channel.Key))
            //{
            //    _serviceChannels.Remove(channel.Key);
            //}
        }

        /// <summary>
        /// Gets the client channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceUri">The service URI.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="isSecurityEnabled">if set to <c>true</c> [is security enabled].</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns></returns>
        private IClientChannel GetClientChannel<T>(string serviceUri, string binding, bool isSecurityEnabled,
                                                   int timeOut)
        {
            return
                (IClientChannel)
                CreateChannelFactory<T>(serviceUri, binding, isSecurityEnabled, timeOut).CreateChannel();
        }

        /// <summary>
        /// Creates the channel factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="bindingType">Type of the binding.</param>
        /// <param name="isSecurityEnabled">if set to <c>true</c> [is security enabled].</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns></returns>
        private ChannelFactory<T> CreateChannelFactory<T>(string url, string bindingType, bool isSecurityEnabled,
                                                          int timeOut)
        {
            //Basic Http
            if (bindingType == Constants.BasicHttp)
            {
                BasicHttpBinding binding = isSecurityEnabled
                                               ? new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                                               : new BasicHttpBinding();

                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.MaxBufferSize = int.MaxValue;
                binding.MaxBufferPoolSize = int.MaxValue;
                binding.OpenTimeout = new TimeSpan(0, timeOut, 0);
                binding.CloseTimeout = new TimeSpan(0, timeOut, 0);
                binding.ReceiveTimeout = new TimeSpan(0, timeOut, 0);
                binding.SendTimeout = new TimeSpan(0, timeOut, 0);
                binding.ReaderQuotas = GetReaderQuotas();

                var cf = new ChannelFactory<T>(binding, new EndpointAddress(url));
                foreach (OperationDescription op in cf.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }

                //cf.Endpoint.Behaviors.Add(new WcfEndpointBehavior());
                return cf;
            }
            if (bindingType == Constants.NetTcp) //Net tcp
            {

                var netTcpBinding = new NetTcpBinding
                    {
                        ReceiveTimeout = new TimeSpan(0, timeOut, 0),
                        CloseTimeout = new TimeSpan(0, timeOut, 0),
                        MaxReceivedMessageSize = int.MaxValue,
                        ReaderQuotas = GetReaderQuotas()
                    };

                if (isSecurityEnabled)
                {
                    var security = new NetTcpSecurity
                        {
                            Mode = SecurityMode.Transport,
                            Transport =
                                new TcpTransportSecurity {ClientCredentialType = TcpClientCredentialType.Windows}
                        };
                    netTcpBinding.Security = security;
                }

                netTcpBinding.MaxReceivedMessageSize = int.MaxValue;
                netTcpBinding.MaxBufferSize = int.MaxValue;
                netTcpBinding.MaxBufferPoolSize = int.MaxValue;
                netTcpBinding.OpenTimeout = new TimeSpan(0, timeOut, 0);
                netTcpBinding.CloseTimeout = new TimeSpan(0, timeOut, 0);
                netTcpBinding.ReceiveTimeout = new TimeSpan(0, timeOut, 0);
                netTcpBinding.SendTimeout = new TimeSpan(0, timeOut, 0);
                netTcpBinding.ReaderQuotas = GetReaderQuotas();

                var cf = new ChannelFactory<T>(netTcpBinding, new EndpointAddress(url));

                foreach (OperationDescription op in cf.Endpoint.Contract.Operations)
                {
                    var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                    if (dataContractBehavior != null)
                    {
                        dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                    }
                }

                //cf.Endpoint.Behaviors.Add(new WcfEndpointBehavior());
                return cf;
            }

            return new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress(url));

        }

        /// <summary>
        /// Gets the reader quotas.
        /// </summary>
        /// <returns></returns>
        private XmlDictionaryReaderQuotas GetReaderQuotas()
        {
            var readerQuotas = new XmlDictionaryReaderQuotas
                {
                    MaxStringContentLength = int.MaxValue,
                    MaxArrayLength = int.MaxValue,
                    MaxBytesPerRead = int.MaxValue,
                    MaxDepth = int.MaxValue,
                    MaxNameTableCharCount = int.MaxValue
                };
            return readerQuotas;
        }

        /// <summary>
        /// Gets the client channel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceUri">The service URI.</param>
        /// <param name="bindingType">Type of the binding.</param>
        /// <returns></returns>
        private IClientChannel GetClientChannel<T>(string serviceUri, string bindingType)
        {
            return (IClientChannel) CreateChannelFactory<T>(serviceUri, bindingType).CreateChannel();
        }

        /// <summary>
        /// Creates the channel factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="bindingType">Type of the binding.</param>
        /// <returns></returns>
        private ChannelFactory<T> CreateChannelFactory<T>(string url, string bindingType)
        {
            //Basic Http
            if (bindingType == Constants.BasicHttp)
            {
                var channelFactory = new ChannelFactory<T>(new BasicHttpBinding(Constants.BasicHttpBindingName),
                                                           new EndpointAddress(url));
                channelFactory = AddEndPointBehavior<T>(channelFactory);
                return channelFactory;
            }
            
            if (bindingType == Constants.NetTcp)//Net Tcp
            {
                var channelFactory = new ChannelFactory<T>(new NetTcpBinding(Constants.NetTcpBindingName), new EndpointAddress(url));
                channelFactory = AddEndPointBehavior<T>(channelFactory);
                return channelFactory;
            }
            return new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress(url));
        }

        /// <summary>
        /// Adds the end point behavior.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channelFactory">The channel factory.</param>
        /// <returns></returns>
        private ChannelFactory<T> AddEndPointBehavior<T>(ChannelFactory channelFactory)
        {
            foreach (var op in channelFactory.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }

            return (ChannelFactory<T>) channelFactory;
        }
    }
}
