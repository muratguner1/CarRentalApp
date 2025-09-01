using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.Car;
using CarRentalApp.Application.Interfaces.IRepositories;
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

        public async Task<Car?> UpdateAsync(Car car)
        {
            var existing = await _context.Cars.FindAsync(car.CarId);
            
            await _context.SaveChangesAsync();
            return existing;
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

        public async Task<IEnumerable<Car>> GetFilteredAsync(CarFilterDto filter)
        {
            var query = _context.Cars.AsQueryable();
            
            if (!string.IsNullOrEmpty(filter.Brand))
                query = query.Where(c => c.Brand.ToLower() == filter.Brand.ToLower());
            if (!string.IsNullOrEmpty(filter.Model))
                query = query.Where(c => c.Model.ToLower() == filter.Model.ToLower());
            if (filter.MinYear.HasValue)
                query = query.Where(c => c.Year >= filter.MinYear.Value);
            if (filter.MaxYear.HasValue)
                query = query.Where(c => c.Year <= filter.MaxYear.Value);
            if (filter.IsAvailable.HasValue)
                query = query.Where(c => c.IsAvailable == filter.IsAvailable.Value);

            query = query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            if(!string.IsNullOrEmpty(filter.SortBy))
            {
                if (filter.SortOrder.ToLower().Equals("desc"))
                    query = query.OrderByDescending(c => EF.Property<object>(c, filter.SortBy));
                else
                    query = query.OrderBy(c => EF.Property<object>(c, filter.SortBy));
            }

            return await query.ToListAsync();

        }
        public async Task<bool> ReturnCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            car!.IsAvailable = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
