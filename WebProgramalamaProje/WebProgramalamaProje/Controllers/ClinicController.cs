using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;

namespace WebProgramalamaProje.Controllers
{
    public class ClinicController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClinicController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var model = from clinic in _db.Clinics
                                        join hospital in _db.Hospital on clinic.HospitalId equals hospital.Id
                                        select new ClinicHospitalFieldsModel
                                        {
                                               Id = clinic.Id,
                                               Name = clinic.Name,
                                               HospitalName = hospital.Name,
                                        };
            var result=await model.ToListAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ClinicHospitalModel model = new ClinicHospitalModel();
            model.Hospitals = await _db.Hospital.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClinicHospitalModel model)
        {
            if(model.Clinic != null)
            {
               await _db.Clinics.AddAsync(model.Clinic);
               await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            ClinicHospitalModel model = new ClinicHospitalModel
            {
                Hospitals = await _db.Hospital.ToListAsync(),
                Clinic = await _db.Clinics.FindAsync(id),
                ClinicWorkTimes = await _db.ClinicWorkTimes.Where(a => a.ClinicId == id).ToListAsync(),
            };


            if (model.Clinic != null)
             return View(model);
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClinicHospitalModel model)
        {
            if(model.Clinic != null)
            {
               _db.Clinics.Update(model.Clinic);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Clinics.FindAsync(id);
            if (model != null)
            {
                _db.Clinics.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }

    }
}
