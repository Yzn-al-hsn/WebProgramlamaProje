using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;
using WebProgramalamaProje.Service;

namespace WebProgramalamaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly LanguageService _localization;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, LanguageService localization, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _localization = localization;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return RedirectToAction("Index");
        }

        public IActionResult ChangeLanguageAdmin(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return RedirectToAction("Privacy");
        }

        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.GetKey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> TakeAppointment()
        {
            var appointment = new AppointmentDoctorUserModel();

            appointment.Doctors = (from doc in _db.Doctors
                                   join cli in _db.Clinics on doc.ClinicId equals cli.Id
                                   join hos in _db.Hospital on cli.HospitalId equals hos.Id
                                   select new DoctorModel
                                   {
                                       Id = doc.Id,
                                       Name = doc.Name + " - " + cli.Name +" - "+hos.Name,

                                   }).ToList();

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> TakeAppointment(AppointmentDoctorUserModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Appointment.ClientId = user.Id;

            var doctorWorkTimes=_db.DoctorWorkTimes.Where(a=>a.Id ==model.Appointment.DoctorId 
            &&a.Day==(int)model.Appointment.StartDateTime.DayOfWeek 
            &&a.Day==(int)model.Appointment.EndDateTime.DayOfWeek
            &&a.ShiftStart.TimeOfDay<=model.Appointment.StartDateTime.TimeOfDay
            && a.ShiftEnd.TimeOfDay>=model.Appointment.EndDateTime.TimeOfDay);

            if(!doctorWorkTimes.Any())
            {
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
                        || (x.StartDateTime <= model.Appointment.EndDateTime && x.EndDateTime >= model.Appointment.EndDateTime )
                        || (x.StartDateTime>=model.Appointment.StartDateTime && x.EndDateTime<=model.Appointment.EndDateTime))
                        && x.StartDateTime.DayOfYear == model.Appointment.StartDateTime.DayOfYear
                        && x.EndDateTime.DayOfYear == model.Appointment.EndDateTime.DayOfYear
                        && x.DoctorId == model.Appointment.DoctorId
                        select x;

            var startLessThanEnd = model.Appointment.StartDateTime <= model.Appointment.EndDateTime;
            //ok?
            if (query.Any() ||!startLessThanEnd)
            {
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

            

            await _db.Appointments.AddAsync(model.Appointment);
            await _db.SaveChangesAsync();

            return RedirectToAction("AppointmentList");
        }

        public async Task<IActionResult> AppointmentList()
        {
            var user = await _userManager.GetUserAsync(User);

            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://localhost:44318/api/AppointmentApi");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var Appointments = JsonConvert.DeserializeObject<List<AppointmentExtraModel>>(jsonResponse);

            Appointments = Appointments.Where(a => a.ClientId == user.Id).ToList();

            return View(Appointments);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var model = await _db.Appointments.FindAsync(id);
            if (model != null)
            {
                _db.Appointments.Remove(model);
                await _db.SaveChangesAsync();

                return Json(new { success = true, deletedId = id });
            }
            return Json(new { error = true });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}