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

        public BugService(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<ApiResponseBuilder> GetBugsAsync()
        {
            var bugs = await _apiDbContext.Bugs.AsNoTracking().ToListAsync();

            if (bugs == null)
                return new ApiResponseBuilder().FailureResponse(404)
                  .SetErrors(new List<string> { "No bugs found" });

            return new ApiResponseBuilder().SuccessResponse().AddData("bugs", bugs);


        }

        public async Task<ApiResponseBuilder> GetUserBug(string userId)
        {
            var bugs = await _apiDbContext.Bugs.Where(i => i.UserId == userId).AsNoTracking().ToListAsync();

            if (bugs == null)
                return new ApiResponseBuilder().FailureResponse(404).SetErrors(["No bugs found"]);

            return  new ApiResponseBuilder().SuccessResponse().AddData("bugs", bugs);
        }

        public async Task<ApiResponseBuilder> GetBugByIdAsync(string id)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null)
                return  new ApiResponseBuilder().FailureResponse(404).SetErrors(["Bug not found"]);

            return new ApiResponseBuilder().SuccessResponse().AddData("bug", bug);
        }



        public async Task<ApiResponseBuilder> CreateBug(string userId,CreateBugModel bugModel)
        {
            BugModel bug = new BugModel
            {
                Title = bugModel.Title,
                Description = bugModel.Description,
                Status = bugModel.Status,
                UserId = userId
            };

            await _apiDbContext.Bugs.AddAsync(bug);
            await _apiDbContext.SaveChangesAsync();

            return new ApiResponseBuilder().SuccessResponse();
        }


        public async Task<ApiResponseBuilder> UpdateBug(string id, UpdateBugModel bugModel)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) 
                return new ApiResponseBuilder().FailureResponse(404).SetErrors(["Bug not found"]);


            bug.Title = bugModel.Title ?? bug.Title;
            bug.Description = bugModel.Description ?? bug.Description;
            bug.Status = bugModel.Status ?? bug.Status;

            _apiDbContext.Bugs.Update(bug);
            await _apiDbContext.SaveChangesAsync();

            return new ApiResponseBuilder().SuccessResponse();
        }


        public async Task<ApiResponseBuilder> DeleteAllBugs()
        {
            var count = await _apiDbContext.Bugs.ExecuteDeleteAsync();

            return  new ApiResponseBuilder().SuccessResponse().AddData("count", count);

                
               
        }

        public async Task<ApiResponseBuilder> DeleteBug(string id)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) 
                return new ApiResponseBuilder().FailureResponse(404).SetErrors(["Bug not found"]);

            _apiDbContext.Bugs.Remove(bug);
            await _apiDbContext.SaveChangesAsync();


            return new ApiResponseBuilder().SuccessResponse();
        }

  

       
       
    }
}
