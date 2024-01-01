using WebProgramalamaProje.Models.DTO;

namespace WebProgramalamaProje.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegistratiopnAsync(RegistrationModel model);
        Task LogoutAsync();
    }
}
