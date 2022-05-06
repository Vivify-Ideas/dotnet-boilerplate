using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Services;
using Application.Common.Contracts.Infrastructure;
using Infrastructure.Mail;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {          

            services.AddTransient<IDateTime, DateTimeService>();

            int port = configuration.GetValue<int>("Smtp:Port");
            string host = configuration.GetValue<string>("Smtp:Server");
            string fromAddress = configuration.GetValue<string>("Smtp:FromAddress");

            services.AddFluentEmail(fromAddress)
                    .AddSmtpSender(host, port);

            //.AddSmtpSender(new SmtpClient("smtp.gmail.com")
            // {
            //     UseDefaultCredentials = false,
            //     Port = gmailPort,
            //     Credentials = new NetworkCredential(gmailSender, gmailPassword),
            //     EnableSsl = true,
            // });

            services.AddScoped<IEmailService, EmailService>();


            return services;
        }




    }
}
