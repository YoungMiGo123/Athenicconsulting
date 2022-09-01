using Athenic.Notifications.Notifications.Models.Entities;

namespace Athenic.Notifications.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task<string> SendEmailAsync(SendEmailModel sendEmail);
        Task<string> SendTextMessage(SendTextMessage sendTextMessage);
    }

}
