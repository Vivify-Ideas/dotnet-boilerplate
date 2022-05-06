using Application.Common.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var jwtOpts = config.GetSection(JwtOptions.Jwt)?.Get<JwtOptions>();
            if (jwtOpts == null) throw new Exception("Configuration error");

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
              options.UseNpgsql(config.GetConnectionString("PostgresConnectionString"),
              b => b.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtOpts.ValidAudience,
                        ValidIssuer = jwtOpts.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Secret))
                    };
                });

            services.AddTransient<ITokenService, JwtService>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

    }
}