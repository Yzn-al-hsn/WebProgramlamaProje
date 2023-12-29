using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Models.DTO;
using WebProgramalamaProje.Repositories.Abstract;

namespace WebProgramalamaProje.Controllers
{
    
    public class AuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;

        public AuthenticationController(IUserAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (model == null)
            {
                return View(model);
            }

            model.Role = "user";
            var result = await _authService.RegistratiopnAsync(model);

            if(result.StatusCode == 0)
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Registration));
            } else
            {
                return RedirectToAction(nameof(Login));
            }
        }
        [HttpGet]
        public IActionResult Login() 
        {
        
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await _authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationModel
            {
                FirstName = "omar",
                LastName = "Shamieh",
                TCNumber = "99353028076",
                Email = "b211210332@sakarya.edu.tr",
                Password = "Qazsew321*",
                PasswordConfirm = "Qazsew321*",
                Address = "istanbul",
                PhoneNumber = "5315034026",
            };

            model.Role = "admin";
            var result = await _authService.RegistratiopnAsync(model);
            return Ok(result);
        }
    }
}
