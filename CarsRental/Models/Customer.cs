using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsRental.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}