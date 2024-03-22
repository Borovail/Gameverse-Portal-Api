using Back_End.Models.AuthModels;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult> GetUserAsync(string id);
        Task<ServiceResult> GetUsersAsync();


        Task<ServiceResult> DeleteUserAsync(string id);
        Task<ServiceResult> DeleteUsersAsync(IReadOnlyList<string> ids);
        

        Task<ServiceResult> CreateUserAsync(RegisterModel model);


        Task<ServiceResult> UpdateUserAsync(RegisterModel model);
    }
}
