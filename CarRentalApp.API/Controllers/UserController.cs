using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserService userService, IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpGet("getusers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            if (users is null)
                return NotFound("Not any user record found!");
            return Ok(users);
        }

        [HttpGet("getuser/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserOrAdmin");
            if (!authResult.Succeeded)
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user is null)
                return NotFound("User not found!");
            return Ok(user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserOrAdmin");
            if (!authResult.Succeeded)
                return Forbid();

            var response = await _userService.UpdateAsync(id, dto);
            if (response is null)
                return NotFound("User not found!");
            return Ok(new { message = "User updated.", response = response });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserOrAdmin");
            if (!authResult.Succeeded)
                return Forbid();

            var success = await _userService.DeleteAsync(id);
            if (!success)
                return NotFound("User not found!");
            return Ok("User removed successfully.");
        }
    }
}
