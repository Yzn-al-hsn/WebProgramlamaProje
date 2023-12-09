using WebProgramalamaProje.Models.Enums;
using WebProgramalamaProje.Models.NonEntity;

namespace WebProgramalamaProje.Models
{
    public class AdminModel : Person
    {
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
