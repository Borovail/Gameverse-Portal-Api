using Back_End.Models.BugModes;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IBugService
    {
        Task<ApiResponseBuilder> GetBugsAsync();
        Task<ApiResponseBuilder> GetUserBug(string userId);

        Task<ApiResponseBuilder> GetBugByIdAsync(string bugIds);

        Task<ApiResponseBuilder> CreateBug(string userId,CreateBugModel bugModel);

        Task<ApiResponseBuilder> UpdateBug(string id, UpdateBugModel bugModel);

        Task<ApiResponseBuilder> DeleteBug(string id);

        Task<ApiResponseBuilder> DeleteAllBugs();
    }
}
