using CarRentalApp.Application.DTOs.Rental;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IUserService userService, IAuthorizationService authorizationService,
            IRentalService rentalService, ICarService carService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
            _rentalService = rentalService;
            _carService = carService;
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
            return Ok(new { message = "User removed succesfully.", response = success });
        }

        [HttpPost("rentcar")]
        public async Task<IActionResult> RentCar(RentalCreateDto dto)
        {
            var user = await _userService.GetByIdAsync(dto.CustomerId);
            if(user is null)
                return NotFound("User not found!");

            var car = await _carService.GetByIdAsync(dto.CarId);
            if(car is null)
                return NotFound("Car not found!");
            if (!car.IsAvailable)
                return BadRequest("Car is not available!");

            var response = await _rentalService.CreateAsync(dto);
            return Ok(response);
        }

        [HttpPost("returncar/{retanlId}")]
        public async Task<IActionResult> ReturnCar(int retanlId)
        {
            var rental = await _rentalService.GetByIdAsync(retanlId);
            if (rental is null)
                return NotFound("Rental not found!");

            var authResult = await _authorizationService.AuthorizeAsync(User, rental.CustomerId, "SameUserOrAdmin");
            if (!authResult.Succeeded)
                return Forbid();

            if (rental.ReturnDate.HasValue)
                return BadRequest("Car is already delivered!");

            var success = await _userService.ReturnCarAsync(retanlId);

            if (!success)
                return NotFound();

            return Ok("Car delivered.");
        }
    }
}
