using Back_End.Models.AuthModels;
using Back_End.Services.Interfaces;
using Back_End.Utils;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtToken _tokenService;

        public AuthenticationService(UserManager<IdentityUser> userManager, JwtToken tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult> Login(LoginModel model)
        {
            IdentityUser user = null;
            string message = null;

            if (model.Login.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.Login);
                if (user == null)
                    return ServiceResult.FailureResult($"User with email {model.Login} not found", 404);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.Login);
                if (user == null)
                    return ServiceResult.FailureResult($"User with name {model.Login} not found", 404);
            }


            var roles = _userManager.GetRolesAsync(user).Result.ToArray();

            var token = _tokenService.GenerateToken(user.Id, roles);

            return ServiceResult.SuccessResult(token);

        }

        public async Task<ServiceResult> Register(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return ServiceResult.FailureResult(result.Errors);

            var token = _tokenService.GenerateToken(user.Id, "User");

            return ServiceResult.SuccessResult(token);
        }

        public async Task<ServiceResult> RegisterAsGuest(string? Nickname)
        {
            throw new NotImplementedException();
        }
    }
}
