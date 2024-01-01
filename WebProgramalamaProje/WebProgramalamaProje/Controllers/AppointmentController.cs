using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;

namespace WebProgramalamaProje.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AppointmentController(ApplicationDbContext db)
        {
            _db = db;
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
                                    Id=use.Id,
                                    FirstName = use.FirstName,
                                    LastName= use.LastName,
                                    Email = use.Email,
                                }).ToList();


            appointment.Doctors = (from doc in _db.Doctors
                                   join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                   select new DoctorModel
                                   {
                                       Id = doc.Id,
                                       Name = doc.Name + " - " + cli.Name,

                                   }).ToList();

            return View(appointment);
        }
        [HttpPost]

        public async Task<IActionResult> Create(AppointmentDoctorUserModel model)
        {
            if(model!=null&& model.Appointment!=null)
            {
                var query = from x in _db.Appointments
                            where ((x.StartDateTime <= model.Appointment.StartDateTime && x.EndDateTime >=model.Appointment.StartDateTime)
                            || (x.EndDateTime >= model.Appointment.EndDateTime && x.StartDateTime<=model.Appointment.EndDateTime))
                            && x.StartDateTime.DayOfYear == model.Appointment.StartDateTime.DayOfYear
                            && x.EndDateTime.DayOfYear == model.Appointment.EndDateTime.DayOfYear
                            && x.DoctorId == model.Appointment.DoctorId
                            select x;
                if(query.Any())
                    return View(model);

                HttpClient client = new HttpClient();

                var json=JsonConvert.SerializeObject(model.Appointment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44318/api/AppointmentApi", content);
                if(response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Edit(AppointmentModel model)
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
