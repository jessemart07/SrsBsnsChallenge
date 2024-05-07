using SrsBsnsChallenge.Server.Data.Models;

namespace SrsBsnsChallenge.Server.Respositories
{
    public interface IContactFormRepository
    {
        Task<IEnumerable<ContactForm>> GetContactFormsAsync();
        Task<ContactForm> GetContactFormByIdAsync(int id);
        Task<bool> AddContactFormAsync(ContactForm contact);
        Task<bool> UpdateContactFormAsync(int id,ContactForm contact);
        Task<bool> DeleteContactFormAsync(int id);
    }
}
