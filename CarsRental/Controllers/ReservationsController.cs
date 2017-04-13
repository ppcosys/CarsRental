using CarsRental.Models;
using CarsRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private HttpContext _currentHttpContext = System.Web.HttpContext.Current;

        // GET: Reservations
        public ActionResult Index()
        {

            var reservationList = db.Reservations.Where(r => r.SessionId == _currentHttpContext.Session.ToString()).ToList();
            ReservationViewModel vm = new ReservationViewModel();
            vm.ReservationsList = reservationList;
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddReservation(Reservation reservation)
        {

            try
            {
                string msgWrongDate = "Nieprawidłowy zakres dat rezerwacji";
                string msgEmptyModel = "Nie podano dat rezerwacji";


                if (!ModelState.IsValid)
                {
                    TempData["Message"] = msgEmptyModel;
                    return RedirectToAction("Index", "Home");
                }


                if (reservation.StartDate.Day < DateTime.Now.Day)
                {
                    TempData["Message"] = msgWrongDate;
                    return RedirectToAction("Index", "Home");
                }
                else if (reservation.EndDate < DateTime.Now)
                {
                    TempData["Message"] = msgWrongDate;
                    return RedirectToAction("Index", "Home");
                }
                else if (reservation.StartDate > reservation.EndDate)
                {
                    TempData["Message"] = msgWrongDate;
                    return RedirectToAction("Index", "Home");
                }
                else if (reservation.StartDate < DateTime.Now && DateTime.Now < reservation.EndDate)
                {
                    TempData["Message"] = msgWrongDate;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //tworzenie rezerwacji do przekazania w ViewModelu
            Reservation newReservation = new Reservation();
            newReservation.StartDate = reservation.StartDate;
            newReservation.EndDate = reservation.EndDate;


            var sessionId = _currentHttpContext.Session.SessionID;

            newReservation.SessionId = sessionId.ToString();


            //obliczanie ilości dni obejmujących rezerwację
            string StartDate = reservation.StartDate.ToString("yyyy-MM-dd");
            string EndDate = reservation.EndDate.ToString("yyyy-MM-dd");

            TimeSpan ts = reservation.EndDate - reservation.StartDate;
            double NrOfDays = ts.TotalDays;
            newReservation.ReservationTime = Convert.ToInt32(NrOfDays);

            List<Car> result = new List<Car>();


            //dodanie dat rezerwacji
            db.Reservations.Add(newReservation);
            db.SaveChanges();

            //wyszukiwanie dostępnych samochodów poza datami rezerwacji
            try
            {
                result = (from c in db.Cars
                          where !db.Reservations.Any(r => r.CarId == c.CarId && (r.EndDate >= reservation.StartDate &&
                          r.StartDate <= reservation.EndDate))
                          select c).ToList();
            }
            catch
            {
                throw new Exception();
            }


            //dodanie danych do ViewModelu
            ReservationViewModel vm = new ReservationViewModel();
            vm.AvailableCarsList = result;
            vm.Reservation = newReservation;



            return View(vm);
        }

        public ActionResult AddReservationToDB(int carId, int reservationId)
        {
            string sessionId = _currentHttpContext.Session.SessionID;

            Reservation newReservation = db.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);

            Car chosenCar = db.Cars.Single(c => c.CarId == carId);
            decimal totalPrice = chosenCar.PricePerDay * newReservation.ReservationTime;
            string carModel = chosenCar.Model;

            newReservation.CarId = carId;
            newReservation.CarModel = carModel;
            newReservation.TotalPrice = totalPrice;
            db.SaveChanges();

            ReservationViewModel vm = new ReservationViewModel();
            vm.Reservation = newReservation;

            return View(vm);
        }



        public ActionResult ReservationsMiniPanel()
        {
            List<Reservation> reservationList = new List<Reservation>();

            try
            {
                reservationList = db.Reservations.
                    Where(r => (r.SessionId == _currentHttpContext.Session.SessionID.
                    ToString()) && (r.CarId != 0))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ReservationViewModel vm = new ReservationViewModel();
            vm.ReservationsList = reservationList;


            return PartialView("_ReservationsMiniPanel", vm);
        }

    }
}