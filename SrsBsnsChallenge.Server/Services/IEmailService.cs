using SrsBsnsChallenge.Server.Data.Models;

namespace SrsBsnsChallenge.Server.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequest emailData);
    }
}
