using Athenic.Notifications.Notifications.Models.Entities;
using AthenicConsulting.Core.Core.Interfaces.Repositories;
using AthenicConsulting.Core.Core.Interfaces.Settings;
using AthenicConsulting.Core.ViewModels;
using AthenicConsulting.Identity.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AthenicConsulting.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private ILogger<AccountController> _logger;

        public IAthenicSettings _settings { get; }

        public AccountController(IUserService userService, ILogger<AccountController> logger, IAthenicSettings  settings)
        {
            _userService = userService;
            _logger = logger;
            _settings = settings;
        }
        public IActionResult Login()
        {
            return View(new SignInViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInViewModel signInViewModel)
        {
            try
            {
                var canSignIn = await _userService.CanSignIn(signInViewModel.Email);
                if (canSignIn)
                {
                    var result = await _userService.SignIn(signInViewModel);
                    if (result.Succeeded)
                    {
                        return Redirect($"{_settings.AthenicConsultingUrl}/Office/Index");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred during logging into account = {ex}");
            }
            return View(signInViewModel);
        }
        public IActionResult Logout()
        {
            try
            {
                if (_userService.isUserSignedIn(User))
                {
                    _userService.SignOut();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"An exception occurred during logging out of account = {ex}");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CreateAccount()
        {
            var vm = new CreateAccountViewModel();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel createAccountViewModel)
        {
            try
            {
                var user = await _userService.CreateUser(createAccountViewModel, RoleFlag.Admin);
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    var sent = await _userService.SendAccountCofirmationEmail(new SendEmailModel
                    {
                        ToEmail = user.Email,
                        Subject = "Please confirm your athenic consulting account",
                    });
                    if (sent)
                    {
                        return RedirectToAction("Index", "Office");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"An exception occurred during create account = {ex}");
            }
           
            return View(createAccountViewModel);
        }
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(forgotPasswordViewModel.Email))
                {
                    var sent = await _userService.SendResetPasswordLink(new SendEmailModel
                    {
                        Subject = "Please use the below link to reset your password",
                        ToEmail = forgotPasswordViewModel.Email
                    });
                    forgotPasswordViewModel.Message = sent
                        ? $"We have sucessfully sent a reset password link to the provided email = {forgotPasswordViewModel.Email}, please check your email"
                        : $"Something went wrong during the process of sending you the password reset link to email = {forgotPasswordViewModel.Email}, please ensure the email is valid and try again";

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred during forgot password = {ex}");
            }
           
            return View(forgotPasswordViewModel);
        }
        public IActionResult ResetPassword(string UserId, string Code)
        {
            Code = HttpUtility.UrlDecode(Code); 
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Code))
            {
                return RedirectToAction("ForgotPassword");
            }
            return View(new ResetPasswordViewModel
            {
                UserId = UserId,
                Code = Code
            });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            try
            {
                var result = await _userService.ResetPassword(resetPasswordViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    resetPasswordViewModel.Message = result?.Errors?.FirstOrDefault()?.Description ?? string.Empty;
                    return View(resetPasswordViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred during reset password = {ex}");
            }

            return RedirectToAction("ForgotPassword");
        }
        public async Task<IActionResult> ConfirmAccount(string UserId, string Code)
        {
            try
            {
                await _userService.ConfirmEmail(UserId, Code);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred during confirm account = {ex}");
            }
            return RedirectToAction("Index", "Office");
        }
    }
}
