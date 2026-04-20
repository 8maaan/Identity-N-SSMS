using Demo.Models.Requests;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("strict")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersService;

        public UsersController(IUsersRepository usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var result = await _usersService.RegisterAsync(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            Console.WriteLine(System.Net.ServicePointManager.SecurityProtocol);
            var token = await _usersService.LoginAsync(request);

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid email or password");

            return Ok(new { token });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersService.GetAllUsers();

            if (users.IsNullOrEmpty())
            {
                return Ok("There are currently no users");
            }

            return Ok(users);
        }
    }
}
