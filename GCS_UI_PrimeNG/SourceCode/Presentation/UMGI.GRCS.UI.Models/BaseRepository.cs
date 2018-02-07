
using System;
using System.ServiceModel;
using UMGI.GRCS.Core.Utilities.logger;
using UMGI.GRCS.UI.Interfaces;

namespace UMGI.GRCS.UI.Models
{
   public class BaseRepository
    {
       public ILogFactory LoggerFactory { get; set; }

       /// <summary>
       /// Closes the service.
       /// </summary>
       /// <param name="service">The service.</param>
       protected void CloseService(object service)
       {
           try
           {
               if (((IClientChannel)service).State == CommunicationState.Opened)
               {
                   ((IClientChannel)service).Close();
               }
           }
           catch (Exception ex)
           {
               LoggerFactory.LogWriter.Error(Category.UI, string.Format(Constants.MethodException, "CloseService"), ex);                
           }
       }
    }
}
