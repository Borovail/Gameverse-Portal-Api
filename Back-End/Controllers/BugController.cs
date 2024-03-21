using Back_End.Data;
using Back_End.Models.BugModes;
using Back_End.Services.Interfaces;
using Back_End.Utils;
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
            var result =await _bugService.GetBugsAsync();

            return StatusCode(result.StatusCode, result.Data);
        }

        // GET api/bug/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBugById(string id)
        {
            var result = await _bugService.GetBugByIdAsync(id);

            return StatusCode(result.StatusCode, result.Data);
        }

        // POST api/<BugController>
        [HttpPost]
        public async Task<IActionResult> CreateBug([FromBody] CreateBugModel bugModel)
        {
            var result = await _bugService.CreateBug(bugModel);

            return StatusCode(result.StatusCode, result.Data);
        }

        // PUT api/<BugController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBug(string id, [FromBody] UpdateBugModel updateBugModel)
        {
            var result = await _bugService.UpdateBug(id, updateBugModel);

            return StatusCode(result.StatusCode, result.Data);
        }

        // DELETE api/<BugController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(string id)
        {
            var result = await _bugService.DeleteBug(id);

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
