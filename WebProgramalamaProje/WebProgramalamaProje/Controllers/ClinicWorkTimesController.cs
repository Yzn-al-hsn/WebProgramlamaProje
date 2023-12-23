using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;

namespace WebProgramalamaProje.Controllers
{
    public class ClinicWorkTimesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ClinicWorkTimesController(ApplicationDbContext db) { 
            _db = db;
        }
        public async Task<IActionResult> Index(int clinicId)
        {
            
            if(clinicId != null)
            {
                var model = from workTime in _db.ClinicWorkTimes
                            where workTime.ClinicId == clinicId
                            select workTime;
                var result = await model.ToListAsync();
                //return View(result);
                return PartialView("_ClinicWorkTimes", result);
            }
            else
            {
                var model = await _db.ClinicWorkTimes.ToListAsync();
                //return View(model);
                return PartialView("_ClinicWorkTimes", model);
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> Create(int clinicId)
        {
            if(clinicId > 0)
            {
                ClinicWorkTimesModel model = new ClinicWorkTimesModel()
                {
                    ClinicId = clinicId, 
                    ShiftEnd = DateTime.Now,
                    ShiftStart = DateTime.Now,
            };
                

                return View(model);
            }

            return RedirectToAction("Edit", "Clinic");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClinicWorkTimesModel model)
        {
            if(model != null)
            {
                await _db.ClinicWorkTimes.AddAsync(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("Edit", "Clinic", new {id = model.ClinicId});
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _db.ClinicWorkTimes.FindAsync(id);
            if(model != null)
            {

                return View(model);
            }
            return RedirectToAction("Edit", "Clinic");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClinicWorkTimesModel model)
        {
            if(model != null)
            {
                _db.ClinicWorkTimes.Update(model);
                await _db.SaveChangesAsync();
                var id = model.ClinicId;
                return RedirectToAction("Edit", "Clinic", new {id=id});


            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.ClinicWorkTimes.FindAsync(id);
            if (model != null)
            {
                _db.ClinicWorkTimes.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }

    }
}
