namespace WebProgramalamaProje.Models.CombinedModels
{
    public class AppointmentExtraModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ApplicationUser Client { get; set; }
        public DoctorModel Doctor { get; set; }
    }
}
