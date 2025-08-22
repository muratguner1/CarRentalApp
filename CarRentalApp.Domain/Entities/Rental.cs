using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Domain.Entities
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? RentAmount { get; set; }
    }
}
