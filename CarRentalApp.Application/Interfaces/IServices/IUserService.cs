using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.User;

namespace CarRentalApp.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> CreateAsync(UserCreateDto dto);
        Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ReturnCarAsync(int rentalId);
    }
}
