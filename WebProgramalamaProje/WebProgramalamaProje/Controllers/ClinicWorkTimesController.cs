using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class ClinicWorkTimesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClinicWorkTimesModel model)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClinicWorkTimesModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}
