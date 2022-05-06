
using Application.Common.Contracts;
using Application.Common.Contracts.Infrastructure;
using Common.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Identity.Models
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, 
            ITokenService tokenService, 
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<Result> RegisterAsync(string email, string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var result = await _userManager.CreateAsync(newUser, password);

                return result.Succeeded ? Result.Success() : Result.Fail(result.Errors.FirstOrDefault()?.Description ?? string.Empty);
            }

            //TODO: localization
            return Result.Fail("User already exist!");
        }

        public async Task<Result> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Result.Fail("User does not exist!");                
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                return await _tokenService.GetToken(user.Id);
            }

            //TODO: localization
            return Result.Fail("Wrong password!");
        }

        public async Task<Result> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return Result.Fail("User not found.");
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded ? Result.Success() : Result.Fail(result.Failure?.FailureReasons?.FirstOrDefault()?.Message ?? string.Empty);
        }

        public async Task<Result<string>> GetUserIdAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Result<string>.Fail(null, "User not found.");
            }

            return Result<string>.Success(user.Id);

        }

        public async Task<Result> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Fail("User not found.");
            }
            
            var appUrl = _configuration.GetValue<string>("App:Url");
            if(appUrl == null)
            {
                return Result.Fail("Url not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string url = $"{appUrl}?email={email}&token={token}";

            var result = await _emailService.SendEmail(new Application.Common.Models.Mail.Email
            {
                To = email,
                Subject = "Reset password",
                Body = $"<p>To reset your password <a href='{url}'>Click here!</a></p>"
            });

            if (result)
            {
                return Result.Success("Reset password URL has been sent to the email successfully.");
            }
            else
            {
                return Result.Fail("Sending mail error.");
            }
        }

        public async Task<Result> ResetPasswordAsync(string email, string token, string password, string confirmPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Result.Fail("User not found.");
            }

            if (password != confirmPassword)
            {
                return Result.Fail("The password and confirmation password do not match.");
            }

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return Result.Success("Password has been reset successfully.");
            }
            else
            {
                return Result.Fail("Error!", result.Errors.Select(o => o.Description).ToList());
            }
        }

        
    }
}
