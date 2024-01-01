using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Service;

namespace WebProgramalamaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly LanguageService _localization;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}