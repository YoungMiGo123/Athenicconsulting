using Athenic.Notifications.Notifications.Interfaces;
using Athenic.Notifications.Notifications.Models.Entities;
using AthenicConsulting.Core.Core.Interfaces.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Athenic.Notifications.Notifications.Models.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAthenicSettings _athenicSettings;
        private readonly ILogger _logger;
        public NotificationService(IAthenicSettings athenicSettings, ILogger<NotificationService> logger)
        {
            _athenicSettings = athenicSettings;
            _logger = logger;
        }
        public async Task<string> SendEmailAsync(SendEmailModel sendEmail)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.mailbaby.net/mail/send"),
                Headers =
                {
                    { "X-API-KEY", _athenicSettings.EmailApiKey },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "to", sendEmail.ToEmail },
                    { "from", sendEmail.FromEmail},
                    { "subject", sendEmail.Subject },
                    { "body", sendEmail.Body },
                }),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var bodydata = await response.Content.ReadAsStringAsync();
                return bodydata;
            }
        }

        public async Task<string> SendTextMessage(SendTextMessage sendTextMessage)
        {
     
            string myURI = "https://api.bulksms.com/v1/messages";
            var sendMessageObject = JsonConvert.SerializeObject(new { to = sendTextMessage.ToPhoneNumber, body = sendTextMessage.Body});
            var request = WebRequest.Create(myURI);
            request.Credentials = new NetworkCredential(_athenicSettings.SMSUserName, _athenicSettings.SMSPassword);
            request.PreAuthenticate = true;
            request.Method = "POST";
            request.ContentType = "application/json";
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(sendMessageObject);
            var stream = request.GetRequestStream();
            stream.Write(encodedData, 0, encodedData.Length);
            stream.Close();
            try
            {
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var result = await reader.ReadToEndAsync();
                return result;
            }
            catch (WebException ex)
            {
                var reader = new StreamReader(ex.Response.GetResponseStream());
                var result = "Error details:" + reader.ReadToEnd();
                _logger.LogError($"An error occurred while sending a message: {ex}\n\n\nDetailed exception: {result}");
                return result;
            }
        }
    }
}
