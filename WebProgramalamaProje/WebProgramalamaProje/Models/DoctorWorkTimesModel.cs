using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramalamaProje.Models
{
    public class DoctorWorkTimesModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(DoctorModel))]
        public int DoctorId { get; set; }
        public int Day { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public DoctorModel Doctor { get; set; }
    }
}
