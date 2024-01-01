using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;
using WebProgramalamaProje.Service;

namespace WebProgramalamaProje.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly LanguageService _localization;


        public AppointmentController(ApplicationDbContext db, LanguageService localization)
        {
            _db = db;
            _localization = localization;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://localhost:44318/api/AppointmentApi");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var Appointments = JsonConvert.DeserializeObject<List<AppointmentExtraModel>>(jsonResponse);

            return View(Appointments);
        }
        [HttpPost]
        public async Task<IActionResult> List()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var appointment = new AppointmentDoctorUserModel();

            appointment.Users = (from use in _db.Users
                                 join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                 join rol in _db.Roles on useRol.RoleId equals rol.Id
                                 where rol.NormalizedName != "ADMIN"
                                 select new ApplicationUser
                                 {
                                     Id = use.Id,
                                     FirstName = use.FirstName,
                                     LastName = use.LastName,
                                     Email = use.Email,
                                 }).ToList();


            appointment.Doctors = (from doc in _db.Doctors
                                   join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                   join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                   select new DoctorModel
                                   {
                                       Id = doc.Id,
                                       Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                   }).ToList();

            return View(appointment);
        }
        [HttpPost]

        public async Task<IActionResult> Create(AppointmentDoctorUserModel model)
        {
            if (model != null && model.Appointment != null)
            {

                var doctorWorkTimes = _db.DoctorWorkTimes.Where(a => a.Id == model.Appointment.DoctorId
            && a.Day == (int)model.Appointment.StartDateTime.DayOfWeek
            && a.Day == (int)model.Appointment.EndDateTime.DayOfWeek
            && a.ShiftStart.TimeOfDay <= model.Appointment.StartDateTime.TimeOfDay
            && a.ShiftEnd.TimeOfDay >= model.Appointment.EndDateTime.TimeOfDay);

                if (!doctorWorkTimes.Any())
                {
                    model.Users = (from use in _db.Users
                                   join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                   join rol in _db.Roles on useRol.RoleId equals rol.Id
                                   where rol.NormalizedName != "ADMIN"
                                   select new ApplicationUser
                                   {
                                       Id = use.Id,
                                       FirstName = use.FirstName,
                                       LastName = use.LastName,
                                       Email = use.Email,
                                   }).ToList();

                    model.Doctors = (from doc in _db.Doctors
                                     join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                     join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                     select new DoctorModel
                                     {
                                         Id = doc.Id,
                                         Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                     }).ToList();

                    ViewBag.ErrorBoolTime = true;
                    ViewBag.ErrorMsgTime = _localization.GetKey("OutOfDoctorWorkTimes");

                    return View(model);
                }


                var query = from x in _db.Appointments
                            where ((x.StartDateTime <= model.Appointment.StartDateTime && x.EndDateTime >= model.Appointment.StartDateTime)
                            || (x.StartDateTime <= model.Appointment.EndDateTime && x.EndDateTime >= model.Appointment.EndDateTime)
                            || (x.StartDateTime >= model.Appointment.StartDateTime && x.EndDateTime <= model.Appointment.EndDateTime))
                            && x.StartDateTime.DayOfYear == model.Appointment.StartDateTime.DayOfYear
                            && x.EndDateTime.DayOfYear == model.Appointment.EndDateTime.DayOfYear
                            && x.DoctorId == model.Appointment.DoctorId
                            select x;

                var startLessThanEnd = model.Appointment.StartDateTime <= model.Appointment.EndDateTime;

                if (query.Any() || !startLessThanEnd)
                {
                    model.Users = (from use in _db.Users
                                   join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                   join rol in _db.Roles on useRol.RoleId equals rol.Id
                                   where rol.NormalizedName != "ADMIN"
                                   select new ApplicationUser
                                   {
                                       Id = use.Id,
                                       FirstName = use.FirstName,
                                       LastName = use.LastName,
                                       Email = use.Email,
                                   }).ToList();

                    model.Doctors = (from doc in _db.Doctors
                                     join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                     join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                     select new DoctorModel
                                     {
                                         Id = doc.Id,
                                         Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                     }).ToList();

                    ViewBag.ErrorBool = true;
                    ViewBag.ErrorMsg = _localization.GetKey("AppointmentError");

                    return View(model);
                }

                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(model.Appointment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44318/api/AppointmentApi", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    await Add(model.Appointment);

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = new AppointmentDoctorUserModel();

            appointment.Appointment = await _db.Appointments.FindAsync(id);

            appointment.Users = (from use in _db.Users
                                 join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                 join rol in _db.Roles on useRol.RoleId equals rol.Id
                                 where rol.NormalizedName != "ADMIN"
                                 select new ApplicationUser
                                 {
                                     Id = use.Id,
                                     FirstName = use.FirstName,
                                     LastName = use.LastName,
                                     Email = use.Email,
                                 }).ToList();


            appointment.Doctors = (from doc in _db.Doctors
                                   join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                   join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                   select new DoctorModel
                                   {
                                       Id = doc.Id,
                                       Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                   }).ToList();

            return View(appointment);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(AppointmentDoctorUserModel model)
        {
            if (model != null && model.Appointment != null)
            {
                var doctorWorkTimes = _db.DoctorWorkTimes.Where(a => a.Id == model.Appointment.DoctorId
            && a.Day == (int)model.Appointment.StartDateTime.DayOfWeek
            && a.Day == (int)model.Appointment.EndDateTime.DayOfWeek
            && a.ShiftStart.TimeOfDay <= model.Appointment.StartDateTime.TimeOfDay
            && a.ShiftEnd.TimeOfDay >= model.Appointment.EndDateTime.TimeOfDay);

                if (!doctorWorkTimes.Any())
                {
                    model.Users = (from use in _db.Users
                                   join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                   join rol in _db.Roles on useRol.RoleId equals rol.Id
                                   where rol.NormalizedName != "ADMIN"
                                   select new ApplicationUser
                                   {
                                       Id = use.Id,
                                       FirstName = use.FirstName,
                                       LastName = use.LastName,
                                       Email = use.Email,
                                   }).ToList();

                    model.Doctors = (from doc in _db.Doctors
                                     join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                     join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                     select new DoctorModel
                                     {
                                         Id = doc.Id,
                                         Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                     }).ToList();

                    ViewBag.ErrorBoolTime = true;
                    ViewBag.ErrorMsgTime = _localization.GetKey("OutOfDoctorWorkTimes");

                    return View(model);
                }

                if (model.Appointment.StartDateTime >= model.Appointment.EndDateTime)
                {
                    model.Appointment = await _db.Appointments.FindAsync(model.Appointment.Id);

                    model.Users = (from use in _db.Users
                                   join useRol in _db.UserRoles on use.Id equals useRol.UserId
                                   join rol in _db.Roles on useRol.RoleId equals rol.Id
                                   where rol.NormalizedName != "ADMIN"
                                   select new ApplicationUser
                                   {
                                       Id = use.Id,
                                       FirstName = use.FirstName,
                                       LastName = use.LastName,
                                       Email = use.Email,
                                   }).ToList();


                    model.Doctors = (from doc in _db.Doctors
                                     join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                     join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                     select new DoctorModel
                                     {
                                         Id = doc.Id,
                                         Name = doc.Name + " - " + cli.Name + " - " + hos.Name,

                                     }).ToList();

                    ViewBag.ErrorBool = true;
                    ViewBag.ErrorMsg = _localization.GetKey("AppointmentError");

                    return View(model);

                }

                HttpClient client = new HttpClient();
                var Json = JsonConvert.SerializeObject(model.Appointment);
                var content = new StringContent(Json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:44318/api/AdminApi/{model.Appointment.Id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    await Update(model.Appointment);
            }

            return RedirectToAction("Index");

        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = _db.Appointments.Find(id);

            if (appointment != null)
            {
                HttpClient client = new HttpClient();
                var response = await client.DeleteAsync($"https://localhost:44318/api/AdminApi/{appointment.Id}");
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, deletedId = id });
                else
                {
                    await Remove(appointment);
                    return Json(new { success = true, deletedId = id });
                }

            }
            return Json(new { error = true });

        }

        #region Extra
        private async Task Add(AppointmentModel model)
        {
            await _db.Appointments.AddAsync(model);
            await _db.SaveChangesAsync();
        }

        private async Task Update(AppointmentModel model)
        {
            _db.Appointments.Update(model);
            await _db.SaveChangesAsync();
        }

        private async Task Remove(AppointmentModel model)
        {
            _db.Appointments.Remove(model);
            await _db.SaveChangesAsync();
        }
        #endregion

    }
}
