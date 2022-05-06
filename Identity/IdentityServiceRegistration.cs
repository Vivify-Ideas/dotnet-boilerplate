using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Identity.Jwt;
using Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Identity.Authorization.Requirements;

namespace Infrastructure
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {          
            services.AddJwtAuthentication(configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanDeleteItemOnly", policy => policy.RequireClaim("Permission", "CanDeleteItem"));
                options.AddPolicy("CanCreateItemOnly", policy => policy.Requirements.Add(new CanCreateItemOnlyRequirement()));
            });
                
            services.AddTransient<IAuthorizationHandler, CanCreateItemOnlyRequirementHandler>();

            return services;
        }

        public static IServiceProvider AddIdentityMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
                context.Database.Migrate();
            }

            return services;
        }
    }
}
