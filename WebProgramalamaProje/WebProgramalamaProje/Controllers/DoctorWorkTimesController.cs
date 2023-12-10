using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class DoctorWorkTimesController : Controller
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
        public async Task<IActionResult> Create(DoctorWorkTimesModel model)
        {
            return View();
        }
        public async Task<IActionResult> Edit(int  id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorWorkTimesModel model)
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
