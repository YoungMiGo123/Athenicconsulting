namespace AthenicConsulting.Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public bool CanDisplayMessage => !string.IsNullOrEmpty(Message);
    }
}
