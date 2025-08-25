using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Application.Interfaces.IRepositories;
using CarRentalApp.Application.Interfaces.IServices;
using CarRentalApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarRentalApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<UserResponseDto?> CreateAsync(UserCreateDto dto)
        {
            _logger.LogInformation("Creating a new user with Username {Username}", dto.UserName);

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var result = await _userRepository.AddAsync(user);

            if (!result)
            {
                _logger.LogError("User with username {UserName} is already exits", dto.UserName);
                return null;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _logger.LogInformation("User created successfully with Id {UserId}", user.UserId);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting user with Id {UserId}", id);

            bool result = await _userRepository.DeleteAsync(id);

            if (result)
                _logger.LogInformation("User deleted successfully with Id {UserId}", id);
            else
                _logger.LogWarning("User with Id {UserId} not found", id);

            return result;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all users.");

            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting user with Id {UserId}", id);

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with Id {UserId} not found", id);
                return null;
            }

            _logger.LogInformation("User with Id {UserId} found", id);
            return _mapper.Map<UserResponseDto>(user);

        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            _logger.LogInformation("Updating user with Id {UserId}", id);

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with Id {UserId} not found for update", id);
                return false;
            }

            _mapper.Map(dto, user);

            if (!string.IsNullOrEmpty(dto.Password))
            {
                _logger.LogInformation("Updating password for user with Id {UserId}", id);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            bool result = await _userRepository.UpdateAsync(user);
            _logger.LogInformation("User with Id {UserId} updated successfully", id);

            return result;

        }
    }
}
