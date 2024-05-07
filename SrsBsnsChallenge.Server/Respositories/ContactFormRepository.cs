using Dapper;
using Microsoft.EntityFrameworkCore;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using System.Data.SqlClient;
namespace SrsBsnsChallenge.Server.Respositories
{
    public class ContactFormRepository 
    {
        private readonly IConfiguration _config;
        private readonly SrsBsnsChallengeDbContext _context;

        public ContactFormRepository(IConfiguration config, SrsBsnsChallengeDbContext context)
        {
            _config = config;
            _context = context;
        }

        //public async Task<bool> AddContactFormAsync(ContactForm contact)
        //{
        //    try
        //    {
        //        // Let EF Core auto-increment the ID.
        //        //ContactForm ContactFormToCreate = new()
        //        //{
        //        //    Id = 0,
        //        //    Name = postToCreateDTO.Name,
        //        //    Subject = postToCreateDTO.Subject,
        //        //    Email = postToCreateDTO.Email,
        //        //    Message = postToCreateDTO.Message,
        //        //};

        //        //dbContext.ContactForms.Add(ContactFormToCreate);

        //        //bool success = await dbContext.SaveChangesAsync() > 0;

        //        //if (success)
        //        //{
        //        //    return true;
        //        //}
        //        //else
        //        //{
        //        //    // 500 = internal server error.
        //        //    return false;
        //        //}

        //    }
        //    catch (SqlException ex)
        //    {
        //        // Log the SQL exception
        //        throw new Exception("Error adding contact", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log other exceptions
        //        throw new Exception("An unexpected error occurred", ex);
        //    }
        //}

        //public async Task<bool> DeleteContactFormAsync(int id)
        //{
        //    try
        //    {
        //        ContactForm? contact = await _context.ContactForms.FindAsync(id);
        //        if (contact == null)
        //        {
        //            // Contact not found
        //            return false;
        //        }

        //        _context.ContactForms.Remove(contact);
        //        var success = await _context.SaveChangesAsync();

        //        return true;
        //    }
        //}

        //public async Task<ContactForm> GetContactFormByIdAsync(int id)
        //{
        //    ContactForm? post = await _context.ContactForms.FindAsync(id);


        //}

        //public Task<IEnumerable<ContactForm>> GetContactFormsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> UpdateContactFormAsync(int id, ContactForm contact)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
