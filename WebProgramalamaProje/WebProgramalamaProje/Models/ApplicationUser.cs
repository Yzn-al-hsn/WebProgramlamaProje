using Microsoft.AspNetCore.Identity;

namespace WebProgramalamaProje.Models
{
    public class ApplicationUser : IdentityUser
    {
        // fields for admin and client
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCNumber { get; set; }
        public string PhoneNumber { get; set; }
        // fields for only clients
        public string Address { get; set; }
        public int Age { get; set; }
        public int GenderId { get; set; }
        public int StatusId { get; set; }
    }
}
