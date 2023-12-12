namespace WebProgramalamaProje.Models.CombinedModels
{
    public class HospitalTownModel
    {
        public HospitalModel Hospital { get; set; }
        public List<TownModel> Towns     { get; set; }
    }
}
