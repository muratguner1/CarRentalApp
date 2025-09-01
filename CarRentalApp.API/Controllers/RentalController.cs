using CarRentalApp.Application.DTOs.Rental;
using CarRentalApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getrentals")]
        public async Task<IActionResult> GetAll()
        {
            var rentals = await _rentalService.GetAllAsync();
            if(rentals is null)
                return NotFound("Not found any rental!");

            return Ok(rentals);
        }

        [HttpGet("getrental/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental is null)
                return NotFound("Rental not found!");

            return Ok(rental);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, RentalUpdateDto dto)
        {
            var success = await _rentalService.UpdateAsync(id, dto);
            if (!success)
                return NotFound("Rental not found!");

            return Ok(new { message = "Rental updated succesfully.", response = success });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _rentalService.DeleteAsync(id);
            if (!success)
                return NotFound("Rental not found!");

            return Ok(new { message = "Rental removed succesfully.", response = success });
        }
    }
}
