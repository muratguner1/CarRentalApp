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
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalDbContext _context;

        public RentalRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .ToListAsync();
        }

        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RentalId == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
                return false;
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Rental rental)
        {
            var existing = await _context.Rentals.FindAsync(rental.RentalId);
            if (existing == null)
                return false;
            existing.ReturnDate = rental.ReturnDate;
            existing.RentAmount = rental.RentAmount;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
