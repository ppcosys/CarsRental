using CarsRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsRental.ViewModels
{
    public class CarsViewModel
    {
        public List<Car> AllCarsList { get; set; }
        public Car Car { get; set; }
    }
}