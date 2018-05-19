using System.Threading.Tasks;
using FileSystem.BLL.DTO;
using FileSystem.BLL.Interfaces;
using FileSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Web.Controllers {
    public class AccountController : Controller {
        private readonly IAccountService accountService;
        
        public AccountController(IAccountService accountService) {
            this.accountService = accountService;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckUserName(string userName) {
            return Json(!await accountService.IsAlreadyRegisteredAsync(userName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                UserDTO user = new UserDTO() {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = "registered"
                };
                var result = await accountService.RegisterAsync(user);
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                UserDTO user = new UserDTO() { UserName = model.UserName, Password = model.Password };
                var result = await accountService.AuthenticateAsync(user, model.RememberMe);

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
            accountService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register() {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        public IActionResult Index() {
            return View();
        }
    }
}