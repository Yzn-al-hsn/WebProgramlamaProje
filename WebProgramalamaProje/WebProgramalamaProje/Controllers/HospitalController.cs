using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;

namespace WebProgramalamaProje.Controllers
{
    [Authorize(Roles = "admin")]
    public class HospitalController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HospitalController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var model = from hospital in _db.Hospital
                        join town in _db.Towns on hospital.TownId equals town.Id
                        select new HospitalTownFieldsModel
                        {
                            Id  = hospital.Id,
                            HospitalName = hospital.Name,
                            HospitalAddress = hospital.Address,
                            HospitalEmail = hospital.Email,
                            HospitalPhoneNumber = hospital.PhoneNumber,
                            HospitalTown = town.Name,
                        };

            
            return View(await model.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HospitalTownModel model = new HospitalTownModel();
            model.Towns = await _db.Towns.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalTownModel model)
        {
            if(model != null)
            {
                
                await _db.Hospital.AddAsync(model.Hospital);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            } 
           return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int hospitalId)
        {
            HospitalTownModel model = new HospitalTownModel();
            model.Towns = await _db.Towns.ToListAsync();
            model.Hospital = await _db.Hospital.FindAsync(hospitalId);

            if(model.Hospital.Id > 0)
                return View(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HospitalTownModel model)
        {
            
            if (model.Hospital != null)
            {
                _db.Hospital.Update(model.Hospital);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
            
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _db.Hospital.FindAsync(id);
            if (model != null)
            {
                _db.Hospital.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }
        
    }
}
