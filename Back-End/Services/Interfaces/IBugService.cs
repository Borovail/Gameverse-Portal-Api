using Back_End.Models.BugModes;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IBugService
    {
        Task<ServiceResult> GetBugsAsync();

        Task<ServiceResult> GetMyBugs();

        Task<ServiceResult> GetUsersBugs(string userId);

        Task<ServiceResult> GetBugByIdAsync(string id);

        Task<ServiceResult> CreateBug(CreateBugModel bugModel);

        Task<ServiceResult> UpdateBug(string id, UpdateBugModel bugModel);

        Task<ServiceResult> DeleteBug(string id);

        Task<ServiceResult> DeleteBugs(params int[] id);

        Task<ServiceResult> DeleteAllBugs();
    }
}
