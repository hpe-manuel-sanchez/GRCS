using System;
using System.Diagnostics;
using System.ServiceModel.Dispatcher;

/* *************************************************************************** 
 * Copyrights ® 2012 Universal Musical Group 
 * *************************************************************************** 
 * File Name      : ParameterInspector.cs
 * Project Code   : UMG-GRCS(C/115921) 
 * Author         : Satheesh Gopal
 * Created on     : 26-04-2013 
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

namespace UMGI.GRCS.UI.Utilities
{
    public class ParameterInspector:IParameterInspector
    {
        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                var message = String.Format("WCF Method Start:{0}:{1}", operationName, DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.ffffff"));
                Trace.WriteLine(message, "Service Method Timing");
                Trace.Flush();
            }
            catch (Exception)
            {
                //No throw
            }
            return null;
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            try
            {
                var message = String.Format("WCF Method End:{0}:{1}", operationName, DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss.ffffff"));
                Trace.WriteLine(message, "Service Method Timing");
                Trace.Flush();
            }
            catch (Exception)
            {
                //No throw
            }
        }
    }
}
