

namespace SrsBsnsChallenge.Server.Data.Models
{
    public class ContactForm
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Subject { get; set; }
        public required string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
