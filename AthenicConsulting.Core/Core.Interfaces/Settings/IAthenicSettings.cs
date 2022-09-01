namespace AthenicConsulting.Core.Core.Interfaces.Settings
{
    public interface IAthenicSettings
    {
        public string ConnectionString { get; set; }
        public string AthenicConsultingUrl { get; set; }
        public string EmailApiKey { get; set; }
        public string SMSTokenId { get; set; }
        public string SMSTokenSecret { get; set; }
        public string SMSBasicAuth { get; set; }
        public string SMSUserName { get; set; }
        public string SMSPassword { get; set; }
        public string FromEmail { get; set; }
    }
}
