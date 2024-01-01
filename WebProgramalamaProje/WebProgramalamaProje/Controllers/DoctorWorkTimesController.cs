using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Service;

namespace WebProgramalamaProje.Controllers
{
    [Authorize(Roles = "Admin")]

    public class DoctorWorkTimesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly LanguageService _localization;

        public DoctorWorkTimesController(ApplicationDbContext db, LanguageService localization)
        {
            _db = db;
            _localization = localization;
        }
        public async Task<IActionResult> Index(int doctorId)
        {
            if (doctorId != null)
            {
                var model = from workTime in _db.DoctorWorkTimes
                            where workTime.DoctorId == doctorId
                            select workTime;
                var result = await model.ToListAsync();
                return PartialView("_DoctorWorkTimes", result);
            }
            else
            {
                var model = await _db.DoctorWorkTimes.ToListAsync();
                return PartialView("_DoctorWorkTimes", model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }
        public async Task<IActionResult> Create(int doctorId)
        {
            if (doctorId > 0)
            {
                var model = new DoctorWorkTimesModel()
                {
                    DoctorId = doctorId,
                    ShiftEnd = DateTime.Now,
                    ShiftStart = DateTime.Now,
                };


                return View(model);
            }

            return RedirectToAction("Edit", "Doctor");
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorWorkTimesModel model)
        {
            if (model != null)
            {
                var doctor=await _db.Doctors.FindAsync(model.DoctorId);
                var clinicWorkTimes = _db.ClinicWorkTimes.Where(a => a.ClinicId == doctor.ClinicId && a.Day==model.Day && a.ShiftStart<=model.ShiftStart && a.ShiftEnd>=model.ShiftEnd);

                if(clinicWorkTimes.Any())
                {
                    await _db.DoctorWorkTimes.AddAsync(model);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Edit", "Doctor", new { id = model.DoctorId });
                }
                ViewBag.ErrorBool = true;
                ViewBag.ErrorMsg = _localization.GetKey("DoctorWorkTimeError");
                
                return View(model);
            }
            return View();
        }
        public async Task<IActionResult> Edit(int  id)
        {
            var model = await _db.DoctorWorkTimes.FindAsync(id);
            if (model != null)
            {

                return View(model);
            }

            return RedirectToAction("Edit", "Doctor");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorWorkTimesModel model)
        {
            if (model != null)
            {
                _db.DoctorWorkTimes.Update(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Edit", "Doctor", new { id = model.DoctorId });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.DoctorWorkTimes.FindAsync(id);
            if (model != null)
            {
                _db.DoctorWorkTimes.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }
    }
}
