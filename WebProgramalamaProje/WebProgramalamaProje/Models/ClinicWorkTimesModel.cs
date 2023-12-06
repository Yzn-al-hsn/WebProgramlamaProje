namespace WebProgramalamaProje.Models
{
    public class ClinicWorkTimesModel
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int Day { get; set; } //DayOfWeek DayOfWeek { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
