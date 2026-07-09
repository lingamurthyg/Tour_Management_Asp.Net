using Microsoft.AspNetCore.Mvc;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult AdminLogin2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Password == "admin" && model.Email == "admin@gmail.com")
            {
                return RedirectToAction("AdminProfile", "Admin");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View("AdminLogin2", model);
        }
    }
}
