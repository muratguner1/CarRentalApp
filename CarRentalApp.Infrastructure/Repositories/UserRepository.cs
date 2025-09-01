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
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _context;
        
        public UserRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User user)
        {
            var exists = await _context.Users.AnyAsync(u => u.UserName == user.UserName);
            if (exists)
                return false;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Rentals)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Rentals)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.UserId);

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
