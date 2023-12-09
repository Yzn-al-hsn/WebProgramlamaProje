using WebProgramalamaProje.Models.Enums;
using WebProgramalamaProje.Models.NonEntity;

namespace WebProgramalamaProje.Models
{
    public class ClientModel
    {
       
        public Person Person { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public MeritalStatus Status { get; set; }
    }
}
