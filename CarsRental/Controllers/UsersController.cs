using CarsRental.Models;
using CarsRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarsRental.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            var roles = db.Roles.ToList();

            UserViewModel vm = new UserViewModel();
            vm.UsersList = users;
            vm.RolesList = roles;


            return View(vm);
        }

        [HttpGet]
        public ActionResult CreateRole()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(string roleName)
        {
            Roles.CreateRole(roleName);

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddRoleToUser(string UserName, string RoleName)
        {

            if (!Roles.RoleExists(RoleName))
                Roles.CreateRole(RoleName);

            Roles.AddUserToRole(UserName, RoleName);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}