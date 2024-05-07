using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using SrsBsnsChallenge.Server.Utils;

namespace SrsBsnsChallenge.Server.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly SrsBsnsChallengeDbContext _context;
        public ContactFormService(SrsBsnsChallengeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SubmitContactFormAsync(ContactFormCreateUpdateDTO createContactFormDTO)
        {
            try
            {
                if (!ValidationUtils.IsValidString(createContactFormDTO.Name) 
                    || !ValidationUtils.IsValidString(createContactFormDTO.Message) 
                    || !ValidationUtils.IsValidString(createContactFormDTO.Subject))
                {
                    return false;
                }

                if (!ValidationUtils.IsValidEmail(createContactFormDTO.Email))
                {
                    return false;
                }

                ContactForm ContactFormToCreate = new()
                {
                    Id = 0,
                    Name = createContactFormDTO.Name,
                    Subject = createContactFormDTO.Subject,
                    Email = createContactFormDTO.Email,
                    Message = createContactFormDTO.Message,
                    CreatedAt = DateTime.Now,
                };

                _context.ContactForms.Add(ContactFormToCreate);

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception 
                throw new Exception("Error submitting contact form", ex);
            }
        }

        public async Task<IEnumerable<ContactForm>> GetAllContactFormsAsync()
        {
            try
            {
                return await _context.ContactForms.ToListAsync();
            }
            catch(Exception ex)
            {
                // Log the exception 
                throw new Exception("Error getting all contact forms", ex);
            }
        }

        public async Task<ContactForm?> GetContactFormByIdAsync(int id)
        {
            try
            {
                return await _context.ContactForms.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception 
                throw new Exception("Error getting contact form", ex);
            }
        }

        public async Task<bool> UpdateContactFormAsync(int id, ContactFormCreateUpdateDTO updatedContactFormDTO)
        {
            try
            {
                var ContactFormToUpdate = await _context.ContactForms.FindAsync(id);

                if (ContactFormToUpdate != null)
                {
                    ContactFormToUpdate.Name = updatedContactFormDTO.Name;
                    ContactFormToUpdate.Subject = updatedContactFormDTO.Subject;
                    ContactFormToUpdate.Email = updatedContactFormDTO.Email;
                    ContactFormToUpdate.Message = updatedContactFormDTO.Message;

                    return await _context.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error updating contact", ex);
            }
        }

        public async Task<bool> DeleteContactFormAsync(int id)
        {
            try
            {
                ContactForm? ContactFormToDelete = await _context.ContactForms.FindAsync(id);

                if (ContactFormToDelete != null)
                {
                    _context.ContactForms.Remove(ContactFormToDelete);

                    return await _context.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error deleting contact", ex);
            }
        }
    }
}
