using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Application.Interfaces.IRepositories
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(int id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task AddAsync(Rental rental);
        Task<bool> UpdateAsync(Rental rental);
        Task<bool> DeleteAsync(int id);
        
    }
}
