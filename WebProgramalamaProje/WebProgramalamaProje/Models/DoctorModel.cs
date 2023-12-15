using System.ComponentModel.DataAnnotations.Schema;
using WebProgramalamaProje.Models.NonEntity;

namespace WebProgramalamaProje.Models
{
    public class DoctorModel : Person
    {
        [ForeignKey(nameof(ClinicModel))]
        public int ClinicId { get; set; }
        public ClinicModel Clinic { get; set; } 

    }
}
