using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthorizeController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto dto)
        {
            var user = await _userService.CreateAsync(dto);
            if (user is null)
                return BadRequest("User already exists!");
            return Created($"/api/users/{user.UserId}",new { message = "Registration successful", user = user });
        } 

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token is null)
                return Unauthorized("User name or password incorrect");
            return Ok(new { message = "Login successful", token = token });
        }
    }
}
