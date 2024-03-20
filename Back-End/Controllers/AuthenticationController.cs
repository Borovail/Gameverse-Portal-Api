using Back_End.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (model.Name == null && model.Email == null)
                return BadRequest("Please provide a username or email address.");

            IdentityUser identityUser;

            if (model.Name != null)
                identityUser = await _userManager.FindByNameAsync(model.Name);
            else
                identityUser = await _userManager.FindByEmailAsync(model.Email);


            if (identityUser == null)
                return NotFound($"A user registration with email address: [{model.Email}] could not be found.");

            if (await _userManager.CheckPasswordAsync(identityUser, model.Password) == false)
                return NotFound("The password provided is incorrect.");


            //cant be implemented until jwt is implemented
            //var roles = await _userManager.GetRolesAsync(user);

            //var token = _tokenService.GenerateToken(user, roles.ToArray());

            //return OperationResult.CreateSuccessResult(data: token);

            return Ok("Login successful");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Ok("Registration successful!");
            else
                return BadRequest(result.Errors);


            //cant be implemented until jwt is implemented
            //if (result.Succeeded)
            //{
            //    var token = _tokenService.GenerateToken(user);

            //    return OperationResult.CreateSuccessResult(data: token);
            //}

            //return OperationResult.CreateFailureResult(errors: result.Errors.Select(e => e.Description).ToList(), "Registration failed");

        }


        //cant be implemented until jwt is implemented
        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteAccount()
        //{
        //}


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
