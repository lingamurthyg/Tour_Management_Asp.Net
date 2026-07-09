using Microsoft.AspNetCore.Mvc;

namespace Tour_Management.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult MainProfilePage()
        {
            return View();
        }
    }
}
