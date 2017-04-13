using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsRental.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public int ReservationTime { get; set; }

        public int CarId { get; set; }

        public string SessionId { get; set; }

        public decimal TotalPrice { get; set; }

        public string CarModel { get; set; }
    }
}