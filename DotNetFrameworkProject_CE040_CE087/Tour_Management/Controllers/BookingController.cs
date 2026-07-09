using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Tour_Management.Controllers
{
    public class BookingController : Controller
    {
        private readonly string _connectionString;

        public BookingController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Order(int id)
        {
            // In a real app, we'd fetch tour name by id
            return View();
        }

        [HttpPost]
        public IActionResult RegisterOrder(OrderViewModel model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Bookings (CustomerName, City, TourName, MobileNumber) VALUES (@NAME, @CITY, @TOUR, @MOBILE)";
                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddWithValue("@NAME", model.CustomerName);
                com.Parameters.AddWithValue("@CITY", model.City);
                com.Parameters.AddWithValue("@TOUR", model.TourName);
                com.Parameters.AddWithValue("@MOBILE", model.MobileNumber);

                conn.Open();
                com.ExecuteNonQuery();
            }

            return RedirectToAction("MyBooking");
        }

        [HttpGet]
        public IActionResult MyBooking()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllBooking()
        {
            return View();
        }
    }
}
