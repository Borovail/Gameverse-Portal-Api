using Azure;
using Back_End.Data;
using Back_End.Models.BugModes;
using Back_End.Services.Interfaces;
using Back_End.Utils;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IBugService _bugService;

        public BugController(IBugService bugService)
        {
            _bugService = bugService;
        }

        // GET: api/bug
        [HttpGet]
        public async Task<IActionResult> GetBugs()
        {
            var response =await _bugService.GetBugsAsync();

            return StatusCode(response.StatusCode, response.Build());
        }

        // GET api/bug/5
        [HttpGet("bug-by-id{id}")]
        public async Task<IActionResult> GetBugById(string id)
        {
            var response = await _bugService.GetBugByIdAsync(id);

            return StatusCode(response.StatusCode, response.Build());
        }

        // GET api/bug
        [HttpGet("user-bug{id}")]
        public async Task<IActionResult> GetUserBug(string userId)
        {
            var response = await _bugService.GetUserBug(userId);

            return StatusCode(response.StatusCode, response.Build());
        }

        // GET api/bug
        [Authorize]
        [HttpGet("my-bugs")]
        public async Task<IActionResult> GetMyBugs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _bugService.GetUserBug(userId);

            return StatusCode(response.StatusCode, response.Build());
        }


        // POST api/<BugController>
        [HttpPost]
        public async Task<IActionResult> CreateBug([FromBody] CreateBugModel bugModel)
        {
            var response = await _bugService.CreateBug(bugModel);

            return StatusCode(response.StatusCode, response.Build());
        }

        // PUT api/<BugController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBug(string id, [FromBody] UpdateBugModel updateBugModel)
        {
            var response = await _bugService.UpdateBug(id, updateBugModel);

            return StatusCode(response.StatusCode, response.Build());
        }

        // DELETE api/<BugController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(string id)
        {
            var response = await _bugService.DeleteBug(id);

            return StatusCode(response.StatusCode, response.Build());
        }

        // DELETE api/<BugController>
        [HttpDelete]
        public async Task<IActionResult> DeleteAllBugs()
        {
            var response = await _bugService.DeleteAllBugs();

            return StatusCode(response.StatusCode, response.Build());
        }
    }
}
