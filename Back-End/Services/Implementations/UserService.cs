using Back_End.Models.AuthModels;
using Back_End.Services.Interfaces;
using Back_End.Utils;

namespace Back_End.Services.Implementations
{
    public class UserService : IUserService
    {
        public async Task<ServiceResult> CreateUserAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteUsersAsync(IReadOnlyList<string> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> UpdateUserAsync(RegisterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
