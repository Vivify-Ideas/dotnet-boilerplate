using Application.Common.Contracts.Infrastructure;
using FluentEmail.Core;
using Email = Application.Common.Models.Mail.Email;

namespace Infrastructure.Mail
{
    public class EmailService : IEmailService
    {        
        private  IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<bool> SendEmail(Email mail)
        {
            var sendResponse = await _fluentEmail
                                        .To(mail.To)
                                        .Subject(mail.Subject)
                                        .Body(mail.Body)
                                        .SendAsync();

            return sendResponse.Successful;
        }
    }
}
