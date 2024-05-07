using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrsBsnsChallenge.Server.Data.Models;
using SrsBsnsChallenge.Server.Services;
using SrsBsnsChallenge.Server.Utils;

namespace SrsBsnsChallenge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailRequest emailData)
        {
            if (emailData == null)
            {
                return BadRequest("Request body is empty.");
            }

            if (!ValidationUtils.IsValidString(emailData.Name))
            {
                return BadRequest("Name is required.");
            }

            if (!ValidationUtils.IsValidEmail(emailData.To))
            {
                return BadRequest("Invalid email format.");
            }

            if (!ValidationUtils.IsValidString(emailData.Body))
            {
                return BadRequest("Message is required.");
            }
            if (!ValidationUtils.IsValidString(emailData.Subject))
            {
                return BadRequest("Subject is required.");
            }

            bool success = await _emailService.SendEmailAsync(emailData);

            if (success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
