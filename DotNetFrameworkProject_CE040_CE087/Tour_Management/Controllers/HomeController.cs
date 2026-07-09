using Microsoft.AspNetCore.Mvc;

namespace Tour_Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
