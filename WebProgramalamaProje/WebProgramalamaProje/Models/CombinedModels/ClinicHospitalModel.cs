namespace WebProgramalamaProje.Models.CombinedModels
{
    public class ClinicHospitalModel
    {
        public List<HospitalModel> Hospitals { get; set; }
        public ClinicModel Clinic { get; set; }
        public List<ClinicWorkTimesModel> ClinicWorkTimes { get; set; }
    }
}
