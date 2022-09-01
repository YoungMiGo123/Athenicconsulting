using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AthenicConsulting.Core.Core.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<ApplicationUser> GetApplicationUserById(string id);
        Task<ApplicationUser> GetApplicationUserByEmail(string email);
        Task<bool> UpdateApplicationUserAsync(ApplicationUser user);
        Task<bool> CanSignIn(ApplicationUser applicationUser);
        Task<bool> CanSignIn(string email);
        Task<SignInResult> SignIn(SignInViewModel signInViewModel);
        Task SignOut();
        List<ApplicationUser> GetAllUsers(int? count = null);
        Task<string> GetUserId(string email);
        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser applicationUser, RoleFlag roleFlag);
        Task<IdentityResult> CreateUser(CreateAccountViewModel applicationUser);
        Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser applicationUser, RoleFlag roleFlag);
        Task<bool> IsUserInRole(ApplicationUser applicationUser, RoleFlag roleFlag);
        Task<string> GetResetPasswordCallbackUrl(ForgotPasswordViewModel forgotPasswordViewModel);
        Task<string> GetAccountConfirmationLink(string email);
        bool IsSignIn(ClaimsPrincipal user);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
    }
}
