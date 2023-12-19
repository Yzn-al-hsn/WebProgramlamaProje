using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using WebProgramalamaProje.Models.Enums;

namespace WebProgramalamaProje.Services
{
    public class Utils
    {
        public static string GetDayAsString(int day)
        {
            List<Days> daysList = Enum.GetValues(typeof(Days))
                .Cast<Days>()
                .ToList();
            return daysList[day].ToString();
        }
    }
}
