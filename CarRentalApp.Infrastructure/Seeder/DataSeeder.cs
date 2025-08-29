using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Domain.Entities;
using CarRentalApp.Infrastructure.Contexts;

namespace CarRentalApp.Infrastructure.Seeder
{
    public class DataSeeder
    {
        private readonly CarRentalDbContext _context;

        public DataSeeder(CarRentalDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            SeedAdmin();
        }

        public void SeedAdmin()
        {
            if(!_context.Users.Any(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin.123"),
                    Role = "Admin",
                };

                _context.Users.Add(admin);
                _context.SaveChanges();
            }
        }
    }
}
