namespace WebProgramalamaProje.Models.CombinedModels
{
    public class DoctorClinicModel
    {
        public List<ClinicModel> Clinics { get; set; }
        public DoctorModel Doctor { get; set; }
        public List<DoctorWorkTimesModel> DoctorWorkTimes { get; set; }
    }
}
