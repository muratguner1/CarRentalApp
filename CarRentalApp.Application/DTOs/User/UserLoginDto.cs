﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Application.DTOs.User
{
    public class UserLoginDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
