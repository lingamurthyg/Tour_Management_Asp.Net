using Microsoft.AspNetCore.Mvc;

namespace Tour_Management.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult AdminProfile()
        {
            return View();
        }
    }
}
