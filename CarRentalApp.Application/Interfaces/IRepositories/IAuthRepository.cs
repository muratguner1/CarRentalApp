using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.User;
using CarRentalApp.Domain.Entities;

namespace CarRentalApp.Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<User?> LoginAsync(string userName, string password);
    }
}
