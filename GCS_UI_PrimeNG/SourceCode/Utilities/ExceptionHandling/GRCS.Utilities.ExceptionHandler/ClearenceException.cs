using System;
using System.Runtime.Serialization;

namespace GRCS.Utilities.ExceptionHandler
{
    [Serializable]
    public class ClearenceException : Exception
    {
        #region Builders

        public ClearenceException()
        {
        }

        public ClearenceException(string message)
        {
            ErrorMessage = message;
        }

        public ClearenceException(string errorCode, string message)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
        }

        public ClearenceException(string message, Exception exception)
        {
            ErrorCode = "0000";
            ErrorMessage = message;
            ErrorDetails = exception;
        }

        public ClearenceException(string errorCode, string message, Exception exception)
            : base(message, exception)
        {
            ErrorCode = errorCode;
            ErrorMessage = message;
            ErrorDetails = exception;
        }

        #endregion Builders

        public string ErrorCode { get; private set; }
        private string ErrorMessage { get; set; }
        public Exception ErrorDetails { get; private set; }

        public override string Message
        {
            get { return ErrorMessage; }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (!string.IsNullOrWhiteSpace(this.ErrorCode)) info.AddValue("ErrorCode", this.ErrorCode);
            if (!string.IsNullOrWhiteSpace(this.ErrorMessage)) info.AddValue("ErrorMessage", this.ErrorMessage);
            base.GetObjectData(info, context);
        }
    }
}