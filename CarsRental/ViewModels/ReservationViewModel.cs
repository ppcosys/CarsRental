using CarsRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsRental.ViewModels
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<Car> AvailableCarsList { get; set; }
        public List<Reservation> ReservationsList { get; set; }
    }
}