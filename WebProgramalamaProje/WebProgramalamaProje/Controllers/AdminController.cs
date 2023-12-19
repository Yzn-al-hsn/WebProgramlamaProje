using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db) { 
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _db.Admins.ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> List() // not working yet
        { 
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminModel model)
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Edit(AdminModel model)
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
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
