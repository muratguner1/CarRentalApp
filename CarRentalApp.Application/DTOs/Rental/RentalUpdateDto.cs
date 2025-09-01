using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApp.Application.DTOs.Rental
{
    public class RentalUpdateDto
    {
        public DateTime? ReturnDate { get; set; }
        public decimal? RentAmount { get; set; }
    }
}
