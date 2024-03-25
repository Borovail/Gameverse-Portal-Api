using Back_End.Models.AuthModels;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task <ApiResponseBuilder> Login(LoginModel model);

        Task <ApiResponseBuilder> Register(RegisterModel model);

        Task <ApiResponseBuilder> RegisterAsGuest(string? Nickname = null);
    }
}
