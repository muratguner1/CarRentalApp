using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalApp.Application.DTOs.Car;
using CarRentalApp.Application.Interfaces.IRepositories;
using CarRentalApp.Application.Interfaces.IServices;
using CarRentalApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarRentalApp.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CarService> _logger;

        public CarService(ICarRepository carRepository, IMapper mapper, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CarResponseDto> CreateAsync(CarCreateDto dto)
        {
            _logger.LogInformation("[Creating a new car: {Brand} {Model}]", dto.Brand, dto.Model);

            var car = _mapper.Map<Car>(dto);
            await _carRepository.AddAsync(car);

            _logger.LogInformation("[Car created successfully with Id {CarId}]", car.CarId);

            return _mapper.Map<CarResponseDto>(car);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting car with Id {CarId}", id);

            bool result = await _carRepository.DeleteAsync(id);

            if(result)
                _logger.LogInformation("Car deleted successfully with Id {CarId}", id);
            else
                _logger.LogInformation("Car with Id {CarId} not found", id);

            _logger.LogInformation("Car removed with Id {CarId}", id);
            return result;
        }

        public async Task<IEnumerable<CarResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all cars.");

            var cars = await _carRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarResponseDto>>(cars);
        }

        public async Task<CarResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting car with Id {CarId}", id);

            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                _logger.LogWarning("Car with Id {CarId} not found", id);
                return null;
            }

            _logger.LogInformation("Car with Id {CarId} found", id);
            return _mapper.Map<CarResponseDto>(car);
        }

        public async Task<CarResponseDto?> UpdateAsync(int id, CarUpdateDto dto)
        {
            _logger.LogInformation("Updating car with Id {CarId}", id);

            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
            {
                _logger.LogWarning("Car with Id {CarId} not found for update", id);
                return null;
            }

            _mapper.Map(dto, car);
            var response = await _carRepository.UpdateAsync(car);

            _logger.LogInformation("Car with Id {CarId} updated successfully", id);

            return _mapper.Map<CarResponseDto>(response);
        }

        public async Task<IEnumerable<CarResponseDto>> GetFilteredAsync(CarFilterDto filter)
        {
            _logger.LogInformation("Getting filtered cars.");
            var cars = await _carRepository.GetFilteredAsync(filter);
            return _mapper.Map<IEnumerable<CarResponseDto>>(cars);
        }
    }
}
