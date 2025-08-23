using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Application.DTOs.Rental
{
    public class RentalResponseDto
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public string CarModel { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? RentAmount { get; set; }
    }
}
