using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Domain.Entities
{
    public class User 
    {
        public int UserId {get; set;}
        public string UserName {get; set;} = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Customer";
    }
}

