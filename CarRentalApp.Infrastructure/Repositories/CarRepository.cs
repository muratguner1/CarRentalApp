using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.Interfaces;
using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _context;
        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars
                .Include(c => c.Rentals)
                .ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.Rentals)
                .FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task<bool> UpdateAsync(Car car)
        {
            var existing = await _context.Cars.FindAsync(car.CarId);
            if (existing == null)
                return false;
            existing.Brand = car.Brand;
            existing.Model = car.Model;
            existing.Year = car.Year;
            existing.DailyPrice = car.DailyPrice;
            existing.IsAvailable = car.IsAvailable;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return false;
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
