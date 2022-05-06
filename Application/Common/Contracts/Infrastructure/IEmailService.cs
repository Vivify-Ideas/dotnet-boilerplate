using Application.Common.Models.Mail;

namespace Application.Common.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
