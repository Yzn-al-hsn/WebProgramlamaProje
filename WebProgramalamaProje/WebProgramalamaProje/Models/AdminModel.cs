using WebProgramalamaProje.Models.Enums;
using WebProgramalamaProje.Models.NonEntity;

namespace WebProgramalamaProje.Models
{
    public class AdminModel
    {
        
        public Person Person { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
