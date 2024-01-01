namespace WebProgramalamaProje.Models.CombinedModels
{
    public class AppointmentDoctorUserModel
    {
        public AppointmentModel Appointment { get; set; }
        public List<DoctorModel> Doctors { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
