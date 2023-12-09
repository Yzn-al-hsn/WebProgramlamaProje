using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramalamaProje.Models
{
    public class ClinicWorkTimesModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ClinicModel))]
        public int ClinicId { get; set; }
        public int Day { get; set; } //DayOfWeek DayOfWeek { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public ClinicModel Clinic { get; set; }
    }
}
