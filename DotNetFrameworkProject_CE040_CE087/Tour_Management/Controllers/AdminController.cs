using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public IActionResult Login(AdminLoginViewModel model)
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

    public class AdminLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
