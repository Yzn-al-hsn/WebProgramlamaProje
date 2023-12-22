using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;

namespace WebProgramalamaProje.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DoctorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var model = from doctor in _db.Doctors
                        join clinic in _db.Clinics on doctor.ClinicId equals clinic.Id
                        select new DoctorClinicFieldsModel
                        {
                            Id = doctor.Id,
                            Name = doctor.Name,
                            SurName = doctor.SurName,
                            TCNumber = doctor.TCNumber,
                            PhoneNumber = doctor.PhoneNumber,
                            ClinicName = clinic.Name
                        };

            var result=await model.OrderByDescending(x => x.Id).ToListAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var model=new DoctorClinicModel();
            model.Clinics=await _db.Clinics.ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorClinicModel model)
        {
            if(model.Doctor!=null)
            {
                //we should add validation here
                //if(model.Doctor.Name==string.Empty)
                //    return RedirectToAction("Create")
                //this validation should be for every propery and write massege using viewbag to print it in the view

                await _db.Doctors.AddAsync(model.Doctor);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction("Index");

            var model = new DoctorClinicModel
            {
                Doctor=await _db.Doctors.FindAsync(id),
                Clinics=await _db.Clinics.ToListAsync(),
                DoctorWorkTimes=await _db.DoctorWorkTimes.Where(x=>x.DoctorId==id).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorClinicModel model)
        {
            if (model!=null && model.Doctor!=null)
            {
                //we should add validation here
                //if(model.Doctor.Name==string.Empty)
                //    return RedirectToAction("Create")
                //this validation should be for every propery and write massege using viewbag to print it in the view

                _db.Doctors.Update(model.Doctor);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Doctors.FindAsync(id);
            if(model!=null)
            {
                _db.Doctors.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });

        }

    }
}
