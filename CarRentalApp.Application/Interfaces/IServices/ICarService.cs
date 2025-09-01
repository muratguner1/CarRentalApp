using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.Car;

namespace CarRentalApp.Application.Interfaces.IServices
{
    public interface ICarService
    {
        Task<CarResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<CarResponseDto>> GetAllAsync();
        Task<CarResponseDto> CreateAsync(CarCreateDto dto);
        Task<CarResponseDto?> UpdateAsync(int id, CarUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CarResponseDto>> GetFilteredAsync(CarFilterDto filter);
    }
}
