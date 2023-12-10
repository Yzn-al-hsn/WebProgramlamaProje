using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class AppointmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            return View();
        }
        public async Task<IActionResult> Create() 
        {
            return View();
        }

        public async Task<IActionResult> Create(AppointmentModel model)
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        public async Task<IActionResult> Edit(AppointmentModel model)
        { 
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

    }
}
