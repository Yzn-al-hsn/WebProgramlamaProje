using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]

        public IActionResult Create(AppointmentModel model)
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]

        public IActionResult Edit(AppointmentModel model)
        { 
            return View();
        }
        [HttpPost]

        public IActionResult Delete(int id)
        {
            return View();
        }

    }
}
