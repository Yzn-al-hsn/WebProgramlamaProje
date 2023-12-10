using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramalamaProje.Models
{
    public class HospitalModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(TownModel))]
        public int TownId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public TownModel Town { get; set; }
    }
}
