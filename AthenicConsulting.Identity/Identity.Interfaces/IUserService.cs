using Athenic.Notifications.Notifications.Models.Entities;
using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AthenicConsulting.Identity.Identity.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> CreateUser(CreateAccountViewModel createAccountViewModel, RoleFlag roleFlag);
        Task<bool> CanSignIn(string email);
        Task<SignInResult> SignIn(SignInViewModel signInViewModel);
        Task SignOut();
        bool isUserSignedIn(ClaimsPrincipal claimsPrincipal);
        Task<bool> SendResetPasswordLink(SendEmailModel forgotPasswordViewModel);
        Task<bool> SendAccountCofirmationEmail(SendEmailModel accountConfirmation);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel);
        Task<IdentityResult> ConfirmEmail(string userId, string code);

    }
}
