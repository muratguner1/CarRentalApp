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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalDbContext _context;
        
        public CustomerRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        /*public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        } */

        public async Task<bool> DeleteAsync(int id)
        {
            return await _context.Customers
                .Where(c => c.CustomerId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Rentals).ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.Rentals).FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            var existing = await _context.Customers.FindAsync(customer.CustomerId);
            if (existing == null)
                return false;
            existing.FullName = customer.FullName;
            existing.Email = customer.Email;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
