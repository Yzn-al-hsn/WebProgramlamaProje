using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class TownController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TownController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _db.Towns.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List()//not using now
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownModel model)
        {
            if(ModelState.IsValid)
            {
                await _db.Towns.AddAsync(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model=await _db.Towns.FindAsync(id);

            if(model!=null)
                return View(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TownModel model)
        {
            if(ModelState.IsValid)
            {
                _db.Towns.Update(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Towns.FindAsync(id);
            if (model != null)
            {
                _db.Towns.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }
    }
}
