using CarRentalApp.Application.DTOs.Car;
using CarRentalApp.Application.Interfaces.IServices;
using CarRentalApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost("addcar")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(CarCreateDto dto)
        {
            var response = await _carService.CreateAsync(dto);
            return Created($"/api/cars/{response.CarId}", new { message = "Car created.", car = response });

        }



        [HttpDelete("delete/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carService.DeleteAsync(id);
            if(!result)
                return NotFound("Car not found!");
            return Ok("Car removed successfully.");
        }

        [HttpGet("getcars")]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            if (cars is null)
                return NotFound("Not any car record found!");
            return Ok(cars);
        }

        [HttpGet("getcar/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _carService.GetByIdAsync(id);
            if (response is null)
                return NotFound("Car not found!");
            return Ok(response);
        }
    }
}
