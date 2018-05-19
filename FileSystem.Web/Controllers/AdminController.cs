using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> DeleteUser(int id) {
            try {
                IdentityResult result = await userService.DeleteUserAsync(id);
                if (result.Succeeded) {
                    return RedirectToAction(nameof(Users));
                } else {
                    return BadRequest(result.Errors);
                }
                
            } catch (Exception ex) {
                return BadRequest($"{ex.Message}");
            }

        }
    }
}