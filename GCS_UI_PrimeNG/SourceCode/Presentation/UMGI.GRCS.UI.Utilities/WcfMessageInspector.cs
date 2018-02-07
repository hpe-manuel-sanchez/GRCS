/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : WcfMessageInspector.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 21-12-2012 
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

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web.Mvc;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Utilities
{
    /// <summary>
    /// Custom Client Message Inspector
    /// </summary>
    public class WcfMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            // Implement this method to inspect/modify messages after a message
            // is received but prior to passing it back to the client 
            //Console.WriteLine("AfterReceiveReply called");
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The  client object channel.</param>
        /// <returns>
        /// The object that is returned as the <paramref name="correlationState "/>argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/> to ensure that no two <paramref name="correlationState"/> objects are the same.
        /// </returns>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            // Implement this method to inspect/modify messages before they 
            // are sent to the service            
            var sessionWrapper = DependencyResolver.Current.GetService<ISessionWrapper>();
            var header= MessageHeader.CreateHeader(Constants.UserName,Constants.NameSpace,sessionWrapper.CurrentUserInfo.UserLoginName);
            request.Headers.Add(header);

            var encodingHeader = MessageHeader.CreateHeader(Constants.AcceptEncoding, Constants.NameSpace, Constants.Compression);
            request.Headers.Add(encodingHeader);
            
            return null;
        }
    }
}

