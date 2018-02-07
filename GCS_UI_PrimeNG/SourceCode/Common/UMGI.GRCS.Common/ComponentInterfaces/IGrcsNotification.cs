using UMGI.GRCS.BusinessEntities.Entities.NotificationEntities;

namespace UMGI.GRCS.Common.ComponentInterfaces
{
    public interface IGrcsNotification
    {
        /// <summary>
        /// Method to Push Rights/Clearance notification Details to MSMQ
        /// </summary>
        /// <param name="id">Primary Key of Entities</param>
        /// <param name="type">Type of Notification to be generated</param>
        /// <returns> true or false based on status of message pushed to MSMQ</returns>
        bool Notify(long id, NotificationType type);
    }
}