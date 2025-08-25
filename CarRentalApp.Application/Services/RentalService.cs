using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalApp.Application.DTOs.Rental;
using CarRentalApp.Application.Interfaces.IRepositories;
using CarRentalApp.Application.Interfaces.IServices;
using CarRentalApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarRentalApp.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RentalService> _logger;

        public RentalService(IRentalRepository rentalRepository, IMapper mapper, ILogger<RentalService> logger)
        {
            _mapper = mapper;
            _rentalRepository = rentalRepository;
            _logger = logger;
        }
        public async Task<RentalResponseDto> CreateAsync(RentalCreateDto dto)
        {
            _logger.LogInformation("Creating a new rental for CarId {CarId} and UserId {UserId}", dto.CarId, dto.CustomerId);

            var rental = _mapper.Map<Rental>(dto);
            await _rentalRepository.AddAsync(rental);

            _logger.LogInformation("Rental created successfully with Id {RentalId}", rental.RentalId);
            return _mapper.Map<RentalResponseDto>(rental);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting rental with Id {RentalId}", id);

            bool result = await _rentalRepository.DeleteAsync(id);

            if (result)
                _logger.LogInformation("Rental deleted successfully with Id {RentalId}", id);
            else
                _logger.LogWarning("Rental with Id {RentalId} not found", id);

            return result;
        }

        public async Task<IEnumerable<RentalResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all rentals.");

            var rentals = await _rentalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RentalResponseDto>>(rentals);
        }

        public async Task<RentalResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting rental with Id {RentalId}", id);

            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                _logger.LogWarning("Rental with Id {RentalId} not found", id);
                return null;
            }

            _logger.LogInformation("Rental with Id {RentalId} found", id);
            return _mapper.Map<RentalResponseDto>(rental);
        }

        public async Task<bool> UpdateAsync(int id, RentalUpdateDto dto)
        {
            _logger.LogInformation("Updating rental with Id {RentalId}", id);

            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                _logger.LogWarning("Rental with Id {RentalId} not found for update", id);
                return false;
            }

            _mapper.Map(dto, rental);
            bool result = await _rentalRepository.UpdateAsync(rental);

            _logger.LogInformation("Rental with Id {RentalId} updated successfully", id);
            return result;
        }
    }
}
