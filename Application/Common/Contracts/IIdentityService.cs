
using Application.Common.Models;
using Common.DTOs;

namespace Application.Common.Contracts
{
    public interface IIdentityService
    {
        Task<Result> RegisterAsync(string email, string username, string password);
        Task<Result> LoginAsync(string username, string password);
        Task<Result> AuthorizeAsync(string userId, string policyName);
        Task<Result<string>> GetUserIdAsync(string username);
        Task<Result> ForgotPasswordAsync(string email);
        Task<Result> ResetPasswordAsync(string email, string token, string password, string confirmPassword);
        
    }
}
