using Back_End.Data;
using Back_End.Models.BugModes;
using Back_End.Services.Interfaces;
using Back_End.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services.Implementations
{
    public class BugService : IBugService
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public BugService(ApiDbContext apiDbContext , UserManager<IdentityUser> userManager)
        {
            _apiDbContext = apiDbContext;
            _userManager = userManager;
        }

        public async Task<ServiceResult> CreateBug(CreateBugModel bugModel)
        {
            BugModel bug = new BugModel
            {
                Title = bugModel.Title,
                Description = bugModel.Description,
                Status = bugModel.Status,
            };

            await _apiDbContext.Bugs.AddAsync(bug);
            await _apiDbContext.SaveChangesAsync();

            return ServiceResult.SuccessResult("Success");
        }

        public async Task<ServiceResult> DeleteAllBugs()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteBug(string id)
        {
           var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) return  ServiceResult.FailureResult("Bug not found", 404);


            _apiDbContext.Bugs.Remove(bug);
            await _apiDbContext.SaveChangesAsync();

          
            return ServiceResult.SuccessResult("Success");
        }

        public async Task<ServiceResult> DeleteBugs(params int[] id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetBugByIdAsync(string id)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) 
                return ServiceResult.FailureResult("Bug not found", 404);

            return ServiceResult.SuccessResult(bug);
        }

        public async Task<ServiceResult> GetBugsAsync()
        {
            var bugs = await _apiDbContext.Bugs.ToListAsync();

            if (bugs == null)
                return ServiceResult.FailureResult("No bugs found", 404);

            return ServiceResult.SuccessResult(bugs);
        }

        public async Task<ServiceResult> GetMyBugs()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> GetUsersBugs(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> UpdateBug(string id, UpdateBugModel bugModel)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) return ServiceResult.FailureResult("Bug not found", 404);


            bug.Title = bugModel.Title ?? bug.Title;
            bug.Description = bugModel.Description ?? bug.Description;
            bug.Status = bugModel.Status ?? bug.Status;

            _apiDbContext.Bugs.Update(bug);
            await _apiDbContext.SaveChangesAsync();

           return ServiceResult.SuccessResult("Success");
        }
    }
}
