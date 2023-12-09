using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramalamaProje.Models
{
    public class ClinicModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(HospitalModel))]
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public HospitalModel Hospital { get; set; }
    }
}
