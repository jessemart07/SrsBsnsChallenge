using ContactFormTest;
using Microsoft.Extensions.Hosting;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using SrsBsnsChallenge.Server.Services;
using System.Xml.Linq;
using System.Configuration;
using Microsoft.Extensions.Options;
using SrsBsnsChallenge.Server;
namespace EmailServiceTest
{
    public class EmailServiceTest
    {
        [Fact]
        public async Task SubmitEmailAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var emailConfig = new EmailConfiguration {
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                SmtpUsername = "jessedev07@gmail.com",
                SmtpPassword = "zelf baim nfgl wjmh",
                FromEmail = "jessedev07@gmail.com"
            };
            var emailConfigMock = Options.Create(emailConfig);
            var service = new EmailService(emailConfigMock);
            var model = new EmailRequest
            {
                Name = "John Doe",
                Subject = "Test Subject",
                To = "jessemart07@gmail.com",
                Body = "Test Body"
            };

            // Act
            var result = await service.SendEmailAsync(model);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null, "john@example.com", "Test Subject", "Test Body")]
        [InlineData("John Doe", null, "Test Subject", "Test Body")]
        [InlineData("John Doe", "john@example.com", null, "Test Body")]
        [InlineData("John Doe", "john@example.com", "Test Subject", null)]
        public async Task SubmitEmailAsync_NullOrEmptyInput_ReturnsFalse(string name, string to, string subject, string body)
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                SmtpUsername = "jessedev07@gmail.com",
                SmtpPassword = "zelf baim nfgl wjmh",
                FromEmail = "jessedev07@gmail.com"
            };
            var emailConfigMock = Options.Create(emailConfig);
            var service = new EmailService(emailConfigMock);
            var model = new EmailRequest
            {
                Name = name,
                Subject = subject,
                To = to,
                Body = body
            };

            // Act
            var result = await service.SendEmailAsync(model);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("invalid.email.com")]
        [InlineData("john@example")]
        public async Task SubmitEmailAsync_InvalidEmailFormat_ReturnsFalse( string to)
        {
            // Arrange
            var emailConfig = new EmailConfiguration
            {
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                SmtpUsername = "jessedev07@gmail.com",
                SmtpPassword = "zelf baim nfgl wjmh",
                FromEmail = "jessedev07@gmail.com"
            };
            var emailConfigMock = Options.Create(emailConfig);
            var service = new EmailService(emailConfigMock);
            var model = new EmailRequest
            {
                Name = "John Doe",
                Subject = "Test Subject",
                To = to,
                Body = "Test Body"
            };

            // Act
            var result = await service.SendEmailAsync(model);

            // Assert
            Assert.False(result);
        }
    }
}