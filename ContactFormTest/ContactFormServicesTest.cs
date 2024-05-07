using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using SrsBsnsChallenge.Server.Services;
using System.Xml.Linq;

namespace ContactFormTest
{
    public class ContactFormTest
    {
        [Fact]
        public async Task SubmitContactFormAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var context = SrsBsnsDbContextMock.GetContext();
            var service = new ContactFormService(context);
            var model = new ContactFormCreateUpdateDTO
            {
                Name = "John Doe",
                Email = "john@example.com",
                Subject = "Test Subject",
                Message = "Test Message"
            };

            // Act
            var result = await service.SubmitContactFormAsync(model);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null, "john@example.com", "Test Subject", "Test Message")]
        [InlineData("John Doe", null, "Test Subject", "Test Message")]
        [InlineData("John Doe", "john@example.com", null, "Test Message")]
        [InlineData("John Doe", "john@example.com", "Test Subject", null)]
        public async Task SubmitContactFormAsync_NullOrEmptyInput_ReturnsFalse(string name, string email, string subject, string message)
        {
            // Arrange
            var context = SrsBsnsDbContextMock.GetContext();
            var service = new ContactFormService(context);
            var model = new ContactFormCreateUpdateDTO
            {
                Name = name,
                Email = email,
                Subject = subject,
                Message = message
            };

            // Act
            var result = await service.SubmitContactFormAsync(model);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("invalidemail")]
        [InlineData("invalid.email.com")]
        [InlineData("john@example")]
        public async Task SubmitContactFormAsync_InvalidEmailFormat_ReturnsFalse(string email)
        {
            // Arrange
            var context = SrsBsnsDbContextMock.GetContext();
            var service = new ContactFormService(context);
            var model = new ContactFormCreateUpdateDTO
            {
                Name = "John Doe",
                Email = email,
                Subject = "Test Subject",
                Message = "Test Message"
            };

            // Act
            var result = await service.SubmitContactFormAsync(model);

            // Assert
            Assert.False(result);
        }
    }
}