using UMGI.GRCS.BusinessEntities.Lookups;

namespace UMGI.GRCS.NotificationInterfaces
{
    /// <summary>
    /// Represents the interface that is used to to generate email template
    /// Notification body is taken from external XML file pointed in Configuration App Setting (NOTIFICATION_MAIL_TEMPLATE)
    /// </summary>
    public interface ITemplateGenerator
    {

        /// <summary>
        /// Method that generates mail content as in the template
        /// Template file is pointed by app.setting under NOTIFICATION_MAIL_TEMPLATE key
        /// </summary>
        /// <typeparam name="T">object used as source for replacements of entries in template</typeparam>
        /// <param name="type">instance of the notification type</param>
        /// <param name="typed">instance of object contents</param>
        /// <param name="emailTo">email id's of the recipients</param>
        /// <returns>Email object</returns>
        Email GenerateEmailContent<T>(NotificationType type,T typed, string emailTo);
    }
}
