using Microsoft.AspNetCore.Mvc;
using SrsBsnsChallenge.Server.Data.Models;

namespace SrsBsnsChallenge.Server.Services
{
    public interface IContactFormService
    {
        Task<IEnumerable<ContactForm>> GetAllContactFormsAsync();
        Task<ContactForm?> GetContactFormByIdAsync(int id);
        Task<bool> SubmitContactFormAsync(ContactFormCreateUpdateDTO contact);
        Task<bool> UpdateContactFormAsync(int id, ContactFormCreateUpdateDTO contact);
        Task<bool> DeleteContactFormAsync(int id);
    }
}
