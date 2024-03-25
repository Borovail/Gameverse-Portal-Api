using Back_End.Models.BugModes;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IBugService
    {
        Task<ServiceResponse> GetBugsAsync();
        Task<ServiceResult> GetUserBug(string userId);

        Task<ServiceResult> GetBugByIdAsync(string bugIds);

        Task<ServiceResult> CreateBug(CreateBugModel bugModel);

        Task<ServiceResult> UpdateBug(string id, UpdateBugModel bugModel);

        Task<ServiceResult> DeleteBug(string id);

        Task<ServiceResult> DeleteAllBugs();
    }
}
