namespace WebProgramalamaProje.Models
{
    public class DoctorWorkTimesModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int Day { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
