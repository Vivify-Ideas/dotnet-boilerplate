using Application.Common.Contracts.Persistance;
using Application.Common.Contracts.Repositories;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                          options.UseNpgsql(configuration.GetConnectionString("PostgresConnectionString")));

            services.AddRepositories();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


            //if (configuration.GetValue<bool>("inmemorydb"))
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseInMemoryDatabase("CleanArchitectureDb"));
            //}
            //else if (configuration.GetValue<bool>("mssql"))
            //{
            //    //services.AddDbContext<ApplicationDbContext>(options =>
            //    //    options.UseSqlServer(
            //    //        configuration.GetConnectionString("DefaultConnection"),
            //    //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            //}
            //else if (configuration.GetValue<bool>("postgresql"))
            //{
            //    //dodati konfiguraciju za postgresql
            //}
            //else
            //{
            //    //logovati gresku
            //} 

            services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceProvider AddPersistenceMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            return services;
        }
    }
}