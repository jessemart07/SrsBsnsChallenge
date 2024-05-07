using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrsBsnsChallenge.Server.Data.Models;
using SrsBsnsChallenge.Server.Services;
using SrsBsnsChallenge.Server.Utils;

namespace SrsBsnsChallenge.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly IContactFormService _contactService;

        public ContactFormController(IContactFormService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<ActionResult> Submit(ContactFormCreateUpdateDTO model)
        {
            if (model == null)
            {
                return BadRequest("Request body is empty.");
            }

            bool success = await _contactService.SubmitContactFormAsync(model);

            if (success)
            {
                return Ok(); 
            }
            else
            {
                return StatusCode(500); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactFormsAsync();
            return Ok(contacts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetContactFormByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactFormCreateUpdateDTO contact)
        {
            var result = await _contactService.UpdateContactFormAsync(id, contact);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContactFormAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
