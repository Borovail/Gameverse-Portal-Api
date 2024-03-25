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

        public async Task<ApiResponseBuilder> Login(LoginModel model)
        {
            IdentityUser user = null;
            string message = null;

            if (model.Login.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.Login);
                if (user == null)
                    return new ApiResponseBuilder().FailureResponse(404)
                        .SetErrors([$"User with email {model.Login} not found" ]);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.Login);
                if (user == null)
                    return new ApiResponseBuilder().FailureResponse(404)
                        .SetErrors([$"User with username {model.Login} not found" ]);
            }


            var roles = _userManager.GetRolesAsync(user).Result.ToArray();

            var token = _tokenService.GenerateToken(user.Id, roles);

            return new ApiResponseBuilder().SuccessResponse().AddData("token", token);

        }

        public async Task<ApiResponseBuilder> Register(RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return new ApiResponseBuilder().FailureResponse()
                .SetErrors(result.Errors.Select(e => e.Description).ToList());

            var token = _tokenService.GenerateToken(user.Id, "User");

            return new ApiResponseBuilder().SuccessResponse().AddData("token", token);
        }

        public async Task<ApiResponseBuilder> RegisterAsGuest(string? Nickname)
        {
            throw new NotImplementedException();
        }
    }
}
