using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Application.Interfaces.IRepositories;
using CarRentalApp.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<string?> LoginAsync(UserLoginDto request)
        {
            _logger.LogInformation("Checking user informations.");

            var user = await _authRepository.LoginAsync(request.UserName, request.Password);

            if (user == null)
            {
                _logger.LogError("UserName or Password incorrect!");
                return null;
            }

            _logger.LogInformation("Creating token.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                ]),

                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation("Token created.");

            return tokenHandler.WriteToken(token);

        }
    }
}
