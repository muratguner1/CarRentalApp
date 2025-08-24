using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.Rental;

namespace CarRentalApp.Application.Interfaces.IServices
{
    public interface IRentalService
    {
        Task<RentalResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<RentalResponseDto>> GetAllAsync();
        Task<RentalResponseDto> CreateAsync(RentalCreateDto dto);
        Task<bool> UpdateAsync(int id, RentalUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
