using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsRental.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Zdjęcie")]
        public string ImageUrl { get; set; }
        [Display(Name = "Typ")]
        public string CarType { get; set; }
        [Display(Name = "Ilość miejsc")]
        public string SeatsNumber { get; set; }
        [Display(Name = "Ilość drzwi")]
        public string DoorNumber { get; set; }
        [Display(Name = "Pojemność silnika")]
        public double EngineSize { get; set; }
        [Display(Name = "Skrzynia biegów")]
        public string GearboxType { get; set; }
        [Display(Name = "Klimatyzacja")]
        public string Aircondition { get; set; }
        [Display(Name = "Opłata")]
        public decimal PricePerDay { get; set; }
    }
}