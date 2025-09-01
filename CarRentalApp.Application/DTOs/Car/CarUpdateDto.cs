using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Application.DTOs.Car
{
    public class CarUpdateDto
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public decimal? DailyPrice { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
