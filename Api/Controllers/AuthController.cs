using Application.Common.Contracts;
using Application.Common.Interfaces;
using Common.DTOs;
using Common.DTOs.Requests.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<Result> Register([FromBody] RegisterRequest request)
        {
            return await _identityService.RegisterAsync(request.Email, request.Username, request.Password);
        }

        [HttpPost]
        [Route("login")]
        public async Task<Result> Login([FromBody] LoginRequest request)
        {
            return await _identityService.LoginAsync(request.Username, request.Password);
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<Result> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            return await _identityService.ForgotPasswordAsync(request.Email);
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<Result> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            return await _identityService.ResetPasswordAsync(request.Email, request.Token, request.Password, request.ConfirmPassword);
        }

        
    }
}
