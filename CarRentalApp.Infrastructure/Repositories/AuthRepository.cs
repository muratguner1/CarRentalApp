using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.Interfaces.IRepositories;
using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CarRentalDbContext _context;

        public AuthRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task<User?> LoginAsync(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user != null && VerifyPassword(password, user.PasswordHash))
                return user;
            return null;
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
