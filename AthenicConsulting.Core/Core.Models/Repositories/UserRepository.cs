using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Core.Interfaces.Settings;
using AthenicConsulting.Core.Data.Contexts;
using AthenicConsulting.Core.Data.Entity;
using AthenicConsulting.Core.Utitlies;
using AthenicConsulting.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Web;

namespace AthenicConsulting.Core.Core.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AthenicConsultingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAthenicSettings _athenicSettings;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AthenicConsultingContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAthenicSettings athenicSettings, ILogger<UserRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _athenicSettings = athenicSettings;
            _logger = logger;
        }
        public async Task<bool> CanSignIn(ApplicationUser applicationUser)
        {
            var canSignIn = await _signInManager.CanSignInAsync(applicationUser);
            return canSignIn;
        }
        public async Task<bool> CanSignIn(string email)
        {
            var user = await GetApplicationUserByEmail(email);
            var canSignIn = await _signInManager.CanSignInAsync(user);
            return canSignIn;
        }
        public async Task<SignInResult> SignIn(SignInViewModel signInViewModel)
        {
            var user = await GetApplicationUserByEmail(signInViewModel.Email);
            var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, signInViewModel.LockOut);
            return result;
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        public bool IsSignIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }
        public List<ApplicationUser> GetAllUsers(int? count = null)
        {
            if (count == null)
            {
                return _context.ApplicationUsers.ToList();
            }
            return _context.ApplicationUsers.Take(count.Value).ToList();
        }
        public async Task<string> GetUserId(string email)
        {
            var user = await GetApplicationUserByEmail(email);
            return user.Id;
        }
        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<ApplicationUser> GetApplicationUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }


        public async Task<bool> UpdateApplicationUserAsync(ApplicationUser user)
        {
            var updatedUser = await _userManager.UpdateAsync(user);
            return updatedUser.Succeeded;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser applicationUser, RoleFlag roleFlag)
        {
            var result = await _userManager.AddToRoleAsync(applicationUser, roleFlag.GetDisplayName());
            return result;
        }
        public async Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser applicationUser, RoleFlag roleFlag)
        {
            var result = await _userManager.RemoveFromRoleAsync(applicationUser, roleFlag.GetDisplayName());
            return result;
        }
        public async Task<bool> IsUserInRole(ApplicationUser applicationUser, RoleFlag roleFlag)
        {
            var isInRole = await _userManager.IsInRoleAsync(applicationUser, roleFlag.GetDisplayName());
            return isInRole;
        }
        public async Task<IdentityResult> CreateUser(CreateAccountViewModel createAccountViewModel)
        {
            var user = new ApplicationUser
            {
                Email = createAccountViewModel.Email,
                UserName = createAccountViewModel.Email,
                NormalizedEmail = createAccountViewModel.Email.ToUpper(),
                EmailConfirmed = false,
                NormalizedUserName = createAccountViewModel.Email.ToLower(),
            };
            var result = await _userManager.CreateAsync(user, createAccountViewModel.Password);
            return result;
        }
        public async Task<string> GetResetPasswordCallbackUrl(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var user = await GetApplicationUserByEmail(forgotPasswordViewModel.Email);

            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encoded = HttpUtility.UrlEncode(code);
                var callbackUrl = $"{_athenicSettings.AthenicConsultingUrl}/Account/ResetPassword?UserId={user.Id}&Code={encoded}";
                return callbackUrl;

            }
            return string.Empty;
        }
        public async Task<string> GetAccountConfirmationLink(string email)
        {
            var link = string.Empty;
            try
            {
                var user = await GetApplicationUserByEmail(email);
                if (user != null)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    link = $"{_athenicSettings.AthenicConsultingUrl}/Account/ConfirmAccount?UserId={user.Id}&Code={token}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception happened {ex}");
            }
            return link;
        }
        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var user = await GetApplicationUserById(userId);
            var confirmedEmail = await _userManager.ConfirmEmailAsync(user, code);
            return confirmedEmail;
        }
        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var user = await GetApplicationUserById(resetPasswordViewModel.UserId);

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);
            return result;
        }
    }
}
