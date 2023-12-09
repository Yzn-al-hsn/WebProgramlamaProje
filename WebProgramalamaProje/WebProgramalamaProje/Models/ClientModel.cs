using WebProgramalamaProje.Models.Enums;
using WebProgramalamaProje.Models.NonEntity;

namespace WebProgramalamaProje.Models
{
    public class ClientModel : Person
    {
       
        public string Address { get; set; }
        public int Age { get; set; }
        public int GenderId { get; set; }
        public int StatusId { get; set; }
    }
}
