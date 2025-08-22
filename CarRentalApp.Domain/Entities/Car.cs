using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public decimal DailyPrice { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
