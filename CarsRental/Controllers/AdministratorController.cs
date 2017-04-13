using CarsRental.Models;
using CarsRental.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsRental.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Administrator
        public ActionResult Index()
        {
            List<Car> Cars = new List<Car>();
            Cars = db.Cars.ToList();


            CarsViewModel vm = new CarsViewModel();


            vm.AllCarsList = Cars;
            return View(vm);
        }

        public ActionResult AddNewCarToDb(Car car, HttpPostedFileBase uploadFile)
        {

            Car AddedCar = new Car();
            AddedCar.Model = car.Model;


            if (uploadFile == null)
            {

                // dodać message w view
                TempData["Message"] = "Nie wybrano obrazka produktu!";

                return RedirectToAction("Index", "Administrator");
            }
            else if (uploadFile.ContentLength > 0 || uploadFile != null)
            {
                var fileName = Path.GetFileName(uploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Resources/Images"), fileName);
                uploadFile.SaveAs(path);

                string displayPath = "~/Resources/Images/" + fileName;

                AddedCar.ImageUrl = displayPath;
            }

            AddedCar.CarType = car.CarType;
            AddedCar.SeatsNumber = car.SeatsNumber;
            AddedCar.DoorNumber = car.DoorNumber;
            AddedCar.EngineSize = car.EngineSize;
            AddedCar.GearboxType = car.GearboxType;
            AddedCar.Aircondition = car.Aircondition;
            AddedCar.PricePerDay = car.PricePerDay;
            db.Cars.Add(AddedCar);
            db.SaveChanges();
            return RedirectToAction("Index", "Administrator");
        }

        [HttpGet]
        public ActionResult EditCarDetails(int id)
        {
            Car editedCar = db.Cars.Single(c => c.CarId == id);

            return View(editedCar);
        }

        [HttpPost]
        public ActionResult EditCarDetails(Car car)
        {
            Car editedCar = db.Cars.Single(c => c.CarId == car.CarId);
            editedCar.Model = car.Model;
            editedCar.ImageUrl = car.ImageUrl;
            editedCar.CarType = car.CarType;
            editedCar.SeatsNumber = car.SeatsNumber;
            editedCar.DoorNumber = car.DoorNumber;
            editedCar.EngineSize = car.EngineSize;
            editedCar.GearboxType = car.GearboxType;
            editedCar.Aircondition = car.Aircondition;
            editedCar.PricePerDay = car.PricePerDay;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



    }
}