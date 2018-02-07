using System.ServiceModel;

namespace GRCS.Utilities.ExceptionHandler
{
    /// <summary>
    /// Handler used to throw the exception from service to client.
    /// </summary>
    public class GRCSExceptionHandler
    {
        private const string _concurrencyMessage = "Concurrency Exception : This record has modified by another user {0}.Your changes cannot be saved";
        /// <summary>
        /// GRCSs the fault exception.
        /// </summary>
        /// <param name="faultCode">The fault code.</param>
        /// <param name="message">The message.</param>
        /// <param name="rightsException">The exception.</param>
        /// <returns></returns>
        public FaultException<ClearenceException> GRCSFaultException(string faultCode, string message, ClearenceException clearenceException)
        {
            return new FaultException<ClearenceException>(clearenceException, new FaultReason(message), new FaultCode(faultCode));
        }

        /// <summary>
        /// GRCSs the fault exception.
        /// </summary>
        /// <param name="faultCode">The fault code.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public FaultException GRCSFaultException(string faultCode, string message)
        {
            return new FaultException(new FaultReason(message), new FaultCode(faultCode));
        }

        public ClearenceException GRCSConcurrencyException(string errorCode, string userName)
        {
            return new ClearenceException(errorCode, string.Format(_concurrencyMessage, userName));
        }

    }

   
}
