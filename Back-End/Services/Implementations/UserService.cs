using Back_End.Models.AuthModels;
using Back_End.Services.Interfaces;
using Back_End.Utils;

namespace Back_End.Services.Implementations
{
    public class UserService : IUserService
    {
        public async Task<ApiResponseBuilder> CreateUserAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> DeleteUsersAsync(IReadOnlyList<string> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseBuilder> UpdateUserAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
