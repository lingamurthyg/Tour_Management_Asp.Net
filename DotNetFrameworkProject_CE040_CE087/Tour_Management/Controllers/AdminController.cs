using Microsoft.AspNetCore.Mvc;
using Tour_Management.Models;

namespace Tour_Management.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Password == "admin" && model.Email == "admin@gmail.com")
            {
                return RedirectToAction("Profile", "Admin");
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
