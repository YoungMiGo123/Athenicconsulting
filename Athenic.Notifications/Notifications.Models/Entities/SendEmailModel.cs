namespace Athenic.Notifications.Notifications.Models.Entities
{
    public class SendEmailModel
    {
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }
    }
}
