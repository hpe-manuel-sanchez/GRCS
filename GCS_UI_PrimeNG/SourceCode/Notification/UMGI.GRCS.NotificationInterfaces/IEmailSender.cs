namespace UMGI.GRCS.NotificationInterfaces
{
    /// <summary>
    /// Operations for sending email
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Represents the method that is used to mail contents to MSMQ
        /// </summary>
        /// <param name="emailMessage">Email content to be sent to the queue</param>
        /// <returns>true if success false on failure/exception</returns>
        bool PushNotificationToMSMQ(Email emailMessage);

        /// <summary>
        /// Represents the method to send Emails on Failure
        /// </summary>
        /// <param name="emailBody">The email body to be used to send out the mail</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="fromAddress">From email address</param>
        void SendInBackGround(string emailBody, string subject = "Alert! Background process failure!", string fromAddress = "grcs-noreply@umusic.com");
    }
}