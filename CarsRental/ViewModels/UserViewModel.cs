using Microsoft.AspNet.Identity.EntityFramework;
using CarsRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarsRental.ViewModels
{
    public class UserViewModel
    {
        public List<IdentityRole> RolesList { get; set; }
        public List<ApplicationUser> UsersList { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}