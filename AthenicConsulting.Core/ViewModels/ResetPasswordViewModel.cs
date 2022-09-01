namespace AthenicConsulting.Core.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
        public bool CanDisplayMessage => !string.IsNullOrEmpty(Message);
    }
}

