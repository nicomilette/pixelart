using Microsoft.AspNetCore.Mvc;
using PixelArtistBackend.Models;

namespace PixelArtistBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // Print the function call and the data passed
            Console.WriteLine($"Login called with Username: {user.Username}, Password: {user.Password}");
            return Ok(new { message = "Login endpoint called", user.Username, user.Password });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // Print the function call and the data passed
            Console.WriteLine($"Register called with Username: {user.Username}, Password: {user.Password}");
            return Ok(new { message = "Register endpoint called", user.Username, user.Password });
        }
    }
}
