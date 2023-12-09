using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class DoctorWorkTimesController : Controller
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorWorkTimesModel model)
        {
            return View();
        }
        public IActionResult Edit(int  id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(DoctorWorkTimesModel model)
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
