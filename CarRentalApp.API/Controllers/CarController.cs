using CarRentalApp.Application.DTOs.Car;
using CarRentalApp.Application.Interfaces.IServices;
using CarRentalApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CarCreateDto dto)
        {
            var response = await _carService.CreateAsync(dto);
            return Created($"/api/cars/{response.CarId}", new { message = "Car created.", car = response });

        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CarUpdateDto dto)
        {
            var response = await _carService.UpdateAsync(id, dto);
            if (response is null)
                return NotFound("Car not found!");
            return Ok(new { message = "Car updated.", response = response });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carService.DeleteAsync(id);
            if(!result)
                return NotFound("Car not found!");
            return Ok(new { message = "Car removed succesfully.", response = result });
        }

        [HttpGet("getcars")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carService.GetAllAsync();
            if (cars is null)
                return NotFound("Not any car record found!");
            return Ok(cars);
        }

        [HttpGet("getcar/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _carService.GetByIdAsync(id);
            if (response is null)
                return NotFound("Car not found!");
            return Ok(response);
        }

        [HttpPost("getfiltered")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFiltered(CarFilterDto filter)
        {
            var filtered = await _carService.GetFilteredAsync(filter);
            if (filtered is null || !filtered.Any())
                return NotFound("No car were found that match this filter!");
            return Ok(filtered);
        }
    }
}
