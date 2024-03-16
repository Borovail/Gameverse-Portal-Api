using Back_End.Data;
using Back_End.Models.BugModes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugController : ControllerBase
    {
        /// <summary>
        /// optimize using of SaveChangesAsync
        /// </summary>


        private readonly ApiDbContext _apiDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public BugController(ApiDbContext apiDbContext, UserManager<IdentityUser> userManager)
        {
            _apiDbContext = apiDbContext;
            _userManager = userManager;
        }


        // GET: api/bug
        [HttpGet]
        public async Task<IActionResult> GetBugsAsync()
        {
            var bugs = await _apiDbContext.Bugs.ToListAsync();

            if (bugs == null) return NotFound();

            return Ok(bugs);
        }

        // GET api/bug/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBugByIdAsync(string id)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) return NotFound();

            return Ok(bug);
        }

        // POST api/<BugController>
        [HttpPost]
        public async Task<IActionResult> CreateBug([FromBody] CreateBugModel bugModel)
        {
            BugModel bug = new BugModel
            {
                Title = bugModel.Title,
                Description = bugModel.Description,
                Status = bugModel.Status,
            };

            //uncomment  when  authentication is implemented
            //bug.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _apiDbContext.Bugs.AddAsync(bug);
            await _apiDbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<BugController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBug(string id, [FromBody] UpdateBugModel updateBugModel)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) return NotFound();


            bug.Title = updateBugModel.Title ?? bug.Title;
            bug.Description = updateBugModel.Description ?? bug.Description;
            bug.Status = updateBugModel.Status ?? bug.Status;

            _apiDbContext.Bugs.Update(bug);
            await _apiDbContext.SaveChangesAsync();

            return Ok();

        }

        // DELETE api/<BugController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(string id)
        {
            var bug = await _apiDbContext.Bugs.FirstOrDefaultAsync(i => i.Id == id);

            if (bug == null) return NotFound();


            _apiDbContext.Bugs.Remove(bug);
            await _apiDbContext.SaveChangesAsync();

            return Ok();

        }
    }
}
