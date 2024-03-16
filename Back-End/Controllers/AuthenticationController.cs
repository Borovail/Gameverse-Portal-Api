using Back_End.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Your code here
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // Your code here
        }



        //func  delete account
        [HttpDelete("delete")]
        public IActionResult DeleteAccount()
        {
            // Your code here
        }

        //func register as guest
        [HttpGet("register-as-guest")]
        public IActionResult RegisterAsGuest()
        {
            // Your code here
        }

}
