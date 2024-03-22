using Back_End.Models.BugModes;
using Back_End.Utils;

namespace Back_End.Services.Interfaces
{
    public interface IBugService
    {
        Task<ServiceResult> GetBugsAsync();

        Task<ServiceResult> GetMyBugs();

        Task<ServiceResult> GetUsersBugs(IReadOnlyList<string> userIds);

        Task<ServiceResult> GetBugByIdAsync(IReadOnlyList<string> bugIds);

        Task<ServiceResult> CreateBug(CreateBugModel bugModel);

        Task<ServiceResult> UpdateBug(string id, UpdateBugModel bugModel);

        Task<ServiceResult> DeleteBugs(IReadOnlyList<string> ids);

        Task<ServiceResult> DeleteAllBugs();
    }
}
