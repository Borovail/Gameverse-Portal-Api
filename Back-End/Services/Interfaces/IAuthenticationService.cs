using Back_End.Models.AuthModels;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task <ServiceResult> Login(LoginModel model);

        Task <ServiceResult> Register(RegisterModel model);

        Task <ServiceResult> RegisterAsGuest(string? Nickname);
    }
}
