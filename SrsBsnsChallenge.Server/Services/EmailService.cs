using MimeKit;
using MailKit.Net.Smtp;
using SrsBsnsChallenge.Server.Data.Models;
using Microsoft.Extensions.Options;
using SrsBsnsChallenge.Server.Utils;

namespace SrsBsnsChallenge.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailService(IOptions<EmailConfiguration> emailConfig) 
        { 
            _emailConfig = emailConfig.Value;
        }
        public async Task<bool> SendEmailAsync(EmailRequest emailData)
        {
            try
            {
                if (!ValidationUtils.IsValidString(emailData.Name)
                    || !ValidationUtils.IsValidString(emailData.Body)
                    || !ValidationUtils.IsValidString(emailData.Subject))
                {
                    return false;
                }

                if (!ValidationUtils.IsValidEmail(emailData.To))
                {
                    return false;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Jesse", _emailConfig.FromEmail));
                message.To.Add(new MailboxAddress("", emailData.To));
                message.Subject = emailData.Subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = emailData.Body;

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
