using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Web.Controllers {
    [Authorize(Roles = "admin")]
    public class AdminController : Controller {
        private readonly IUserService userService;

        public AdminController(IUserService userService) {
            this.userService = userService;
        }

        public IActionResult Users() {
            UsersViewModel model = new UsersViewModel {
                Users = userService.GetUsers()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id) {
            try {
                userService.DeleteUser(id);
                return RedirectToAction(nameof(Users));
            } catch (Exception ex) {
                return BadRequest($"{ex.Message}");
            }

        }
    }
}