using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class DoctorController : Controller
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
        public async Task<IActionResult> Create(DoctorModel model)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClinicModel model)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(ClinicModel model)
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
