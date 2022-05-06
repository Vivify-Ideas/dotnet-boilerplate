using Application.Common.Contracts;
using Common.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Jwt
{
    public class JwtService : ITokenService
    {
        private IConfiguration _configuration { get; set; }

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result> GetToken(string userId)
        {
            if (userId != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var jwtOpts = _configuration.GetSection(JwtOptions.Jwt)?.Get<JwtOptions>();
                if (jwtOpts == null) throw new Exception("Configuration error");

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Secret));

                var token = new JwtSecurityToken(
                    issuer: jwtOpts.ValidIssuer,
                    audience: jwtOpts.ValidAudience,
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

                return await Task.FromResult(Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token)));
            }

            return await Task.FromResult(Result.Fail("Username is null."));
        }
    }
}
