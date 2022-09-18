using AthenicConsulting.Core.Core.Interfaces.Repositories;

namespace AthenicConsulting.Core.ViewModels
{
    public class CreateAccountViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }    
        public bool RememberMe { get; set; }
        public RoleFlag? RoleFlag { get; set; }
    }
}
