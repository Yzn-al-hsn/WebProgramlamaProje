namespace WebProgramalamaProje.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateTime { get; set; }

    }
}
