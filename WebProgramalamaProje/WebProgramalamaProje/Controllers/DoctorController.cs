using Microsoft.AspNetCore.Mvc;

namespace WebProgramalamaProje.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
