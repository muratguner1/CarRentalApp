using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Application.DTOs.User;

namespace CarRentalApp.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserLoginDto request);
    }
}
