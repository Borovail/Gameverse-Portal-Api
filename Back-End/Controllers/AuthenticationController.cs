using Back_End.Models.AuthModels;
using Back_End.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Back_End.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenService _tokenService;

        public AuthenticationController(UserManager<IdentityUser> userManager, JwtTokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (model.Name == null && model.Email == null)
                return BadRequest("Please provide a username or email address.");

            IdentityUser user;

            if (model.Name != null)
                user = await _userManager.FindByNameAsync(model.Name);
            else
                user = await _userManager.FindByEmailAsync(model.Email);


            if (user == null)
                return NotFound($"A user registration with email address: [{model.Email}] could not be found.");

            if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                return NotFound("The password provided is incorrect.");


            //var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user.Id);

            return Ok($"Login successful!\n your token:{token}");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);



            var token = _tokenService.GenerateToken(user.Id);

            return Ok($"Registration successful!\n your token:{token}");


        }


        //cant be implemented until jwt is implemented
        [Authorize(Policy ="AdminAccess")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return NotFound("User is not found. If you think this is an error, please contact support or make sure you already have an account.");

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return Ok("Your account has been deleted.");
            else
                return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }


        //[HttpGet("register-as-guest")]
        //public async Task<IActionResult> RegisterAsGuest()
        //{
        //    await Console.Out.WriteLineAsync("i am here RegisterAsGuest()");

        //    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        //    var randomPart = new Random().Next(1000, 9999);
        //    var uniqueId = timestamp + '_' + randomPart;
        //    string userName = "Guest" + '_' + uniqueId.ToString();

        //    var user = new IdentityUser { UserName = userName , Email = null};
        //    await Console.Out.WriteLineAsync("i am here RegisterAsGuest() again");

        //    var result = await _userManager.CreateAsync(user);
        //    await Console.Out.WriteLineAsync("i am not here RegisterAsGuest() again");

        //    if (result.Succeeded)
        //        return Ok($"You are registered as a guest\nYour unique nickname is {userName}");
        //    else
        //        return BadRequest(result.Errors);
        //}







    }
}
