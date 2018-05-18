using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Web.Controllers {
    public class AdminController : Controller {
        public IActionResult Index() {
            UsersViewModel model = new UsersViewModel();
            return View("Users", model);
        }
    }
}