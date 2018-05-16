using System.Threading.Tasks;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Web.Controllers {
    public class AccountController : Controller {
        private readonly IUserService userService;
        
        public AccountController(IUserService userService) {
            this.userService = userService;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckUserName(string userName) {
            return Json(!await userService.IsAlreadyRegisteredAsync(userName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                UserDTO user = new UserDTO() {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = "registered"
                };
                var result = await userService.RegisterAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Login");
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                UserDTO user = new UserDTO() { UserName = model.UserName, Password = model.Password };
                var result = await userService.AuthenticateAsync(user, model.RememberMe);

                if (result.Succeeded) {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
                        return Redirect(model.ReturnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                } else {
                    ModelState.AddModelError("", "Invalid username and (or) password");
                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult LogOut() {
            userService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register() {
            return View();
        }

        public IActionResult Login(string returnUrl = null) {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        public IActionResult Index() {
            return View();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}