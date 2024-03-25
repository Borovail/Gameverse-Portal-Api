using Back_End.Models.AuthModels;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseBuilder> GetUserAsync(string id);
        Task<ApiResponseBuilder> GetUsersAsync();


        Task<ApiResponseBuilder> DeleteUserAsync(string id);

        Task<ApiResponseBuilder> CreateUserAsync(RegisterModel model);


        Task<ApiResponseBuilder> UpdateUserAsync(RegisterModel model);
    }
}
