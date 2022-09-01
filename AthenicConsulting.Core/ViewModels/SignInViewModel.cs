namespace AthenicConsulting.Core.ViewModels
{
    public class SignInViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }    
        public bool RememberMe { get; set; }    
        public bool LockOut { get; set; } = false;
    }
}
